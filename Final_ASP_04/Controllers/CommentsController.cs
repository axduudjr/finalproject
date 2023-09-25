﻿using Final_ASP_04.Models.EFModels;
using Final_ASP_04.Models.Repositories;
using Final_ASP_04.Models.Services;
using Final_ASP_04.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_ASP_04.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments
        public ActionResult ListComments(int branchId)
        {
            var sercice = new CommentService();
            var comments = sercice.GetAllCommentsByBranchId(branchId);
            var commentVms = comments.Select(x => new CommentListVm
            {
                Description = x.Description,
				Rank = x.Rank,
				CreatedTime = x.CreatedTime
            });

            return PartialView(commentVms.ToList());
        }
        public ActionResult CreateComment(int orderId)
        {
			var vm = new CommentCreateVm
            {
				OrderId = orderId
			};

            var repo = new OrderRepository();
            var order = repo.GetOrder(orderId);

            ViewBag.Order = order;

			return View(vm);
		}
        [HttpPost]
        public ActionResult CreateComment(CommentCreateVm vm)
        {
            var service = new CommentService();
            var dto = vm.ToCommentCreateDTO();

            service.CreateComment(dto);

            return RedirectToAction("ListRoomTypes", "Branches");
        }
    }
}