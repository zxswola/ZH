using System;
using DTO;

namespace IService
{
    public interface IHouseAppointmentService:IServiceSupport
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="name">姓名</param>
        /// <param name="phoneNum">手机号</param>
        /// <param name="houseId">房间</param>
        /// <param name="visitDate">预约看房时间</param>
        /// <returns></returns>
        long AddNew(long? userId, string name, string phoneNum, long houseId, DateTime visitDate);
        //乐观锁解决并发问题
        bool Follow(long adminUserId, long houseAppointmentId);
        //根据id 获取预约
        HouseAppointmentDTO GetById(long id);
        /// <summary>
        /// 得到cityid这个城市中状态为status的预约单数
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        long GetTotalCount(long cityId, string status);
        //分页获取数据
        HouseAppointmentDTO[] GetPageData(long cityId, string status, int pageSize, int currentIndex);

    }
}