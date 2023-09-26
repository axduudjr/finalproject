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
		public int Id { get; set; }
		public int RoomTypeId { get; set; }
		public string RoomTypeName { get; set; }
		public string RoomTypeDescription { get; set; }
		public int RoomTypeDisplayOrder { get; set; }
		public string RoomTypeFileName { get; set; }
		public int GuestNumberId { get; set; }
		public string GuestNumberName { get; set; }		
		public string RoomName { get; set; }
		public string RoomDescription { get; set; }
		public int Price { get; set; }
		public string RoomFileName { get; set; }
	}
	public static class RoomExts
	{
		public static RoomVm ToRoomVm(this RoomListDTO dto)
		{
			return new RoomVm
			{
				Id = dto.Id,
				RoomTypeId = dto.RoomType.Id,
				RoomTypeName = dto.RoomType.Name,
				RoomTypeDescription = dto.RoomType.Description,
				RoomTypeDisplayOrder = dto.RoomType.DisplayOrder,
				RoomTypeFileName = dto.RoomType.FileName,
				GuestNumberId = dto.GuestNumber.Id,
				GuestNumberName = dto.GuestNumber.Name,				
				RoomName = dto.Name,
				RoomDescription = dto.Description,
				Price = dto.Price,
				RoomFileName = dto.FileName
			};
		}
	}
}