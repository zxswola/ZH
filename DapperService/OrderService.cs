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
    public class OrderService : IOrderService
    {
       
        public string QueryBarcode(string shopItemId)
        {
            try
            {
                using (var con = new OracleConnection(OracleHelper.connectionString))
                {
                    con.Open();
                    string sql = "Select barcode FROM D_ARC_ITEM DAI where shop_itemId=:shop_itemId ";
                    return con.Query<string>(sql, new { shop_itemId = shopItemId }).SingleOrDefault();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        //获取有赞平台等待发货的订单ID


        public List<string> QueryDTOrder(string billId)
        {
            try
            {
                using (var con = new OracleConnection(OracleHelper.connectionString))
                {
                    con.Open();
                    string sql = "select ITEMID  FROM E_BL_ORDER_DT WHERE BILLID =:billId ";
                    return con.Query<string>(sql, new { billId = billId }).ToList();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        //TODO 改回 t1.CANCELSIGN = 'N'
        public List<OrderModel> QueryOrder(string sellCompanyId, string buyCompanyId, ref List<string> sourceIds)
        {
            if (sourceIds == null) throw new ArgumentNullException(nameof(sourceIds));
            try
            {
                using (var con = new OracleConnection(OracleHelper.connectionString))
                {
                    con.Open();
                    string sql = " SELECT T1.BILLID,T1.SOURCEORDERID,T1.EXPRESSWAY,t1.ISSPLIT,t1.WAYBILLID,t2.\"OrderDTList\",T3.EXPRESSNAME From E_BL_ORDER_HD t1 ";
                    sql += "INNER JOIN E_INTERFACE_EXPRESS t2 on t1.BILLID = t2.BILLID ";
                    sql += "INNER JOIN D_ARC_EXPRESS T3 ON t1.EXPRESSWAY = T3.EXPRESSCODE ";
                    sql += "where t1.EXPRESSWAY != ' ' AND t1.CANCELSIGN = 'N' AND t1.STATUS = '017' and t2.STATE = 'N' and t2.CANCELSIGN = 'N' ";
                    sql += "and t1.SellCompanyID=:SellCompanyID and t1.BuyCompanyID=:BuyCompanyID";
                    string sql2 = @"SELECT T1.SOURCEORDERID From E_BL_ORDER_HD t1
                INNER JOIN E_INTERFACE_EXPRESS t2 on t1.BILLID = t2.BILLID
                INNER JOIN D_ARC_EXPRESS T3 ON t1.EXPRESSWAY = T3.EXPRESSCODE
                where t1.EXPRESSWAY != ' ' AND t1.CANCELSIGN = 'N' AND t1.STATUS = '017' and t2.STATE = 'N' and t2.CANCELSIGN = 'N' 
                and t1.SellCompanyID=:SellCompanyID and t1.BuyCompanyID=:BuyCompanyID 
                Group by t1.SOURCEORDERID";
                    sourceIds = con.Query<string>(sql2, new { SellCompanyID = sellCompanyId, BuyCompanyID = buyCompanyId }).ToList();
                    return con.Query<OrderModel>(sql, new { SellCompanyID = sellCompanyId, BuyCompanyID = buyCompanyId }).ToList();

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool QueryOrderIsExit(string sourceOrderId)
        {
            try
            {
                using (var con = new OracleConnection(OracleHelper.connectionString))
                {
                    con.Open();
                    string sql = "select t1.BILLID  from e_bl_order_hd t1 inner join E_INTERFACE_EXPRESS t2 on t1.BILLID=t2.BILLID where t1.sourceorderid =:sourceorderid and t1.cancelsign='N'";
                    return con.Query(sql, new { sourceorderid = sourceOrderId }).Any();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public  bool UpdateOrderSourceTypeID(List<string> billIds,string sourceType)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                var transaction = con.BeginTransaction();
                string sql = " Update e_bl_order_hd set SourceTypeID=:sourceType WHERE BILLID=:billid ";
                try
                {
                    foreach (var billid in billIds)
                    {
                         con.Execute(sql, new { sourceType= sourceType, billid = billid}, transaction);
                    }

                    transaction.Commit();
                    return true;
                }
                catch(Exception)
                {
                    transaction.Rollback();
                    return false;
                   
                }

                
            }
        }
    }
}
