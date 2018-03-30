using System;
using Npgsql;
using System.Data;
using System.Data.Common;
using PgAdmin.Services.Model;

namespace PgAdmin.Services
{
    public class ClientQueryService
    {
        private const string Conn = "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=";
        public GetInfoByIdModel GetInfoById(string Id, string DataName, string TableName, CommandType cmdType, 
            params DbParameter[] cmdParms)
        {
            string cmdText = @"select id,data,mt_last_modified from "+TableName+" where id ='"+Id+"'";
            var result = ExecuteQuery(Conn+DataName, cmdType, cmdText, cmdParms);
            GetInfoByIdModel getInfoByIdModel = new GetInfoByIdModel();
            if (result.Tables[0].Rows.Count != 0)
            {
                getInfoByIdModel.Id = result.Tables[0].Rows[0]["id"].ToString();
                getInfoByIdModel.Data = result.Tables[0].Rows[0]["data"].ToString();
                getInfoByIdModel.MtLastModified = (DateTime)(result.Tables[0].Rows[0]["mt_last_modified"]);
            }          
            return getInfoByIdModel;
        }

   
        public DbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                NpgsqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
      

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
                        try
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds, "ds");
                            cmd.Parameters.Clear();
                            return ds;
                        }
                        catch (Exception e)
                        { return null; }
                    }
                }
            }
        }
      

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
