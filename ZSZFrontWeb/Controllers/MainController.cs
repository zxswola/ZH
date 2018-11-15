using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IService;

namespace ZSZFrontWeb.Controllers
{
    public class MainController : Controller
    {
        public ICityService cityService { get; set; }
        // GET: Main
        public ActionResult Index()
        {
            if (Session["ss"] != null)
            {
                return Content(Session["ss"].ToString());
            }

//            cityService.AddNew("北京");
            string s = "abc";
            Session["ss"] = s;
            return Content("ok");
        }
    }
}