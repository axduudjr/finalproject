using AutoMapper;
using Final_ASP_04.Models.DTOs;
using Final_ASP_04.Models.EFModels;
using Final_ASP_04.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.App_Start
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{			
			CreateMap<RoomTypeListDTO, RoomType>();
			CreateMap<RoomType, RoomTypeListDTO>();
		}
	}
}