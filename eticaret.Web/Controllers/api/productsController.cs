using eticaret.Services.productsServices;
using Microsoft.AspNetCore.Http;
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
            string userip = "";
            try
            {
                userip = HttpContext.Request.Headers.ContainsKey("CF-CONNECTING-IP") ? HttpContext.Request.Headers["CF-CONNECTING-IP"] : HttpContext.Connection.RemoteIpAddress.ToString();
            }
            catch { }
            var response = _IproductsService.getProduct(id, userip);
            var data = response["data"];
            var responsePIL = response["productImageList"];
            var relatedProducts = response["relatedProducts"];
            var comments = response["comments"];
            var categoryName = response["categoryName"];
            var categoryID = response["categoryID"];
            var title = response["title"];
            var type = response["type"];
            var message = response["message"];
            var productView = response["productView"];
            var averageRating = response["averageRating"];
            return Ok(new { type = type, message = message, data = data, categoryName = categoryName, categoryID = categoryID, title = title, comments = comments, productImageList = responsePIL, relatedProducts = relatedProducts, productView = productView, averageRating = averageRating });
        }

        [Route("[action]"), HttpPost]
        public IActionResult updateComment([FromForm] Guid productID, [FromForm] int rating, [FromForm] string detail)
        {
            if (HttpContext.Session.GetString("login") == "true" && !string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {
                Guid userID = Guid.Parse(HttpContext.Session.GetString("id"));
                var response = _IproductsService.updateComment(userID, productID, rating, detail);
                var type = response["type"];
                var message = response["message"];
                return Ok(new { type = type, message = message });
            }
            else
            {
                return Ok(new { type = "error", message = "Yetkisiz işlem" });
            }
        }

        [Route("[action]"), HttpPost]
        public IActionResult getTags([FromForm] string number)
        {
            var response = _IproductsService.getTags(number);
            return Ok(new { type = "success", message = "", data = response });
        }


    }
}
