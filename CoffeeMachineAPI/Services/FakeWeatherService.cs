using CoffeeMachineAPI.Interfaces;

namespace CoffeeMachineAPI.Services
{
    public class FakeWeatherService : IWeatherService
    {
        public Task<double> GetTemperatureAsync()
        {
            //return > 30 
            return Task.FromResult(35.0);
        }
    }

}
