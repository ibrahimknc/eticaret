using eticaret.DLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System; 
using System.Linq;
using System.Reflection;

namespace eticaret.Web.Controllers.api
{
	[Route("api/[controller]")]
	[ApiController]
	public class userController : ControllerBase
	{
		[HttpPost, Route("[action]")]
		public IActionResult register([FromForm] string firstName, [FromForm] string lastName, [FromForm] string email, [FromForm] string password)
		{
			try
			{
				using (dbeticaretContext ec = new dbeticaretContext())
				{
					var clUsers = ec.users;
					try
					{
						if (string.IsNullOrEmpty(firstName) | string.IsNullOrEmpty(lastName) | string.IsNullOrEmpty(email) | string.IsNullOrEmpty(password))
						{
							return Ok(new { type = "error", message = "Lütfen Tüm Kutucukları Doldurunuz." });
						}
						if (clUsers.AsQueryable().Any(x => x.email == email))
						{
							return Ok(new { type = "error", message = "Bu Mail Adresi Daha Önce Kayıt Olmuştır." });
						}
						if (!veriyoneticisi.passwordChecker(password))
						{
							return Ok(new { type = "error", message = "Şifrenin en az 8 karakter uzunluğunda olması, en az bir büyük harf, en az bir küçük harf ve en az bir rakam içermesi gerekmektedir." }); ;
						}
						if (!veriyoneticisi.emailChecker(email))
						{
							return Ok(new { message = "Lütfen geçerli bir Mail adresi giriniz.", type = "error" });
						}

							user u = new user()
						{
							firstName = firstName,
							lastName = lastName,
							email = email,
							password = veriyoneticisi.MD5Hash(password),
							isActive = true,
							creatingDate = DateTime.Now
						};
						clUsers.Add(u);
						ec.SaveChanges();
						return Ok(new { type = "success", message = "✔ Kayıt Başarılı. Lütfen Giriş Yapınız." });
					}
					catch { }
					return Ok(new { type = "error", message = "" });
				}
			}
			catch { }
			return Ok(new { type = "error", message = "" });

		}

		[HttpPost, Route("[action]")]
		public IActionResult login([FromForm] string email, [FromForm] string password)
		{

			 if (HttpContext.Session.GetString("login") == "true")
			{
				return Ok(new { message = "", type = "success" });
			}
			else if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
			{
				if(veriyoneticisi.emailChecker(email))
				{
					using (dbeticaretContext ec = new dbeticaretContext())
					{
						var clUser = ec.users; 
						var selUser = clUser.AsQueryable().FirstOrDefault(x => x.email == email && x.password == veriyoneticisi.MD5Hash(password));

						if (selUser != null)
						{
							if (selUser.isActive == true)
							{
								operations.log(HttpContext, selUser.id, 1, "Kullanıcı Giriş Yaptı.");
								ec.Database.ExecuteSqlRaw("Update users set lastLoginDate={1} where id={0}", selUser.id, DateTime.Now);

								HttpContext.Session.SetString("id", selUser.id.ToString());
								HttpContext.Session.SetString("login", "true");
								var setData = refreshAndGetLogin(HttpContext);
								if (setData != null)
								{
									return Ok(new { message = "İşlem başarılı! Hoşgeldiniz.", type = "success" });
								}
								else
								{
									return Ok(new { message = "İşlem Başarısız", type = "error" });
								}

							}
							else
							{
								return Ok(new { message = "Üzgünüz Banlandınız.", type = "error" });
							}
						}
						else
						{
							return Ok(new { message = "Yanlış kullanıcı adı şifre.", type = "error" });
						}
					}
				}
				else
				{
					return Ok(new { message = "Lütfen geçerli bir Mail adresi giriniz.", type = "error" });
				}

			}
			else
			{
				return Ok(new { message = "Lütfen Boş Bırakmayınız.", type = "error" });
			}
		}

		public static dynamic refreshAndGetLogin(HttpContext context)
		{
			if (!string.IsNullOrEmpty(context.Session.GetString("login")))
			{
				int aid = int.Parse(context.Session.GetString("id"));
				using (dbeticaretContext ec = new dbeticaretContext())
				{
					var clAccounts = ec.users;
					var response = clAccounts.AsQueryable().Where(x => x.id == aid).Take(1).ToList();
					if (response.Count == 1)
					{
						var selAccount = response.Select(x => new
						{
							_id = x.id,
							x.email,
							x.isActive,
							x.lastLoginDate,
							x.creatingDate
						}).First();
						if (selAccount.isActive == true)
						{
							foreach (PropertyInfo pi in selAccount.GetType().GetProperties())
							{
								context.Session.SetString(pi.Name, Convert.ToString(pi.GetValue(selAccount, null)));
							}
							return selAccount;
						}
					}
				}
			}
			if (string.IsNullOrEmpty(context.Session.GetString("ga")))
			{
				context.Session.Clear();
			}
			return null;
		}

		[Route("[action]")]
		public IActionResult logout()
		{
			try
			{
				if (!string.IsNullOrEmpty(HttpContext.Session.GetString("login")))
				{
					int userID = Convert.ToInt32(HttpContext.Session.GetString("id"));
					HttpContext.Session.Clear();
					operations.log(HttpContext, userID, 0, "Kullanıcı Çıkış Yaptı.");
					return Ok(new { type = "success", message = "Çıkış işlemi başarılı." });
				}
			}
			catch { } 
			return Ok(new { type = "error", message = "" });
		}
	}
}
