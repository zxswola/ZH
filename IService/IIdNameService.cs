using DTO;

namespace IService
{
    public interface IIdNameService:IServiceSupport
    {
        IdNameDTO[] GetAll(string name);
    }
}