using Final_ASP_04.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Final_ASP_04.Models.ViewModels
{
	public class RegisterVm
	{
		[Display(Name = "姓名")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(50)]
		public string Name { get; set; }

		[Display(Name = "帳號")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(50)]
		public string Account { get; set; }

		[Display(Name = "密碼")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(50)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "確認密碼")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(50)]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }

		[Display(Name = "手機")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(10)]
		public string PhoneNumber { get; set; }

		[Display(Name = "信箱")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(50)]
		[EmailAddress(ErrorMessage = DAHelper.EmailAddress)]
		public string Email { get; set; }
	}
}