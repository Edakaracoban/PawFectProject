﻿using Microsoft.AspNetCore.Mvc;

namespace PawFect.WebUI.ViewComponents
{
	public class _MailViewComponents:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
