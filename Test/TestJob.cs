using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Quartz;

namespace Test
{
    public class TestJob:IJob
    {
        public void Execute(IJobExecutionContext context)
        {

            try
            {
                SqlConnection conn = new SqlConnection();
                conn.Open();
                Console.WriteLine("任务执行了" + DateTime.Now);
            }
            catch (Exception e)
            {

                ILog log = LogManager.GetLogger(typeof(TestJob));
                log.Error("执行出错" + e);
            }
        }
    }
}
