using Final_ASP_04.Models.EFModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Repositories
{
	public class RoomRepository
	{		
		public List<Room> GetRooms(int roomTypeId, int guestNumberId)
		{
			var db = new AppDbContext();

			IQueryable<Room> rooms = db.Rooms.Include("RoomType").Include("GuestNumber");

			if (roomTypeId > 0)
			{
				rooms = rooms.Where(x => x.RoomTypeId == roomTypeId);
			}
			if (guestNumberId > 0)
			{
				rooms = rooms.Where(x => x.GuestNumberId == guestNumberId);
			}

			var result = rooms.DistinctBy(x => new { x.RoomTypeId, x.GuestNumberId, x.Description }).ToList();

			return result;
		}
		public List<Order> GetOrdersByRoomId(int roomId, DateTime checkIn, DateTime checkOut)
		{
			var db = new AppDbContext();

			var orders = db.Orders.Where(x => x.RoomId == roomId && x.StartDateTime <= checkOut && x.EndDateTime >= checkIn).ToList();

			return orders;
		}
	}
}