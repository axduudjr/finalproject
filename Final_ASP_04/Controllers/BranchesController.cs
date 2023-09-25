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
				FileName = "hotel-2294856_1920.jpg"
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
    }
}