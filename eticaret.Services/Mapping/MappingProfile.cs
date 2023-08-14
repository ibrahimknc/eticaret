﻿using AutoMapper;
using eticaret.Domain.Entities;
using eticaret.Services.productCheckoutServices.Dto;
using eticaret.Services.settingsServices.Dto;
using eticaret.Services.sliderServices.Dto;

namespace eticaret.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
             CreateMap<Slider, sliderDto>(); // Slider sınıfını SliderDto sınıfına eşledim
             CreateMap<Setting, settingsDto>();  
        }
    }
}
