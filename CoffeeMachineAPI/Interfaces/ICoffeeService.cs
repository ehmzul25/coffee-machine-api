using CoffeeMachineAPI.Models;

namespace CoffeeMachineAPI.Interfaces
{
    public interface ICoffeeService
    {
        (int StatusCode, CoffeeResponse? Response) BrewCoffee();
    }
}
