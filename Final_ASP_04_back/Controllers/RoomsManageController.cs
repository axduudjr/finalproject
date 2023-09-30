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
				room.Status = CheckIfRoomBooked(room.Id);
			}

            string branchName; 
            if(branchId == 0)
            {
				branchName = "全部分館";
			}
            else
            {
				branchName = new BranchRepository().GetBranch(branchId).Name;
			}

            ViewBag.BranchName = branchName;

			return View(roomsVm);
        }
		private string CheckIfRoomBooked(int id)
		{
			var repo = new RoomRepository();

			var order = repo.GetCurrentReservationForRoom(id);
            
            if(order != null)
            {
                if(order.Status == "已入住")
                {
                    return "已入住";
                }
                if(order.Status =="已付款" || order.Status == "已修改")
                {
                    return "已預訂";
                }
                else
                {
					return "已取消";
				}               
            }

            return "空房";
		}
	}
}