using Final_ASP_04.Models.DTOs;
using Final_ASP_04.Models.EFModels;
using Final_ASP_04.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Services
{
	public class CartService
	{
		public void CreateCart(CartCreateDTO dto)
		{
			var cart = new Cart
			{
				BranchId = dto.BranchId,
				RoomId = dto.RoomId,
				MemberId = dto.MemberId,
				Price = dto.Price,
				StartDateTime = dto.StartDateTime,
				EndDateTime = dto.EndDateTime,
			};
			var repo = new CartRepository();
			repo.CreateCart(cart);
		}
	}
}