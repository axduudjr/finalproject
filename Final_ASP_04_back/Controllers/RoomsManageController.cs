using Final_ASP_04_back.Models.Repositories;
using Final_ASP_04_back.Models.Services;
using Final_ASP_04_back.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_ASP_04_back.Controllers
{
    public class RoomsManageController : Controller
    {
        // GET: RoomsManage
        public ActionResult Index(string roomTypeName, int branchId=0)
        {
            var service = new RoomService();
            var roomsVm = service.GetRooms(branchId, roomTypeName).Select(r => r.ToRoomManageVm()).ToList();

            foreach (var room in roomsVm)
            {
				room.IsBooked = CheckIfRoomBooked(room.Id);
			}

            return View(roomsVm);
        }
		private bool CheckIfRoomBooked(int id)
		{
			var repo = new RoomRepository();

			var order = repo.GetCurrentReservationForRoom(id);

			return order != null;
		}
	}
}