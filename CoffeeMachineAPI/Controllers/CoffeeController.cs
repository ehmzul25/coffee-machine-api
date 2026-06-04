using CoffeeMachineAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachineApi.Controllers
{
    [ApiController]
    [Route("brew-coffee")]
    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeService _coffeeService;

        public CoffeeController(ICoffeeService coffeeService)
        {
            _coffeeService = coffeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Brew()
        {
            var result = await _coffeeService.BrewCoffee();

            if (result.StatusCode == 200)
                return Ok(result.Response);

            Response.StatusCode = result.StatusCode;
            return new EmptyResult();
        }
    }
}
