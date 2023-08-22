using eticaret.Domain.Entities;
using eticaret.Services.bulletinServices;
using eticaret.Services.defaultPageServices;
using eticaret.Services.myordersServices;
using eticaret.Services.productCheckoutServices;
using eticaret.Services.productCheckoutServices.Dto;
using eticaret.Services.searchService;
using eticaret.Services.settingsServices;
using eticaret.Services.sliderServices;
using eticaret.Services.viewCategoryServices;
using eticaret.Services.viewsFavoriteServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace eticaret.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class defaultController : ControllerBase
    {
        readonly IsettingsService _IsettingsService;
        readonly ImyordersService _ImyordersService;
        readonly IsearchService _IsearchService;
        readonly IviewsFavoriteService _IviewsFavoriteService;
        readonly IsliderService _IsliderService;
        readonly IviewCategoryService _IviewCategoryService;
        readonly IproductCheckoutService _IproductCheckoutService;
        readonly IbulletinService _IbulletinService;
        readonly IdefaultPageService _IdefaultPageService;
        public defaultController(IsettingsService IsettingsService, IviewsFavoriteService IviewsFavoriteService, IsliderService IsliderService, IviewCategoryService IviewCategoryService, IsearchService IsearchService, IproductCheckoutService IproductCheckoutService, ImyordersService ImyordersService, IbulletinService IbulletinService, IdefaultPageService IdefaultPageService)
        {
            _IsettingsService = IsettingsService;
            _IviewsFavoriteService = IviewsFavoriteService;
            _IsliderService = IsliderService;
            _IviewCategoryService = IviewCategoryService;
            _IsearchService = IsearchService;
            _IproductCheckoutService = IproductCheckoutService;
            _ImyordersService = ImyordersService;
            _IbulletinService = IbulletinService;
            _IdefaultPageService = IdefaultPageService;
        }
        [Route("[action]")]
        public IActionResult getSettings()
        {
            var response = _IsettingsService.GetAllSetting();
            veriyoneticisi.isActive = response[0].isActive;
            return Ok(new { type = "success", message = "", data = response });
        }

        [Route("[action]"), HttpPost]
        public IActionResult getFavoriteProducts([FromForm] string whichDay)
        {
            var response = _IviewsFavoriteService.GetFavoriteProducts(whichDay);
            return Ok(new { type = "success", message = "", data = response });
        }

        [Route("[action]"), HttpPost]
        public IActionResult getSlider()
        {
            var response = _IsliderService.getSliders();
            return Ok(new { type = "success", message = "", data = response });
        }

        [Route("[action]"), HttpPost]
        public IActionResult getCategories()
        {
            var response = _IviewCategoryService.geCategories();
            return Ok(new { type = "success", message = "", data = response });
        }

        [Route("[action]"), HttpPost]
        public IActionResult getSearchProduct([FromForm] int page, [FromForm] int itemsPerPage, [FromForm] string search, [FromForm] int listSorting)
        {
            var response = _IsearchService.getSearchProduct(page, itemsPerPage, search, listSorting);
            var data = response["data"];
            var type = response["type"];
            var c = response["c"];
            var message = response["message"];
            return Ok(new { type = type, message = message, data = data, c = c });
        }

        [Route("[action]"), HttpPost]
        public IActionResult updatePayment([FromForm] string billingFirstName, [FromForm] string billingLastName, [FromForm] string billingCompanyName, [FromForm] string billingCountry, [FromForm] string billingCity, [FromForm] string billingAddress, [FromForm] string shippingTitle, [FromForm] string shippingFirstName, [FromForm] string shippingLastName, [FromForm] string shippingCountry, [FromForm] string shippingCity, [FromForm] string shippingAddress, [FromForm] string basket)
        {
            if (HttpContext.Session.GetString("login") == "true" && !string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {
                var basketData = JsonConvert.DeserializeObject<List<ProductBasket>>(basket);

                Guid userID = Guid.Parse(HttpContext.Session.GetString("id"));
                productCheckoutDto productCheckoutData = new productCheckoutDto()
                {
                    userID = userID,
                    billingFirstName = billingFirstName,
                    billingLastName = billingLastName,
                    billingCompanyName = billingCompanyName,
                    billingCountry = billingCountry,
                    billingCity = billingCity,
                    billingAddress = billingAddress,
                    shippingTitle = shippingTitle,
                    shippingFirstName = shippingFirstName,
                    shippingLastName = shippingLastName,
                    shippingCountry = shippingCountry,
                    shippingCity = shippingCity,
                    shippingAddress = shippingAddress,
                };
                string userip = "";
                try
                {
                    userip = HttpContext.Request.Headers.ContainsKey("CF-CONNECTING-IP") ? HttpContext.Request.Headers["CF-CONNECTING-IP"] : HttpContext.Connection.RemoteIpAddress.ToString();
                }
                catch { }
                var response = _IproductCheckoutService.updatePayment(productCheckoutData, basketData, userip);
                var respData = response["data"];
                var type = response["type"];
                var message = response["message"];
                return Ok(new { type = type, message = message, data = respData });
            }
            else
            {
                return Ok(new { message = "Yetkisiz işlem.", type = "error" });
            }
        }

        [Route("[action]"), HttpPost]
        public IActionResult responseCheck([FromForm] string token)
        {
            var response = _IproductCheckoutService.responseCheck(token);
            var respData = response["data"];
            var type = response["type"];
            var message = response["message"];
            return Ok(new { type = type, message = message, data = respData });
        }

        [Route("[action]"), HttpPost]
        public IActionResult updateAddress([FromForm] string title, [FromForm] string firstName, [FromForm] string lastName, [FromForm] string country, [FromForm] string city, [FromForm] string address)
        {
            if (HttpContext.Session.GetString("login") == "true" && !string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {
                Guid userID = Guid.Parse(HttpContext.Session.GetString("id"));
                UserAddress uA = new UserAddress()
                {
                    userID = userID,
                    title = title,
                    firstName = firstName,
                    lastName = lastName,
                    country = country,
                    city = city,
                    address = address,
                };
                var response = _IproductCheckoutService.updateAddress(uA);
                var respData = response["data"];
                var type = response["type"];
                var message = response["message"];
                return Ok(new { type = type, message = message, data = respData });
            }
            else
            {
                return Ok(new { message = "Yetkisiz işlem.", type = "error" });
            }
        }

        [Route("[action]"), HttpPost]
        public IActionResult getUserAddress()
        {
            if (HttpContext.Session.GetString("login") == "true" && !string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {
                Guid userID = Guid.Parse(HttpContext.Session.GetString("id"));
                var response = _IproductCheckoutService.getUserAddress(userID);
                var respData = response["data"];
                var type = response["type"];
                var message = response["message"];
                return Ok(new { type = type, message = message, data = respData });
            }
            else
            {
                return Ok(new { message = "Yetkisiz işlem.", type = "error" });
            }
        }

        [HttpPost, Route("[action]")]
        public IActionResult getMyorders([FromForm] int page, [FromForm] int itemsPerPage, [FromForm] string search, [FromForm] int listSorting)
        {
            if (HttpContext.Session.GetString("login") == "true" && !string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {
                Guid userID = Guid.Parse(HttpContext.Session.GetString("id"));
                var response = _ImyordersService.getMyorders(userID, page, itemsPerPage, search, listSorting);
                var data = response["data"];
                var type = response["type"];
                var message = response["message"];
                var c = response["c"];
                return Ok(new { type = type, message = message, data = data, c = c });
            }
            else
            {
                return Ok(new { message = "Yetkisiz işlem.", type = "error" });
            }
        }

        [Route("[action]"), HttpPost]
        public IActionResult updateBulletin([FromForm] string email)
        {  
            var response = _IbulletinService.updateBulletin(email);
            var type = response["type"];
            var message = response["message"];
            return Ok(new { type = type, message = message });

        }

        [Route("[action]"), HttpPost]
        public IActionResult getLastComments([FromForm] string number)
        {
            var response = _IdefaultPageService.getLastComments(number);
            return Ok(new { type = "success", message = "", data = response });
        }

        [Route("[action]"), HttpPost]
        public IActionResult getLastProducts([FromForm] string number)
        {
            var response = _IdefaultPageService.getLastProducts(number);
            return Ok(new { type = "success", message = "", data = response });
        }
    }
}
