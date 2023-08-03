using Microsoft.AspNetCore.Mvc; 

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
    }
}
