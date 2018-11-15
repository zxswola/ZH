using DTO;

namespace IService
{
    public interface IRegionService:IServiceSupport
    {
        RegionDTO GetById(long id);

        RegionDTO[] GetAll(long cityId);

    }
}