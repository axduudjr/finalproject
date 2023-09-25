using Final_ASP_04.Models.EFModels;
using Final_ASP_04.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Services
{
	public class BranchService
	{
		public List<Branch> GetAllBranches()
		{
			var repo = new BranchRepository();
			var branches = repo.GetAllBranches();

			return branches;
		}
	}
}