using eticaret.Data; 
using eticaret.Services.sliderServices.Dto;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace eticaret.Services.sliderServices
{
    public class sliderService : IsliderService
    {
        readonly dbeticaretContext _dbeticaretContext;
        readonly IMapper _mapper;

        public sliderService(dbeticaretContext dbeticaretContext, IMapper mapper)
        {
            _dbeticaretContext = dbeticaretContext;
            _mapper = mapper;
        }

        public List<sliderDto> getSliders()
        {
            var sliders = _dbeticaretContext.sliders
                     .Where(x => x.isActive) // "IsActive" sütunu "true" olanları alır
                     .OrderBy(x => x.rank) // "Rank" sütununa göre sıralar
                     .ToList();

            var response = _mapper.Map<List<sliderDto>>(sliders); 
            return response;
        }
    }
}
