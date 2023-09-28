﻿using Final_ASP_04_back.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Final_ASP_04_back.Controllers
{
	public class FAQsController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: FAQs
		public ActionResult Index()
		{
			return View(db.FAQs.ToList());
		}

		// GET: FAQs/Details/5
		public ActionResult Details(int? id)
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
			return View(fAQ);
		}

		// GET: FAQs/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: FAQs/Create
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Question,Answer")] FAQ fAQ)
		{
			if (ModelState.IsValid)
			{
				db.FAQs.Add(fAQ);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(fAQ);
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
			return View(fAQ);
		}

		// POST: FAQs/Edit/5
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Question,Answer")] FAQ fAQ)
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
			return View(fAQ);
		}

		// POST: FAQs/Delete/5
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