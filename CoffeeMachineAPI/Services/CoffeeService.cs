using CoffeeMachineAPI.Interfaces;
using CoffeeMachineAPI.Models;


namespace CoffeeMachineApi.Services
{
    public class CoffeeService : ICoffeeService
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IWeatherService _weatherService;
        private int _counter = 0;

        public CoffeeService(IDateTimeProvider dateTimeProvider, IWeatherService weatherService)
        {
            _dateTimeProvider = dateTimeProvider;
            _weatherService = weatherService;
        }

        public async Task<(int StatusCode, CoffeeResponse? Response)> BrewCoffee()
        {
            var now = _dateTimeProvider.Now;

            //If the date is April 1st, then all calls to the endpoint defined in #1 should return 418 I'm a teapot instead, with an empty response body
            if (now.Month == 4 && now.Day == 1)
                return (418, null);

            _counter++;

            //On every fifth call to the endpoint defined in #1, the endpoint should return 503 Service Unavailable with an empty response body,
            if (_counter % 5 == 0)
                return (503, null);

            //if the current temperature is greater than 30°C, the returned message should be changed to "Your refreshing iced coffee is ready"
            var temperature = await _weatherService.GetTemperatureAsync();

            var message = temperature > 30
                ? "Your refreshing iced coffee is ready"
                : "Your piping hot coffee is ready";

            //When the endpoint GET /brew-coffee is called, the endpoint returns a 200 OK
            return (200, new CoffeeResponse
            {
                Message = message,
                Prepared = now.ToString("yyyy-MM-ddTHH:mm:sszzz")
            });
        }
    }
}