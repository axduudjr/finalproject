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
        public ActionResult CreateComment()
        {
            return View();
        }
    }
}