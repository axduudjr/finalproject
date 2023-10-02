using Final_ASP_04.Models.EFModels;
using Final_ASP_04.Models.Repositories;
using Final_ASP_04.Models.Services;
using Final_ASP_04.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_ASP_04.Controllers
{
	public class BranchesController : Controller
	{
		// GET: Branches
		public ActionResult ChooseBranch()
		{
			ViewBag.branches = BranchBlockPartial();
			return View();
		}
		[HttpGet]
		public ActionResult BranchMain(int branchId)
		{
			var newsvm = GetNewsVM(branchId);
			var branchInfoVms = GetBranchInfoVm(branchId);
			var branchPic = GetBranchPic(branchId);
			var product = GetProductVm(branchId);

			//todo 記得在頁面導入 picture
			ViewBag.News = newsvm;
			ViewBag.BranchInfo = branchInfoVms;
			ViewBag.BranchPic = branchPic;
			ViewBag.BranchId = branchId;
			ViewBag.Product = product;

			return View();
		}
		[Authorize]
		public ActionResult Mail()
		{
			var db = new AppDbContext();
			var user = User.Identity.Name;
			var member = db.Members.FirstOrDefault(m => m.Account == user);

			if (member == null)
			{
				//todo
				return View();
			}
			ViewBag.Member = member;

			return View();
		}
		[Authorize]
		[HttpPost]
		public ActionResult Mail(MailVm vm)
		{
			var db = new AppDbContext();
			var user = User.Identity.Name;
			var member = db.Members.FirstOrDefault(m => m.Account == user);

			if (member == null)
			{
				//todo
				return View();
			}

			var newMail = new Mail
			{
				MemberId = member.Id,
				Title = vm.Title,
				Description = vm.Description,
				CreatedTime = DateTime.Now,
			};

			db.Mails.Add(newMail);
			db.SaveChanges();

			return View();
		}
		public ActionResult FAQs()
		{
			var db = new AppDbContext();
			var qanda = db.FAQs.ToList();

			ViewBag.FAQs = qanda;
			return View();
		}
		private List<BranchPicture> GetBranchPic(int branchId)
		{
			var db = new AppDbContext();
			return db.BranchPictures.Where(b => b.BranchId == branchId).ToList();
		}
		private BranchInfoVm GetBranchInfoVm(int branchId)
		{
			var db = new AppDbContext();
			var result = db.Branches.Where(b => b.Id == branchId)
								.Select(b => new BranchInfoVm
								{
									Name = b.Name,
									Address = b.Address,
								}).FirstOrDefault();

			var trafficInfo = db.TrafficInfos.Where(t => t.BranchId == branchId)
												.Select(t => new TrafficInfoVm
												{
													Title = t.Title,
													Description = t.Description,
												})
												.ToList();

			result.Traffics = trafficInfo;
			return result;
		}
		private static List<NewsVm> GetNewsVM(int branchId)
		{
			var db = new AppDbContext();
			var model = db.News.Where(b => b.BranchId == branchId).ToList();

			List<NewsVm> vm = model.Select(m => new NewsVm
			{
				Id = m.Id,
				Title = m.Title,
				Description = m.Description,
				FileName = m.FileName,
			}).ToList();
			return vm;
		}
		public List<BranchVm> BranchBlockPartial()
		{
			var db = new AppDbContext();
			var branches = db.Branches.Select(
						b => new BranchVm
						{
							Id = b.Id,
							Name = b.Name,
							Description = b.Description,
							Address = b.Address,
							PhoneNumber = b.PhoneNumber,
							CarouselExampleId = "CarouselExample" + b.Id,
							DataBsTarget = "#CarouselExample" + b.Id
						}).ToList();

			foreach (var br in branches)
			{
				br.FileName = BranchPicPartial(br.Id);
			}

			return branches;
		}
		public List<PictureVm> BranchPicPartial(int id)
		{
			var db = new AppDbContext();
			//todo 範例
			return db.BranchPictures.Where(p => p.BranchId == id).Select(p => new PictureVm
			{
				Id = p.DisplayOrder,
				FileName = p.FileName,
			}).ToList();

			//return Enumerable.Range(1, 3).Select(i => new PictureVm
			//{
			//    Id = i,
			//    FileName = "/img/hotel-458860_1920.jpg",
			//}).ToList();
		}
		public ActionResult ListRoomTypes(int selectedBranchId = 1)
		{
			var service = new RoomTypeService();
			var roomTypeDtos = service.GetRoomTypesByBranchId(selectedBranchId);

			var roomTypeVms = roomTypeDtos.Select(x => x.ToRoomTypeVm()).ToList();
			ViewBag.RoomTypeVms = roomTypeVms;
			ViewBag.SelectedBranchId = selectedBranchId;

			var branchService = new BranchService();
			var branches = branchService.GetAllBranches();

			ViewBag.Branches = branches;

			return View(roomTypeVms);
		}
		private List<RoomType> GetProductVm(int branchId)
		{
			var db = new AppDbContext();
			var product = db.RoomTypes.Where(r => r.BranchId == branchId)
										.OrderBy(r => r.DisplayOrder)
										.Take(3)
										.ToList();
			return product;
		}
	}
}