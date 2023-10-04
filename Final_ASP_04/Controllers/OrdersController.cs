using Final_ASP_04.Models.EFModels;
using Final_ASP_04.Models.ViewModels;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Final_ASP_04.Controllers
{
	public class OrdersController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: Orders
		[Authorize]
		public ActionResult Index(int branchId = 0)
		{
			var buyer = User.Identity.Name;

			PrepareBranchDataSource();

			IQueryable<Order> orders = db.Orders.Include(o => o.Branch)
								  .Include(o => o.Member)
								  .Include(o => o.PaymentType)
								  .Include(o => o.Room)
								  .OrderByDescending(o => o.StartDateTime);
			if (branchId == 0)
			{
				orders = orders.Where(o => o.Member.Account == buyer);
			}
			else
			{
				orders = orders.Where(o => o.BranchId == branchId && o.Member.Account == buyer);
			}

			var vm = orders.Select(o => new OrderVM
			{
				Id = o.Id,
				BranchName = o.Branch.Name,
				RoomType = o.Room.RoomType.Name + o.Room.GuestNumber.Name,
				StartDate = o.StartDateTime,
				EndDate = o.EndDateTime,
				OrderTime = o.OrderTime,
				Price = o.Price,
				Status = o.Status,
				RoomPicFile = o.Room.FileName,
				IsCommented = db.Comments.Any(c => c.OrderId == o.Id)
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
		[HttpGet]		
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Order orders = db.Orders.Find(id);
			if (orders == null)
			{
				return HttpNotFound();
			}

			var vm = new OrderCancelVM
			{
				Id = orders.Id,
				BranchName = orders.Branch.Name,
				RoomType = orders.Room.RoomType.Name + orders.Room.GuestNumber.Name,
				StartDate = orders.StartDateTime,
				EndDate = orders.EndDateTime,
				Price = orders.Price,
				PaymentType = orders.PaymentType.Name,
				Status = orders.Status
			};
			return View(vm);
		}

		[HttpPost]
		public ActionResult Edit(OrderCancelVM vm, string submitButton)
		{
			var orderInDb = db.Orders.Find(vm.Id);

			if (!ModelState.IsValid) return View(vm);

			if (submitButton == "取消訂單")
			{
				if (orderInDb.Status == "已取消" || orderInDb.Status == "已入住")
				{
					return View(vm);
				}
				orderInDb.Status = "已取消";
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			if (submitButton == "確認更改時間")
			{
				if (orderInDb.Status == "已修改" || orderInDb.Status == "已入住")
				{
					return View(vm);
				}

				bool isOverlap = db.Orders.Any(o =>
									o.RoomId == orderInDb.RoomId &&
									o.BranchId == orderInDb.BranchId &&
									o.StartDateTime < vm.EndDate && o.EndDateTime > vm.StartDate &&
									o.Status != "已取消"
									);

				if (isOverlap)
				{
					ModelState.AddModelError("", "該時段已被預訂，請選擇其他日期");
					return View(vm);
				}

				orderInDb.StartDateTime = vm.StartDate;
				orderInDb.EndDateTime = vm.EndDate;
				orderInDb.Status = "已修改";
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