using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMvc;
using DapperService;
using DTO;
using IService;
using ZSZFrontWeb.Models;

namespace ZSZFrontWeb.Controllers
{
    public class MainController : Controller
    {
        private ILinkService linkService = new LinkService();
       
        public ActionResult Index()
        {
            string cacheKey = "MainLinks";
            // LinkDTO[] model =(LinkDTO[])HttpContext.Cache[cacheKey];
           // LinkModel model = new LinkModel();
            LinkModel model = (LinkModel)MemcacheMgr.Instance.GetValue(cacheKey);
            if (model == null)
            {
                 model = new LinkModel();
                model.Links = linkService.GetAll();
               // HttpContext.Cache.Insert(cacheKey, model, null,DateTime.Now.AddMinutes(1),TimeSpan.Zero);
                MemcacheMgr.Instance.SetValue(cacheKey, model, TimeSpan.FromMinutes(1));
            }
            string html = MVCHelper.RenderViewToString(ControllerContext, "~/Views/Main/Index.cshtml", model);
            System.IO.File.WriteAllText(@"D:\workspace\ZH\ZSZFrontWeb\1.html", html);


            return View(model);
        }
        [HttpGet]
        public ActionResult Edit()
        {
            var links = linkService.GetAll();
            return View(links);
        }
        [HttpPost]
        public ActionResult Edit(LinkEditModel model)
        {
            linkService.UpdateLinks(model.Ids, model.Links);
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}