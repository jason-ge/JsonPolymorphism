using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IO.Swagger.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiWithJsonPolymorphism.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public string HealthMonitor()
        {
            return "Good to go!";
        }

        [HttpPost("Update")]
        public Person Update(Person person)
        {
            if (person.Pet is Dog)
            {
                _logger.LogInformation("Received a person with pet Dog");
            }
            else if (person.Pet is Cat)
            {
                _logger.LogInformation("Received a person with pet Cat");
            }
            else if (person.Pet is BasePet)
            {
                _logger.LogWarning("Received a person with pet BasePet");
            }

            return person;
        }
    }
}
