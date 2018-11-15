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
    public class CommunityService : ICommunityService
    {
        public CommunityDTO[] GetByRegionId(long regionId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<CommunityEntity> bs = new BaseService<CommunityEntity>(ctx);
                return  bs.GetAll().AsNoTracking().Where(c => c.RegionId == regionId).ToList().Select(c => ToDTO(c)).ToArray();
            }
        }

        CommunityDTO ToDTO(CommunityEntity community)
        {
            CommunityDTO dto = new CommunityDTO();
            dto.BulitYear = community.BuiltYear;
            dto.CreateDateTime = community.CreateDateTIme;
            dto.Id = community.Id;
            dto.Loaction = community.Location;
            dto.Name = community.Name;
            dto.RegionId = community.RegionId;
            dto.Traffic = community.Traffic;
            return dto;
        }

    }
}
