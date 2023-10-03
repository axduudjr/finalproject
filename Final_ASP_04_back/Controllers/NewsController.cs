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
	public class NewsController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: News
		[Authorize]
		public ActionResult Index()
		{
			var news = db.News.Include(n => n.Branch).ToList();

			List<NewsVm> vms = new List<NewsVm>();
			foreach (var n in news)
			{
				var vm = new NewsVm
				{
					Id = n.Id,
					BranchId = n.BranchId,
					BranchName = n.Branch.Name,
					Title = n.Title,
					Description = n.Description,
					CreatedTime = n.CreatedTime,
					ModifiedTime = n.ModifiedTime,
				};
				vms.Add(vm);
			}

			return View(vms);
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
			News news = db.News.FirstOrDefault(n => n.Id == id);
			if (news == null)
			{
				return HttpNotFound();
			}

			var vm = new NewsVm
			{
				Id = news.Id,
				BranchId = news.BranchId,
				BranchName = news.Branch.Name,
				Title = news.Title,
				Description = news.Description,
				CreatedTime = news.CreatedTime,
				ModifiedTime = news.ModifiedTime,
			};

			ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name", news.BranchId);
			return View(vm);
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
			News news = db.News.FirstOrDefault(n => n.Id == id);
			if (news == null)
			{
				return HttpNotFound();
			}

			var vm = new NewsVm
			{
				Id = news.Id,
				BranchId = news.BranchId,
				BranchName = news.Branch.Name,
				Title = news.Title,
				Description = news.Description,
				CreatedTime = news.CreatedTime,
				ModifiedTime = news.ModifiedTime,
			};
			return View(vm);
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
