using eticaret.Data;
using eticaret.Domain.Entities; 
using System.Collections.Generic;
using System.Linq; 

namespace eticaret.Services.settingsServices
{ 
	public class settingsService : IsettingsService
	{
		readonly dbeticaretContext _dbeticaretContext;

		public settingsService(dbeticaretContext dbeticaretContext)
		{
			_dbeticaretContext = dbeticaretContext;
		}
		 
		public List<Setting> GetAllSetting()
		{
			List<Setting> settings = new List<Setting>();
			var list = _dbeticaretContext.settings.AsQueryable().First();
			settings.Add(list);
			return settings;
		}
	}
}
