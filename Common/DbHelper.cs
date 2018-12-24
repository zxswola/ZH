using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DbHelper
    {
        public static string ConnectString
        {
            get { return ConfigurationManager.ConnectionStrings["oracleConStr"].ConnectionString; }
        }
    }
}
