using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 

namespace eticaret.Web.Controllers
{
    [Route("[controller]"), Route("ajax/[controller]")]
    public class userController : Controller
	{ 
        [HttpGet("profile/{id}")]
        public IActionResult profile()
        { 
            if (veriyoneticisi.isActive == true)
            {
				if (HttpContext.Session.GetString("login") == "true" && !string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
				{
					if (!Request.Path.Value.Contains("/ajax/"))
						return View();
					else
						return PartialView();
				}
				else
				{
                    return Redirect("/user/login");
                }
            }
            else
            {
                return Redirect("/underConstruction");
            }
        }

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
