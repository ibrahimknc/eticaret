using eticaret.Data;
using eticaret.Domain.Entities;
using eticaret.Services.logServices.Dto;
using Microsoft.AspNetCore.Http;
using System;

namespace eticaret.Services.logServices
{
	public class logService : IlogService
	{
		readonly dbeticaretContext _dbeticaretContext;
		public logService(dbeticaretContext dbeticaretContext)
		{
			_dbeticaretContext = dbeticaretContext;
		}

		public bool addLog(logDto logDto, HttpContext context)
		{
			var clLogs = _dbeticaretContext.logs;
			string userip = "";
			try
			{
				userip = context.Request.Headers.ContainsKey("CF-CONNECTING-IP") ? context.Request.Headers["CF-CONNECTING-IP"] : context.Connection.RemoteIpAddress.ToString();
			}
			catch { }
			Log lg = new Log()
			{
				type = logDto.type,
				creatingTime = DateTime.UtcNow,
				updatedTime = DateTime.UtcNow,
				userID = logDto.userID,
				ip = userip,
				note = logDto.note,
				isActive = true
			};
			clLogs.Add(lg);
			_dbeticaretContext.SaveChanges();

			return true;
		}
	}
}
