namespace CoffeeMachineAPI.Interfaces
{
    public interface IWeatherService
    {
        Task<double> GetTemperatureAsync();
    }
}
