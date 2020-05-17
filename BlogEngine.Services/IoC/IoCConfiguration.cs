using Autofac;
using BlogEngine.DataAccess;
using BlogEngine.DataAccess.Context;
using BlogEngine.DataAccess.Implementations;
using BlogEngine.DataAccess.Interfaces;
using BlogEngine.DataAccess.Models;
using BlogEngine.Services.Contracts;
using BlogEngine.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Services.IoC
{
    public class IoCConfiguration : Module
    {
        protected override void Load(ContainerBuilder builder) {

            RegisterContext(builder);
            RegisterServices(builder);
            RegisterRepositories(builder);
        }

        private static void RegisterContext(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(BlogEngineContext)).As(typeof(IContext)).InstancePerLifetimeScope();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<PostServices>().As<IPostService>();
            builder.RegisterType<UserServices>().As<IUserService>();
            builder.RegisterType<PasswordByUserServices>().As<IPasswordByUserService>();
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<PasswordByUserRepository>().As<IPasswordByUser>();
            builder.RegisterType<PostRepository>().As<IPost>();
            builder.RegisterType<RolRepository>().As<IRol>();
            builder.RegisterType<UserRepository>().As<IUser>();
        }


    }
}
