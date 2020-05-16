using Autofac;
using BlogEngine.Helpers;

namespace BlogEngine.Controllers
{
    public class AuthenticateController : BaseController
    {
        public AuthenticateController(IComponentContext component) : base(component)
        {
        }
    }
}