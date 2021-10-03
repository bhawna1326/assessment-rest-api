using System.Threading.Tasks;

namespace VopakAssesmentApp.Interfaces
{
    public interface IOpenWeatherApiCaller
    {
        Task<double> GetTemperatureByCity(string cityName);
    }
}
