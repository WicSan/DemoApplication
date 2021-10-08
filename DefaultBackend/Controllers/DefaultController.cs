using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DefaultBackend.Controllers
{
    [ApiController]
    [Route("")]
    public class DefaultController : ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;

        public DefaultController(ILogger<DefaultController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            _logger.LogInformation("Default backend endpoint called.");

            _logger.LogInformation(
                "Request {@request}",
                new { Request.Headers, Request.Cookies, Request.Host });

            return NotFound(Request.Headers);
        }
    }
}
