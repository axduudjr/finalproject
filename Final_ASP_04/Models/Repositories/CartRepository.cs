using Final_ASP_04.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Repositories
{
	public class CartRepository
	{
		public int CreateCart(Cart cart)
		{
			var db = new AppDbContext();
			db.Carts.Add(cart);
			db.SaveChanges();

			return cart.Id;
		}
		public Cart GetCartByMemberId(int memberId)
		{
			var db = new AppDbContext();
			var cart = db.Carts.FirstOrDefault(x => x.MemberId == memberId);

			return cart;
		}
		public void DeleteCart(int cartId)
		{
			var db = new AppDbContext();

			var cart = db.Carts.FirstOrDefault(x => x.Id == cartId);
			db.Carts.Remove(cart);

			db.SaveChanges();
		}
	}
}