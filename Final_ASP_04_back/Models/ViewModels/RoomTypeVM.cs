using Final_ASP_04_back.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Final_ASP_04_back.Models.ViewModels
{
	public class RoomTypeVM
	{
		[Display(Name = "分店名稱")]
		public string BranchName { get; set; }
		[Display(Name = "房型名稱")]
		public string RoomTypeName { get; set; }
		[Display(Name = "房型介紹")]
		public string Description { get; set; }
	}

	public class CreateRoomTypeVM
	{
		[Display(Name = "分店名稱")]
		[Required(ErrorMessage = "{0} 必填")]
		public int BranchId { get; set; }
		[Display(Name = "房型名稱")]
		[Required(ErrorMessage = "{0} 必填")]
		public string RoomTypeName { get; set; }
		[Display(Name = "房型介紹")]
		[Required(ErrorMessage = "{0} 必填")]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }
		[Display(Name = "上傳房型照片")]
		[Required(ErrorMessage = "{0} 必填")]
		public string FileName { get; set; }
		[Display(Name = "排列順序")]
		[Required(ErrorMessage = "{0} 必填")]
		public int DisplayOrder { get; set; }
	}

	public static class RoomTypeExts
	{
		public static RoomTypeVM RoomType2VM(this RoomType model)
		{
			return new RoomTypeVM
			{
				BranchName = model.Branch.Name,
				RoomTypeName = model.Name,
				Description = model.Description
			};
		}

		public static RoomType VM2RoomType(this RoomTypeVM vm)
		{
			return new RoomType
			{

			};
		}
	}
}