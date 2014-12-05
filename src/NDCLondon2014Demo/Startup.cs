using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using WebApplication11.Controllers;

namespace WebApplication11
{
    public class Startup
    {
        public Startup()
        {
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(Configuration);
            services.Configure<SiteOptions>(Configuration);
            services.AddSingleton<IHelloService, HelloService>();
        }

        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment hostingEnv,
                              ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole((category, level)=> level == TraceType.Error);

            if (string.Equals(hostingEnv.EnvironmentName, "Development"))
            {
                app.UseErrorPage(ErrorPageOptions.ShowAll);
            }
            else
            {
                app.UseErrorHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }

    public class SiteOptions
    {
        public string Message { get; set; }
    }

    public interface IHelloService
    {
        string GetHello();
    }
}
