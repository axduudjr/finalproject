using Final_ASP_04.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Final_ASP_04.AutoMapperHelper;

namespace Final_ASP_04.Models.DTOs
{
	public class CommentCreateDTO
	{
		public int OrderId { get; set; }
		public int Rank { get; set; }
		public string Description { get; set; }
		public DateTime CreatedTime { get; set; }
		public DateTime ModifiedTime { get; set; }
	}
	public static class CommentCreateDTOExts
	{
		public static Comment DtoToComment(this CommentCreateDTO dto)
		{
			return MapperObj.Map<CommentCreateDTO, Comment>(dto);
		}
	}
}