using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eticaret.Web.Controllers
{
    public class defaultController : Controller
    {
        [Route("[action]"), Route("ajax/[action]")]
        public IActionResult underConstruction()
        {
            if (Convert.ToBoolean(layout.settings("isActive")) == false)
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
            if (Convert.ToBoolean(layout.settings("isActive")) == true)
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
            if (Convert.ToBoolean(layout.settings("isActive")) == true)
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
