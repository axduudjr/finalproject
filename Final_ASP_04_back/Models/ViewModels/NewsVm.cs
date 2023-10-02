using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_ASP_04_back.Models.ViewModels
{
	public class NewsVm
	{
		public int BranchName { get; set; }

		[Required]
		[StringLength(50)]
		public string Title { get; set; }

		[Required]
		public string Description { get; set; }

		public DateTime CreatedTime { get; set; }

		public DateTime ModifiedTime { get; set; }


	}
}