using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Dapper;
using DTO;
using IService;
using Service.Entities;

namespace DapperService
{
    public class RegionService : IRegionService
    {
        private static string conStr = DbHelper.ConnectString;
        public RegionDTO[] GetAll(long cityId)
        {
            using (DbConnection db = new SqlConnection(conStr))
            {
                string sql = "select * from T_Regions t ,T_Cities a where  t.CityId=a.Id and t.CityId=@cityId";
                var regions = db.Query<RegionEntity, CityEntity, RegionEntity>(sql, (region, city) =>
                    {
                        region.City = city;
                        return region;
                    }, new {cityId = cityId});
                if (!regions.Any())
                {
                    return null;
                }

                return regions.ToList().Select(r => ToDTO(r)).ToArray();
            }
        }

        public RegionDTO GetById(long id)
        {
            return null;
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
