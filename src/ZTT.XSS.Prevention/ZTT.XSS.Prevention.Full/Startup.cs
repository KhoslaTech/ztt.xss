using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ZTT.XSS.Prevention.Full
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			ASPSecurityKitConfiguration.ConfigureServices(services, Configuration);
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			ASPSecurityKitConfiguration.ConfigureContainer(builder);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			ASPSecurityKitConfiguration.Configure(app, env);
			app.UseStaticFiles();
			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
