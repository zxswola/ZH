using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DapperService
{
    public class DbFactory
    {
        private static  string connectionString = ConfigurationManager.ConnectionStrings["oracleConStr"].ConnectionString;

        public static OracleConnection GetConnection()
        {
            OracleConnection db = CallContext.GetData("DbOracle") as OracleConnection;

            if (db == null)
            {
                db=new OracleConnection(connectionString);
                CallContext.SetData("DbOracle", db);
            }
            return db;
        }
    }
}
