using Final_ASP_04.Models.Repositories;
using Final_ASP_04.Models.Services;
using Final_ASP_04.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using Final_ASP_04.Models.EFModels;

namespace Final_ASP_04.Controllers
{
    public class RoomsController : Controller
    {
		private int _pageSize = 5;
		// GET: Rooms
		public ActionResult SearchRoom(int page= 1, int branchId = 1, int roomTypeId = 0, int guestNumberId = 0, DateTime? startDateTime = null, DateTime? endDateTime = null )
        {
			int pageNumber = page < 1 ? 1 : page;

			if (!startDateTime.HasValue)
			{
				startDateTime = DateTime.Now;
			}

			if (!endDateTime.HasValue)
			{
				endDateTime = DateTime.Now;
			}

			var service = new RoomService();
            var rooms = service.SearchRoom(branchId, roomTypeId, guestNumberId, startDateTime.Value, endDateTime.Value);

            var roomsVm = rooms.Select(x => x.ToRoomVm()).ToList();

            ViewBag.TotalCount = roomsVm.Count();
            ViewBag.RoomTypeId = roomTypeId;
            ViewBag.GuestNumberId = guestNumberId;
			ViewBag.StartDateTime = startDateTime.Value.ToString("yyyy-MM-dd");
			ViewBag.EndDateTime = endDateTime.Value.ToString("yyyy-MM-dd");
			ViewBag.BranchName = GetBranch(branchId).Name;

            var result = roomsVm.ToPagedList(pageNumber, _pageSize);

            return View(result);
        }
		private Branch GetBranch(int branchId)
		{
			var db = new AppDbContext();
			var branch = db.Branches.Find(branchId);

			return branch;
		}
    }
}