using eticaret.Data;
using eticaret.Domain.Entities;
using eticaret.Services.userServices.Dto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eticaret.Services.userServices
{
    public class userService : IuserService
    {
        readonly dbeticaretContext _dbeticaretContext;
        public userService(dbeticaretContext dbeticaretContext)
        {
            _dbeticaretContext = dbeticaretContext;
        }
        Dictionary<string, object> response = new Dictionary<string, object>
            {
                { "type", null }, // success-error
				{ "message",null },
                { "data", null},
                { "c", null}
            };
        public Dictionary<string, object> getUserProfile(Guid id)
        {
            try
            {
                var responseUser = _dbeticaretContext.users.AsQueryable().FirstOrDefault(x => x.id == id);
                if (responseUser.isActive == true)
                {
                    response["type"] = "success";
                    response["data"] = responseUser;
                }
                else
                {
                    response["type"] = "inActive"; response["message"] = "";
                }
            }
            catch
            {
                response["type"] = "error"; response["message"] = "";
            }

            return response;
        }

        public Dictionary<string, string> login(userDto user)
        {
            Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "type", "" }, // success-error
				{ "message", "" },
                { "data", "" }
            };
            try
            {
                if (!string.IsNullOrEmpty(user.email) && !string.IsNullOrEmpty(user.password))
                {
                    if (veriyoneticisi.emailChecker(user.email))
                    {

                        var clUser = _dbeticaretContext.users;
                        var selUser = clUser.AsQueryable().FirstOrDefault(x => x.email == user.email && x.password == veriyoneticisi.MD5Hash(user.password));
                        var selUser2 = clUser.AsQueryable().Where(x => x.email == user.email && x.password == veriyoneticisi.MD5Hash(user.password));

                        if (selUser != null)
                        {
                            if (selUser.isActive == true)
                            {
                                var userLastLoginDate = _dbeticaretContext.users.FirstOrDefault(x => x.id == selUser.id);
                                if (userLastLoginDate != null)
                                {
                                    userLastLoginDate.lastLoginDate = DateTime.UtcNow;
                                    _dbeticaretContext.SaveChanges();

                                    response["type"] = "success"; response["data"] = JsonConvert.SerializeObject(userLastLoginDate);
                                }
                                else
                                {
                                    response["type"] = "error"; response["message"] = "Kullanıcı Bulunamadı.";
                                }

                            }
                            else
                            {
                                response["type"] = "error"; response["message"] = "Üzgünüz Banlandınız.";
                            }
                        }
                        else
                        {
                            response["type"] = "error"; response["message"] = "Yanlış kullanıcı adı şifre.";
                        }

                    }
                    else
                    {
                        response["type"] = "error"; response["message"] = "Lütfen geçerli bir Mail adresi giriniz.";
                    }
                }
                else
                {
                    response["type"] = "error"; response["message"] = "Lütfen Boş Bırakmayınız.";
                }
            }
            catch { response["type"] = "error"; response["message"] = ""; }

            return response;
        }

        public Dictionary<string, string> register(userDto user)
        {
            Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "type", "" }, // success-error
				{ "message", "" },
                { "data", "" }
            };
            try
            {
                var clUsers = _dbeticaretContext.users;
                try
                {
                    if (string.IsNullOrEmpty(user.firstName) | string.IsNullOrEmpty(user.lastName) | string.IsNullOrEmpty(user.email) | string.IsNullOrEmpty(user.password))
                    {
                        response["type"] = "error"; response["message"] = "Lütfen Tüm Kutucukları Doldurunuz.";
                    }
                    else if (clUsers.AsQueryable().Any(x => x.email == user.email))
                    {
                        response["type"] = "error"; response["message"] = "Bu Mail Adresi Daha Önce Kayıt Olmuştur.";
                    }
                    else if (!veriyoneticisi.passwordChecker(user.password))
                    {
                        response["type"] = "error"; response["message"] = "Şifrenin en az 8 karakter uzunluğunda olması, en az bir büyük harf, en az bir küçük harf ve en az bir rakam içermesi gerekmektedir.";
                    }
                    else if (!veriyoneticisi.emailChecker(user.email))
                    {
                        response["type"] = "error"; response["message"] = "Lütfen geçerli bir Mail adresi giriniz.";
                    }
                    else
                    {
                        User u = new User()
                        {
                            firstName = user.firstName,
                            lastName = user.lastName,
                            email = user.email,
                            password = veriyoneticisi.MD5Hash(user.password),
                            isActive = true,
                            creatingTime = DateTime.UtcNow
                        };
                        clUsers.Add(u);
                        _dbeticaretContext.SaveChanges();
                        response["type"] = "success"; response["message"] = "✔ Kayıt Başarılı. Lütfen Giriş Yapınız.";
                    }
                }
                catch { response["type"] = "error"; response["message"] = ""; }
            }
            catch { response["type"] = "error"; response["message"] = ""; }

            return response;
        }

        public Dictionary<string, object> updateUser(userDto user)
        {
            try
            {
                var clUsers = _dbeticaretContext.users.AsQueryable().FirstOrDefault(x => x.id == user.id);
                try
                {
                    if (string.IsNullOrEmpty(user.firstName) | string.IsNullOrEmpty(user.lastName))
                    {
                        response["type"] = "error"; response["message"] = "Lütfen Tüm Kutucukları Doldurunuz.";
                    }
                    else if (clUsers == null)
                    {
                        response["type"] = "error"; response["message"] = "Üzgünüm Böyle Bir Kullanıcı Bulunamadı.";
                    }
                    else
                    {
                        clUsers.firstName = user.firstName;
                        clUsers.lastName = user.lastName;
                        clUsers.phone = user.phone;
                        clUsers.address = user.address;
                        clUsers.updatedTime = DateTime.UtcNow;

                        _dbeticaretContext.SaveChanges();
                        response["type"] = "success"; response["message"] = "✔ Profil Güncelleme Başarılı.";
                    }
                }
                catch { response["type"] = "error"; response["message"] = ""; }
            }
            catch { response["type"] = "error"; response["message"] = ""; }

            return response;
        }

        public Dictionary<string, object> updateUserPassword(userDto user, string newPassword)
        {
            try
            {
                var clUsers = _dbeticaretContext.users.AsQueryable().FirstOrDefault(x => x.id == user.id);
                try
                {
                    if (string.IsNullOrEmpty(user.password) | string.IsNullOrEmpty(newPassword))
                    {
                        response["type"] = "error"; response["message"] = "Lütfen Tüm Kutucukları Doldurunuz.";
                    }
                    else if (!veriyoneticisi.passwordChecker(newPassword))
                    {
                        response["type"] = "error"; response["message"] = "Yeni Şifrenin en az 8 karakter uzunluğunda olması, en az bir büyük harf, en az bir küçük harf ve en az bir rakam içermesi gerekmektedir.";
                    }
                    else if (clUsers.password == veriyoneticisi.MD5Hash(newPassword))
                    {
                        response["type"] = "error"; response["message"] = "Yeni Şifreniz ile eski şifreniz aynıdır.";
                    }
                    else if (clUsers == null)
                    {
                        response["type"] = "error"; response["message"] = "Üzgünüm Böyle Bir Kullanıcı Bulunamadı.";
                    }
                    else if (clUsers.password != veriyoneticisi.MD5Hash(user.password))
                    {
                        response["type"] = "error"; response["message"] = "Lütfen Mevcut Şifrenizi Doğru Giriniz.";
                    }
                    else
                    {
                        clUsers.password = veriyoneticisi.MD5Hash(newPassword);
                        clUsers.updatedTime = DateTime.UtcNow;
                        _dbeticaretContext.SaveChanges();
                        response["type"] = "success"; response["message"] = "✔ Şifre Güncelleme Başarılı.";
                    }
                }
                catch { response["type"] = "error"; response["message"] = ""; }
            }
            catch { response["type"] = "error"; response["message"] = ""; }

            return response;
        }

        public Dictionary<string, object> updateUserFavorite(Guid userID, Guid productID)
        {
            try
            {
                var isUserFavorite = _dbeticaretContext.userFavorites.Any(x => x.productID == productID && x.userID == userID);
                if (!isUserFavorite)
                {
                    UserFavorite userFavorite = new UserFavorite()
                    {
                        userID = userID,
                        productID = productID,
                        isActive = true,
                        creatingTime = DateTime.UtcNow,
                        updatedTime = DateTime.UtcNow
                    };
                    _dbeticaretContext.userFavorites.Add(userFavorite);
                    var isCompleted = _dbeticaretContext.SaveChanges();

                    if (isCompleted > 0)
                    {
                        response["type"] = "succcess"; response["message"] = "Favori Başarıyla Eklendi.";
                    }
                    else
                    {
                        response["type"] = "error"; response["message"] = "";
                    }
                }
                else
                {
                    response["type"] = "error"; response["message"] = "Favori Daha Önce Eklenmiş.";
                }
            }
            catch
            {
                response["type"] = "error"; response["message"] = "";
            }

            return response;
        }
  
        public Dictionary<string, object> getUserFavorites(Guid userID, int page, int itemsPerPage, string search, int listSorting)
        {
            try
            { 
                var query = _dbeticaretContext.userFavorites.Include(c => c.Product).AsQueryable().Where(x =>
                (listSorting <= 4 | (listSorting == 5 && x.Product.stock > 0)) & 
                x.Product.isActive == true &
                x.userID == userID &
                (string.IsNullOrEmpty(search) | (!string.IsNullOrEmpty(search) &
                ((x.Product.name.Contains(search)) |
                (x.Product.tags.Contains(search)) |
                (x.Product.details.Contains(search)))
                )));

                switch (listSorting)
                {
                    case 1:
                        query = query.OrderBy(x => x.Product.salePrice);
                        break;
                    case 2:
                        query = query.OrderByDescending(x => x.Product.salePrice);
                        break;
                    case 3:
                        query = query.OrderBy(x => x.Product.name);
                        break;
                    case 4:
                        query = query.OrderByDescending(x => x.Product.name);
                        break;
                    default:
                        query = query.OrderByDescending(x => x.Product.id);
                        break;
                }

                var count = query.Count();
                var responseList = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList(); 

                response["type"] = "success"; response["data"] = responseList; response["c"] = count;  

            }
            catch
            {
                response["type"] = "error"; response["message"] = "";
            }

            return response;
        }
    }
}
