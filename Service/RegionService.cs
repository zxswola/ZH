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
    public class RegionService:IRegionService
    {
        public RegionDTO GetById(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<RegionEntity> bs = new BaseService<RegionEntity>(ctx);
                var region = bs.GetAll().Include(r => r.City).Where(r => r.Id == id).SingleOrDefault();
                return  region==null? null: ToDTO(region);
            }

           
        }

        public RegionDTO[] GetAll(long cityId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<RegionEntity> bs = new BaseService<RegionEntity>(ctx);
                return bs.GetAll().Include(r=>r.City).Where(r => r.CityId == cityId).ToList().Select(r => ToDTO(r)).ToArray();
            }
        }

        public RegionDTO ToDTO(RegionEntity region)
        {
            RegionDTO dto = new RegionDTO();
            dto.CityId = region.CityId;
            dto.CityName = region.City.Name;
            dto.Id = region.Id;
            dto.Name = region.Name;
            dto.CreateDateTime = region.CreateDateTIme;
            return dto;
        }
    }
}
