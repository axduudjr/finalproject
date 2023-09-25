using Final_ASP_04.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Repositories
{
	public class BranchRepository
	{
		public Branch GetBranchByRoomId(int roomId)
		{
			var db = new AppDbContext();
			var room = db.Rooms.Include("RoomType").Include("RoomType.Branch").FirstOrDefault(x => x.Id == roomId);

			return room.RoomType.Branch;
		}
		public List<Branch> GetAllBranches()
		{
			var db = new AppDbContext();
			var branches = db.Branches.ToList();

			return branches;
		}
	}
}