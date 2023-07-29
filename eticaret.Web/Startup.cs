using eticaret.Data;
using eticaret.Services.logServices;
using eticaret.Services.settingsServices;
using eticaret.Services.userServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace eticaret.Web
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		 
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<dbeticaretContext>(
			options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), 
			b => b.MigrationsAssembly("eticaret.Data")));
			services.AddScoped<DbContext>(provider => provider.GetService<dbeticaretContext>());

			services.AddScoped<IsettingsService, settingsService>();
			services.AddScoped<IuserService, userService>();
			services.AddScoped<IlogService, logService>();

			services.AddSession();
			services.AddControllersWithViews().AddRazorRuntimeCompilation();
			services.AddSignalR(); 

		}
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			
			if (env.IsDevelopment())
			{
				veriyoneticisi.isDevelopment = true;
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection(); // Http gelen url'yi HTTPS ye Çevirir..
			app.UseStaticFiles();      // wwwroot dosyasında kontrol ediyoruz..
			app.UseStaticFiles(new StaticFileOptions()
			{
				FileProvider = new PhysicalFileProvider(veriyoneticisi.isDevelopment ? Path.Combine("c:", "eticaret") : Path.Combine(Directory.GetCurrentDirectory(), "admin.eticaret.com/eticaret")),
				RequestPath = new PathString("/uploads")
			});
			app.UseSession();          // www.root/ erişim sağlar..
			app.UseRouting();          // Raut kurallar için kullanıyoruz..

			app.UseEndpoints(endpoints =>
			{
				if (env.IsDevelopment())
				{
					app.UseDeveloperExceptionPage();
				}
				app.UseHttpsRedirection();
				app.UseStaticFiles();
				app.UseRouting();

				app.UseEndpoints(endpoints =>
				{
					endpoints.MapControllerRoute(
					   name: "default",
					   pattern: "{controller=Default}/{action=Index}/{id?}",
					   defaults: new { controller = "Default", action = "Index" })
					;
					endpoints.MapControllerRoute(
					 name: "default",
					 pattern: "{action=Index}/{id?}",
					 defaults: new { controller = "Default", action = "Index" })
				  ;
					endpoints.MapControllerRoute(
					 name: "default",
					 pattern: "ajax/{action=Index}/{id?}",
					 defaults: new { controller = "Default", action = "Index" })
				  ;

				});
			});
		}
	}
}
