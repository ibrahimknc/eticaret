using eticaret.DLL.Models;
using System.Linq;

public class layout
{
	public static string settings(string data)
	{
		using (dbeticaretContext ec = new dbeticaretContext())
		{
			var setting = ec.settings.AsQueryable().FirstOrDefault(x => x.id == 1);
			string reqest = "";
			switch (data)
			{
				case "isActive":
					reqest = setting.isActive.ToString();
					break;
				case "title":
					reqest = setting.title.ToString();
					break;
				case "email":
					reqest = setting.email.ToString();
					break;
				case "phone":
					reqest = setting.phone.ToString();
					break;
				case "address":
					reqest = setting.address.ToString();
					break;
				case "keywords":
					reqest = setting.keywords.ToString();
					break;
				case "description":
					reqest = setting.description.ToString();
					break; 
				default:
					break;
			}

			return reqest;
		}
	}
}
