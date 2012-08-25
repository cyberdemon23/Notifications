using Autofac;
using Autofac.Integration.Mvc;
using Newtonsoft.Json;
using Notifications.Web.Areas.Api.Controllers;
using Notifications.Web.Connections;
using Notifications.Web.Models;
using SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Notifications.Web
{
    public static class DependencyResolverConfig
    {
        public static void RegisterDependencyResolvers()
        {
            var container = GetContainer();

            //MVC 4
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //WebApi
            GlobalConfiguration.Configuration.DependencyResolver = new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(container);

            //SignalR
            //Trying to override the dependency resolver that autofac was using is causing problems, so we're just going to register
            //the component that we need in the Connections with their default dependency resolver
            GlobalHost.DependencyResolver.Register(typeof(NotificationHub), () => container.Resolve<NotificationHub>());
        }

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(GlobalHost.ConnectionManager).As<IConnectionManager>();
            builder.RegisterType<NotificationController>();
            builder.RegisterType<NotificationHub>().SingleInstance();
            builder.RegisterType<NotificationQueue>().As<INotificationQueue>();
            builder.RegisterType<NotificationRepository>().As<INotificationRepository>().SingleInstance();
            builder.RegisterType<UserConnectionRepository>().As<IUserConnectionRepository>().SingleInstance();

            return builder.Build();
        }

        
    }
}