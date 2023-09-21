using Final_ASP_04.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Repositories
{
	public class CommentRepository
	{
		public List<Comment> GetHighRankCommentsByRoomTypeId(int roomTypeId)
		{
			var db = new AppDbContext();

			var comments = db.Comments.Where(x => x.Order.Room.RoomTypeId == roomTypeId && x.Rank >= 4).ToList();

			return comments;
		}
	}
}