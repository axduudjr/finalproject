using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Final_ASP_04_back.Models.ViewModels
{
	public class MemberVM
	{
		public int Id { get; set; }
		[Display(Name = "會員姓名")]
		public string Name { get; set; }
		[Display(Name = "會員電話")]
		public string PhoneNumber { get; set; }
		[Display(Name = "會員Email")]
		public string Email { get; set; }
		[Display(Name = "會員狀態")]
		public bool Enabled { get; set; }
		[Display(Name = "是否驗證")]
		public bool? IsConfirmed { get; set; }
	}
}