using Npgsql;
using PgAdmin.Services.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgAdmin.Services
{
    public class FindAllDbAndTables
    {
        public IList<DbName> FindDbAndTables(string connectionString, CommandType cmdType, string cmdText,
           params DbParameter[] cmdParms)
        {
            var findAllDataName = postgresHelper.ExecuteQuery(connectionString, cmdType, cmdText, null);
            IList<DbName> dbNameS = new List<DbName>();
            foreach (DataRow datNameS in findAllDataName.Tables[0].Rows)
            {
                var datName = datNameS["datname"].ToString();
                IList<TableName> tNameS = new List<TableName>();
                if (!datName.Contains("template"))
                {
                    string connStr = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=" + datName;
                    var findTable = postgresHelper.ExecuteQuery(connStr, cmdType,
                        @"SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'", null);
                    foreach (DataRow tableNameS in findTable.Tables[0].Rows)
                    {
                        tNameS.Add(
                            new TableName()
                            {
                                TName = tableNameS["table_name"].ToString()
                            });
                    }
                }
                dbNameS.Add(new DbName()
                {
                    TName = tNameS,
                    DName = datName
                });
            }
            return dbNameS;
        }

        ClientQueryService postgresHelper = new ClientQueryService();

    }
}
