using CoffeeMachineAPI.Interfaces;

namespace CoffeeMachineApi.Tests
{
    public class FakeDateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now { get; set; }
    }
}
