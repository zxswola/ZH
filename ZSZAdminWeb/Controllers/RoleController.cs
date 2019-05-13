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
    public class RoleController : Controller
    {
        public IRoleService roleService { get; set; }
        public IPermissionService perService { get; set; }
        [CheckPermission("Role.List")]
        public ActionResult List()
        {
            var role= roleService.GetAll();
            return View(role);
        }
        [HttpGet]
        [CheckPermission("Role.Add")]
        public ActionResult Add()
        {
            var pers=perService.GetAll();
            return View(pers);
        }
        [HttpPost]
        [CheckPermission("Role.Add")]
        public ActionResult Add(RoleAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult {Status = "error",ErrorMsg=MVCHelper.GetValidMsg(ModelState)});
            }

            //transactionScope
            int roleId=(int)roleService.AddNew(model.Name);
            perService.AddPermids(roleId, model.PermissionIds);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpGet]
        [CheckPermission("Role.Edit")]
        public ActionResult Edit(long id)
        {
            var role=roleService.GetById(id);
            var rolePerms=perService.GetByRoleId(id);
            var allPerms = perService.GetAll();
            RoleEditGetModel model = new RoleEditGetModel();
            model.Role = role;
            model.RolePers = rolePerms;
            model.AllPers = allPerms;
            return View(model);
        }
        [HttpPost]
        [CheckPermission("Role.Edit")]
        public ActionResult Edit(RoleEditModel model)
        {
            roleService.Update(model.Id, model.Name);
            perService.UpdatePermids(model.Id, model.PermissionIds);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("Role.Delete")]
        public ActionResult Delete(int id)
        {
            roleService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("Role.Delete")]
        public ActionResult BatchDelete(int[] selectIds)
        {
            foreach (int id in selectIds)
            {
                roleService.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }

    }
}