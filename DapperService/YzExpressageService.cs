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
using YZOpenSDK;

namespace DapperService
{
    public class YzExpressageService : IExpressageService
    {
        //SEQ_E_INTERFACE_EXPRESS.nextval
        private YzShopService shopService = new YzShopService();
        public bool InsertExpressage(string billId, string orderDtList)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "insert into  E_INTERFACE_EXPRESS (Series,Billid,State,Cancelsign,\"OrderDTList\",Createtime,Updatetime) values (SEQ_E_INTERFACE_EXPRESS.nextval,:Billid,:State,:Cancelsign,:odtlist,:Createtime,:Updatetime)";
                return con.Execute(sql, new {Billid = billId, State = "N", Cancelsign ="N", odtlist = orderDtList, Createtime =DateTime.Now, Updatetime =DateTime.Now}) > 0;
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
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = " select billid from E_INTERFACE_EXPRESS where CANCELSIGN='N' and billid =:billid ";
                return con.Query(sql, new { billid = billId }).Any();
            }
        }

        public List<AllExpress> GetExpress(string token)
        {
            Auth auth = new Token(token);
            YZClient yzClient = new DefaultYZClient(auth);
            Dictionary<string, object> dict = new Dictionary<string, object>();
            var result = yzClient.Invoke("youzan.logistics.express.get", "3.0.0", "POST", dict, null);
            if (!string.IsNullOrEmpty(result))
            {
                GetExpressResponse expressResponse = CommonHelper.DeJson<GetExpressResponse>(result);
                return expressResponse.Response.allExpress;
            }
            else
            {
                return null;
            }

        }

        public bool AddExpress(string token, string tid, string oids, string out_sid, string out_stype, string outer_tid)
        {
            //if (shopService.GetShipments(token, tid))
            //{
            //    return true;
            //}
            Auth auth = new Token(token);
            YZClient yzClient = new DefaultYZClient(auth);
            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                {"tid", tid},
                {"is_no_express", 0},
                {"oids", oids},
                {"out_sid", out_sid},
                {"out_stype", out_stype}
            };
            string result = yzClient.Invoke("youzan.logistics.online.confirm", "3.0.0", "GET", dict, null);
            if (!string.IsNullOrEmpty(result))
            {
                ExpressResponse expressResponse = CommonHelper.DeJson<ExpressResponse>(result);
                if (expressResponse.response != null)
                {
                    if (expressResponse.response.is_success)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void UpdateExpressage(List<string> listBillId)
        {
            throw new NotImplementedException();
        }


    }
}
