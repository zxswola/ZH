using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CaptchaGen;
using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Filters.Watermarks;
using Common;
using log4net;
using MyBLL;
using MyIBLL;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //            string s = CommonHelper.CreateVerifyCode(4);
            //            ImageProcessingJob job = new ImageProcessingJob();
            //            job.Filters.Add(new FixedResizeConstraint(200, 200));
            //            job.SaveProcessedImageToFileSystem(@"H:\image\timg.jpg", @"H:\image\timg1.jpg");
            //            ImageWatermark

            /*
         ImageWatermark imgWatermark = new ImageWatermark(@"D:\a\sauce.png");
         imgWatermark.ContentAlignment = System.Drawing.ContentAlignment.BottomRight;//水印位置
         imgWatermark.Alpha = 100;//透明度，需要水印图片是背景透明的png图片
         ImageProcessingJob jobNormal = new ImageProcessingJob();
         jobNormal.Filters.Add(imgWatermark);//添加水印
         jobNormal.Filters.Add(new FixedResizeConstraint(300, 300));//限制图片的大小，避免生成大图。如果想原图大小处理，就不用加这个Filter
         jobNormal.SaveProcessedImageToFileSystem(@"D:\a\Tulips.jpg", @"D:\a\2.png");
         */


            //            using (MemoryStream ms =
            //                ImageFactory.GenerateImage("a123fdasfa32", 80, 150,
            //                30, 10))
            //            using (FileStream fs = File.OpenWrite(@"d:\1.jpg"))
            //            {
            //                ms.CopyTo(fs);
            //            }
            log4net.Config.XmlConfigurator.Configure();
            //           ILog log= LogManager.GetLogger(typeof(Program));
            //            log.Debug("飞行高度10000米");
            //            log.Warn("youya");
            //            log.Error("destory");
            //            IScheduler sched = new StdSchedulerFactory().GetScheduler();
            //            JobDetailImpl jdBossReport = new JobDetailImpl("jdTest", typeof(TestJob));
            //            var bulider = CalendarIntervalScheduleBuilder.Create();
            //            bulider.WithInterval(3, IntervalUnit.Second);//每隔三秒执行一次
            //            IMutableTrigger triggerBossReport = bulider.Build();
            //            //IMutableTrigger triggerBossReport = CronScheduleBuilder.DailyAtHourAndMinute(9, 53).Build();//每天 23:45 执行一次
            //            triggerBossReport.Key = new TriggerKey("triggerTest");
            //            sched.ScheduleJob(jdBossReport,triggerBossReport);
            //            sched.Start();
            //            IUserBll bll = new UserBll();
            //            bll.AddNew("aaa", "123");

            ContainerBuilder builder = new ContainerBuilder();
            //builder.RegisterType<UserBll>().As<IUserBll>();//注册实现类 Service1，当请求 IService1 接
            Assembly ams = Assembly.Load("MyBLL");
            builder.RegisterAssemblyTypes(ams).AsImplementedInterfaces();
            IContainer container= builder.Build();
            IUserBll bll = container.Resolve<IUserBll>();
            bll.AddNew("aa", "222");
            Console.WriteLine("ok");
            Console.ReadKey();

           
        }
    }
}
