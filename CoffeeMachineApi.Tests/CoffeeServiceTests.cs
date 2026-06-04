using CoffeeMachineApi.Services;
using CoffeeMachineAPI.Services;

namespace CoffeeMachineApi.Tests
{
    public class CoffeeServiceTests
    {
        private CoffeeService _service;
        private FakeDateTimeProvider _fakeDate;
        private FakeWeatherService _fakeWeather;


        [SetUp]
        public void Setup()
        {
            _fakeDate = new FakeDateTimeProvider
            {
                Now = DateTimeOffset.Now
            };

            _fakeWeather = new FakeWeatherService();

            _service = new CoffeeService(_fakeDate, _fakeWeather);
        }

        [Test]
        public async Task NormalCall_ShouldReturn200()
        {
            var result = await _service.BrewCoffee();

            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Response, Is.Not.Null);
        }

        [Test]
        public async Task FifthCall_ShouldReturn503()
        {
            for (int i = 1; i <= 4; i++)
            {
                await _service.BrewCoffee();
            }

            var result = await _service.BrewCoffee();

            Assert.That(result.StatusCode, Is.EqualTo(503));
            Assert.That(result.Response, Is.Null);
        }

        [Test]
        public async Task April1st_ShouldReturn418()
        {
            _fakeDate.Now = new DateTimeOffset(2024, 4, 1, 10, 0, 0, TimeSpan.Zero);

            var result = await _service.BrewCoffee();

            Assert.That(result.StatusCode, Is.EqualTo(418));
            Assert.That(result.Response, Is.Null);
        }

        [Test]
        public async Task Response_ShouldContainCorrectMessage()
        {
            var result = await _service.BrewCoffee();

            Assert.That(result.Response.Message, Is.EqualTo("Your piping hot coffee is ready")
                .Or.EqualTo("Your refreshing iced coffee is ready"));
        }

        [Test]
        public async Task Response_ShouldHaveIso8601Date()
        {
            _fakeDate.Now = new DateTimeOffset(2024, 5, 10, 12, 30, 0, TimeSpan.Zero);

            var result = await _service.BrewCoffee();

            Assert.That(result.Response.Prepared, Does.Match(@"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.*"));
        }

        [Test]
        public async Task PreparedDate_ShouldMatchInjectedDate()
        {
            _fakeDate.Now = new DateTimeOffset(2024, 5, 10, 12, 30, 0, TimeSpan.Zero);

            var result = await _service.BrewCoffee();

            Assert.That(result.Response.Prepared, Does.StartWith("2024-05-10T12:30:00"));
        }

        [Test]
        public async Task FourthCall_ShouldStillReturn200()
        {

            for (int i = 0; i < 3; i++)
                await _service.BrewCoffee();

            var result = await _service.BrewCoffee();

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task SixthCall_ShouldReturn200Again()
        {
            for (int i = 0; i < 5; i++)
                await _service.BrewCoffee();

            var result = await _service.BrewCoffee();

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task April1st_ShouldOverrideFifthCall()
        {
            _fakeDate.Now = new DateTimeOffset(2024, 4, 1, 10, 0, 0, TimeSpan.Zero);

            for (int i = 0; i < 5; i++)
            {
                var result = await _service.BrewCoffee();
                Assert.That(result.StatusCode, Is.EqualTo(418));
            }
        }

        [Test]
        public async Task FifthCall_ShouldReturnNullResponseBody()
        {
            for (int i = 0; i < 4; i++)
                await _service.BrewCoffee();

            var result = await _service.BrewCoffee();

            Assert.That(result.Response, Is.Null);
        }

        [Test]
        public async Task NewInstance_ShouldResetCounter()
        {
            for (int i = 0; i < 5; i++)
                await _service.BrewCoffee();

            var newService = new CoffeeService(_fakeDate, _fakeWeather);
            var result = await newService.BrewCoffee();

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
    }
}