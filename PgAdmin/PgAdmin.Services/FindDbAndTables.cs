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
            var findAllDataName = ExecuteQuery(connectionString, cmdType, cmdText, null);
            IList<DbName> dbNameS = new List<DbName>();
            foreach (DataRow datNameS in findAllDataName.Tables[0].Rows)
            {
                var datName = datNameS["datname"].ToString();
                IList<TableName> tNameS = new List<TableName>();
                string connStr = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=" + datName;
                var findTable = ExecuteQuery(connStr, cmdType,
                    @"SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'", null);
                TableName name = new TableName();
                foreach (DataRow tableNameS in findTable.Tables[0].Rows)
                {
                    name.TName = tableNameS["table_name"].ToString();
                    tNameS.Add(name);
                }
                DbName dbName = new DbName();
                dbName.TName = tNameS;
                dbName.DName = datName;
                dbNameS.Add(dbName);
            }
            return dbNameS;
        }

        /// <summary>
        /// 执行查询，返回DataSet
        /// </summary>
        public DataSet ExecuteQuery(string connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                        return ds;
                    }
                }
            }
        }
        /// <summary>
        /// 生成要执行的命令
        /// </summary>
        /// <remarks>参数的格式：冒号+参数名</remarks>
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType,
            string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText.Replace("@", ":").Replace("?", ":").Replace("[", "\"").Replace("]", "\"");

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (NpgsqlParameter parm in cmdParms)
                {
                    parm.ParameterName = parm.ParameterName.Replace("@", ":").Replace("?", ":");

                    cmd.Parameters.Add(parm);
                }
            }
        }
    }
}
