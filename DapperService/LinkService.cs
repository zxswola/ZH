using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Dapper;
using DapperService.Entities;
using DTO;
using IService;
using Oracle.ManagedDataAccess.Client;

namespace DapperService
{
    public class LinkService : ILinkService
    {
        public LinkDTO[] GetAll()
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sql = "select * from T_Links  order by id";
                var roles = con.Query<LinkDTO>(sql);
                return roles.ToArray();
            }
        }

        public void UpdateLinks(string[] ids, string[] links)
        {
            using (var con = new OracleConnection(OracleHelper.connectionString))
            {
                con.Open();
                string sqlUpdate = "update T_Links t set t.link=:link where t.id=:id";
                var transaction = con.BeginTransaction();
                try
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        con.Execute(sqlUpdate, new { link = links[i], id = ids[i] }, transaction);
                    }
                    transaction.Commit();
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
