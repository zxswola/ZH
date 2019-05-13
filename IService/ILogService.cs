using System.Collections.Generic;
using System.Threading.Tasks;
using CommonMvc;
using DTO;

namespace IService
{
    public interface ILogService : IServiceSupport
    {
         Task<LogDTO[]> GetPageData(int pageSize, int index,string begin,string end);
        Task<AjaxResult> AddLog(string msg);
        int GetCount();
    }
}