using eticaret.DLL.Models;
using System;
using Microsoft.AspNetCore.Http;

public class operations
{
	public static bool log(HttpContext context,int id, byte type, string note)
	{
		using (dbeticaretContext ec = new dbeticaretContext())
		{
			string userip = "";
			try
			{
				userip = context.Request.Headers.ContainsKey("CF-CONNECTING-IP") ? context.Request.Headers["CF-CONNECTING-IP"] : context.Connection.RemoteIpAddress.ToString();
			}
			catch { }
			log lg = new log()
			{
				type = type,
				date = DateTime.Now,
				userID = id,
				ip = userip,
				note = note
			};
			ec.logs.Add(lg);
			ec.SaveChanges();
		}
		return true;
	}
}