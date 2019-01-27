using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CaptchaGen;
using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Filters.Watermarks;
using Common;
using DapperService;
using DTO;
using IService;
using log4net;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;


namespace Test
{
    class Program
    {
        static  void Main(string[] args)
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
            //log4net.Config.XmlConfigurator.Configure();
            //           ILog log= LogManager.GetLogger(typeof(Program));
            //            log.Debug("飞行高度10000米");
            //            log.Warn("youya");
            //            log.Error("destory");
            log4net.Config.XmlConfigurator.Configure();
            //IScheduler sched = new StdSchedulerFactory().GetScheduler();
            //{
            //    JobDetailImpl jdAddExp = new JobDetailImpl("jdAddExp", typeof(AddExpressJob));
            //    var bulider = CalendarIntervalScheduleBuilder.Create();
            //    bulider.WithInterval(300, IntervalUnit.Second);//每隔60秒执行一次
            //    IMutableTrigger triggerAddExp = bulider.Build();
            //    //IMutableTrigger triggerBossReport = CronScheduleBuilder.DailyAtHourAndMinute(9, 53).Build();//每天 23:45 执行一次
            //    triggerAddExp.Key = new TriggerKey("addExp");
            //    sched.ScheduleJob(jdAddExp, triggerAddExp);
            //}
            //{
            //    JobDetailImpl jdDownloadOrder = new JobDetailImpl("jdDownloadOrder", typeof(DownloadOrdersJob));
            //    var bulider2 = CalendarIntervalScheduleBuilder.Create();
            //    bulider2.WithInterval(300, IntervalUnit.Second);//每隔60秒执行一次
            //    IMutableTrigger triggerDownloadOrder = bulider2.Build();
            //    //IMutableTrigger triggerBossReport = CronScheduleBuilder.DailyAtHourAndMinute(9, 53).Build();//每天 23:45 执行一次
            //    triggerDownloadOrder.Key = new TriggerKey("DownloadOrder");
            //    sched.ScheduleJob(jdDownloadOrder, triggerDownloadOrder);
            //}
            //{
            //    JobDetailImpl jdQuantityUpdate = new JobDetailImpl("jdQuantityUpdate", typeof(QuantityUpdateJob));
            //    var bulider3 = CalendarIntervalScheduleBuilder.Create();
            //    bulider3.WithInterval(600, IntervalUnit.Second);//每隔60秒执行一次
            //    IMutableTrigger triggerQuantityUpdate = bulider3.Build();
            //    //IMutableTrigger triggerBossReport = CronScheduleBuilder.DailyAtHourAndMinute(9, 53).Build();//每天 23:45 执行一次
            //    triggerQuantityUpdate.Key = new TriggerKey("QuantityUpdate");
            //    sched.ScheduleJob(jdQuantityUpdate, triggerQuantityUpdate);
            //}
            //sched.Start();









            //YzShopService service = new YzShopService();
            //string token = service.QueryToken();
            //var goods = service.GetGoods(token);
            //var items = service.GetSku_Root(446856109, token);

            //int count = 101;
            //int count2 = 151;
            //int a = count % 100 == 0 ? count / 100 : (count / 100) + 1;
            //int b = count2 % 100 == 0 ? count2 / 100 : (count2 / 100) + 1;




            //IScheduler sched = new StdSchedulerFactory().GetScheduler();
            //JobDetailImpl jdDownloadOrder = new JobDetailImpl("jdDownloadOrder", typeof(DownloadOrdersJob));
            //var bulider = CalendarIntervalScheduleBuilder.Create();
            //bulider.WithInterval(600, IntervalUnit.Second);//每隔60秒执行一次
            //IMutableTrigger triggerDownloadOrder = bulider.Build();
            ////IMutableTrigger triggerBossReport = CronScheduleBuilder.DailyAtHourAndMinute(9, 53).Build();//每天 23:45 执行一次
            //triggerDownloadOrder.Key = new TriggerKey("DownloadOrder");
            //sched.ScheduleJob(jdDownloadOrder, triggerDownloadOrder);
            //sched.Start();

            //            IUserBll bll = new UserBll();
            //            bll.AddNew("aaa", "123");

            //ContainerBuilder builder = new ContainerBuilder();
            ////builder.RegisterType<UserBll>().As<IUserBll>();//注册实现类 Service1，当请求 IService1 接
            //Assembly ams = Assembly.Load("MyBLL");
            //builder.RegisterAssemblyTypes(ams).AsImplementedInterfaces();
            //IContainer container= builder.Build();
            //IUserBll bll = container.Resolve<IUserBll>();
            //bll.AddNew("aa", "222");
            //using (MyDbContext ctx = new MyDbContext())
            //{
            //    ctx.Database.Delete();
            //    ctx.Database.Create();
            //}
            //new AdminLogService().AddNew(1, "测试消息");
            //IRegionService regionService = new RegionService();
            //var regions = regionService.GetAll(1);
            //AdminUserService service = new AdminUserService();
            //RoleService roleService = new RoleService();
            //roleService.AddNew("hahahha");

            //roleService.AddRoles(2, new int[] {3, 2});
            //var list=service.GetAll();
            //var list2 = service.GetById(1);
            //roleService.GetByAdminUserId(2);
            //var list = service.GetAll(name);
            //service.AddAdminUser("test7", "wweeweewwwqqw", "123456", "","");
            //service.UpdateAdminUser(7, "moweeeai", "gaiqeeeng", "123123", "wuo@126.com", "333333");







