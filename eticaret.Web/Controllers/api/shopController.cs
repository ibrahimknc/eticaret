using eticaret.Services.shopServices; 
using Microsoft.AspNetCore.Mvc;
using System;

namespace eticaret.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class shopController : ControllerBase
    {
        readonly IshopService _IshopService;
        public shopController(IshopService IshopService)
        {
            _IshopService = IshopService;
        }

        [Route("[action]"), HttpPost]
        public IActionResult getShopProduct([FromForm] Guid id, [FromForm] int page, [FromForm] int itemsPerPage, [FromForm] string search, [FromForm] int listSorting)
        {
            var response = _IshopService.getProduct(id, page, itemsPerPage, search, listSorting);
            var data = response["data"];
            var title = response["title"];
            var titleIMG = response["titleIMG"];
            var type = response["type"];
            var c = response["c"];
            var message = response["message"];
            return Ok(new { type = type, message = message, data = data, title = title, c = c, titleIMG = titleIMG });
        }
    }
}

