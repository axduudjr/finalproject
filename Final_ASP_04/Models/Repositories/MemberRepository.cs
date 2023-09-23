using Final_ASP_04.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Repositories
{
	public class MemberRepository
	{
		public Member GetMemberByAccount(string account)
		{
			var db = new AppDbContext();
			return db.Members.FirstOrDefault(m => m.Account == account);
		}
	}
}