using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_ASP_04_back.Models.EFModels;
using Final_ASP_04_back.Models.ViewModels;

namespace Final_ASP_04_back.Controllers
{
	public class FAQsController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: FAQs
		[Authorize]
		public ActionResult Index()
		{
			var vm = db.FAQs.Select(o => new FaqVM
			{
				Id = o.Id,
				Question = o.Question,
				Answer = o.Answer
			}).ToList();

			return View(vm);
		}

		// GET: FAQs/Create
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(FaqVM vm)
		{
			if (ModelState.IsValid)
			{
				var fAQ = vm.VM2FAQ();
				db.FAQs.Add(fAQ);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(vm);
		}

		// GET: FAQs/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			FAQ fAQ = db.FAQs.Find(id);
			if (fAQ == null)
			{
				return HttpNotFound();
			}

			var vm = fAQ.FAQ2VM();

			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(FAQ fAQ)
		{
			if (ModelState.IsValid)
			{
				db.Entry(fAQ).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(fAQ);
		}

		// GET: FAQs/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			FAQ fAQ = db.FAQs.Find(id);
			if (fAQ == null)
			{
				return HttpNotFound();
			}
			var vm = fAQ.FAQ2VM();
			return View(vm);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			FAQ fAQ = db.FAQs.Find(id);
			db.FAQs.Remove(fAQ);
			db.SaveChanges();
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
