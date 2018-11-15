using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using IService;
using Service.Entities;

namespace Service
{
    public class CityService : ICityService
    {
      
        public long AddNew(string cityName)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<CityEntity> bs = new BaseService<CityEntity>(ctx);
                //判断是否已存在   用Any效率会比较高
                bool exists= bs.GetAll().Any(c => c.Name == cityName);
                if (exists)
                {
                    throw new ArgumentException("城市已经存在");
                }

                CityEntity city = new CityEntity();
                city.Name = cityName;
                ctx.Cities.Add(city);
                ctx.SaveChanges();
                return city.Id;
            }
        }

        private CityDTO ToDTO(CityEntity city)
        {
            CityDTO dto = new CityDTO();
            dto.CreateDateTime = city.CreateDateTIme;
            dto.Id = city.Id;
            dto.Name = city.Name;
            return dto;
        }

        public CityDTO[] GetAll()
        {
            
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<CityEntity> bs = new BaseService<CityEntity>(ctx);
               return  bs.GetAll().AsNoTracking().ToList().Select(c=>ToDTO(c)).ToArray();
               
            }
        }

        public CityDTO GetById(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<CityEntity> bs = new BaseService<CityEntity>(ctx);
               var city= bs.GetById(id);
                if (city == null)
                {
                    return null;
                }

                return ToDTO(city);
            }
        }
    }
}
