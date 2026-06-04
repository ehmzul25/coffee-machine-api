using CoffeeMachineAPI.Interfaces;

namespace CoffeeMachineAPI.Services
{
    //sample real API Implementation
    public class OpenWeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        private const string apiKey = "REPLACE_WITH_OPENWEATHER_API_KEY";
        private const string latitude = "14.5995"; //Manila
        private const string longitude = "120.9842";

        public OpenWeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<double> GetTemperatureAsync()
        {
            var url = $"https://api.openweathermap.org/data/4.0/onecall/current?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric";

            var response = await _httpClient.GetFromJsonAsync<dynamic>(url);

            return (double)response.current.temp;
        }
    }
}
