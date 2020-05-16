using Autofac;
using BlogEngine.Helpers;

namespace BlogEngine.Controllers
{
    public class PendingPostsController : BaseController
    {
        public PendingPostsController(IComponentContext component) : base(component)
        {
        }
    }
}