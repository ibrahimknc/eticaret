using Microsoft.AspNetCore.Mvc;
using System;

namespace eticaret.Web.Controllers
{
	[Route("[controller]"), Route("ajax/[controller]")]
	public class categoriesController : Controller
	{
		[Route("[action]")]
		public IActionResult list()
		{
			if (veriyoneticisi.isActive == true)
			{
				if (!Request.Path.Value.Contains("/ajax/"))
					return View();
				else
					return PartialView();
			}
			else
			{
				return Redirect("/underConstruction");
			}
		}
	}
}
