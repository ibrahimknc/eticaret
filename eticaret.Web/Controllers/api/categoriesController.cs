using eticaret.Services.categoriesServices;
using eticaret.Services.categoriesServices.Dto;
using eticaret.Services.userServices.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Xml.Linq;

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
        public IActionResult getCategoriList([FromForm] Guid id, [FromForm] int page, [FromForm] int itemsPerPage, [FromForm] string search, [FromForm] string price, [FromForm] int listSorting)
        {
            var response = _IcategoriesService.getCategoriList(id, page, itemsPerPage, search, price, listSorting);
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
