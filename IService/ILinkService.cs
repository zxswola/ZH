using System.Collections.Generic;
using DTO;

namespace IService
{
    public interface ILinkService
    {
        LinkDTO[] GetAll();
        void UpdateLinks(string[] ids,string[] links);
    }
}