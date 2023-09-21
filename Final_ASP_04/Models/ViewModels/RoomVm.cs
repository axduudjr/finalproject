using Final_ASP_04.Models.DTOs;
using Final_ASP_04.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.ViewModels
{
	public class RoomVm
	{
		public string RoomTypeName { get; set; }
		public string RoomTypeDescription { get; set; }
		public int RoomTypeDisplayOrder { get; set; }
		public string RoomTypeFileName { get; set; }
		public string GuestNumberName { get; set; }
		public string RoomName { get; set; }
		public string RoomDescription { get; set; }
		public int Price { get; set; }
	}
	public static class RoomExts
	{
		public static RoomVm ToRoomVm(this RoomListDTO dto)
		{
			return new RoomVm
			{
				RoomTypeName = dto.RoomType.Name,
				RoomTypeDescription = dto.RoomType.Description,
				RoomTypeDisplayOrder = dto.RoomType.DisplayOrder,
				RoomTypeFileName = dto.RoomType.FileName,
				GuestNumberName = dto.GuestNumber.Name,
				RoomName = dto.Name,
				RoomDescription = dto.Description,
				Price = dto.Price
			};
		}
	}
}