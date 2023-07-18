using Microsoft.AspNetCore.Mvc;
using System;

namespace eticaret.Web.Controllers
{
    public class userController : Controller
    {
        [Route("[action]"), Route("ajax/[action]")]
        public IActionResult register()
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
        public IActionResult login()
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
