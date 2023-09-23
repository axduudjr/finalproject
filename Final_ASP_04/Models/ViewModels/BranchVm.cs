using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.ViewModels
{
	public class BranchVm
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public List<PictureVm> FileName { get; set; }
		public string CarouselExampleId { get; set; }
		public string DataBsTarget { get; set; }
	}
}