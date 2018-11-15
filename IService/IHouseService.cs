using System;
using DTO;

namespace IService
{
    public interface IHouseService:IServiceSupport
    {
        HouseDTO GetById(long id);
        //分页获取TypeId这种房源类别下CITYID这个城市中房源
        long GetTotalCount(long cityId, long typeId, int pageSize, int currentIndex);
        //增加房源
        long AddNew(HouseDTO house);
        //更新房源
        void Update(HouseDTO house);
        //软删除
        void MakeDeleted(long id);
        //得到房源的图片
        HousePicDTO[] GetPics(long houseId);
        long AddNewHousePic(HousePicDTO housePicDTO);
        //软删除房源图片
        void DeleteHousePic(long housePicId);
        //搜索 返回值包含 总条数 和houseDTO[] 
        HouseSearchResult Search(HouseSearchOptions options);
        int GetCount(long cityId, DateTime startDateTime, DateTime endDateTime);



    }

    public class HouseSearchOptions
    {
        public long CityId { get; set; }//城市 id
        public long TypeId{get;set;}//房源类型，可空
        public long? RegionId{get;set;}//区域，可空
        public int? StartMonthRent{get;set;}//起始月租，可空
        public int? EndMonthRent {get;set;}//结束月租，可空
        public OrderByType OrderByType { get; set; } = OrderByType. MonthRentAsc;//排序 方式
        public string Keywords{get;set;}//搜索关键字，可空
        public int PageSize{get;set;}//每页数据条数
        public int CurrentIndex{get;set;}//当前页码   
    }

    public class HouseSearchResult
    {
        public HouseDTO[] result { get; set; }//当前页数据
        public long totalCount { get; set; }//搜索的结果总条数
    }

    public enum OrderByType
    {
        MonthRentDesc=1,
        MonthRentAsc=2,
        AreaDesc=4,
        AreaAsc=8,
        CreateDateDesc=16
    }
}