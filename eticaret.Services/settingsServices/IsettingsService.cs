using eticaret.Domain.Entities; 
using System.Collections.Generic; 

namespace eticaret.Services.settingsServices
{
	public interface IsettingsService
	{
		public List<Setting> GetAllSetting();
	 
	}
}
