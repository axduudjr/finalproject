using Final_ASP_04_back.Models.DTOs;
using Final_ASP_04_back.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_ASP_04_back.Models.ViewModels
{
	public class RoomManageVm
	{
		public int Id { get; set; }
		public int BranchId { get; set; }
		public int RoomTypeId { get; set; }
		[Display(Name = "房型名稱")]
		public string RoomTypeName { get; set; }
		public int GuestNumberId { get; set; }
		[Display(Name = "可容納人數")]
		public int GuestNumberCapacity { get; set; }
		[Display(Name = "房間號碼")]
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
		[Display(Name = "房間細部規格")]
		[StringLength(50)]
		public string Description { get; set; }
		[Display(Name = "房間價格")]
		[Required]
		public int Price { get; set; }
		public bool IsBooked { get; set; }
	}
	public static class RoomManageVmExts
	{
		public static RoomManageVm ToRoomManageVm(this RoomManageDTO dto)
		{
			return new RoomManageVm
			{
				Id = dto.Id,
				BranchId = dto.RoomType.BranchId,
				RoomTypeId = dto.RoomType.Id,
				RoomTypeName = dto.RoomType.Name,
				GuestNumberId = dto.GuestNumber.Id,
				GuestNumberCapacity = dto.GuestNumber.Capacity,
				Name = dto.Name,
				Description = dto.Description,
				Price = dto.Price
			};
		}
	}
}