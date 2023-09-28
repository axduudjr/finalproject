using Final_ASP_04_back.Infra.FileValidator;
using Final_ASP_04_back.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Final_ASP_04_back.Models.ViewModels;

namespace Final_ASP_04_back.Controllers
{
	public class RoomTypesController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: RoomTypes
		public ActionResult Index()
		{
			var roomTypes = db.RoomTypes.Include(r => r.Branch);
			var vm = roomTypes.Select(o => new RoomTypeVM
			{
				BranchName = o.Branch.Name,
				Description = o.Description,
				RoomTypeName = o.Name
			}).ToList();

			return View(vm);
		}

		public ActionResult Create()
		{
			List<Branch> items = db.Branches.ToList();
			IEnumerable<SelectListItem> branchList = new SelectList(items, "Id", "Name");

			ViewBag.BranchId = branchList;

			return View();
		}

		[HttpPost]
		public ActionResult Create(CreateRoomTypeVM roomType, HttpPostedFileBase myfile)
		{
			List<Branch> items = db.Branches.ToList();
			IEnumerable<SelectListItem> branchList = new SelectList(items, "Id", "Name");

			ViewBag.BranchId = branchList;

			#region 將上傳檔案存到 /Files 資料夾下
			string fileName;
			string path = Server.MapPath("~/Images");
			IFileValidator[] validators =
				new IFileValidator[]
				{
					new FileRequired(), // 必填
                    new ImageValidator(), // 必須是圖檔
                    new FileSizeValidator(1024) // 1MB
                };

			try
			{
				fileName = UploadFileHelper.Save(myfile, path, validators);

				//// copy一份到前台網站的資料夾
				//string sourceFullPath = Path.Combine(path, fileName);

				//// string dest = @"c:\MyFiles\";
				//string dest = System.Configuration
				//    .ConfigurationManager
				//    .AppSettings["frontSiteRootPath"];

				//string destFullPath = Path.Combine(dest, fileName);

				//System.IO.File.Copy(sourceFullPath, destFullPath, true);

			}
			catch (Exception ex)
			{
				ModelState.AddModelError("myfile", ex.Message);
				return View(roomType);
			}

			#endregion

			// 將檔名存入
			roomType.FileName = fileName;

			// 將紀錄存檔
			var newRoomType = new RoomType
			{
				BranchId = roomType.BranchId,
				Name = roomType.RoomTypeName,
				Description = roomType.Description,
				FileName = fileName,
				DisplayOrder = roomType.DisplayOrder
			};

			db.RoomTypes.Add(newRoomType);
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