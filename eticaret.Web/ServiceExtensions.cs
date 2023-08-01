using AutoMapper;
using eticaret.Data;
using eticaret.Services.logServices;
using eticaret.Services.Maping;
using eticaret.Services.settingsServices;
using eticaret.Services.sliderServices;
using eticaret.Services.userServices;
using eticaret.Services.viewsFavoriteServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eticaret.Web
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<DbContext>(provider => provider.GetService<dbeticaretContext>());

            services.AddScoped<IsettingsService, settingsService>();
            services.AddScoped<IuserService, userService>();
            services.AddScoped<IlogService, logService>();
            services.AddScoped<IviewsFavoriteService, viewsFavoriteService>();
            services.AddScoped<IsliderService, sliderService>();

            services.AddAutoMapper(typeof(Startup));
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }
    }
}
