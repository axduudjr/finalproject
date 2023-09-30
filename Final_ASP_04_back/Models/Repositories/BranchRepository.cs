using Final_ASP_04_back.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04_back.Models.Repositories
{
	public class BranchRepository
	{
		public Branch GetBranch(int id)
		{
			var db = new AppDbContext();

			var branch = db.Branches.Where(x => x.Id == id).FirstOrDefault();

			return branch;
		}
	}
}