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
		public RoomTypeListDTO GetRoomTypeById(int roomTypeId)
		{
			var repo = new RoomTypeRepository();
			var roomType = repo.GetRoomTypeById(roomTypeId);

			var roomTypeListDTO = roomType.ToRoomTypeListDTO();

			return roomTypeListDTO;
		}
		public List<RoomTypeListDTO> GetOtherRoomTypesInBranch(int branchId, int selectedRoomTypeId)
		{
			var repo = new RoomTypeRepository();
			var roomTypes = repo.GetOtherRoomTypesInBranch(branchId, selectedRoomTypeId);

			var roomTypeListDTOs = roomTypes.Select(x => x.ToRoomTypeListDTO()).ToList();

			return roomTypeListDTOs;
		}
		public int GetBestPrice(int roomTypeId)
		{
			var repo = new RoomTypeRepository();
			var bestPrice = repo.GetRoomTypeBestPrice(roomTypeId).Price;

			return bestPrice;
		}
	}
}