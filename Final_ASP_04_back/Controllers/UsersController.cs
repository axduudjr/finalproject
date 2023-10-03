using Final_ASP_04_back.Models.EFModels;
using Final_ASP_04_back.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Final_ASP_04_back.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserVm vm)
        {
            try
            {
                ValidLogin(vm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            var proccessResult = ProcessLogin(vm);

            Response.Cookies.Add(proccessResult.Cookie);

            return Redirect(proccessResult.ReturnUrl); ;
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        private bool ValidLogin(UserVm vm)
        {
            var db = new AppDbContext();
            var member = db.Users.FirstOrDefault(u => u.Account == vm.Account);
            if (member == null)
            {
                throw new Exception("帳號或密碼不正確");
            }
            if (member.Password != vm.Password)
            {
                throw new Exception("帳號或密碼不正確");
            }
            if (!member.Enabled)
            {
                throw new Exception("你好像被開除了");
            }
            return true;
        }

        private (string ReturnUrl, HttpCookie Cookie) ProcessLogin(UserVm vm)
        {
            var rememberMe = false;
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
            FormsAuthentication.RedirectFromLoginPage(account, false);


            return (url, cookie);
        }
    }
}