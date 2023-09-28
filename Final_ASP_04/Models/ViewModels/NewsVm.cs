using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.ViewModels
{
	public class NewsVm
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string FileName { get; set; }
	}
}