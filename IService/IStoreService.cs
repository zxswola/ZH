using System.Collections.Generic;
using System.Threading.Tasks;
using CommonMvc;
using DTO;

namespace IService
{
    public interface IStoreService:IServiceSupport
    {
        BasicModel GetBasic(string companyId);
        List<SrorageModel> GetSrorage(List<string> listStockCk, string itemId);
        Task<AjaxResult> SetUpdate(string itemId);
        Task<AjaxResult> SetNoUpdate(string itemId);
        Task<List<string>> GetItems();
        Task<List<string>> GetPageData(int page, int size);

    }
}