            //获取有赞订单同步至ERP
            //YzShopService yzService = new YzShopService();
            //YzOrderService orderService = new YzOrderService();
            //YzExpressageService expService = new YzExpressageService();
            //string token = yzService.QueryToken();
            //if (token != null)
            //{
            //    bool flag = true;
            //    while (flag)
            //    {
            //        int pageNo = 1;

            //        OrderRequest or = new OrderRequest()
            //        {
            //            start_created = Convert.ToDateTime("2018-12-1 00:00:00"),
            //            end_created = DateTime.Now,
            //            page_no = pageNo,
            //            page_size = 100,
            //            status = "WAIT_SELLER_SEND_GOODS"
            //        };
            //        OrderResponse orders = yzService.GetOrder(or, token);
            //        if (orders != null)
            //        {
            //            if (orders.response.total_results == 0)
            //            {
            //                flag = false;
            //                break;
            //            }

            //            if (orders.response.total_results > 0 && orders.response.total_results <= 100)
            //            {
            //                //100条内 循环完退出
            //                flag = false;
            //                foreach (var order in orders.response.full_order_info_list)
            //                {
            //                    //if (orderService.QueryOrderIsExit(order.full_order_info.order_info.tid))
            //                    //{
            //                    //    continue;
            //                    //}

            //                    if (order.full_order_info.order_info.status_str == "待发货")
            //                    {
            //                        string orderIds = yzService.AddOrder(order);
            //                        if (!string.IsNullOrEmpty(orderIds))
            //                        {
            //                            List<string> listorderId = orderIds.Split(',').ToList();
            //                            List<string> listOrderId = new List<string>();
            //                            foreach (var orderId in listorderId)
            //                            {
            //                                if (!string.IsNullOrEmpty(orderId))
            //                                {
            //                                    //快递中间表没数据
            //                                    if (!expService.IsExit(orderId))
            //                                    {
            //                                        listOrderId.Add(orderId);
            //                                    }
            //                                }
            //                            }
            //                            if (listOrderId.Count == 0)
            //                            {
            //                                continue;
            //                            }
            //                            //更新订单来源为有赞
            //                            orderService.UpdateOrderSourceTypeID(listOrderId);
            //                            if (listOrderId.Count == 1)
            //                            {
            //                                expService.InsertExpressage(listOrderId[0], " ");
            //                            }
            //                            else
            //                            {
            //                                Dictionary<string, string> dicOrder = new Dictionary<string, string>();
            //                                foreach (var orderId in listOrderId)
            //                                {
            //                                    List<OrderList> orderList = new List<OrderList>();
            //                                    var listItems = orderService.QueryDTOrder(orderId);
            //                                    foreach (var itemId in listItems)
            //                                    {
            //                                        var ol = order.full_order_info.orders.Where(o => (o.outer_sku_id.Substring(o.outer_sku_id.LastIndexOf("|") + 1, o.outer_sku_id.Length - o.outer_sku_id.LastIndexOf("|") - 1)) == itemId).SingleOrDefault();
            //                                        orderList.Add(new OrderList { itemid = itemId, oid = ol.oid });
            //                                    }

            //                                    dicOrder.Add(orderId, CommonHelper.ToJson(orderList));

            //                                }
            //                                expService.InsertExpressageAll(dicOrder);
            //                            }

            //                        }
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                pageNo++;
            //            }
            //        }
            //    }
            //}


            //HttpClient hc = new HttpClient();

            //Dictionary<string, string> dict = new Dictionary<string, string>();
            ////dict.Add()
            //FormUrlEncodedContent content = new FormUrlEncodedContent(dict);
            //HttpResponseMessage msg = await hc.PostAsync("sssss", content);
            //MessageBox.Show("响应码" + msg.StatusCode);
            //string res = await msg.Content.ReadAsStringAsync();
            //MessageBox.Show(res);
            //string secret = "eaa9dd6562a78156fe973f74e9dbbe38";
            //string appId = "epjv";
            //string session = "82aa50b0163552005c2323152ba54";
            ////string method = "beibei.outer.logistics.company.get";
            //string method = "beibei.outer.trade.order.get";
            //string gateway = "http://api.open.beibei.com/outer_api/out_gateway/route.html";
            //HttpClient hc = new HttpClient();
            //Dictionary<string, string> dict = new Dictionary<string, string>();
            //dict.Add("method", method);
            //dict.Add("app_id",appId);
            //dict.Add("session", session);
            //dict.Add("timestamp",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //dict.Add("time_range", "pay_time");
            //dict.Add("start_time", DateTime.Now.AddDays(-27).ToString("yyyy-MM-dd HH:mm:ss"));
            //dict.Add("end_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //dict.Add("status", "1");
            //string sing = CommonHelper.BbOpera(dict, secret);
            //dict.Add("sign", sing);
            ////dict.Add()
            //FormUrlEncodedContent content = new FormUrlEncodedContent(dict);
            //Task<HttpResponseMessage> taskMsg = hc.PostAsync(gateway, content);
            //HttpResponseMessage msg = taskMsg.Result;
            //Task<string> taskRead = msg.Content.ReadAsStringAsync();
            //string res = taskRead.Result;
            // var companys = CommonHelper.DeJson<BbCompanyResponse>(res);

            //BbShopService bbService = new BbShopService();
            //var a = bbService.QueryGoods();
            // var a =  bbService.GetListItem().Result;

            //string strattime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-25";
            //string endtime = DateTime.Now.ToString("yyyy-MM") + "-25";


            //Console.WriteLine("开始同步"+ strattime+"        "+ endtime);

           int a= DateTime.Now.Month;
            Console.WriteLine(a);
            Console.ReadKey();

        }









    }
}
