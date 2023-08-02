using Microsoft.AspNetCore.Mvc; 

namespace eticaret.Web.Controllers
{
    [Route("[controller]"), Route("ajax/[controller]")]
    public class userController : Controller
	{
		[Route("[action]"), Route("ajax/[action]")]
		public IActionResult register()
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

		[Route("[action]"), Route("ajax/[action]")]
		public IActionResult login()
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
