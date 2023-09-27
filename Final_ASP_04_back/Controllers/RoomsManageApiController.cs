using Final_ASP_04_back.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_ASP_04_back.Controllers
{
    public class RoomsManageApiController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetBranches()
        {
            var db = new AppDbContext();
            var branches = db.Branches.Select(b => new { b.Id, b.Name }).ToList();

            return Ok(branches);
        }
    }
}
