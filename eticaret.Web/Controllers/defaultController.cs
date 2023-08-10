using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eticaret.Web.Controllers
{
    public class defaultController : Controller
    {
        [Route("[action]"), Route("ajax/[action]")]
        public IActionResult underConstruction()
        {
            if (veriyoneticisi.isActive == false)
            {
                return PartialView();
            }
            else
            {
                return Redirect("/");
            }
        }
        public IActionResult Index()
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
        public IActionResult contact()
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
        public IActionResult aboutus()
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
        public IActionResult basket()
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
        public IActionResult productCheckout()
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
    }
}
