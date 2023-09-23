using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Final_ASP_04.Models.ViewModels
{
	public class ForgetPasswordVm
	{
		[Display(Name = "信箱")]
		[StringLength(50)]
		[EmailAddress]
		public string Email { get; set; }
	}
}