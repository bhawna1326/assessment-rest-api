namespace VopakAssesmentApp.Models
{
    public class Temperature
    {
        public double TemperatureInCelcius { get; set; }

        public double TemperatureInFahrenheit => 32 + (TemperatureInCelcius / 0.5556);
    }
}
