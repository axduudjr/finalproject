using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Final_ASP_04_back.Models.ViewModels
{
	public class LoginVM
	{
		[Display(Name = "帳號")]
		[StringLength(50)]
		public string Account { get; set; }

		[Display(Name = "密碼")]
		[StringLength(50)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}