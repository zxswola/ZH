using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaGen;
using Common;
using CommonMvc;
using IService;
using Service;
using ZSZAdminWeb.Models;

namespace ZSZAdminWeb.Controllers
{
    public class MainController : Controller
    {
        public IAdminUserService UserService { get; set; }

        // GET: Main
        public ActionResult Index()
        {
            var userId = Session["LoginUserId"];
            if (userId == null)
            {
                return Redirect("~/Main/Login");
            }

            var user=UserService.GetById(Convert.ToInt32(userId));

            return View(user);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult {Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState)});
            }

            //if (model.VerifyCode != (string)TempData["verifyCode"])
            //{
            //    return Json(new AjaxResult { Status = "error", ErrorMsg = "验证码错误" });
            //}

            bool result =UserService.CheckLogin(model.UserName, model.Password);

            if (result)
            {
                //Session中保存当前登录用户Id
                //Session["LoginUserId"]
                //    = UserService.GetByUserName(model.UserName).Id;
                Session["LoginUserId"]
                    = UserService.GetByPhoneNum(model.UserName).Id;
                //给后面检查“当前Session登录的这个用户有没有***的权限”
                return Json(new AjaxResult { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "用户名或者密码错误" });
            }
        }

        public ActionResult CreateVerifyCode()
        {
            string verifyCode = CommonHelper.CreateVerifyCode(4);
            TempData["verifyCode"] = verifyCode;
            MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 50, 100, 20, 2);
            return File(ms,"image/jpeg");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return Redirect("~/Main/Login");
        }
    }
}