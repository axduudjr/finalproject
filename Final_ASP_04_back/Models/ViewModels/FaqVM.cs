using Final_ASP_04_back.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Final_ASP_04_back.Models.ViewModels
{
	public class FaqVM
	{
		public int Id { get; set; }
		[Display(Name = "FAQ問題")]
		[Required(ErrorMessage = "{0} 必填")]
		[DataType(DataType.MultilineText)]
		public string Question { get; set; }
		[Display(Name = "FAQ答案")]
		[Required(ErrorMessage = "{0} 必填")]
		[DataType(DataType.MultilineText)]
		public string Answer { get; set; }
	}

	public static class FAQExts
	{
		public static FaqVM FAQ2VM(this FAQ faq)
		{
			return new FaqVM
			{
				Id = faq.Id,
				Question = faq.Question,
				Answer = faq.Answer
			};
		}

		public static FAQ VM2FAQ(this FaqVM vm)
		{
			return new FAQ
			{
				Id = vm.Id,
				Question = vm.Question,
				Answer = vm.Answer
			};
		}
	}
}