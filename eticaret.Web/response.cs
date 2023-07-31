using eticaret.Services.viewsFavoriteServices.Dto;
using Newtonsoft.Json; 
using System.Collections.Generic;
using System.Net.Http; 
using System.Threading.Tasks;

public class response
{

    public static async Task<List<viewsFavoriteDto>> productFavoritesAsync(string whichDay)
    {
        string apiUrl = veriyoneticisi.projectSettings["siteUrl"] +"/api/default/getFavoriteProducts";
          
        using (HttpClient client = new HttpClient())
        {
            var formValues = new Dictionary<string, string>
                {
                    { "whichDay", whichDay }  
                };

            var formContent = new FormUrlEncodedContent(formValues);
            HttpResponseMessage response = await client.PostAsync(apiUrl, formContent); 
            string responseContent = await response.Content.ReadAsStringAsync(); 
            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
            List<viewsFavoriteDto> favoritesList = responseObject.data.ToObject<List<viewsFavoriteDto>>();

            return favoritesList;
        }
    }
      

    //public static List<Slider> getSliders()
    //   {
    //       List<Slider> response = new List<Slider> { };
    //       using (dbeticaretContext ec = new dbeticaretContext())
    //       {
    //           // Burada aktif olan tüm slider görsellerini getiren ve rank isteğe göre listeleyen komut.
    //           response = ec.sliders.AsQueryable().Where(x => x.isActive == true).OrderBy(x => x.rank).ToList();
    //       }
    //       return response;
    //   }

    //public static List<ViewsCategory> geCategories()
    //{
    //    List<ViewsCategory> response = new List<ViewsCategory> { };
    //    using (dbeticaretContext ec = new dbeticaretContext())
    //    {
    //        // Burada aktif olan tüm kategorileri ve her kategoriden kaç tane ürün olduğunu getirir(db de views yazıldı)
    //        response = ec.viewsCategories.AsQueryable().ToList();
    //    }
    //    return response;
    //}
}

