using Final_ASP_04.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Repositories
{
	public class OrderRepository
	{
		public Order GetOrder(int id)
		{
			var db = new AppDbContext();
			var order = db.Orders.Include("Branch").Include("Room").Include("Room.RoomType").FirstOrDefault(x => x.Id == id);

			return order;
		}
	}
}