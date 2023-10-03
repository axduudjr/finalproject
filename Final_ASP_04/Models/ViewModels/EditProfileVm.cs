using Final_ASP_04.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Final_ASP_04.Models.ViewModels
{
	public class EditProfileVm
	{
		public int Id { get; set; }

		[Display(Name = "帳號")]
		[StringLength(50)]
		public string Account { get; set; }

		[Display(Name = "姓名")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(50)]
		public string Name { get; set; }

		[Display(Name = "手機號碼")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(10)]
		public string PhoneNumber { get; set; }

		[Display(Name = "信箱")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(50)]
		[EmailAddress]
		public string Email { get; set; }
	}
}