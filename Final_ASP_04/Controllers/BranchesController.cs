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
		public ActionResult ListRoomTypes(string selectedBranchId = "1")
        {
            var branchIdInt = int.Parse(selectedBranchId);

            var service = new RoomTypeService();
            var roomTypeDtos = service.GetRoomTypesByBranchId(branchIdInt);

            var roomTypeVms = roomTypeDtos.Select(x => x.ToRoomTypeVm()).ToList();
            ViewBag.RoomTypeVms = roomTypeVms;
            ViewBag.SelectedBranchId = selectedBranchId;

			return View(roomTypeVms);
        }
    }
}