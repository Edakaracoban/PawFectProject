using Microsoft.AspNetCore.Mvc;
namespace PawFect.WebUI.ViewComponents
{
	public class _SvgLayout:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
