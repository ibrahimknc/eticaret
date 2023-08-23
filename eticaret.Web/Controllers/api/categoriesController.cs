using eticaret.Services.categoriesServices;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eticaret.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoriesController : ControllerBase
    {
        readonly IcategoriesService _IcategoriesService;
        public categoriesController(IcategoriesService IcategoriesService)
        {
            _IcategoriesService = IcategoriesService;
        }

        [Route("[action]"), HttpPost]
        public IActionResult getCategoriList([FromForm] Guid id, [FromForm] int page, [FromForm] int itemsPerPage, [FromForm] string search, [FromForm] string price, [FromForm] int listSorting, [FromForm] int rating, [FromForm] int isStock)
        {
            var response = _IcategoriesService.getCategoriList(id, page, itemsPerPage, search, price, listSorting, rating, isStock);
            var data = response["data"];
            var name = response["name"];
            var type = response["type"];
            var message = response["message"];
            var c = response["c"];
            var tags = response["tags"];
            return Ok(new { type = type, message = message, data = data, c = c, name = name, tags = tags });
        }
    }
}
