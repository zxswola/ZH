using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace ZSZFrontWeb
{
    public class MemcacheMgr
    {
        private  MemcachedClient client;
        public static MemcacheMgr Instance { get; }

        static MemcacheMgr()
        {
            Instance = new MemcacheMgr();
        }

        private MemcacheMgr()
        {
            MemcachedClientConfiguration config = new MemcachedClientConfiguration();
            config.Servers.Add(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11211));
            config.Protocol = MemcachedProtocol.Binary;
             client = new MemcachedClient(config);
        }

        public void SetValue(string key,object value,TimeSpan expires)
        {
            if (!value.GetType().IsSerializable)
            {
                throw new ArgumentException("value必须是可序列化的");
            }

            client.Store(StoreMode.Set, key, value, expires);//还可以指定第四个参数指定数据的过期时间。
        }

        public object GetValue(string key)
        {
            return client.Get(key);
        }
    }
}