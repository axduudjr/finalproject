using Final_ASP_04_back.Models.DTOs;
using Final_ASP_04_back.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04_back.Models.Services
{
	public class RoomService
	{
		public List<RoomManageDTO> GetRooms(int branchId, string roomTypeName)
		{
			var repo = new RoomRepository();
			var rooms = repo.GetRooms(branchId, roomTypeName).Select(r => r.ToRoomManageDTO()).ToList();

			return rooms;
		}
	}
}