using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Oracle.ManagedDataAccess.Client;

namespace Common
{
    public class OracleHelper
    {
        //private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        private readonly static object _lock = new object();
        public readonly static string connectionString = ConfigurationManager.ConnectionStrings["oracleConStr"].ConnectionString;
        private static OracleConnection _connection = null;
        public static OracleConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    lock (_lock)
                    {
                        if (_connection == null)
                        {
                            _connection = new OracleConnection(connectionString);
                        }
                    }
                }
                _connection.Disposed += new EventHandler(connection_Disposed);
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }

        private static void connection_Disposed(object sender, EventArgs e)
        {
            _connection = null;

        }

        public OracleConnection ConnFactory()
        {
            if (CallContext.GetData("DB") != null)
            {
                return CallContext.GetData("DB") as OracleConnection;
            }
            else
            {
                _connection = new OracleConnection(connectionString);
                CallContext.SetData("DB", _connection);
                return _connection;
            }
        }
        /// <summary>
        /// (Insert、Update、Delete)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns>返回受影响行数</returns>
        public int Operation(string sql, object param = null)
        {

            using (var conn = ConnFactory())
            {
                try
                {
                    conn.Open();
                    return conn.Execute(sql, param);
                }
                catch(Exception e) { }
                finally
                {
                    conn.Close();
                }
            }
            return 0;
        }

        /// <summary>
        /// 批量(Insert、Update、Delete)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="t"></param>
        /// <returns>返回受影响行数</returns>
        public int OperationList<T>(string sql, List<T> t)
        {
            using (var conn = ConnFactory())
            {
                try
                {
                    conn.Open();
                    return conn.Execute(sql, t);
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
            return 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns>返回Model</returns>
        public T QueryT<T>(string sql, object param = null)
        {
            using (var conn = ConnFactory())
            {
                try
                {
                    conn.Open();
                    return conn.Query<T>(sql, param).SingleOrDefault();
                }
                catch
                {

                }
                finally
                {
                    conn.Close();
                }
            }
            return default(T);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param">查询条件</param>
        /// <returns>返回List</returns>
        public List<T> QueryListT<T>(string sql, object param = null)
        {

            using (var conn = Connection)
            {
                try
                {
                    conn.Open();
                    return conn.Query<T>(sql, param).ToList();
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
            return default(List<T>);
        }

        /// <summary>
        /// 数据库事务
        /// </summary>
        /// <param name="sqlList">sql语句集合</param>
        /// <returns></returns>
        public async Task<bool> ExcuteTran(List<string> sqlList)
        {
            using (var conn = Connection)
            {
                conn.Open();
                OracleTransaction transaction = null;
                try
                {
                    if (sqlList != null && sqlList.Count > 0)
                    {
                        await conn.OpenAsync();
                        transaction = conn.BeginTransaction();
                        foreach (var sql in sqlList)
                        {
                            int flag = await conn.ExecuteAsync(sql);
                            if (flag > 0)
                            {
                                continue;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                        transaction.Commit();
                        return true;
                    }
                }
                catch
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    return false;

                }
                finally
                {
                    if (conn != null && conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }


            }
            return true;
        }
    

    //批量操作
    public int OperationList(string sql, object param = null)
        {
            using (var conn = new OracleConnection(connectionString))
            {
                var transaction = conn.BeginTransaction();
                var rowsAffectd = 0;

                // const string sql = @"INSERT INTO dbo.DapperNETDemo(Test) VALUES (@Test)";
                try
                {
                    rowsAffectd = conn.Execute(sql, param, transaction);
                    transaction.Commit();
                    return rowsAffectd;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }


            }


        }
    }
}
