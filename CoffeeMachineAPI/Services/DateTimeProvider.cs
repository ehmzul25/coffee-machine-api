using CoffeeMachineAPI.Interfaces;

namespace CoffeeMachineAPI.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}


