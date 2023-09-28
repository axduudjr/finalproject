using Final_ASP_04_back.Models.EFModels;
using Final_ASP_04_back.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Final_ASP_04_back.Controllers
{
	public class MailsController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: Mails
		public ActionResult Index(string memberName = "")
		{
			IQueryable<Mail> mails = db.Mails.Include(p => p.Member).OrderByDescending(o => o.CreatedTime);

			if (!string.IsNullOrEmpty(memberName))
			{
				mails = mails.Where(p => p.Member.Name.Contains(memberName));
			}

			var vm = mails.Select(p => new MailVM
			{
				MemberName = p.Member.Name,
				Title = p.Title,
				Description = p.Description,
				CreateTime = p.CreatedTime
			});

			return View(vm.ToList());
		}

		// GET: Mails/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Mail mail = db.Mails.Find(id);
			if (mail == null)
			{
				return HttpNotFound();
			}
			return View(mail);
		}

		// GET: Mails/Create
		public ActionResult Create()
		{
			ViewBag.MemberId = new SelectList(db.Members, "Id", "Account");
			return View();
		}

		// POST: Mails/Create
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,MemberId,Title,Description,CreatedTime")] Mail mail)
		{
			if (ModelState.IsValid)
			{
				db.Mails.Add(mail);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.MemberId = new SelectList(db.Members, "Id", "Account", mail.MemberId);
			return View(mail);
		}

		// GET: Mails/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Mail mail = db.Mails.Find(id);
			if (mail == null)
			{
				return HttpNotFound();
			}
			ViewBag.MemberId = new SelectList(db.Members, "Id", "Account", mail.MemberId);
			return View(mail);
		}

		// POST: Mails/Edit/5
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,MemberId,Title,Description,CreatedTime")] Mail mail)
		{
			if (ModelState.IsValid)
			{
				db.Entry(mail).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.MemberId = new SelectList(db.Members, "Id", "Account", mail.MemberId);
			return View(mail);
		}

		// GET: Mails/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Mail mail = db.Mails.Find(id);
			if (mail == null)
			{
				return HttpNotFound();
			}
			return View(mail);
		}

		// POST: Mails/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Mail mail = db.Mails.Find(id);
			db.Mails.Remove(mail);
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