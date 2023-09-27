using Final_ASP_04_back.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04_back.Models.Repositories
{
	public class RoomRepository
	{
		public List<Room> GetRooms(int branchId, string roonTypeName)
		{
			var db = new AppDbContext();

			IQueryable<Room> rooms = db.Rooms.Include("RoomType").Include("RoomType.Branch");

			if(branchId > 0)
			{
				rooms = rooms.Where(x => x.RoomType.BranchId == branchId);
			}
			if(!string.IsNullOrEmpty(roonTypeName))
			{
				rooms = rooms.Where(x => x.RoomType.Name.Contains(roonTypeName));
			}

			return rooms.ToList();

		}
		public Order GetCurrentReservationForRoom(int id)
		{
			var db = new AppDbContext();
			
			var order = db.Orders.Where(x => x.RoomId == id && x.StartDateTime <= DateTime.Now && x.EndDateTime >= DateTime.Now).FirstOrDefault();
			
			return order;
		}
	}
}