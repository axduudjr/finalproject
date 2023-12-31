﻿using Final_ASP_04.Models.EFModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Repositories
{
	public class RoomRepository
	{		
		public List<Room> GetRooms(int branchId, int roomTypeId, int guestNumberId)
		{
			var db = new AppDbContext();

			IQueryable<Room> rooms = db.Rooms.Include("RoomType").Include("RoomType.Branch").Include("GuestNumber").Where(x => x.RoomType.BranchId == branchId);

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

			var orders = db.Orders.Where(x => x.RoomId == roomId && x.StartDateTime <= checkOut && x.EndDateTime >= checkIn && x.Status != "已取消").ToList();

			return orders;
		}
		//public Room GetFirstRoom(string roomTypeName, string guestNumberName, string roomTypeDescription)
		//{
		//	var db = new AppDbContext();

		//	var room = db.Rooms.Include("RoomType").Include("GuestNumber")
		//		.FirstOrDefault(x => x.RoomType.Name == roomTypeName 
		//						&& x.GuestNumber.Name == guestNumberName 
		//						&& x.RoomType.Description == roomTypeDescription);

		//	return room;
		//}
	}
}