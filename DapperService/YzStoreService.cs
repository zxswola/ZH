using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Dapper;
using DTO;
using IService;
using Oracle.ManagedDataAccess.Client;

namespace DapperService
{
    public class YzStoreService : IStoreService
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
                string sql = "select StorageID,ItemID,EndQty From e_acc_inv_ava_gl where CANCELSIGN='N' and StorageID in (";

                foreach (var ck in listStockCk)
                {
                    if (!string.IsNullOrEmpty(ck))
                    {
                        sql += "'" + ck + "',";
                    }
                }
                sql = sql.Substring(0, sql.Length - 1);
                sql += ") and endqty>0 and itemId=:itemId";
               return con.Query<SrorageModel>(sql, new {itemId = itemId}).ToList();
            }
        }
    }
}
