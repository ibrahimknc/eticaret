using eticaret.Data;
using eticaret.Domain.Entities;
using eticaret.Services.userServices.Dto;
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

		Dictionary<string, string> response = new Dictionary<string, string>
			{
				{ "type", "" }, // success-error
				{ "message", "" },
				{ "data", "" } 
			};


		public Dictionary<string, string> login(userDto user)
		{
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
	}
}
