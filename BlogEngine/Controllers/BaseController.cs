﻿using Autofac;
using BlogEngine.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Helpers
{
    public class BaseController : Controller
    {
        public readonly IComponentContext component;
        public readonly IPostService postServices;
        public readonly IUserService userServices;
        public readonly IPasswordByUserService passwordByUserServices;

        public BaseController(IComponentContext component)
        {
            this.component = component;
            postServices = this.component.Resolve<IPostService>();
            userServices = this.component.Resolve<IUserService>();
            passwordByUserServices = this.component.Resolve<IPasswordByUserService>();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}