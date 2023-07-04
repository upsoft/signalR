using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace SignalRServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        
        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetMachineName")]
        public string Get()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return $"{assembly.GetName().Version} {Environment.MachineName}";
        }
    }
}