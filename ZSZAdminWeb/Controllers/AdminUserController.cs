using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using CommonMvc;
using DTO;
using IService;
using ZSZAdminWeb.App_Start;
using ZSZAdminWeb.Models;

namespace ZSZAdminWeb.Controllers
{
    public class AdminUserController : Controller
    {
        public ICityService cityService { get; set; }
        public IRoleService roleService { get; set; }

        public IAdminUserService adminUserService { get; set; }
        [CheckPermission("AdminUser.List")]
      
        public ActionResult List(int pageIndex=1,int pageSize=100)
        {
            var users=adminUserService.GetPageData(pageSize, pageIndex);
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalCount= adminUserService.GetAll().Count();
            return View(users);
        }
        [CheckPermission("AdminUser.Add")]
        [HttpGet]
        public ActionResult Add()
        {

            var roles = roleService.GetAll();
            AdminUserAddViewModel model = new AdminUserAddViewModel();
            //model.Cities = cities.ToArray();
            model.Roles = roles;
            return View(model);
        }
        [CheckPermission("AdminUser.Add")]
        [HttpPost]
        public ActionResult Add(AdminUserAddModel model)
        {
            if (!ModelState.IsValid)
            {
                string msg = MVCHelper.GetValidMsg(ModelState);
                return Json(new AjaxResult {Status = "error", ErrorMsg = msg});
            }
           int id= adminUserService.AddAdminUser(model.Name, model.UserName, model.Password, model.Email, model.PhoneNum);
           roleService.AddRoles(id, model.RoleIds);
            return Json(new AjaxResult{Status="ok"});
        }

        public ActionResult CheckUserName(string userName,int? userId)
        {
            var admin = adminUserService.GetByUserName(userName);
            bool isOK = false;
            //如果没有给userId，则说明是“插入”，只要检查是不是存在这个手机号
            if (userId == null)
            {
                isOK = (admin == null);
            }
            else//如果有userId，则说明是修改，则要把自己排除在外
            {
                isOK = (admin == null || admin.Id == userId);
            }
            return Json(new AjaxResult { Status = isOK ? "ok" : "exists" });
        }
        [CheckPermission("AdminUser.Delete")]
        public ActionResult Delete(int id)
        {
            adminUserService.MarkDeleted(id);
            return Json(new AjaxResult{Status="ok"});
        }
        public ActionResult BatchDelete(int[] selectIds)
        {
            foreach (int id in selectIds)
            {
                adminUserService.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("AdminUser.Edit")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user=adminUserService.GetById(id);
            if (user == null)
            {
                return View("Error",(object)"id 指定的人员不存在");
            }

            var userRoles=roleService.GetByAdminUserId(id);
            var roles = roleService.GetAll();
            AdminUserEditViewModel model = new AdminUserEditViewModel();
            model.RoleIds = userRoles.Select(r => r.Id).ToArray();
            model.User = user;
            model.Roles = roles;
            return View(model);
        }
        [CheckPermission("AdminUser.Edit")]
        [HttpPost]
        public ActionResult Edit(AdminUserEditModel model)
        {
            adminUserService.UpdateAdminUser(model.Id, model.Name, model.UserName, model.Password, model.Email,
                model.PhoneNum);
            roleService.UpdateRoleIds(model.Id, model.RoleIds);
            return Json(new AjaxResult { Status = "ok" });

        }



    }
}