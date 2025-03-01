using Microsoft.AspNetCore.Mvc;

namespace PawFect.WebUI.ViewComponents
{
	public class _SocialMediaViewComponents:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
