using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common;
using Dapper;
using DTO;
using IService;
using Oracle.ManagedDataAccess.Client;

namespace DapperService
{
    public class ExpressageService : IExpressageService
    {
      
        public bool AddExpress(string token, string tid, string oids, string out_sid, string out_stype, string outer_tid)
        {
            throw new NotImplementedException();
        }

        public List<AllExpress> GetExpress(string token)
        {
            throw new NotImplementedException();
        }

    

        public bool InsertExpressage(string billId, string orderDtList)
        {
            try
            {
                using (var con = new OracleConnection(OracleHelper.connectionString))
                {
                    con.Open();
                    string sql = "insert into  E_INTERFACE_EXPRESS (Series,Billid,State,Cancelsign,\"OrderDTList\",Createtime,Updatetime) values (SEQ_E_INTERFACE_EXPRESS.nextval,:Billid,:State,:Cancelsign,:odtlist,:Createtime,:Updatetime)";
                    return con.Execute(sql, new { Billid = billId, State = "N", Cancelsign = "N", odtlist = orderDtList, Createtime = DateTime.Now, Updatetime = DateTime.Now }) > 0;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void InsertExpressageAll(Dictionary<string, string> dics)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();

                var transaction = con.BeginTransaction();
                string sql = "insert into  E_INTERFACE_EXPRESS (Series,Billid,State,Cancelsign,\"OrderDTList\",Createtime,Updatetime) values (SEQ_E_INTERFACE_EXPRESS.nextval,:Billid,:State,:Cancelsign,:odtlist,:Createtime,:Updatetime)";
                try
                {

                    foreach (var dic in dics)
                    {
                        con.Execute(sql, new { Billid = dic.Key, State = "N", Cancelsign = "N", odtlist = dic.Value, Createtime = DateTime.Now, Updatetime = DateTime.Now }, transaction);
                    }

                    transaction.Commit();


                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }


            }
        }

        public bool IsExit(string billId)
        {
            try
            {
                using (var con = new OracleConnection(OracleHelper.connectionString))
                {
                    con.Open();
                    string sql = " select billid from E_INTERFACE_EXPRESS where CANCELSIGN='N' and billid =:billid ";
                    return con.Query(sql, new { billid = billId }).Any();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void UpdateExpressage(List<string> listBillId)
        {
            throw new NotImplementedException();
        }
    }
}
