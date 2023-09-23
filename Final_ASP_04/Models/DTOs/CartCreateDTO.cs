using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.DTOs
{
	public class CartCreateDTO
	{
		public int MemberId { get; set; }
		public int BranchId { get; set; }
		public int RoomId { get; set; }
		public int Price { get; set; }
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
		public int PaymentTypeId { get; set; }
		public string Status { get; set; }
	}
}