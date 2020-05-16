using BlogEngine.Helpers;
using Autofac;

namespace BlogEngine.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IComponentContext component) : base(component)
        {
        }

    }
}
