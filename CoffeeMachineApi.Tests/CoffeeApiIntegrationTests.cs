using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace CoffeeMachineApi.Tests
{
    public class CoffeeApiIntegrationTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client?.Dispose();
            _factory?.Dispose();
        }

        [Test]
        public async Task BrewCoffee_ShouldReturnValidStatus()
        {
            var response = await _client.GetAsync("/brew-coffee");

            Assert.That(response.StatusCode,
                Is.EqualTo(HttpStatusCode.OK)
                .Or.EqualTo(HttpStatusCode.ServiceUnavailable)
                .Or.EqualTo((HttpStatusCode)418));
        }
    }
}