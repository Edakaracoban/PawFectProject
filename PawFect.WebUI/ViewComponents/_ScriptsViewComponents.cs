using Microsoft.AspNetCore.Mvc;

namespace PawFect.WebUI.ViewComponents
{
	public class _ScriptsViewComponents:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
