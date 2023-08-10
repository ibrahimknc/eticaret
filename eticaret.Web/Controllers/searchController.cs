using Microsoft.AspNetCore.Mvc;

namespace eticaret.Web.Controllers
{

    [Route("[controller]"), Route("ajax/[controller]")]
    public class searchController : Controller
    {

        [HttpGet("{id}")]
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
    }
}
