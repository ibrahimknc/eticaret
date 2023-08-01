using AutoMapper;
using eticaret.Data; 
using eticaret.Services.settingsServices.Dto;
using System.Collections.Generic;
using System.Linq; 

namespace eticaret.Services.settingsServices
{ 
	public class settingsService : IsettingsService
	{
		readonly dbeticaretContext _dbeticaretContext;
        readonly IMapper _mapper;

        public settingsService(dbeticaretContext dbeticaretContext, IMapper mapper)
		{
			_dbeticaretContext = dbeticaretContext;
            _mapper = mapper;
        }
		 
		public List<settingsDto> GetAllSetting()
		{
            var settingsList = _dbeticaretContext.settings.AsQueryable().ToList(); 
            var response = _mapper.Map<List<settingsDto>>(settingsList); 
			return response;
		}
	}
}
