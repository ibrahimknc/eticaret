using eticaret.Services.userServices.Dto;
using eticaret.Services.userServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection; 
using Newtonsoft.Json; 
using eticaret.Services.logServices;
using eticaret.Services.logServices.Dto;

namespace eticaret.Web.Controllers.api
{
	[Route("api/[controller]")]
	[ApiController]
	public class userController : ControllerBase
	{
		readonly IuserService _IuserService;
		readonly IlogService _IlogService;
		public userController(IuserService IuserService, IlogService IlogService)
		{
			_IuserService = IuserService;
			_IlogService = IlogService;
		}

		[HttpPost, Route("[action]")]
		public IActionResult register([FromForm] string firstName, [FromForm] string lastName, [FromForm] string email, [FromForm] string password)
		{
			userDto uD = new userDto()
			{
				firstName = firstName,
				lastName = lastName,
				email = email,
				password = password
			};

			var response = _IuserService.register(uD);
			return Ok(new { type = response["type"].ToString(), message = response["message"].ToString() });
		}
		 
		[HttpPost, Route("[action]")]
		public IActionResult login([FromForm] string email, [FromForm] string password)
		{
			if (HttpContext.Session.GetString("login") == "true")
			{
				return Ok(new { message = "", type = "success" });
			}
			else
			{
				userDto uD = new userDto()
				{
					email = email,
					password = password
				};
				var response = _IuserService.login(uD);
				if (response["type"] == "success")
				{
					userDto selUser = JsonConvert.DeserializeObject<userDto>(response["data"]);
					#region Log Operation
					logDto lD = new logDto() { userID = selUser.id.ToString(), type = 1, note = "Kullanıcı Giriş Yaptı." };
					_IlogService.addLog(lD, HttpContext);
					#endregion 
					HttpContext.Session.SetString("id", selUser.id.ToString());
					HttpContext.Session.SetString("login", "true");
					var setData = refreshAndGetLogin(HttpContext, selUser);
					if (setData != null)
					{
						return Ok(new { message = "İşlem başarılı! Hoşgeldiniz.", type = "success" });
					}
					else
					{
						return Ok(new { message = "İşlem Başarısız.", type = "error" });
					}
				}
				else
				{
					return Ok(new { type = response["type"].ToString(), message = response["message"].ToString() });
				}

			}

		}

		public static dynamic refreshAndGetLogin(HttpContext context, userDto user)
		{
			if (!string.IsNullOrEmpty(context.Session.GetString("login")))
			{
				Guid userID = Guid.Parse(context.Session.GetString("id"));
				if (user.id != Guid.Empty & userID != Guid.Empty)
				{
					if (user.isActive == true)
					{
						foreach (PropertyInfo pi in user.GetType().GetProperties())
						{
							context.Session.SetString(pi.Name, Convert.ToString(pi.GetValue(user, null)));
						}
						return user;
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
					Guid userID = Guid.Parse(HttpContext.Session.GetString("id"));
					HttpContext.Session.Clear(); 
					#region Log Operation
					logDto lD = new logDto() { userID = userID.ToString(), type = 0, note = "Kullanıcı Çıkış Yaptı." };
					_IlogService.addLog(lD, HttpContext);
					#endregion
					return Ok(new { type = "success", message = "Çıkış işlemi başarılı." });
				}
			}
			catch { }
			return Ok(new { type = "error", message = "" });
		}
	}
}