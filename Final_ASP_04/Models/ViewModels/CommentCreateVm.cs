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
}