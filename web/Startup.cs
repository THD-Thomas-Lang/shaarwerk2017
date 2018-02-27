using backend.DataSource;
using backend.Service;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace web
{
    /// <summary>
    /// Startup class.
    /// Hooks everything up to start the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Starts things up.
        /// </summary>
        /// <param name="env">A given hosting environment.</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// Configuration Property.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddRouting();
            services.AddMvcCore()
                .AddJsonFormatters()
                .AddRazorViewEngine()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            services.AddDbContext<PostgreSqlDataContext>(options => options.UseNpgsql(Configuration["DbContextSettings:PostgresShaarwerkDatasource"]));
            // Add framework services.
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, options => options.ResourcesPath = "Resources")
                .AddDataAnnotationsLocalization();

            services.AddTransient<ICustomerService, CustomerService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">A reference to the given app.</param>
        /// <param name="env">A reference to the given hosting environment.</param>
        /// <param name="loggerFactory">A reference to the given logging factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            TelemetryConfiguration.Active.DisableTelemetry = true;
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("de-DE"),
            });

            app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
