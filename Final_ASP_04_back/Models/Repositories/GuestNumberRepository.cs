using Final_ASP_04_back.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04_back.Models.Repositories
{
	public class GuestNumberRepository
	{
		public GuestNumber GetGuestNumberByCapacity(int capacity)
		{
			var db = new AppDbContext();
			var guestNumber = db.GuestNumbers.Where(g => g.Capacity == capacity).FirstOrDefault();

			return guestNumber;
		}
	}
}