using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VopakAssesmentApp.Interfaces;

namespace VopakAssesmentApp.ApiCaller
{
    public class OpenWeatherAPiCaller: BaseApiCaller, IOpenWeatherApiCaller
    {
        private string _apiKey;

        public OpenWeatherAPiCaller(HttpClient client, string apiKey) : base(client)
        {
            _apiKey = apiKey;
        }

        public async Task<double> GetTemperatureByCity(string cityName)
        {
            var apiPath = $"weather?q={cityName}&appid={_apiKey}&units=metric";

            var response = await ExecuteAsync(httpClient => httpClient.GetAsync(apiPath));

            var content = await response.Content.ReadAsStringAsync();

            var parsedJson = JsonDocument.Parse(content);

            var deserializedResponse = parsedJson.RootElement.GetProperty("main").GetProperty("temp").GetDouble();

            return deserializedResponse;
        }
    }
}