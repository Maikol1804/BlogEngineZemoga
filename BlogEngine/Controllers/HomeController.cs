using BlogEngine.Helpers;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using BlogEngine.Models;
using BlogEngine.Transverse.Enumerator;
using BlogEngine.Transverse.Constants;

namespace BlogEngine.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IComponentContext component) : base(component)
        {
        }

        [HttpPost]
        public JsonResult Logout()
        {
            ResponseViewModel response = new ResponseViewModel();

            HttpContext.Session.Clear();

            response.Code = BasicEnums.State.Ok.GetHashCode().ToString();

            return Json(response);
        }
    }
}
