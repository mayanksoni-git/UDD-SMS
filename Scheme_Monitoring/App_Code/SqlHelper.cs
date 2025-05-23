using System;
using System.Data;
using System.Data.SqlClient;

public static class SqlHelper
{
    public static string ConnectionString = ""; // Assign this in your startup code or config.

    public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
    }

    public static DataSet ExecuteDataset(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    cmd.Parameters.Clear();
                    return ds;
                }
            }
        }
    }

    public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
    {
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand();

        try
        {
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return rdr;
        }
        catch
        {
            conn.Close();
            throw;
        }
    }

    public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
    }

    private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
    {
        if (conn.State != ConnectionState.Open)
            conn.Open();

        cmd.Connection = conn;
        cmd.CommandText = cmdText;
        cmd.CommandType = cmdType;

        if (trans != null)
            cmd.Transaction = trans;

        if (cmdParams != null)
        {
            foreach (SqlParameter p in cmdParams)
            {
                cmd.Parameters.Add(p);
            }
        }
    }
}
