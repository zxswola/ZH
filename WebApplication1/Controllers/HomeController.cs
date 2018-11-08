using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IService;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
       public IUserService UserService { get; set; }

        // GET: Home
        public ActionResult Index()
        {
            bool b = UserService.CheckLogin("aaa", "123");
//            return View();
            return Content(b.ToString());
        }
    }
}