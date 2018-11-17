using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IService;

namespace ZSZAdminWeb.Controllers
{
    public class PermissionController : Controller
    {
        public IPermissionService PermSvc { get; set; }

        // GET: Permission
        public ActionResult List()
        {
            var perms = PermSvc.GetAll();
            return View(perms);
        }
    }
}