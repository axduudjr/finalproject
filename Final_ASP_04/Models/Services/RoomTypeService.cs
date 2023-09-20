using Final_ASP_04.Models.DTOs;
using Final_ASP_04.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Services
{
	public class RoomTypeService
	{
		public List<RoomTypeListDTO> GetRoomTypesByBranchId(int branchId)
		{
			var repo = new RoomTypeRepository();
			var roomTypes = repo.GetRoomTypesByBranchId(branchId);

			var roomTypeListDTOs = roomTypes.Select(x => x.ToRoomTypeListDTO()).ToList();

			return roomTypeListDTOs;
		}
	}
}