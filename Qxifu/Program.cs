using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxifu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入你的名字");
            string name = Console.ReadLine();
            string connstr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            long rowver = 0;
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select Id,Name,BF, convert(bigint,rowver)as rowver from T_Girls where Id=1";
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            Console.WriteLine("没找到ID=1");
                        }
                        string bf = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("BF")))
                        {
                            bf = reader.GetString(reader.GetOrdinal("BF"));
                        }

                        if (!string.IsNullOrEmpty(bf))
                        {
                            if (bf == name)
                            {
                                Console.WriteLine("早就是我的人了哈哈");
                            }
                            else
                            {
                                Console.WriteLine(bf+"妻不客气");
                            }

                            Console.ReadKey();
                            return;
                        }

                        rowver=reader.GetInt64(reader.GetOrdinal("rowver"));
                    }
                }

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "update T_Girls set BF=@bf where Id=1 and rowver=@rv";
                    cmd.Parameters.Add(new SqlParameter("@bf", name));
                    cmd.Parameters.Add(new SqlParameter("@rv", rowver));
                    int row=cmd.ExecuteNonQuery();
                    if (row <= 0)
                    {
                        Console.WriteLine("被别人抢先了");
                    }
                    else
                    {
                        Console.WriteLine("被我抢到了");
                    }
                }
            }

            Console.ReadKey();
        }

        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}
