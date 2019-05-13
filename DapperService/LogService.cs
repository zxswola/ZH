using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using CommonMvc;
using Dapper;
using DapperService.Entities;
using DTO;
using IService;
using Oracle.ManagedDataAccess.Client;

namespace DapperService
{
    public class LogService : ILogService
    {
        public async Task<AjaxResult> AddLog(string msg)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select t.id from T_LOG t where t.contents=:msg";
                var count = con.Query(sql, new {msg = msg}).Count();
                if (count > 0)
                {
                    return new AjaxResult {Status = "fail", ErrorMsg = "已经存在此异常信息"};
                }
            
                string sqlInsert = "insert into T_LOG(messageid,contents,isdeleted) values('1',:msg,0)";
                await con.ExecuteAsync(sqlInsert, new { msg = msg });
                return new AjaxResult { Status = "ok" };
            }
        }

        public int GetCount()
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select count(*) from T_LOG t ";
                return con.Query<int>(sql).First();
            }
        }

        public async Task<LogDTO[]> GetPageData(int pageSize, int index,string begin,string end)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string strWhere = "1=1";
                if (!string.IsNullOrEmpty(begin))
                {
                   strWhere += " AND updatetime>=to_date('" + (begin+" 00:00:00") + "','yyyy-mm-dd hh24:mi:ss')";
                }

                if (!string.IsNullOrEmpty(end))
                {
                    strWhere += " AND updatetime<=to_date('" + (end+" 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')";
                }
                //and {0}
                string sql =string.Format(@"SELECT log.id,log.messageid,log.contents,log.updatetime,log.isdeleted FROM(SELECT ROWNUM AS rowno, r.*
                FROM(select t.Id,t.messageid,t.contents,t.updatetime ,t.isdeleted,t.rowid from T_LOG t where t.isdeleted=0 and {0}
                 ORDER BY t.UpdateTime desc) r
                where ROWNUM <= :pageindex  * :pageSize
                ) log
                WHERE log.rowno > (:pageindex-1) * :pageSize", strWhere);
                var logs =await con.QueryAsync<LogEntity>(sql, new { pageindex = index, pageSize = pageSize });
                return logs.Select(a => ToDto(a)).ToArray();
            }
        }

        private LogDTO ToDto(LogEntity entity)
        {
            LogDTO dto = new LogDTO
            {
                Id = entity.Id,
                MessageId = entity.MessageId,
                Contents = entity.Contents,
                UpdateTime = entity.UpdateTime
            };
            return dto;
        }
    }
}
