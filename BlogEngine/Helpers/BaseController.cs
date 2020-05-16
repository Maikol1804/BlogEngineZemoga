using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using BlogEngine.Services.Contracts;
using BlogEngine.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Helpers
{
    public class BaseController : Controller
    {
        public readonly IComponentContext component;
        public readonly IPostService postServices;

        public BaseController(IComponentContext component)
        {
            this.component = component;
            postServices = this.component.Resolve<IPostService>();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}