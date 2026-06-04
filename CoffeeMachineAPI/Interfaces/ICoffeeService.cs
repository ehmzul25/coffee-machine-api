using CoffeeMachineAPI.Models;

namespace CoffeeMachineAPI.Interfaces
{
    public interface ICoffeeService
    {
        Task<(int StatusCode, CoffeeResponse? Response)> BrewCoffee();
    }
}
