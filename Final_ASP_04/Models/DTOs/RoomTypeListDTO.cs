using Final_ASP_04.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Final_ASP_04.AutoMapperHelper;

namespace Final_ASP_04.Models.DTOs
{
	public class RoomTypeListDTO
	{
		public int Id { get; set; }
		public Branch Branch { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string FileName { get; set; }
		public int DisplayOrder { get; set; }
	}
	public static class RoomTypeListDTOExts
	{
		public static RoomTypeListDTO ToRoomTypeListDTO(this RoomType roomType)
		{
			return MapperObj.Map<RoomType, RoomTypeListDTO>(roomType);
		}
	}
}