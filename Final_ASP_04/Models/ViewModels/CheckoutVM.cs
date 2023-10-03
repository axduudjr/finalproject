using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Final_ASP_04.Models.ViewModels
{
	public class CheckoutVM
	{
		public int Id { get; set; }
		[Display(Name = "姓名")]
		[Required(ErrorMessage = "{0}必填")]
		public string Name { get; set; }
		[Display(Name = "電話")]
		[Required(ErrorMessage = "{0}必填")]
		public string CellPhone { get; set; }
		[Display(Name = "Email")]
		[Required(ErrorMessage = "{0}必填")]
		public string Email { get; set; }

		public string BranchName { get; set; }
		public string RoomName { get; set; }
		public int Price { get; set; }
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
		[Display(Name = "付款方式")]
		public int? PaymentTypeId { get; set; }		
	}
}