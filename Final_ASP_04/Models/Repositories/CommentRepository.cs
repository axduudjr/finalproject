using Dapper;
using Final_ASP_04.Models.EFModels;
using Final_ASP_04.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace Final_ASP_04.Models.Repositories
{
	public class CommentRepository
	{		
		public List<Comment> GetAllCommentsByBranchId(int branchId)
		{
			using (var conn = new SqlDb().GetConnection("AppDbContext"))
			{
				var parameters = new DynamicParameters();
				parameters.Add("@branchId", branchId);

				var result = conn.Query<Comment, Order, Branch, Comment>(
					"SelectCommentsByBranchId",
					(comment, order, branch) =>
					{
						comment.Order = order;
						order.Branch = branch;
						return comment;
					},
					parameters,splitOn: "Id",commandType: CommandType.StoredProcedure).ToList();

				return result;
			}
		}
		public void CreateComment(Comment comment)
		{			
			using (var conn = new SqlDb().GetConnection("AppDbContext"))
			{				
				var parameters = new DynamicParameters();
				parameters.Add("@orderId", comment.OrderId);
				parameters.Add("@Rank", comment.Rank);
				parameters.Add("@Description", comment.Description);
				parameters.Add("@CreatedTime", comment.CreatedTime);
				parameters.Add("@ModifiedTime", comment.ModifiedTime);

				conn.Execute("CreateComment", parameters, commandType: CommandType.StoredProcedure);
			}
		}
		public Comment GetCommentByOrderId(int orderId)
		{
			var db = new AppDbContext();
			var comment = db.Comments.FirstOrDefault(x => x.OrderId == orderId);

			return comment;
		}
	}
}