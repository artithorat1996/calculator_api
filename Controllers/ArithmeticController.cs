using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/arithmetic")]
    [ApiController]
    public class ArithmeticController : ControllerBase
    {
        // GET api/arithmetic/add?a=5&b=3
        [HttpGet("add")]
        [Authorize]
        public ActionResult<int> Add([FromQuery]Arithmetic arithmetic)
        {
            return arithmetic.First_Value + arithmetic.Second_Value;
        }

        // GET api/arithmetic/subtract?a=10&b=4
        [HttpGet("subtract")]
        public ActionResult<int> Subtract([FromQuery]Arithmetic arithmetic)
        {
            return arithmetic.First_Value - arithmetic.Second_Value;
        }

        // GET api/arithmetic/multiply?a=6&b=7
        [HttpPost("multiply")]
        public ActionResult<int> Multiply(Arithmetic arithmetic)
        {
            return arithmetic.First_Value * arithmetic.Second_Value;
        }

        // GET api/arithmetic/divide?a=12&b=3
        [HttpPost("divide")]
        public ActionResult<double> Divide(Arithmetic arithmetic)
        {
            if (arithmetic.Second_Value == 0)
            {
                return BadRequest("Division by zero is not allowed.");
            }

            double result = Convert.ToDouble(arithmetic.First_Value) / Convert.ToDouble(arithmetic.Second_Value);
            return result;
        }
    }
}
