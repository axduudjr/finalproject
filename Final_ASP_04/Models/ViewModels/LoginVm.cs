﻿using Final_ASP_04.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Final_ASP_04.Models.ViewModels
{
	public class LoginVm
	{
		[Display(Name = "帳號")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(50)]
		public string Account { get; set; }

		[Display(Name = "密碼")]
		[Required(ErrorMessage = DAHelper.Required)]
		[StringLength(50)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "記住我")]
		public bool remeberMe { get; set; }
	}
}