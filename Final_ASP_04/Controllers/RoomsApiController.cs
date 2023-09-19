using Final_ASP_04.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_ASP_04.Controllers
{
    public class RoomsApiController : ApiController
    {
		[HttpGet]
		public IHttpActionResult GetRoomTypes()
		{
			var roomtypes = new AppDbContext().RoomTypes.OrderBy(x => x.DisplayOrder).Select(x => new { x.Id, x.Name, x.DisplayOrder }).ToList();

			return Ok(roomtypes);
		}
		[HttpGet]
		public IHttpActionResult GetGuestNumbers()
		{
			var guestnumbers = new AppDbContext().GuestNumbers.OrderBy(x => x.DisplayOrder).Select(x => new { x.Id, x.Name, x.DisplayOrder }).ToList();

			return Ok(guestnumbers);
		}
	}
}
