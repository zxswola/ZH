using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using DapperService;
using DTO;
using Quartz;

namespace ZSZAdminWeb.Jobs
{
    public class YzQuantityUpdateJob:IJob
    {
        private YzShopService shopService = new YzShopService();
        private StoreService storeService = new StoreService();
        private string token = string.Empty;
        private BasicModel bm = new BasicModel();
        private List<string> listStockCk = new List<string>();
        private List<string> listStockPer = new List<string>();
        private int countUpdate;
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(YzQuantityUpdateJob));
        public void Execute(IJobExecutionContext context)
        {
            log.Debug("准备开始ERP同步库存至有赞店铺" + DateTime.Now);
            try
            {
                QuaUpdate();
                // log.Debug("ERP同步库存至有赞店铺结束" + DateTime.Now);
            }
            catch (Exception e)
            {
                log.Error("执行QuantityUpdateJob出错", e);
            }
        }

        public void QuaUpdate()
        {
            countUpdate = 0;
            token = shopService.QueryToken();
            //获取有赞商店里所有的商品
            var items = shopService.GetGoods(token);
            log.Debug("一共有" + items.Count + "件商品需要更新库存");
            bm = storeService.GetBasic("ZY0112");
            //仓库
            listStockCk = bm.StockCk.Split(',').ToList();
            //比例
            listStockPer = bm.StockPer.Split(',').ToList();
            int count = 10;//每次执行条数
            int pageAll = items.Count % count == 0 ? items.Count / count : (items.Count / count) + 1;
            int lastCount = items.Count % count;
            for (int i = 0; i < pageAll; i++)
            {

                //最后一页
                if (i == pageAll - 1)
                {
                    if (lastCount != 0)
                    {
                        ThreadPool.QueueUserWorkItem(UpdateData, items.Skip(i * count).Take(lastCount).ToList());
                    }
                    else
                    {
                        ThreadPool.QueueUserWorkItem(UpdateData, items.Skip(i * count).Take(count).ToList());
                    }
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(UpdateData, items.Skip(i * count).Take(count).ToList());
                }
            }
            if (countUpdate == items.Count)
            {
                log.Debug("ERP同步库存至有赞店铺完成 一共更新了" + countUpdate + "件商品");
            }

            //foreach (var item in items)
            //{

            //    //await GetData(item);
            //    //获取上传数据
            // // var listUpdateData = GetData(item.item_id).Result;
            //    //上传数据


            //}
        }
        //获取数据和更新有赞商店库存
        public void UpdateData(object items)
        {
            try
            {
                foreach (var item in (List<ItemsItem>)items)
                {
                    GetUpdateList(item);
                }

            }
            catch (Exception e)
            {

                log.Error("执行QuantityUpdateJob的UpdateData方法出错", e);
                return;
            }
            Thread.Sleep(1000);
        }

        //获取待上传的数据
        //获取后上传
        public void GetUpdateList(ItemsItem item)
        {
            countUpdate++;
            List<DYGoods> listUpdateData = new List<DYGoods>();
            //获取有赞商店里商品的明细类别 如大小 L M 号的sku编码
            var skuRoot = shopService.GetSku_Root(item.item_id, token);
            var itemDetails = skuRoot.response.item.skus;
            foreach (var itemDetail in itemDetails)
            {
                //编码处理
                string itemNo = string.Empty;
                int index = itemDetail.item_no.LastIndexOf("|");
                itemNo = index != -1 ? itemDetail.item_no.Substring(index + 1, itemDetail.item_no.Length - index - 1) : itemDetail.item_no;
                //获取erp系统中的库存明细
                var erpStorages = storeService.GetSrorage(listStockCk, itemNo);
                //计算库存量
                int qty = 0;
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
                }
               var goods= new DYGoods { ItemID = item.item_id, Qty = qty, ItemSku = itemDetail.sku_id.ToString() };
                if (goods.Qty != itemDetail.quantity)
                {
                    shopService.UpdateSrorage(goods, token);
                }

                //if (goods.Qty == itemDetail.quantity)
                //{
                //    log.Debug("erp库存与有赞库存相同不需要同步 :" + goods.GoodNo + "库存数量" + goods.Qty + DateTime.Now);
                //}
            }

            //if (listUpdateData.Count == 0)
            //{
            //    log.Error("erp系统中没有有赞商城的数据 itemId:" + item.item_id);
            //}
            //else
            //{
            //    //同步数据
            //    foreach (var data in listUpdateData)
            //    {
            //        if (data.Qty != item.quantity)
            //        {
            //            // log.Debug("准备开始同步 :" + CommonHelper.ToJson(data));
            //            shopService.UpdateSrorage(data, token);
            //            // log.Debug("同步成功 :" + CommonHelper.ToJson(data));
            //        }
            //        //else
            //        //{
            //        //    //log.Debug("库存数目与有赞商店相同不需要同步:" + CommonHelper.ToJson(data));
            //        //}
            //    }
            //}
            // log.Debug("有赞已经库存同步了:" + countUpdate+"条数据");
        }
    }
}