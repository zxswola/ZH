using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using CommonMvc;
using DapperService;
using IService;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using ZSZAdminWeb.App_Start;
using ZSZAdminWeb.Jobs;

namespace ZSZAdminWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //private const string Url = "~/Main/Login";

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            ModelBinders.Binders.Add(typeof(string), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(int), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(long), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(double), new TrimToDBCModelBinder());

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();//把当前 程序集中的 Controller 都注册 //不要忘了.PropertiesAutowired() // 获取所有相关类库的程序集
            //Assembly[] assemblies = new Assembly[] { Assembly.Load("DapperService") };
            Assembly[] assemblies = new Assembly[] { Assembly.Load("Service") };
            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => !type.IsAbstract && typeof(IServiceSupport).IsAssignableFrom(type))
                .AsImplementedInterfaces().PropertiesAutowired();
            var container = builder.Build();
            //注册系统级别的 DependencyResolver，这样当 MVC 框架创建 Controller 等对象的时候都 是管 Autofac 要对象。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//!!! 

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalFilters.Filters.Add(new ZSZExceptionFilter());
            GlobalFilters.Filters.Add(new JsonNetActionFilter());
            GlobalFilters.Filters.Add(new MyAuthorizeFilter());
            //BbQuartz();
            //YzQuartz();
        }

        //void Application_End(object sender, EventArgs e)
        //{
        //    // 在应用程序关闭时运行的代码 
        //    //解决应用池回收问题 
        //    Response.Redirect(Url);
        //}


        private void BbQuartz()
        {

            IScheduler sched = new StdSchedulerFactory().GetScheduler();
           
            {
                JobDetailImpl jdAddExp = new JobDetailImpl("jdBbExp", typeof(BbExpressJob));
                var bulider = CalendarIntervalScheduleBuilder.Create();
                bulider.WithInterval(600, IntervalUnit.Second);//每隔600秒执行一次
                IMutableTrigger triggerAddExp = bulider.Build();
                triggerAddExp.Key = new TriggerKey("BbExp");
                sched.ScheduleJob(jdAddExp, triggerAddExp);
            }
            {
                JobDetailImpl jdDownloadOrder = new JobDetailImpl("jdBbDownloadOrder", typeof(BbDownloadOrderJob));
                var bulider2 = CalendarIntervalScheduleBuilder.Create();
                bulider2.WithInterval(600, IntervalUnit.Second);//每隔600秒执行一次
                IMutableTrigger triggerDownloadOrder = bulider2.Build();
                triggerDownloadOrder.Key = new TriggerKey("BbDownloadOrder");
                sched.ScheduleJob(jdDownloadOrder, triggerDownloadOrder);
            }
            {
                JobDetailImpl jdQuantityUpdate = new JobDetailImpl("jdQuantityUpdate", typeof(BbQtyUpdateJob));
                var bulider3 = CalendarIntervalScheduleBuilder.Create();
                bulider3.WithInterval(600, IntervalUnit.Second);//每隔600秒执行一次
                IMutableTrigger triggerQuantityUpdate = bulider3.Build();
                //IMutableTrigger triggerBossReport = CronScheduleBuilder.DailyAtHourAndMinute(9, 53).Build();//每天 23:45 执行一次
                triggerQuantityUpdate.Key = new TriggerKey("BbQtyUpdate");
                sched.ScheduleJob(jdQuantityUpdate, triggerQuantityUpdate);
            }
            sched.Start();

        }

        private void  YzQuartz()
        {
            IScheduler sched = new StdSchedulerFactory().GetScheduler();
            {
                JobDetailImpl jdAddExp = new JobDetailImpl("jdYzExp", typeof(YzExpressJob));
                var bulider = CalendarIntervalScheduleBuilder.Create();
                bulider.WithInterval(350, IntervalUnit.Second);//每隔600秒执行一次
                IMutableTrigger triggerAddExp = bulider.Build();
                triggerAddExp.Key = new TriggerKey("YzExp");
                sched.ScheduleJob(jdAddExp, triggerAddExp);
            }
            {
                JobDetailImpl jdDownloadOrder = new JobDetailImpl("jdYzDownloadOrder", typeof(YzDownloadOrderJob));
                var bulider2 = CalendarIntervalScheduleBuilder.Create();
                bulider2.WithInterval(350, IntervalUnit.Second);//每隔600秒执行一次
                IMutableTrigger triggerDownloadOrder = bulider2.Build();
                triggerDownloadOrder.Key = new TriggerKey("YzDownloadOrder");
                sched.ScheduleJob(jdDownloadOrder, triggerDownloadOrder);
            }
            {
                JobDetailImpl jdQuantityUpdate = new JobDetailImpl("jdYzQuantityUpdate", typeof(YzQuantityUpdateJob));
                var bulider3 = CalendarIntervalScheduleBuilder.Create();
                bulider3.WithInterval(600, IntervalUnit.Second);//每隔60秒执行一次
                IMutableTrigger triggerQuantityUpdate = bulider3.Build();
                triggerQuantityUpdate.Key = new TriggerKey("YzQuantityUpdate");
                sched.ScheduleJob(jdQuantityUpdate, triggerQuantityUpdate);
            }
            sched.Start();
        }


    }
}
