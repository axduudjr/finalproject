using Final_ASP_04.Models.EFModels;
using Final_ASP_04.Models.Infra;
using Final_ASP_04.Models.Exts;
using Final_ASP_04.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections;

namespace Final_ASP_04.Controllers
{
	public class MembersController : Controller
	{
		//暫時的跳轉畫面
		// GET: Members
		public ActionResult Index()
		{
			return View();
		}

		//登入頁面
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginVm vm)
		{
			var repo = new MemberRepo();
			if (!ModelState.IsValid)
			{
				return View();
			}

			try
			{
				repo.ValidLogin(vm);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}

			var processResult = repo.ProcessLogin(vm);

			Response.Cookies.Add(processResult.Cookie);

			return Redirect(processResult.ReturnUrl);
		}

		//註冊頁面
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegisterVm vm)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}

			var repo = new MemberRepo();
			try
			{
				repo.RegisterMember(vm);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(vm);
			}

			repo.SendConfirmEmail(vm);

			return RedirectToAction("RegisterConfirm");
		}

		//登出功能
		[Authorize]
		public ActionResult Logout()
		{
			Session.Abandon();
			FormsAuthentication.SignOut();
			return Redirect("/Home/Index");
		}

		//會員資料維護
		[Authorize]
		public ActionResult EditProfile()
		{
			var currentUserAccount = User.Identity.Name;
			var account = MemberRepo.GetMemberProfile(currentUserAccount);

			return View(account);
		}

		//帳號目前不給修改
		[Authorize]
		[HttpPost]
		public ActionResult EditProfile(EditProfileVm vm)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}

			var user = User.Identity.Name;
			var repo = new MemberRepo();
			try
			{
				repo.ProcessEditProfile(vm, user);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(vm);
			}

			return RedirectToAction("MemberProfile");
		}

		//忘記密碼
		public ActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ForgetPassword(ForgetPasswordVm vm)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var repo = new MemberRepo();
			var urlTemplate = Request.Url.Scheme + "://" + Request.Url.Authority + "/" +
				"Members/ResetPassword?memberid={0}&confirmCode={1}";

			try
			{
				repo.ValidEmail(vm.Email, urlTemplate);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(vm);
			}

			return RedirectToAction("ForgetPasswordResult");
		}

		public ActionResult ForgetPasswordResult()
		{
			return View();
		}

		//重設密碼
		public ActionResult ResetPassword(int memberid, string confirmCode)
		{
			return View();
		}

		[HttpPost]
		public ActionResult ResetPassword(ResetPasswordVm vm, int memberid, string confirmCode)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}

			var repo = new MemberRepo();
			try
			{
				repo.ProcessResetPassword(vm, memberid, confirmCode);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
			}

			Session.Abandon();
			FormsAuthentication.SignOut();
			return RedirectToAction("Login");
		}

		[Authorize]
		public ActionResult ChangePassword()
		{
			return View();
		}

		[Authorize]
		[HttpPost]
		public ActionResult ChangePassword(ChangePasswordVm vm)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var name = User.Identity.Name;
			var repo = new MemberRepo();
			try
			{
				repo.ProcessChangePassword(vm, name);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}

			//todo要先登出

			return RedirectToAction("Login");
		}

		public ActionResult RegisterConfirm()
		{
			return View();
		}

		public ActionResult ConfirmCode(int memberId, string confirmCode)
		{
			var db = new AppDbContext();
			var member = db.Members.FirstOrDefault(m => m.Id == memberId);

			member.ConfirmCode = null;
			member.IsConfirmed = true;
			db.SaveChanges();

			return View();
		}

		//使用的是EditProfileVm
		[Authorize]
		public ActionResult MemberProfile()
		{
			var user = User.Identity.Name;
			var db = new AppDbContext();
			var member = db.Members.FirstOrDefault(m => m.Account == user);

			if (member == null)
			{
				return RedirectToAction("Login");
			}

			EditProfileVm vm = new EditProfileVm
			{
				Id = member.Id,
				Account = member.Account,
				Name = member.Name,
				PhoneNumber = member.PhoneNumber,
				Email = member.Email,
			};

			return View(vm);
		}
	}

	public class MemberRepo
	{
		//檢查是否為有效登入
		internal void ValidLogin(LoginVm vm)
		{
			var db = new AppDbContext();

			var member = db.Members.FirstOrDefault(m => m.Account == vm.Account);
			if (member == null)
			{
				throw new Exception("帳號或密碼錯誤");
			}
			if (member.IsConfirmed == false)
			{
				throw new Exception("您尚未開通會員資格");
			}

			var salt = HashUtility.GetSalt();
			var hashedPassword = HashUtility.ToSHA256(vm.Password, salt);

			if (string.Compare(member.EncryptedPassword, hashedPassword, true) != 0)
			{
				throw new Exception("帳號或密碼錯誤");
			}
		}

		//在登入為有效地之後,建立會員資料的cookie
		internal (string ReturnUrl, HttpCookie Cookie) ProcessLogin(LoginVm vm)
		{
			var rememberMe = vm.remeberMe;
			var account = vm.Account;
			var roles = string.Empty;

			var ticket = new FormsAuthenticationTicket(
				1,
				account,
				DateTime.Now,
				DateTime.Now.AddDays(2),
				rememberMe,
				roles,
				"/"
				);

			var value = FormsAuthentication.Encrypt(ticket);

			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);

			var url = FormsAuthentication.GetRedirectUrl(account, true);

			return (url, cookie);
		}

		//註冊Member
		internal void RegisterMember(RegisterVm vm)
		{
			var db = new AppDbContext();

			var isExist = db.Members.FirstOrDefault(m => m.Account == vm.Account);
			if (isExist != null)
			{
				throw new Exception("此帳號已存在");
			}

			var isValid = db.Members.FirstOrDefault(m => m.Email == vm.Email);
			if (isValid != null)
			{
				throw new Exception("此信箱已被使用");
			}

			var member = vm.RegisterVmToMember();

			db.Members.Add(member);
			db.SaveChanges();

			//todo發出確認信
		}

		internal static EditProfileVm GetMemberProfile(string currentUserAccount)
		{
			var db = new AppDbContext();
			var member = db.Members.FirstOrDefault(m => m.Account == currentUserAccount);
			if (member == null)
			{
				throw new Exception("找不到此帳號");
			}

			return member.MemberToEditProfileVm();
		}

		internal void ProcessEditProfile(EditProfileVm vm, string user)
		{
			var db = new AppDbContext();
			var memberInDb = db.Members.FirstOrDefault(m => m.Id == vm.Id);
			var validAccount = db.Members.Count(m => m.Account == vm.Account);

			if (memberInDb.Account != user)
			{
				throw new Exception("你沒有修改他人資料的權限");
			}
			//if (vm.Account != user && validAccount > 0)
			//{
			//	throw new Exception("此帳號已被使用");
			//}

			memberInDb.Name = vm.Name;
			memberInDb.Email = vm.Email;
			memberInDb.PhoneNumber = vm.PhoneNumber;
			db.SaveChanges();
		}

		//驗證忘記密碼頁面輸入的email,並呼叫SendForgetEmail
		internal void ValidEmail(string email, string urlTemplate)
		{
			var db = new AppDbContext();
			var member = db.Members.FirstOrDefault(m => m.Email == email);

			if (member == null)
			{
				throw new Exception("信箱並未被註冊,請重新輸入");
			}
			if (member.IsConfirmed == false)
			{
				throw new Exception("帳號並未驗證,請去信箱收取驗證信,再重新登入");
			}

			var confirmCode = Guid.NewGuid().ToString("N");
			member.ConfirmCode = confirmCode;
			db.SaveChanges();

			var url = string.Format(urlTemplate, member.Id, confirmCode);

			var sendEmail = new EmailHelper();
			//todo 顯示帳號或姓名
			sendEmail.SenderForgetPasswordEmail(url, member.Name, email);
		}

		internal void ProcessResetPassword(ResetPasswordVm vm, int memberid, string confirmCode)
		{
			var db = new AppDbContext();

			var member = db.Members.FirstOrDefault(m => m.Id == memberid &&
														m.IsConfirmed == true &&
														m.ConfirmCode == confirmCode);

			if (member == null)
			{
				return;
			}

			var salt = HashUtility.GetSalt();
			var hashedPassword = HashUtility.ToSHA256(vm.Password, salt);
			member.EncryptedPassword = hashedPassword;
			member.ConfirmCode = null;
			db.SaveChanges();
		}

		internal void ProcessChangePassword(ChangePasswordVm vm, string user)
		{
			if (string.Compare(vm.Password, vm.OldPassword, false) == 0)
			{
				throw new Exception("新舊密碼不能相同");
			}

			var db = new AppDbContext();
			var member = db.Members.FirstOrDefault(m => string.Compare(m.Account, user, true) == 0);

			var salt = HashUtility.GetSalt();
			var hashedOldPassword = HashUtility.ToSHA256(vm.OldPassword, salt);

			if (string.Compare(hashedOldPassword, member.EncryptedPassword, false) != 0)
			{
				throw new Exception("原始密碼不正確");
			}

			var hashedPassword = HashUtility.ToSHA256(vm.Password, salt);

			member.EncryptedPassword = hashedPassword;
			db.SaveChanges();
		}

		internal void SendConfirmEmail(RegisterVm vm)
		{
			var db = new AppDbContext();
			var member = db.Members.FirstOrDefault(m => m.Account == vm.Account);

			var emailHelper = new EmailHelper();
			emailHelper.SenderConfirmRegisterEmail($"https://localhost:44335/Members/ConfirmCode?memberId={member.Id}&confirmCode={member.ConfirmCode}", member.Name, member.Email);
		}
	}
}