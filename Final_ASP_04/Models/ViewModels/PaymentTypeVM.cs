using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Final_ASP_04.Models.ViewModels
{
	public class PaymentTypeVM
	{
		public int Id { get; set; }
		[Display(Name = "選擇付款方式")]
		public string PaymentName { get; set; }
	}
}