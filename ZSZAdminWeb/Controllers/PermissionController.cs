using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMvc;
using IService;
using ZSZAdminWeb.App_Start;
using ZSZAdminWeb.Models;

namespace ZSZAdminWeb.Controllers
{
    public class PermissionController : Controller
    {
        public IPermissionService PermSvc { get; set; }

        // GET: Permission
        [CheckPermission("Permission.List")]
        public ActionResult List()
        {
            var perms = PermSvc.GetAll();
            return View(perms);
        }
        //不推荐用 
        public ActionResult Delete(int id)
        {
            PermSvc.MarkDelete(id);
            return RedirectToAction("List");//删除后刷新
        }
        [CheckPermission("Permission.Delete")]
        public ActionResult Delete2(int id)
        {
            PermSvc.MarkDelete(id);
            return Json(new AjaxResult{Status = "ok"});//删除后刷新
        }

        [HttpGet]
        [CheckPermission("Permission.Add")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [CheckPermission("Permission.Add")]
        public ActionResult Add(PermissionAddNewModel model)
        {
            PermSvc.AddPermission(model.Name,model.Description);
            //return RedirectToAction("List");
            //todo 
            return Json(new AjaxResult {Status = "ok"});
        }
        [HttpGet]
        [CheckPermission("Permission.Edit")]
        public ActionResult Edit(int id)
        {
            var perm = PermSvc.GetById(id);
            return View(perm);
        }
        [HttpPost]
        [CheckPermission("Permission.Edit")]
        public ActionResult Edit(PermissionEditModel model)
        {
            PermSvc.UpdatePermission(model.Id, model.Name,model.Description);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("Permission.Delete")]
        public ActionResult BatchDelete(int[] selectIds)
        {
            foreach (int id in selectIds)
            {
                PermSvc.MarkDelete(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }

    }
}