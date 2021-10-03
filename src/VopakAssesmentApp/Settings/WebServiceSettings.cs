using Microsoft.Extensions.Configuration;

namespace VopakAssesmentApp.Settings
{
    internal class WebServiceSettings
    {
        public OpenWeatherApiSettings OpenWeatherApiSettings { get; set; } = new OpenWeatherApiSettings();

        public WebServiceSettings(IConfiguration configuration)
        {
            configuration.Bind(this);
        }
    }
}
