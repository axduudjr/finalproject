using Final_ASP_04.Models.EFModels;
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
        public ActionResult RoomTypeInfo(int selectedBranchId = 1, int selectedRoomTypeId = 1)
        {
            var service = new RoomTypeService();

            var roomTypeDto = service.GetRoomTypeById(selectedRoomTypeId);
            var roomTypesDto = service.GetRoomTypesByBranchId(selectedBranchId);

            var otherRoomTypesDto = service.GetOtherRoomTypesInBranch(selectedBranchId, selectedRoomTypeId);

            var roomTypeVm = roomTypeDto.ToRoomTypeVm();
            roomTypeVm.BestPrice = new RoomTypeService().GetBestPrice(selectedRoomTypeId);

            var roomTypesVm = roomTypesDto.Select(x => x.ToRoomTypeVm()).ToList();

            var otherRoomTypesVm = otherRoomTypesDto.Select(x => x.ToRoomTypeVm()).ToList();

            ViewBag.roomTypePictures = GetRoomTypePictures(roomTypeVm);

            var commentService = new CommentService();
            var allComments = commentService.GetAllCommentsByBranchId(selectedBranchId);
            var allCommentsVm = allComments.Select(x => new CommentListVm
            {
				Description = x.Description,
				Rank = x.Rank,
				CreatedTime = x.CreatedTime
			}).ToList();

            ViewBag.AllComments = allCommentsVm;

			ViewBag.selectedBranchId = selectedBranchId;
            ViewBag.selectedRoomTypeId = selectedRoomTypeId;
			ViewBag.otherRoomTypes = otherRoomTypesVm;

			return View(roomTypeVm);
        }

		public List<RoomTypePicture> GetRoomTypePictures(RoomTypeVm roomTypeVm)
		{
            var db = new AppDbContext();

            var roomTypePictures = db.RoomTypePictures.Where(x => x.RoomTypeName == roomTypeVm.RoomTypeName).OrderBy(x => x.DisplayOrder).ToList();

            return roomTypePictures;
		}
	}
}