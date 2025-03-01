using Microsoft.AspNetCore.Mvc;

namespace PawFect.WebUI.ViewComponents
{

	public class _FooterViewComponents : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
