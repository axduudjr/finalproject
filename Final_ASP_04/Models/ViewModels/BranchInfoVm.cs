using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.ViewModels
{
	public class BranchInfoVm
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public List<TrafficInfoVm> Traffics { get; set; }
	}

	public class TrafficInfoVm
	{
		public string Title { get; set; }
		public string Description { get; set; }
	}
}