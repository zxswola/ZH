﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var builder = new ContainerBuilder();



            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();//把当前 程序集中的 Controller 都注册 //
            Assembly asmService = Assembly.Load("Service");
            builder.RegisterAssemblyTypes(asmService).Where(t=>!t.IsAbstract)
                .AsImplementedInterfaces().PropertiesAutowired();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}
