using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonMvc;
using DTO;
using IService;
using ZSZAdminWeb.Models;

namespace ZSZAdminWeb.Controllers
{
   
    public class HouseController : Controller
    {
        public IAdminUserService userService { get; set; }
        public IHouseService houseService { get; set; }

        public IRegionService regionService { get; set; }
        public IIdNameService idNameService { get; set; }
        public IAttachmentService attService { get; set; }
        public ICommunityService communityService { get; set; }

        // GET: House
        public ActionResult List(long typeId=14,int pageIndex=1)
        {
            long? userId = AdminHelper.GetUserId(HttpContext);
            if (userId == null)
            {
                return Redirect("~/Main/Login");
            }

            long? cityId = userService.GetById(userId.Value).CityId;
            if (cityId == null)
            {
                return View("Error", (object) "总部不能进行房源管理");
            }

            HouseSearchOptions options = new HouseSearchOptions();
            options.CityId = cityId.Value;
            options.TypeId = 14;
            options.PageSize = 10;
            options.CurrentIndex = pageIndex;
            var result = houseService.Search(options);
            ViewBag.PageIndex = pageIndex;
            ViewBag.TotalCount = (int)result.totalCount;
            ViewBag.typeId = typeId;
            //var house=houseService.s

            return View(result.result);
        }

        [HttpGet]
        public ActionResult Add()
        {
            long? userId = AdminHelper.GetUserId(HttpContext);
            if (userId == null)
            {
                return Redirect("~/Main/Login");
            }
            long? cityId = userService.GetById(userId.Value).CityId;
            if (cityId == null)
            {
                return View("Error", (object)"总部不能进行房源管理");
            }
           
            var regions = regionService.GetAll(cityId.Value);
            var roomTypes = idNameService.GetAll("户型");
            var statuses = idNameService.GetAll("房屋状态");
            var decorateStatuses = idNameService.GetAll("装修状态");
            var types = idNameService.GetAll("房屋类别");
            var attachments = attService.GetAll();
            HouseAddViewModel model = new HouseAddViewModel();
            model.Regions = regions;
            model.RoomTypes = roomTypes;
            model.Statuses = statuses;
            model.DecorateStatuses = decorateStatuses;
            model.Types = types;
            model.Attachments = attachments;
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Add(HouseAddModel model)
        {
            long? userId = AdminHelper.GetUserId(HttpContext);
            if (userId == null)
            {
                return Redirect("~/Main/Login");
            }
            long? cityId = userService.GetById(userId.Value).CityId;
            if (cityId == null)
            {
                return View("Error", (object)"总部不能进行房源管理");
            }

            HouseDTO dto = new HouseDTO();
            dto.Address = model.Address;
            dto.Area = model.Area;
            dto.AttachementIds = model.AttachmentIds;
            dto.CheckInDateTime = model.CheckInDateTime;
            dto.CommunityId = model.CommunityId;
            dto.DecorateStatusId = model.DecorateStatusId;
            dto.Descriotion = model.Description;
            dto.Direction = model.Direction;
            dto.FloorIndex = model.FloorIndex;
            dto.LookableDateTime = model.LookableDateTime;
            dto.MonthRent = model.MonthRent;
            dto.OwnerName = model.OwnerName;
            dto.OwnerPhoneNum = model.OwnerPhoneNum;
            dto.RoomTypeId = model.RoomTypeId;
            dto.StatusId = model.StatusId;
            dto.TotalFloorCount = model.TotalFloor;
            dto.TypeId = model.TypeId;
            houseService.AddNew(dto);

            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult LoadCommunities(long regionId)
        {
            var communities = communityService.GetByRegionId(regionId);
            return Json(new AjaxResult {Status = "ok", Data = communities});
        }
    }
}