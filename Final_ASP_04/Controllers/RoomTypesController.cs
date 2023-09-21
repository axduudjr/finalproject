using Final_ASP_04.Models.Services;
using Final_ASP_04.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_ASP_04.Controllers
{
    public class RoomTypesController : Controller
    {
        // GET: RoomTypes
        public ActionResult RoomTypeInfo(string selectedBranchId = "1", string selectedRoomTypeId = "1")
        {
            var selectedRoomTypeIdInt = int.Parse(selectedRoomTypeId);
            var selectedBranchIdInt = int.Parse(selectedBranchId);

            var service = new RoomTypeService();
            var roomTypeDto = service.GetRoomTypeById(selectedRoomTypeIdInt);
            var roomTypesDto = service.GetRoomTypesByBranchId(selectedBranchIdInt);

            var roomTypeVm = roomTypeDto.ToRoomTypeVm();
            var roomTypesVm = roomTypesDto.Select(x => x.ToRoomTypeVm()).ToList();
            
            ViewBag.otherRoomTypes = roomTypesVm;

            return View(roomTypeVm);
        }
    }
}