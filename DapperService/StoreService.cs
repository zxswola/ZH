using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using CommonMvc;
using Dapper;
using DTO;
using IService;
using Oracle.ManagedDataAccess.Client;

namespace DapperService
{
    public class StoreService : IStoreService
    {
        public BasicModel GetBasic(string companyId)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "Select StockCk,StockPer From e_interface_basic where CompanyID=:CompanyID AND CANCELSIGN='N'";
                return con.Query<BasicModel>(sql, new {CompanyID = companyId}).SingleOrDefault();
            }
        }

        public List<SrorageModel> GetSrorage(List<string> listStockCk, string itemId)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select StorageID,ItemID,EndQty From e_acc_inv_ava_gl where CANCELSIGN='N' and memo=' ' and StorageID in (";

                foreach (var ck in listStockCk)
                {
                    if (!string.IsNullOrEmpty(ck))
                    {
                        sql += "'" + ck + "',";
                    }
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += ") and itemId=:itemId";
               
               return con.Query<SrorageModel>(sql, new {itemId = itemId}).ToList();
            }
        }
        public List<SrorageModel> GetBbSrorage(List<string> listStockCk, string itemId)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select StorageID,ItemID,EndQty From e_acc_inv_ava_gl where CANCELSIGN='N'  and StorageID in (";

                foreach (var ck in listStockCk)
                {
                    if (!string.IsNullOrEmpty(ck))
                    {
                        sql += "'" + ck + "',";
                    }
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += ") and endqty>0 and itemId=:itemId";

                return con.Query<SrorageModel>(sql, new { itemId = itemId }).ToList();
            }
        }

        public async Task<AjaxResult> SetUpdate(string itemId)
        {
            if (itemId.Length < 5)
            {
                return new AjaxResult { Status = "fail", ErrorMsg = "请输入5位以上符合规范的商品货号" };
            }
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select count(distinct t.itemdesign)  from d_arc_item t where t.itemdesign=:itemId ";
                IEnumerable<int> countList=await con.QueryAsync<int>(sql,new {itemId= itemId });
                var count = countList.First();
                if (count==0)
                {
                    return new AjaxResult{Status="fail",ErrorMsg= "没有找到ItemId为" + itemId + "的商品" };
                   
                }

                string sqlUpdatae = "update e_acc_inv_ava_gl set memo=' ' where itemid like :itemId ";
                await con.ExecuteAsync(sqlUpdatae, new {itemId = itemId + "%"});
                return new AjaxResult { Status = "ok", ErrorMsg = "设置ItemId为" + itemId + "的商品为更新状态" };
            }
        }

        public async Task<AjaxResult> SetNoUpdate(string itemId)
        {
            if (itemId.Length < 5)
            {
                return new AjaxResult { Status = "fail", ErrorMsg = "请输入5位以上符合规范的商品货号" };
            }

            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select count(distinct t.itemdesign)  from d_arc_item t where t.itemdesign=:itemId ";
                IEnumerable<int> countList = await con.QueryAsync<int>(sql, new { itemId = itemId  });
                var count = countList.First();
                if (count == 0)
                {
                    return new AjaxResult { Status = "fail", ErrorMsg = "没有找到商品货号为" + itemId + "的商品" };
                   
                }
                string sqlUpdatae = "update e_acc_inv_ava_gl set memo='N' where itemid like :itemId ";
                await con.ExecuteAsync(sqlUpdatae, new { itemId = itemId + "%" });
                return new AjaxResult { Status = "ok", ErrorMsg = "设置商品货号为" + itemId + "的商品取消更新" };
            }
        }

        public async Task<List<string>> GetItems()
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select distinct b.itemdesign From e_acc_inv_ava_gl a,d_arc_item b where a.itemid=b.itemid and a.CANCELSIGN='N' and a.Memo='N' ";
                IEnumerable<string>  items = await con.QueryAsync<string>(sql);
                return items.ToList();
            }
        }

        public async Task<List<string>> GetPageData(int page, int size)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = @"select itemdesign from (SELECT ROWNUM AS rowno, itemdesign FROM  
                                    (select distinct b.itemdesign From e_acc_inv_ava_gl a,d_arc_item b where a.itemid=b.itemid and a.CANCELSIGN='N' and a.Memo='N' ) r 
                                     where ROWNUM <= :pageindex  * :pageSize) a 
                                      where  a.rowno > (:pageindex-1) * :pageSize";
                IEnumerable<string> items = await con.QueryAsync<string>(sql, new { pageindex = page, pageSize = size });
                return items.ToList();
            }
        }
    }
}
