using System.Web;
using System.Web.Mvc;

namespace Final_ASP_04_back
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
