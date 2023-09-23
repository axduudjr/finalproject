using Final_ASP_04.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Final_ASP_04.Models.ViewModels
{
	public class ChangePasswordVm
	{
		[Display(Name = "原始密碼")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(50)]
		[DataType(DataType.Password)]
		public string OldPassword { get; set; }

		[Display(Name = "新密碼")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(50)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "確認新密碼")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(50)]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
	}
}