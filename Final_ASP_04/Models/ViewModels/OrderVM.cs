using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Final_ASP_04.Models.ViewModels
{
	public class OrderVM
	{
		public int Id { get; set; }
		[Display(Name = "分店名稱")]
		public string BranchName { get; set; }
		[Display(Name = "房型")]
		public string RoomType { get; set; }
		[Display(Name = "入住日期")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime StartDate { get; set; }
		[Display(Name = "退房日期")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime EndDate { get; set; }
		[Display(Name = "預定時間")]
		public DateTime OrderTime { get; set; }
		[Display(Name = "總金額")]
		[DisplayFormat(DataFormatString = "NT$ {0:#,#}")]
		public int Price { get; set; }
		[Display(Name = "訂單狀態")]
		public string Status { get; set; }
		public string RoomPicFile { get; set; }
		public bool IsCommented { get; set; }
	}

	public class OrderCancelVM
	{
		public int Id { get; set; }

		[Display(Name = "分店名稱")]
		public string BranchName { get; set; }
		[Display(Name = "房型")]
		public string RoomType { get; set; }
		[Display(Name = "入住日期")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime StartDate { get; set; }
		[Display(Name = "退房日期")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime EndDate { get; set; }
		[Display(Name = "總金額")]
		[DisplayFormat(DataFormatString = "NT$ {0:#,#}")]
		public int Price { get; set; }
		[Display(Name = "付款方式")]
		public string PaymentType { get; set; }
		[Display(Name = "訂單狀態")]
		public string Status { get; set; }

	}

	public class OrderSuccessVM
	{
		public int Id { get; set; }

		[Display(Name = "分店名稱")]
		public string BranchName { get; set; }
		[Display(Name = "房型")]
		public string RoomType { get; set; }
		[Display(Name = "入住日期")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime StartDate { get; set; }
		[Display(Name = "退房日期")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime EndDate { get; set; }
		[Display(Name = "總金額")]
		[DisplayFormat(DataFormatString = "NT$ {0:#,#}")]
		public int Price { get; set; }
		[Display(Name = "付款方式")]
		public string PaymentType { get; set; }
		[Display(Name = "訂單狀態")]
		public string Status { get; set; }
		public string RoomPicFile { get; set; }

	}
}