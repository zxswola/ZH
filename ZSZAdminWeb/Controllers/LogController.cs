using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DapperService;
using IService;

namespace ZSZAdminWeb.Controllers
{
    public class LogController : Controller
    {
        public ILogService logService { get; set; }

        // GET: Log
        public async Task<ActionResult> List( string begin,string end, int pageIndex=1, int pageSize=30)
        {
            var logs = await logService.GetPageData(pageSize, pageIndex, begin, end);
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalCount = logService.GetCount();
            ViewBag.begin = begin;
            ViewBag.end = end;
            return View(logs);
        }
    }
}