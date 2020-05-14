using Microsoft.Extensions.Logging;
using BlogEngine.Helpers;

namespace BlogEngine.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

    }
}
