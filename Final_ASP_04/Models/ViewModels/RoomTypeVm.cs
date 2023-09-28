using Final_ASP_04.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Final_ASP_04.Models.ViewModels
{
	public class RoomTypeVm
	{
		public int Id { get; set; }
		public int BranchId { get; set; }
		public string BranchName { get; set; }
		public string RoomTypeName { get; set; }
		public string RoomTypeDescription { get; set; }
		public string FileName { get; set; }
		public int BestPrice { get; set; }
	}
	public static class RoomTypeExts
	{
		public static RoomTypeVm ToRoomTypeVm(this RoomTypeListDTO dto)
		{
			return new RoomTypeVm
			{
				Id = dto.Id,
				BranchId = dto.Branch.Id,
				BranchName = dto.Branch.Name,
				RoomTypeName = dto.Name,
				RoomTypeDescription = dto.Description,
				FileName = dto.FileName
			};
		}
	}
}