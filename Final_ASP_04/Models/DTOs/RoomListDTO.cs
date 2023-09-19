using Final_ASP_04.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.DTOs
{
	public class RoomListDTO
	{
		public int Id { get; set; }
		public RoomType RoomType { get; set; }
		public GuestNumber GuestNumber { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Price { get; set; }
	}
	public static class RoomListDTOExts 
	{ 		
		public static RoomListDTO ToRoomListDTO(this Room room)
		{
			return new RoomListDTO
			{
				Id = room.Id,
				RoomType = room.RoomType,
				GuestNumber = room.GuestNumber,
				Name = room.Name,
				Description = room.Description,
				Price = room.Price
			};
		}
	}
}