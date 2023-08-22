using AutoMapper;
using eticaret.Data;
using eticaret.Services.bulletinServices;
using eticaret.Services.categoriesServices;
using eticaret.Services.logServices; 
using eticaret.Services.Mapping;
using eticaret.Services.myordersServices;
using eticaret.Services.productCheckoutServices;
using eticaret.Services.productsServices;
using eticaret.Services.searchService;
using eticaret.Services.searchServices;
using eticaret.Services.settingsServices;
using eticaret.Services.shopServices;
using eticaret.Services.sliderServices;
using eticaret.Services.userServices;
using eticaret.Services.viewCategoryServices;
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
            services.AddScoped<IviewCategoryService, viewCategoryService>();
            services.AddScoped<IcategoriesService, categoriesService>();
            services.AddScoped<IproductsService, productsService>();
            services.AddScoped<IshopService, shopService>();
            services.AddScoped<IsearchService, searchService>();
            services.AddScoped<IproductCheckoutService, productCheckoutService>();
            services.AddScoped<ImyordersService, myordersService>();
            services.AddScoped<IbulletinService, bulletinService>();

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
