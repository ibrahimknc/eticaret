using eticaret.Data;
using eticaret.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace eticaret.Services.bulletinServices
{
    public class bulletinService : IbulletinService
    {
        readonly dbeticaretContext _dbeticaretContext;
        public bulletinService(dbeticaretContext dbeticaretContext)
        {
            _dbeticaretContext = dbeticaretContext;
        }
        Dictionary<string, object> response = new Dictionary<string, object>
            {
                { "type", null }, // success-error
				{ "message",null },
                { "data", null}
            };
        public Dictionary<string, object> updateBulletin(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    response["type"] = "error"; response["message"] = "Lütfen Boş Bırakmayınız.";
                    return response;
                }
                if (!veriyoneticisi.emailChecker(email))
                {
                    response["type"] = "error"; response["message"] = "Lütfen geçerli bir Mail adresi giriniz.";
                    return response;
                }
                var selBulletin = _dbeticaretContext.bulletins;
                if (selBulletin.Any(x => x.email == email && x.isActive == false))
                {
                    response["type"] = "error"; response["message"] = "Daha önce kayıt olmuşsunuz ama Onaylanmamışsınız.";
                    return response;
                }
                if (selBulletin.Any(x => x.email == email && x.isActive == true))
                {
                    response["type"] = "error"; response["message"] = "Daha önce kayıt olmuşsunuz ve Onaylanmışsınız ✔";
                    return response;
                }

                Bulletin b = new Bulletin()
                {
                    email = email,
                    isActive = false
                };
                selBulletin.Add(b);
                _dbeticaretContext.SaveChanges();
                response["type"] = "success"; response["message"] = "✔ Bülten Kaydınız Başarılı. Lütfen Onay Bekleyiniz.";
            }
            catch
            {
                response["type"] = "error"; response["message"] = "";
            }
            return response;
        }
    }
}
