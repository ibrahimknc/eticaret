using eticaret.Services.defaultPageServices.Dto;
using eticaret.Services.productsServices.Dto;
using eticaret.Services.sliderServices.Dto;
using eticaret.Services.viewCategoryServices.Dto;
using eticaret.Services.viewsFavoriteServices.Dto;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public class response
{

    public static async Task<List<viewsFavoriteDto>> productFavoritesAsync(string whichDay)
    {
        string apiUrl = veriyoneticisi.projectSettings["siteUrl"] + "/api/default/getFavoriteProducts";

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
            List<viewsFavoriteDto> list = responseObject.data.ToObject<List<viewsFavoriteDto>>();
            return list;
        }
    }


    public static async Task<List<sliderDto>> getSlidersAsync()
    {
        string apiUrl = veriyoneticisi.projectSettings["siteUrl"] + "/api/default/getSlider";

        using (HttpClient client = new HttpClient())
        {
            var formValues = new Dictionary<string, string> { { "", "" } };
            var formContent = new FormUrlEncodedContent(formValues);
            HttpResponseMessage response = await client.PostAsync(apiUrl, formContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
            List<sliderDto> list = responseObject.data.ToObject<List<sliderDto>>();
            return list;
        }

    }

    public static async Task<List<viewCategoryDto>> geCategoriesAsync()
    {
        string apiUrl = veriyoneticisi.projectSettings["siteUrl"] + "/api/default/getCategories";

        using (HttpClient client = new HttpClient())
        {
            var formValues = new Dictionary<string, string> { { "", "" } };
            var formContent = new FormUrlEncodedContent(formValues);
            HttpResponseMessage response = await client.PostAsync(apiUrl, formContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
            List<viewCategoryDto> list = responseObject.data.ToObject<List<viewCategoryDto>>();
            return list;
        }
    }

    public static async Task<List<lastCommentsDto>> getLastComments(string number)
    {
        string apiUrl = veriyoneticisi.projectSettings["siteUrl"] + "/api/default/getLastComments";

        using (HttpClient client = new HttpClient())
        {
            var formValues = new Dictionary<string, string> { { "number", number } };
            var formContent = new FormUrlEncodedContent(formValues);
            HttpResponseMessage response = await client.PostAsync(apiUrl, formContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
            List<lastCommentsDto> list = responseObject.data.ToObject<List<lastCommentsDto>>();
            return list;
        }
    }

    public static async Task<List<lastProductsDto>> getLastProducts(string number)
    {
        string apiUrl = veriyoneticisi.projectSettings["siteUrl"] + "/api/default/getLastProducts";

        using (HttpClient client = new HttpClient())
        {
            var formValues = new Dictionary<string, string> { { "number", number } };
            var formContent = new FormUrlEncodedContent(formValues);
            HttpResponseMessage response = await client.PostAsync(apiUrl, formContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
            List<lastProductsDto> list = responseObject.data.ToObject<List<lastProductsDto>>();
            return list;
        }
    }

    public static async Task<List<tagsDto>> getTags(string number)
    {
        string apiUrl = veriyoneticisi.projectSettings["siteUrl"] + "/api/products/getTags";

        using (HttpClient client = new HttpClient())
        {
            var formValues = new Dictionary<string, string> { { "number", number } };
            var formContent = new FormUrlEncodedContent(formValues);
            HttpResponseMessage response = await client.PostAsync(apiUrl, formContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
            List<tagsDto> list = responseObject.data.ToObject<List<tagsDto>>();
            return list;
        }
    }
}

