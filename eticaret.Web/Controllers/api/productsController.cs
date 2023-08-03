using eticaret.Services.productsServices;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eticaret.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class productsController : ControllerBase
    {
        readonly IproductsService _IproductsService;
        public productsController(IproductsService IproductsService)
        {
            _IproductsService = IproductsService;
        }
        [Route("[action]"), HttpPost]
        public IActionResult getProduct([FromForm] Guid id)
        {
            var response = _IproductsService.getProduct(id);
            var data = response["data"];
            var responsePIL = response["productImageList"];
            var relatedProducts = response["relatedProducts"];
            var comments = response["comments"];
            var categoryName = response["categoryName"];
            var categoryID = response["categoryID"];
            var title = response["title"];
            var type = response["type"];
            var message = response["message"];
            return Ok(new { type = type, message = message, data = data, categoryName = categoryName, categoryID = categoryID, title = title, comments = comments, productImageList = responsePIL, relatedProducts = relatedProducts });

        }
    }
}
