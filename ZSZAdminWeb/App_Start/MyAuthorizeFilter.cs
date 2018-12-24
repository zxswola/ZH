 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CommonMvc;
using IService;


namespace ZSZAdminWeb.App_Start
{
    public class MyAuthorizeFilter : IAuthorizationFilter
    {
      //  public IAdminUserService userService { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //获得当前要执行的Action上标注的CheckPermissionAttribute 实例对象 
            CheckPermissionAttribute[] permAttrs= (CheckPermissionAttribute[])filterContext.ActionDescriptor
                .GetCustomAttributes(typeof(CheckPermissionAttribute), false);
           
            if (permAttrs.Length == 0)//没有标注Attr
            {
                return;//登录界面 不要求用户登录的功能
            }
            var userId = filterContext.HttpContext.Session["LoginUserId"];
           // int? userId =(int?) filterContext.HttpContext.Session["LoginUserId"];
            if (userId == null)
            {
                //filterContext.HttpContext.Response.Write("没有登录");
                //filterContext.Result = new ContentResult { Content = "没有登录" };
                //根据不同的请求,给予不同的返回格式,确保ajax请求,浏览器也能收到json格式
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonNetResult
                    {
                        Data = new AjaxResult { Status = "redirect", Data = "/Main/Login", ErrorMsg = "没有登录" }
                    };
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Main/Login");
                }


                return;
            }

            IAdminUserService userService = DependencyResolver.Current.GetService<IAdminUserService>();
            foreach (var permAtt in permAttrs)
            {
                if (!userService.HasPermission(Convert.ToInt32(userId), permAtt.Permission))
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new JsonNetResult
                        {
                            Data = new AjaxResult { Status = "error", ErrorMsg = "没有权限" + permAtt.Permission }
                        };
                    }
                    else
                    {
                        filterContext.Result = new ContentResult { Content = "没有" + permAtt.Permission + "这个权限" };
                    }

                    return;
                }
            }


        }
    }
}