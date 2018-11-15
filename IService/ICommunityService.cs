using DTO;

namespace IService
{
    public interface ICommunityService:IServiceSupport
    {
        CommunityDTO[] GetByRegionId(long regionId);

    }
}