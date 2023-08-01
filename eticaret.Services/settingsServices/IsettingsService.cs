using eticaret.Services.settingsServices.Dto;
using System.Collections.Generic; 

namespace eticaret.Services.settingsServices
{
	public interface IsettingsService
	{
		public List<settingsDto> GetAllSetting();
	 
	}
}
