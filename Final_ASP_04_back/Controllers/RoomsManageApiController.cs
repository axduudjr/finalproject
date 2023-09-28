using Final_ASP_04_back.Models.EFModels;
using Final_ASP_04_back.Models.Repositories;
using Final_ASP_04_back.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_ASP_04_back.Controllers
{
    public class RoomsManageApiController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetBranches()
        {
            var db = new AppDbContext();
            var branches = db.Branches.Select(b => new { b.Id, b.Name }).ToList();

            return Ok(branches);
        }
        [HttpGet]
        public IHttpActionResult GetRoomTypes(string branchId)
        {
            var db = new AppDbContext();
			var roomTypes = db.RoomTypes.Where(r => r.BranchId.ToString() == branchId).Select(r => new { r.Id, r.Name }).ToList();

			return Ok(roomTypes);
        }
        [HttpPost]
        public IHttpActionResult EditRoom(RoomManageVm vm)
        {
            var db = new AppDbContext();

			var room = db.Rooms.Find(vm.Id);

			room.RoomTypeId = vm.RoomTypeId;
            room.GuestNumberId = new GuestNumberRepository().GetGuestNumberByCapacity(vm.GuestNumberCapacity).Id;
            room.Description = vm.Description;
            room.Price = vm.Price;

			db.SaveChanges();

			return Ok(new { success = true });
		}
    }
}
