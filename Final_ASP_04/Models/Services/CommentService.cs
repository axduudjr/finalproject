using Final_ASP_04.Models.DTOs;
using Final_ASP_04.Models.EFModels;
using Final_ASP_04.Models.Repositories;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Services
{
	public class CommentService
	{
		public List<Comment> GetAllCommentsByBranchId(int branchId)
		{
			var repo = new CommentRepository();
			var comments = repo.GetAllCommentsByBranchId(branchId).OrderByDescending(x => x.Rank).ToList();

			return comments;
		}
		public void CreateComment(CommentCreateDTO dto)
		{
			var repo = new CommentRepository();	
			var comment = dto.DtoToComment();

			repo.CreateComment(comment);
		}
	}
}