using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VopakAssesmentApp.ApiCaller;
using VopakAssesmentApp.Interfaces;
using VopakAssesmentApp.Settings;

namespace VopakAssesmentApp
{
    public class Startup
    {
        private WebServiceSettings _settings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = GetConfiguration();
            _settings = new WebServiceSettings(configuration);

            services.AddControllers();

            ConfigureApiCallers(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureApiCallers(IServiceCollection services)
        {
            services.AddSingleton(CreateOpenWeatherApiCaller());
        }

        private IOpenWeatherApiCaller CreateOpenWeatherApiCaller()
        {
            var client = BaseApiCaller.GetClient(_settings.OpenWeatherApiSettings.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(_settings.OpenWeatherApiSettings.TimeoutSeconds);

            return new OpenWeatherAPiCaller(client, _settings.OpenWeatherApiSettings.ApiKey);
        }

        private IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddEnvironmentVariables();

            builder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
