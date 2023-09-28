using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Final_ASP_04_back.Models.ViewModels
{
	public class OrderListVM
	{
		public int Id { get; set; }
		[Display(Name = "會員姓名")]
		public string MemberName { get; set; }
		[Display(Name = "分店名稱")]
		public string BranchName { get; set; }
		[Display(Name = "房型")]
		public string RoomTypeName { get; set; }
		[Display(Name = "房號")]
		public string RoomName { get; set; }
		[Display(Name = "入住日期")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime StartDateTime { get; set; }
		[Display(Name = "退房日期")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime EndDateTime { get; set; }
		[Display(Name = "訂單狀態")]
		public string Status { get; set; }
	}
}