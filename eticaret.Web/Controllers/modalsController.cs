using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Policy;

namespace eticaret.Web.Controllers
{
    [Route("[controller]"), Route("ajax/[controller]")]
    public class modalsController : Controller
    {
        [Route("[action]")]
        public IActionResult share([FromQuery] string url, [FromQuery] string title)
        {
            try
            { 
                ViewBag.url = url; 
                ViewBag.title = title;
                return PartialView();
            }
            catch { }
            return NotFound();
        }

        [Route("[action]")]
        public IActionResult iyzipay([FromQuery] string data)
        {
            try
            {
                ViewBag.data = data;
                return PartialView();
            }
            catch { }
            return NotFound();
        }
    }
}
