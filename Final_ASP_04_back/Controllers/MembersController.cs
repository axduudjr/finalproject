using Final_ASP_04_back.Models.EFModels;
using Final_ASP_04_back.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Final_ASP_04_back.Controllers
{
	public class MembersController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: Members
		[Authorize]
		public ActionResult Index(string memberName = "", string phoneNumber = "", string email = "")
		{
			IQueryable<Member> members = db.Members;

			if (!string.IsNullOrEmpty(memberName))
			{
				members = members.Where(p => p.Name.Contains(memberName));
			}
			if (!string.IsNullOrEmpty(phoneNumber))
			{
				members = members.Where(p => p.PhoneNumber.Equals(phoneNumber));
			}
			if (!string.IsNullOrEmpty(email))
			{
				members = members.Where(p => p.Email.Contains(email));
			}

			var vm = members.Select(o => new MemberVM
			{
				Id = o.Id,
				Name = o.Name,
				PhoneNumber = o.PhoneNumber,
				Email = o.Email,
				Enabled = o.Enabled,
				IsConfirmed = o.IsConfirmed
			}).ToList();

			return View(vm);
		}

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Member member = db.Members.Find(id);
			if (member == null)
			{
				return HttpNotFound();
			}

			var vm = new MemberVM
			{
				Id = member.Id,
				Name = member.Name,
				PhoneNumber = member.PhoneNumber,
				Email = member.Email,
				Enabled = member.Enabled,
				IsConfirmed = member.IsConfirmed
			};

			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Member member)
		{
			var memberInDb = db.Members.Find(member.Id);

			memberInDb.Enabled = member.Enabled;
			db.SaveChanges();

			return RedirectToAction("Index");
		}

		// GET: Members/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Member member = db.Members.Find(id);
			if (member == null)
			{
				return HttpNotFound();
			}
			return View(member);
		}

		// POST: Members/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Member member = db.Members.Find(id);
			db.Members.Remove(member);
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