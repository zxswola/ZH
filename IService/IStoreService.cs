using System.Collections.Generic;
using DTO;

namespace IService
{
    public interface IStoreService
    {
        BasicModel GetBasic(string companyId);
        List<SrorageModel> GetSrorage(List<string> listStockCk, string itemId);

    }
}