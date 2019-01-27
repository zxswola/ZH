using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperService;
using DTO;
using Quartz;

namespace ZSZAdminWeb.Jobs
{
    public class BbQtyUpdateJob : IJob
    {
        private BbShopService shopService = new BbShopService(); 
        private StoreService storeService = new StoreService();
        private BasicModel bm = new BasicModel();
        private List<string> listStockCk = new List<string>();
        private List<string> listStockPer = new List<string>();
        private int countUpdate;
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(BbQtyUpdateJob));
        public void Execute(IJobExecutionContext context)
        {
            log.Debug("准备开始ERP同步库存至贝贝店铺" + DateTime.Now);
            try
            {
                QtyUpdate();
            }
            catch (Exception e)
            {
                log.Error("执行BbQtyUpdateJob出错", e);
            }
        }

        public async void QtyUpdate()
        {
            try
            {
                countUpdate = 0;
                var items = await shopService.GetListItem();
                log.Debug("一共有" + items.Count + "件商品需要更新库存");
                bm = storeService.GetBasic("ZY0113");
                //仓库
                listStockCk = bm.StockCk.Split(',').ToList();
                //比例
                listStockPer = bm.StockPer.Split(',').ToList();
                foreach (var item in items)
                {
                    GetAndUpdate(item);
                }
                if (countUpdate == items.Count)
                {
                    log.Debug("ERP同步库存至贝贝店铺完成 一共更新了" + countUpdate + "件商品");
                }
            }
            catch (Exception e)
            {

                log.Error("执行QtyUpdate出错", e);
            }
        }

        public   void GetAndUpdate(BbItemDetail itemDetail)
        {
            countUpdate++;
            //List<BbGood> listGood = new List<BbGood>();
            foreach (var item in itemDetail.Sku)
            {
                string itemNo = string.Empty;
                int index = item.Outer_Id.LastIndexOf("|");
                itemNo = index != -1 ? item.Outer_Id.Substring(index + 1, item.Outer_Id.Length - index - 1): item.Outer_Id;
                //获取erp系统中的库存明细
                var erpStorages = storeService.GetSrorage(listStockCk, itemNo);
                //计算库存量
                int qty = 0;

                if (erpStorages.Count > 0)
                {
                    foreach (var storage in erpStorages)
                    {
                        int index1 = listStockCk.FindIndex(e => e == storage.StorageID);
                        if (index1 >= 0)
                        {
                            double a = Convert.ToDouble(storage.EndQty) * Convert.ToDouble(listStockPer[index1]);
                            if (a >= 1)
                            {
                                qty += Convert.ToInt32(Math.Floor(a));
                            }
                        }

                       
                        //if (qty == item.Num)
                        //{
                        //    log.Debug("erp库存与贝店库存相同不需要同步 :" + goods.Outer_Id +"库存数量"+qty + " =====" + DateTime.Now);
                        //}
                    }
                }

                BbGood goods = new BbGood
                {
                    Iid = itemDetail.Iid,
                    Sku_Id = item.Id.ToString(),
                    Outer_Id = item.Outer_Id,
                    Qty = qty.ToString()
                };
                if (qty != item.Num)
                {
                    shopService.UpdateItemQty(goods);
                    //log.Debug("ERP同步库存至贝店 :"+goods.Outer_Id + "库存数量" + qty+" ====="+ DateTime.Now);
                }

            }
           
            //log.Debug("贝店已经库存同步了:" + countUpdate + "条数据");
        }
    }
}