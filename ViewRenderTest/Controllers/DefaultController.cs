using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMvc;

namespace ViewRenderTest.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            string html = MVCHelper.RenderViewToString(ControllerContext, "~/Views/Default/Index.cshtml", "hello");
            System.IO.File.WriteAllText("d:/1.txt", html);
            return View();
        }
    }
}