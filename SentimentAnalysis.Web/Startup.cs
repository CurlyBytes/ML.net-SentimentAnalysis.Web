namespace SentimentAnalysis.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SentimentAnalysis.Web.Interfaces;
    using SentimentAnalysis.Web.Services;
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            // Register asp.net dependencies
            services.AddMvc();
            services.AddMemoryCache();

            // Register project specific dependencies
            services.AddSingleton<IClassifyService, ClassifyService>();
            services.AddSingleton<IBlobStorageService, BlobStorageService>();
            services.AddSingleton<IAppInsightsLoggerService, AppInsightsLoggerService>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
