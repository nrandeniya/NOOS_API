using Microsoft.AspNetCore.Mvc;
using NOOS_API.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NOOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly ILoggerService _logger;

        public HomeController(ILoggerService logger)   // once the controller is called, initialize the ILogger
        {
            _logger = logger;   //initialize the read only object _logger to the object logger passed in whenthe controller is called
        }
        // GET: api/<HomeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInfo("Access Home Controller");
            return new string[] { "value1", "value2" };
        }
       
        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        
        public string Get(int id)
        {
            _logger.LogDebug("Geting a Certain ID");
            return "value";
        }

        // POST api/<HomeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HomeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            _logger.LogWarn("Watch the Security Protocols");
        }

        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogError("This is an Error");
        }
    }
}
