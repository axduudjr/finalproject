using Final_ASP_04_back.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_ASP_04_back.Models.ViewModels
{
	public class NewsVm
	{
		public int Id { get; set; }

		public int BranchId { get; set; }

		[Display(Name = "標題")]
		[Required]
		[StringLength(50)]
		public string Title { get; set; }

		[Display(Name = "內容")]
		[Required]
		public string Description { get; set; }

		[Display(Name = "創建時間")]
		public DateTime CreatedTime { get; set; }

		[Display(Name = "更改時間")]
		public DateTime ModifiedTime { get; set; }

		[Display(Name = "分店店名")]
		public string BranchName { get; set; }
	}
}