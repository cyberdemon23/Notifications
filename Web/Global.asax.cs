using Autofac;
using Autofac.Integration.Mvc;
using Notifications.Web.Connections;
using SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Notifications.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //I would prefer to leave this unpolluted, but I have to because of the way that 
            //all of the dependency injection isn't consistent and how I have to pass the 
            //SignalR Dependency Resolver into the MapConnection method
            var container = GetContainer();

            //MVC 4
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //WebApi
            GlobalConfiguration.Configuration.DependencyResolver = new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(container);

            //SignalR
            var signalRDependencyResolver = new AutofacSignalRDependencyResolver(container);
            GlobalHost.DependencyResolver = signalRDependencyResolver;
            RouteTable.Routes.MapConnection<MyConnection>("echo", "echo/{*operation}", signalRDependencyResolver);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AreaRegistration.RegisterAllAreas();
        }

        private IContainer GetContainer()
        {
            var configuration = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();

            builder.RegisterType<UserConnectionRepository>().As<IUserConnectionRepository>().SingleInstance();

            return builder.Build();
        }
    }
}