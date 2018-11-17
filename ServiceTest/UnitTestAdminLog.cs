using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;

namespace ServiceTest
{
    [TestClass]
    public class UnitTestAdminLog
    {
        [TestMethod]
        public void TestMethod1()
        {
            new AdminLogService().AddNew(1, "测试消息");
        }
    }
}
