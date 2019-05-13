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
    public class HouseService : IHouseService
    {
        public long AddNew(HouseDTO house)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                HouseEntity entity = new HouseEntity();
                entity.CommunityId = house.CommunityId;
                entity.RoomTypeId = house.RoomTypeId;
                entity.Address = house.Address;
                entity.StatusId = house.StatusId;
                entity.MonthRent = house.MonthRent;
                entity.Area = house.Area;
                entity.DecorateStatusId = house.DecorateStatusId;
                entity.TotalFloorCount = house.TotalFloorCount;
                entity.FloorIndex = house.FloorIndex;
                entity.TypeId = house.TypeId;
                entity.Direction = house.Direction;
                entity.LookableDateTime = house.LookableDateTime;
                entity.CheckInDateTime = house.CheckInDateTime;
                entity.OwnerName = house.OwnerName;
                entity.OwnerPhoneNum = house.OwnerPhoneNum;
                BaseService<AttachmentEntity> attBS = new BaseService<AttachmentEntity>(ctx);
                var atts= attBS.GetAll().Where(e => house.AttachementIds.Contains(e.Id));
                foreach (var att in atts)
                {
                    entity.Attachments.Add(att);
                }
                ctx.Houses.Add(entity);
                ctx.SaveChanges();
                return entity.Id;
            }
        }

        public long AddNewHousePic(HousePicDTO housePicDTO)
        {
            HousePicEntity entity = new HousePicEntity();
            entity.HouseId = housePicDTO.HouseId;
            entity.ThumbUrl = housePicDTO.ThumbUrl;
            entity.Url = housePicDTO.Url;
            using (MyDbContext ctx = new MyDbContext())
            {
                ctx.HousePics.Add(entity);
                return entity.Id;
            }
        }

        public void DeleteHousePic(long housePicId)
        {
            throw new NotImplementedException();
        }

        public HouseDTO GetById(long id)
        {
            throw new NotImplementedException();
        }

        public int GetCount(long cityId, DateTime startDateTime, DateTime endDateTime)
        {
            throw new NotImplementedException();
        }

        public HousePicDTO[] GetPics(long houseId)
        {
            throw new NotImplementedException();
        }

        public long GetTotalCount(long cityId, long typeId, int pageSize, int currentIndex)
        {
            throw new NotImplementedException();
        }

        public void MakeDeleted(long id)
        {
            throw new NotImplementedException();
        }

        public HouseSearchResult Search(HouseSearchOptions options)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<HouseEntity> bs = new BaseService<HouseEntity>(ctx);
                var items = bs.GetAll().Where(e =>
                    e.Community.Region.CityId == options.CityId && e.TypeId == options.TypeId);
                if (options.RegionId != null)
                {
                    items = items.Where(e => e.Community.RegionId == options.RegionId);
                }

                if (options.StartMonthRent != null)
                {
                    items = items.Where(e => e.MonthRent >= options.StartMonthRent);
                }

                if (options.EndMonthRent != null)
                {
                    items = items.Where(e => e.MonthRent <= options.EndMonthRent);
                }

                if (!string.IsNullOrEmpty(options.Keywords))
                {
                    items = items.Where(e => e.Address.Contains(options.Keywords)
                                             || e.Description.Contains(options.Keywords)
                                             ||e.Community.Name.Contains(options.Keywords)
                                             ||e.Community.Name.Contains(options.Keywords)
                                             ||e.Community.Location.Contains(options.Keywords)
                                             ||e.Community.Traffic.Contains(options.Keywords)
                                             );
                }

                long totalCount = items.LongCount();
                items = items.Include(e => e.Attachments)
                    .Include(e => e.Community)
                    .Include(nameof(HouseEntity.Community) + "." + nameof(CommunityEntity.Region))
                    .Include(nameof(HouseEntity.Community)+"."+nameof(CommunityEntity.Region)+"."+nameof(RegionEntity.City))
                    .Include(e => e.DescorateStatus)
                    .Include(e => e.Status)
                    .Include(e => e.RoomType)
                    .Include(e => e.Type);
                switch (options.OrderByType)
                {
                    case OrderByType.AreaAsc:
                        items = items.OrderBy(e => e.Area);
                        break;
                    case OrderByType.AreaDesc:
                        items = items.OrderByDescending(e => e.Area);
                        break;
                    case OrderByType.CreateDateDesc:
                        items = items.OrderByDescending(e => e.CreateDateTIme);
                        break;
                    case OrderByType.MonthRentAsc:
                        items = items.OrderBy(e => e.MonthRent);
                        break;
                    case OrderByType.MonthRentDesc:
                        items = items.OrderByDescending(e => e.MonthRent);
                        break;
                }

                items = items.Skip((options.CurrentIndex - 1) * options.PageSize).Take(options.PageSize);
                HouseSearchResult searchResult = new HouseSearchResult();
                searchResult.totalCount = totalCount;
                List<HouseDTO> houses = new List<HouseDTO>();
                foreach (var item in items)
                {
                    houses.Add(ToDTO(item));
                }

                searchResult.result = houses.ToArray();
                return searchResult;
            }
            //throw new NotImplementedException();
        }

        public void Update(HouseDTO house)
        {
            throw new NotImplementedException();
        }

        private HouseDTO ToDTO(HouseEntity entity)
        {
            HouseDTO dto = new HouseDTO();
            dto.Address = entity.Address;
            dto.Area = entity.Area;
            dto.AttachementIds = entity.Attachments.Select(e => e.Id).ToArray();
            dto.CheckInDateTime = entity.CheckInDateTime;
            dto.CityId = entity.Community.Region.CityId;
            dto.CityName = entity.Community.Region.City.Name;
            dto.CommunityId = entity.CommunityId;
            dto.CommunityLocation = entity.Community.Location;
            dto.CommunityName = entity.Community.Name;
            dto.CommunityTraffic = entity.Community.Traffic;
            dto.CreateDateTime = entity.CreateDateTIme;
            dto.DecorateStatusId = entity.DecorateStatusId;
            dto.DecorateStatusName = entity.DescorateStatus.Name;
            dto.Descriotion = entity.Description;
            dto.Direction = entity.Direction;
            dto.FloorIndex = entity.FloorIndex;
            //var firstPic = entity.HousePics.FirstOrDefault();
            //if (firstPic != null) dto.FristThumbUrl = firstPic.ThumbUrl;
            dto.Id = entity.Id;
            dto.LookableDateTime = entity.LookableDateTime;
            dto.MonthRent = entity.MonthRent;
            dto.OwnerName = entity.OwnerName;
            dto.OwnerPhoneNum = entity.OwnerPhoneNum;
            dto.RegionId = entity.Community.RegionId;
            dto.RegionName = entity.Community.Region.Name;
            dto.RoomTypeId = entity.RoomTypeId;
            dto.RoomTypeName = entity.RoomType.Name;
            dto.StatusId = entity.StatusId;
            dto.StatusName = entity.Status.Name;
            dto.TotalFloorCount = entity.TotalFloorCount;
            dto.TypeId = entity.TypeId;
            dto.TypeName = entity.Type.Name;
            return dto;
        }
    }
}
