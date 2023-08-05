using eticaret.Domain.Entities;
using Newtonsoft.Json; 
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

public class layout
{
    public static async Task<string> settingsAsync(string data)
    {
        var reqest = "";
        if (string.IsNullOrEmpty(veriyoneticisi.setting.title))
        {
            string apiUrl = veriyoneticisi.projectSettings["siteUrl"] + "/api/default/getSettings";
            using (HttpClient client = new HttpClient())
            {
                var formValues = new Dictionary<string, string> { { "", "" } };
                var formContent = new FormUrlEncodedContent(formValues);
                HttpResponseMessage response = await client.PostAsync(apiUrl, formContent);
                string responseContent = await response.Content.ReadAsStringAsync();
                dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
                List<Setting> settingsList = responseObject.data.ToObject<List<Setting>>();
                veriyoneticisi.setting = settingsList.FirstOrDefault();
            }
        }

        switch (data)
        {
            case "isActive":
                reqest = veriyoneticisi.setting.isActive.ToString();
                break;
            case "title":
                reqest = veriyoneticisi.setting.title.ToString();
                break;
            case "email":
                reqest = veriyoneticisi.setting.email.ToString();
                break;
            case "phone":
                reqest = veriyoneticisi.setting.phone.ToString();
                break;
            case "address":
                reqest = veriyoneticisi.setting.address.ToString();
                break;
            case "keywords":
                reqest = veriyoneticisi.setting.keywords.ToString();
                break;
            case "description":
                reqest = veriyoneticisi.setting.description.ToString();
                break;
            default:
                break;
        }

        return reqest;
    }
}