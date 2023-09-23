using Final_ASP_04.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Repositories
{
	public class CartRepository
	{
		public void CreateCart(Cart cart)
		{
			var db = new AppDbContext();
			db.Carts.Add(cart);
			db.SaveChanges();
		}
	}
}