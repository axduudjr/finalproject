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
using Final_ASP_04.Models.DTOs;

namespace Final_ASP_04.Controllers
{
    public class RoomsController : Controller
    {
		private int _pageSize = 5;
		// GET: Rooms
		public ActionResult SearchRoom(int page = 1, int branchId = 1, int roomTypeId = 0, int guestNumberId = 0, DateTime? startDateTime = null, DateTime? endDateTime = null, int sortvalue = 1)
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
			ViewBag.BranchId = branchId;
			ViewBag.StartDateTime = startDateTime.Value.ToString("yyyy-MM-dd");
			ViewBag.EndDateTime = endDateTime.Value.ToString("yyyy-MM-dd");
			ViewBag.BranchName = GetBranchByBranchId(branchId).Name;

			roomsVm = ApplySort(roomsVm, sortvalue);

			ViewBag.SortValue = sortvalue;

			var result = roomsVm.ToPagedList(pageNumber, _pageSize);

			ViewBag.RoomsVm = result;

			return View(result);
		}
		[HttpPost]
		public ActionResult CreateCart(RoomVm roomVm, DateTime startDateTime, DateTime endDateTime)
		{

			//var buyer = User.Identity.Name;
			var service = new CartService();
			var dto = new CartCreateDTO
			{
				BranchId = new BranchRepository().GetBranchByRoomId(roomVm.Id).Id,
				RoomId = roomVm.Id,
				MemberId = 30, //待取得真正的會員Id
				Price = roomVm.Price,
				StartDateTime = startDateTime,
				EndDateTime = endDateTime,
			};
			var cartId = service.CreateCart(dto);

			return RedirectToAction("CheckoutStepOne", "Checkout", new { cartId });
		}

		private List<RoomVm> ApplySort(List<RoomVm> roomsVm, int sortvalue)
		{
			if (sortvalue == 1)
			{
				roomsVm = roomsVm.OrderBy(x => x.Price).ToList();
			}
			if (sortvalue == 2)
			{
				roomsVm = roomsVm.OrderByDescending(x => x.Price).ToList();
			}
			if (sortvalue == 3)
			{
				roomsVm = roomsVm.OrderByDescending(x => x.RoomTypeDisplayOrder).ToList();
			}

			return roomsVm;
		}

		//[HttpPost]
		//public ActionResult CreateCart(int page= 1, int branchId = 1, int roomTypeId = 0, int guestNumberId = 0, DateTime? startDateTime = null, DateTime? endDateTime = null, int sortvalue = 1)
  //      {
		//	int pageNumber = page < 1 ? 1 : page;

		//	if (!startDateTime.HasValue)
		//	{
		//		startDateTime = DateTime.Now;
		//	}

		//	if (!endDateTime.HasValue)
		//	{
		//		endDateTime = DateTime.Now;
		//	}

		//	var service = new RoomService();
  //          var rooms = service.SearchRoom(branchId, roomTypeId, guestNumberId, startDateTime.Value, endDateTime.Value);

  //          var roomsVm = rooms.Select(x => x.ToRoomVm()).ToList();

  //          ViewBag.TotalCount = roomsVm.Count();
  //          ViewBag.RoomTypeId = roomTypeId;
  //          ViewBag.GuestNumberId = guestNumberId;
		//	ViewBag.BranchId = branchId;
		//	ViewBag.StartDateTime = startDateTime.Value.ToString("yyyy-MM-dd");
		//	ViewBag.EndDateTime = endDateTime.Value.ToString("yyyy-MM-dd");
		//	ViewBag.BranchName = GetBranchByBranchId(branchId).Name;
			
		//	roomsVm = ApplySort(roomsVm, sortvalue);			
			
		//	ViewBag.SortValue = sortvalue;

		//	var result = roomsVm.ToPagedList(pageNumber, _pageSize);

		//	ViewBag.RoomsVm = result;

		//	if (Request.IsAjaxRequest())
		//	{
		//		return PartialView("_RoomSearchResultPartial", result);
		//	}
		//	else
		//	{
		//		return View(result);
		//	}

		//}
		private Branch GetBranchByBranchId(int branchId)
		{
			var db = new AppDbContext();
			var branch = db.Branches.Find(branchId);

			return branch;
		}
    }
}