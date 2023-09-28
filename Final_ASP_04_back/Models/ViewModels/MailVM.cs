using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Final_ASP_04_back.Models.ViewModels
{
	public class MailVM
	{
		[Display(Name = "會員姓名")]
		public string MemberName { get; set; }
		[Display(Name = "來信主旨")]
		public string Title { get; set; }
		[Display(Name = "來信內容")]
		public string Description { get; set; }
		[Display(Name = "來信建立時間(由新到舊)")]
		public DateTime CreateTime { get; set; }
	}
}