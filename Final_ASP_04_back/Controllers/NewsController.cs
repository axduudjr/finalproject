using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_ASP_04_back.Models.EFModels;

namespace Final_ASP_04_back.Controllers
{
	public class NewsController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: News
		public ActionResult Index()
		{
			var news = db.News.Include(n => n.Branch);
			var branches = db.Branches
							.Distinct()
							.Select(b => new { Id = b.Id, Name = b.Name })
							.ToList();
			ViewBag.Branch = branches;
			return View(news.ToList());
		}

		// GET: News/Create
		public ActionResult Create()
		{
			ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name");
			return View();
		}

		// POST: News/Create
		// 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,BranchId,FileName,Title,Description,CreatedTime,ModifiedTime")] News news)
		{
			news.CreatedTime = DateTime.Now;
			news.ModifiedTime = DateTime.Now;

			if (ModelState.IsValid)
			{
				db.News.Add(news);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name", news.BranchId);
			return View(news);
		}

		// GET: News/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			News news = db.News.Find(id);
			if (news == null)
			{
				return HttpNotFound();
			}
			ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name", news.BranchId);
			return View(news);
		}

		// POST: News/Edit/5
		// 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,BranchId,FileName,Title,Description,CreatedTime,ModifiedTime")] News news)
		{
			if (ModelState.IsValid)
			{
				var target = db.News.FirstOrDefault(n => n.Id == news.Id);
				target.ModifiedTime = DateTime.Now;
				target.Title = news.Title;
				target.Description = news.Description;
				target.BranchId = news.BranchId;
				target.FileName = news.FileName;

				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name", news.BranchId);
			return View(news);
		}

		// GET: News/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			News news = db.News.Find(id);
			if (news == null)
			{
				return HttpNotFound();
			}
			return View(news);
		}

		// POST: News/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			News news = db.News.Find(id);
			db.News.Remove(news);
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
