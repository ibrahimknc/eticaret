using eticaret.Services.searchService;
using eticaret.Services.settingsServices;
using eticaret.Services.sliderServices;
using eticaret.Services.viewCategoryServices;
using eticaret.Services.viewsFavoriteServices;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eticaret.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class defaultController : ControllerBase
    {
        readonly IsettingsService _IsettingsService;
        readonly IsearchService _IsearchService;
        readonly IviewsFavoriteService _IviewsFavoriteService;
        readonly IsliderService _IsliderService;
        readonly IviewCategoryService _IviewCategoryService;
        public defaultController(IsettingsService IsettingsService, IviewsFavoriteService IviewsFavoriteService, IsliderService IsliderService, IviewCategoryService IviewCategoryService, IsearchService IsearchService)
        {
            _IsettingsService = IsettingsService;
            _IviewsFavoriteService = IviewsFavoriteService;
            _IsliderService = IsliderService;
            _IviewCategoryService = IviewCategoryService;
            _IsearchService = IsearchService;
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
    }
}
