using Final_ASP_04.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.ViewModels
{
	public class CommentCreateVm
	{
		public int OrderId { get; set; }
		public int Rank { get; set; }
		public string Description { get; set; }
	}
	public static class CommentCreateVmExts 
	{ 
		public static CommentCreateDTO ToCommentCreateDTO(this CommentCreateVm vm)
		{
			return new CommentCreateDTO
			{
				OrderId = vm.OrderId, 
				Rank = vm.Rank, 
				Description = vm.Description, 
				CreatedTime = DateTime.Now, 
				ModifiedTime = DateTime.Now 
			};
		}
	}
}