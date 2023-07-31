using eticaret.Services.settingsServices;
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
        public defaultController(IsettingsService IsettingsService, IviewsFavoriteService IviewsFavoriteService)
        {
            _IsettingsService = IsettingsService;
            _IviewsFavoriteService = IviewsFavoriteService;
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

        //[Route("[action]"), HttpPost]
        //      public IActionResult getProductFavorites([FromForm] string whichDay)
        //      {
        //          try
        //          {
        //              List<ViewsFavorite> response = new List<ViewsFavorite> { };
        //              using (dbeticaretContext ec = new dbeticaretContext())
        //              {
        //                  // Burada whichDay gelen değere göre bugünün mü en çok favoriye eklenen 5 ürünleri listeleme işlemi
        //                  // Dünün mü en çok favoriye eklenen 5 ürünleri listeleme işlemi
        //                  // Yoksa genel olarak mı en çok favoriye eklenen 5 ürünleri listeleme işlemi yapıyoruz.
        //                  response = ec.viewsFavorites.AsQueryable().Where(x => whichDay == "Today" ? (x.creatingDate >= DateTime.Today) : whichDay == "Yesterday" ? (x.creatingDate < DateTime.Today && x.creatingDate >= DateTime.Today.AddDays(-1)) : x.productID > 0).OrderByDescending(x => x.favCount).Take(10).ToList();

        //                  if (response.Count < 5)
        //                  {
        //                      //Burada bugun en çok favoriye eklenen ürünler 5 ten az ise bunu tamamalamak için en çok favoriye eklenen ürünleri
        //                      //listeleyerek liste sayısını 5'e tamamlıyoruz.
        //                      var yesterdaysRecords = ec.viewsFavorites.AsQueryable().Where(x => whichDay == "Today" ? (x.creatingDate < DateTime.Today) : whichDay == "Yesterday" ? (x.creatingDate < DateTime.Today.AddDays(-1)) : x.productID > 0).OrderByDescending(x => x.favCount).Take(5 - response.Count).ToList();
        //                      response.AddRange(yesterdaysRecords);
        //                  }
        //              }
        //              return Ok(new { type = "success", message = "", data = response });

        //          }
        //          catch { }
        //          return Ok(new { type = "error", message = "" });
        //      }

        //      [Route("[action]"), HttpPost]
        //      public IActionResult getSliders()
        //      {
        //          try
        //          {
        //              List<Slider> response = new List<Slider> { };
        //              using (dbeticaretContext ec = new dbeticaretContext())
        //              {
        //                  // Burada aktif olan tüm slider görsellerini getiren ve rank isteğe göre listeleyen komut.
        //                  response = ec.sliders.AsQueryable().Where(x => x.isActive == true).OrderBy(x => x.rank).ToList();
        //              }
        //              return Ok(new { type = "success", message = "", data = response });
        //          }
        //          catch { }
        //          return Ok(new { type = "error", message = "" });
        //      }
    }
}
