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

            return View(roomsVm);
        }
        public ActionResult RoomDetails(int id)
        {
			var service = new RoomService();
			var roomVm = service.GetRoom(id).ToRoomManageVm();

			return PartialView(roomVm);
		}
    }
}