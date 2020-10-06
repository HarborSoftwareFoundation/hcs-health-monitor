using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Helix.Hcs.Health.Service.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var pathBase = _configuration["PATH_BASE"];
            return Redirect($"{pathBase}/hc-ui");
        }

        [HttpGet("/config")]
        public IActionResult Config()
        {
            var configurationValues = _configuration.GetSection("HealthChecksUI:HealthChecks")
                .GetChildren()
                .SelectMany(cs => cs.GetChildren())
                .Union(_configuration.GetSection("HealthChecks-UI:HealthChecks")
                .GetChildren()
                .SelectMany(cs => cs.GetChildren()))
                .ToDictionary(v => v.Path, v => v.Value);

            return View(configurationValues);
        }

        public IActionResult Error() => View();
    }
}
