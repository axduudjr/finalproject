using Final_ASP_04.Models.EFModels;
using Final_ASP_04.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_ASP_04.Controllers
{
	public class CheckoutController : Controller
	{
		private AppDbContext db = new AppDbContext();
		// GET: Checkout
		public ActionResult CheckoutStepOne(int cartId)
		{
			var cart = GetCart(cartId);
			ViewBag.CartId = cartId;

			return View(cart);
		}

		private CheckoutVM GetCart(int cartId)
		{
			var cart = db.Carts.FirstOrDefault(o => o.Id == cartId);
			var vm = new CheckoutVM
			{
				Id = cart.Id,
				Name = cart.Member.Name,
				CellPhone = cart.Member.PhoneNumber,
				Email = cart.Member.Email,
				BranchName = cart.Branch.Name,
				RoomName = cart.Room.RoomType.Name + cart.Room.GuestNumber.Name,
				Price = cart.Price,
				StartDateTime = cart.StartDateTime,
				EndDateTime = cart.EndDateTime,
				PaymentTypeId = cart.PaymentTypeId
			};
			return vm;
		}

		public ActionResult CheckoutStepTwo(int cartId)
		{
			var cart = GetCart(cartId);
			PreparePaymentTypeDataSource();
			return View(cart);
		}
		private void PreparePaymentTypeDataSource()
		{
			List<PaymentType> items = db.PaymentTypes.ToList();
			var paymentTypeList = new SelectList(items, "Id", "Name");
			ViewBag.PaymentTypes = paymentTypeList;

		}
		[HttpPost]
		public ActionResult CheckoutStepTwo(CheckoutVM vm)
		{
			var cartInDb = db.Carts.Find(vm.Id);
			cartInDb.PaymentTypeId = vm.PaymentTypeId;
			db.SaveChanges();

			var newId = CreateOrder(cartInDb);

			// 清空Cart，測試先拿掉
			// EmptyCart(cartInDb);

			return RedirectToAction("CheckoutStepThree", new { newId });

		}

		private void EmptyCart(Cart cartInDb)
		{
			var cart = db.Carts.FirstOrDefault(o => o.Id == cartInDb.Id);
			if (cart == null) return;

			db.Carts.Remove(cart);
			db.SaveChanges();
		}

		private int CreateOrder(Cart cart)
		{
			var order = new Order
			{
				MemberId = cart.Member.Id,
				BranchId = cart.Branch.Id,
				RoomId = cart.Room.Id,
				Price = cart.Price,
				StartDateTime = cart.StartDateTime,
				EndDateTime = cart.EndDateTime,
				PaymentTypeId = cart.PaymentTypeId.Value,
				OrderTime = DateTime.Now,
				Status = "已付款"
			};

			db.Orders.Add(order);
			db.SaveChanges();

			return order.Id;
		}

		public ActionResult CheckoutStepThree(int newId)
		{
			var order = db.Orders.FirstOrDefault(x => x.Id == newId);
			var vm = new OrderSuccessVM
			{
				BranchName = order.Branch.Name,
				RoomType = order.Room.RoomType.Name + order.Room.GuestNumber.Name,
				StartDate = order.StartDateTime,
				EndDate = order.EndDateTime,
				Price = order.Price,
				PaymentType = order.PaymentType.Name,
				Status = order.Status
			};
			return View(vm);
		}
	}
}