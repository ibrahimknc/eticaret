using eticaret.Services.settingsServices;
using eticaret.Services.sliderServices;
using eticaret.Services.viewCategoryServices;
using eticaret.Services.viewsFavoriteServices;
using Microsoft.AspNetCore.Mvc;

namespace eticaret.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class defaultController : ControllerBase
    {
        readonly IsettingsService _IsettingsService;
        readonly IviewsFavoriteService _IviewsFavoriteService;
        readonly IsliderService _IsliderService;
        readonly IviewCategoryService _IviewCategoryService;
        public defaultController(IsettingsService IsettingsService, IviewsFavoriteService IviewsFavoriteService, IsliderService IsliderService, IviewCategoryService IviewCategoryService)
        {
            _IsettingsService = IsettingsService;
            _IviewsFavoriteService = IviewsFavoriteService;
            _IsliderService = IsliderService;
            _IviewCategoryService = IviewCategoryService;
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
    }
}
