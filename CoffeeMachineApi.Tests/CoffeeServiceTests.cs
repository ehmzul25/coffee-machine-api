using CoffeeMachineApi.Services;

namespace CoffeeMachineApi.Tests
{
    public class CoffeeServiceTests
    {
        private CoffeeService _service;
        private FakeDateTimeProvider _fakeDate;


        [SetUp]
        public void Setup()
        {
            _fakeDate = new FakeDateTimeProvider
            {
                Now = DateTimeOffset.Now
            };

            _service = new CoffeeService(_fakeDate);
        }

        [Test]
        public void NormalCall_ShouldReturn200()
        {
            var result = _service.BrewCoffee();

            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Response, Is.Not.Null);
        }

        [Test]
        public void FifthCall_ShouldReturn503()
        {
            for (int i = 1; i <= 4; i++)
            {
                _service.BrewCoffee();
            }

            var result = _service.BrewCoffee();

            Assert.That(result.StatusCode, Is.EqualTo(503));
            Assert.That(result.Response, Is.Null);
        }

        [Test]
        public void April1st_ShouldReturn418()
        {
            _fakeDate.Now = new DateTimeOffset(2024, 4, 1, 10, 0, 0, TimeSpan.Zero);

            var result = _service.BrewCoffee();

            Assert.That(result.StatusCode, Is.EqualTo(418));
            Assert.That(result.Response, Is.Null);
        }

        [Test]
        public void Response_ShouldContainCorrectMessage()
        {
            var result = _service.BrewCoffee();

            Assert.That(result.Response.Message, Is.EqualTo("Your piping hot coffee is ready"));
        }

        [Test]
        public void Response_ShouldHaveIso8601Date()
        {
            _fakeDate.Now = new DateTimeOffset(2024, 5, 10, 12, 30, 0, TimeSpan.Zero);

            var result = _service.BrewCoffee();

            Assert.That(result.Response.Prepared, Does.Match(@"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.*"));
        }

        [Test]
        public void PreparedDate_ShouldMatchInjectedDate()
        {
            _fakeDate.Now = new DateTimeOffset(2024, 5, 10, 12, 30, 0, TimeSpan.Zero);

            var result = _service.BrewCoffee();

            Assert.That(result.Response.Prepared, Does.StartWith("2024-05-10T12:30:00"));
        }

        [Test]
        public void FourthCall_ShouldStillReturn200()
        {

            for (int i = 0; i < 3; i++)
                _service.BrewCoffee();

            var result = _service.BrewCoffee();

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void SixthCall_ShouldReturn200Again()
        {
            for (int i = 0; i < 5; i++)
                _service.BrewCoffee();

            var result = _service.BrewCoffee();

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void April1st_ShouldOverrideFifthCall()
        {
            _fakeDate.Now = new DateTimeOffset(2024, 4, 1, 10, 0, 0, TimeSpan.Zero);

            for (int i = 0; i < 5; i++)
            {
                var result = _service.BrewCoffee();
                Assert.That(result.StatusCode, Is.EqualTo(418));
            }
        }

        [Test]
        public void FifthCall_ShouldReturnNullResponseBody()
        {
            for (int i = 0; i < 4; i++)
                _service.BrewCoffee();

            var result = _service.BrewCoffee();

            Assert.That(result.Response, Is.Null);
        }

        [Test]
        public void NewInstance_ShouldResetCounter()
        {
            for (int i = 0; i < 5; i++)
                _service.BrewCoffee();

            var newService = new CoffeeService(_fakeDate);
            var result = newService.BrewCoffee();

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
    }
}