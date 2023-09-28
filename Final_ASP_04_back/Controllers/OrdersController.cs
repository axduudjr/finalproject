using Final_ASP_04_back.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Final_ASP_04_back.Models.ViewModels;

namespace Final_ASP_04_back.Controllers
{
	public class OrdersController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: Orders
		public ActionResult Index(int branchId = 0, string memberName = "", DateTime? startDateTime = null)
		{
			var orders = db.Orders.Include(o => o.Branch)
								  .Include(o => o.Member)
								  .Include(o => o.PaymentType)
								   .Include(o => o.Room)
								   ;

			PrepareBranchDataSource();

			if (branchId > 0)
			{
				orders = orders.Where(o => o.BranchId == branchId);
			}


			if (!string.IsNullOrEmpty(memberName))
			{
				orders = orders.Where(p => p.Member.Name.Contains(memberName));
			}

			if (!startDateTime.HasValue)
			{
				startDateTime = DateTime.Now;
			}
			else
			{
				orders = orders.Where(p => p.StartDateTime >= startDateTime.Value);
			}

			orders = orders.OrderByDescending(o => o.StartDateTime);

			var vm = orders.Select(o => new OrderListVM
			{
				Id = o.Id,
				MemberName = o.Member.Name,
				BranchName = o.Branch.Name,
				RoomTypeName = o.Room.RoomType.Name + o.Room.GuestNumber.Name,
				RoomName = o.Room.Name,
				StartDateTime = o.StartDateTime,
				EndDateTime = o.EndDateTime,
				Status = o.Status
			}).ToList();

			ViewBag.Orders = vm;

			return View(vm);
		}
		private void PrepareBranchDataSource()
		{
			var items = db.Branches
				.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name })
				.ToList()
				.Prepend(new SelectListItem() { Value = "0", Text = "請選擇" });

			ViewData["BranchId"] = new SelectList(items, "Value", "Text", null);
		}
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Order order = db.Orders.Find(id);
			if (order == null)
			{
				return HttpNotFound();
			}

			return View(order);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Order order, string submitButton)
		{
			var orderInDb = db.Orders.Find(order.Id);

			if (submitButton == "確認顧客已入住")
			{
				orderInDb.Status = "已入住";
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			if (submitButton == "顧客來電取消訂單")
			{
				orderInDb.Status = "已取消";
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}