using DTO;

namespace IService
{
    public interface ICityService:IServiceSupport
    {
        /// <summary>
        /// 新增城市
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns>新增城市的id</returns>
        long AddNew(string cityName);
        //得到所有城市
        CityDTO[] GetAll();

        CityDTO GetById(long id);
    }
}