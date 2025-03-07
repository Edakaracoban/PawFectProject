using Microsoft.AspNetCore.Mvc;
namespace PawFect.WebUI.ViewComponents
{
	public class _HeadLayout:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
