using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.ViewModels
{
	public class CommentListVm
	{
		public string Description { get; set; }
		public int Rank { get; set; }
		public DateTime CreatedTime { get; set; }
	}
}