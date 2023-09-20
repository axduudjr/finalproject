using Final_ASP_04.Models.DTOs;
using Final_ASP_04.Models.EFModels;
using Final_ASP_04.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Services
{
	public class RoomService
	{
		public List<RoomListDTO> SearchRoom(int branchId, int roomTypeId, int guestNumberId, DateTime checkIn, DateTime checkOut)
		{
			var repo = new RoomRepository();
			var rooms = repo.GetRooms(branchId, roomTypeId, guestNumberId);

			List<Room> resultRooms = new List<Room>();
			foreach (var room in rooms)
			{
				var orders = repo.GetOrdersByRoomId(room.Id, checkIn, checkOut);
				if (orders.Count == 0)
				{
					resultRooms.Add(room);
				}
			}

			var RoomListDTO = resultRooms.Select(x => x.ToRoomListDTO()).ToList();

			return RoomListDTO;
		}
	}
}