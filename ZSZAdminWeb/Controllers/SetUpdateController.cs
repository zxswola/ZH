using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CommonMvc;
using IService;

namespace ZSZAdminWeb.Controllers
{
    public class SetUpdateController : Controller
    {
        public IStoreService StoreService { get; set; }

        // GET: SetUpdate
        public async Task<ActionResult> Index(int pageIndex = 1, int pageSize = 30)
        {
            var items = await StoreService.GetPageData(pageIndex, pageSize);
            var list = await StoreService.GetItems();
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalCount = list.Count;
            return View(items);
        }

        [HttpPost]
        public async Task<ActionResult> SetUpdate(string itemId)
        {
            var result= await StoreService.SetUpdate(itemId);
            return Json(result);
        }
        [HttpPost]
        public async Task<ActionResult> SetNoUpdate(string itemId)
        {
            var result = await StoreService.SetNoUpdate(itemId);
            return Json(result);
        }
        [HttpPost]
        public async Task<ActionResult> BatchSetOn(string[] selectIds)
        {
            foreach (string id in selectIds)
            {
                var result = await StoreService.SetUpdate(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}