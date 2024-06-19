using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
/// <summary>
/// Summary description for DAL
/// </summary>
public class DAL
{
    string ConStr = "";
    public DAL()
    {
        ConStr = ConfigurationManager.AppSettings.Get("conn");
    }
    public void InsertWithParam(string statement, SqlParameter[] param)
    {
        using (SqlConnection conn = new SqlConnection(ConStr))
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(statement, conn); cmd.CommandType = CommandType.Text;
                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                } if (conn.State == ConnectionState.Open)
                    conn.Close();
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insertion Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
    public DataTable FetchData(string statement)
    {
        using (SqlConnection conn = new SqlConnection(ConStr))
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(statement, conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    dt.Load(dataReader);
                }
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return dt;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Fetching Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
    public void UpdateWithParam(string statement, SqlParameter[] param)
    {
        using (SqlConnection conn = new SqlConnection(ConStr))
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(statement, conn); cmd.CommandType = CommandType.Text;

                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                }
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Updating Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }

    public DataTable FetchDataWithParam_sp(string statement, SqlParameter[] param)
    {
        using (SqlConnection conn = new SqlConnection(ConStr))
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(statement, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                }
                if (cmd.Connection.State == ConnectionState.Closed)
                    cmd.Connection.Open();

                cmd.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(dt);

                cmd.Connection.Close();
                return dt;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Fetching Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                conn.Close();
            }
        }
    }
    public DataTable FetchDataWithParam(string statement, SqlParameter[] param)
    {
        using (SqlConnection conn = new SqlConnection(ConStr))
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(statement, conn);
                cmd.CommandType = CommandType.Text;
                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                } if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {

                    dt.Load(dataReader);
                }
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                return dt;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Fetching Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }

    public DataTable GetDataByProcedure(string statement)
    {
        using (SqlConnection conn = new SqlConnection(ConStr))
        {
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand(statement, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    dt.Load(dataReader);
                }
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return dt;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Fetching Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
    public DataTable GetDataByProcedure(string statement, SqlParameter[] param)
    {
        using (SqlConnection conn = new SqlConnection(ConStr))
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(statement, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                }
                cmd.CommandTimeout = 0;
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.SelectCommand.CommandTimeout = 0;
                adap.Fill(dt);
                return dt;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Fetching Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }

    public int ExecuteProcedure(string statement)
    {
        using (SqlConnection conn = new SqlConnection(ConStr))
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(statement, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (cmd.Connection.State == ConnectionState.Closed)
                    cmd.Connection.Open();
                int myvalue = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return myvalue;

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
    public int ExecuteProcedure(string statement, SqlParameter[] param)
    {
        using (SqlConnection conn = new SqlConnection(ConStr))
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(statement, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                }
                if (cmd.Connection.State == ConnectionState.Closed)
                    cmd.Connection.Open();
                int myvalue = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return myvalue;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }

    public int ExecuteScalarProcedure(string procedureName, SqlParameter[] param)
    {
        using (SqlConnection conn = new SqlConnection(ConStr))
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(procedureName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < param.Length; i++)
                {
                    cmd.Parameters.Add(param[i]);
                }
                // Assuming the procedure returns a single integer value
                object result = cmd.ExecuteScalar();
                int value;
                if (result != null && int.TryParse(result.ToString(), out value))
                {
                    return value;
                }
                else
                {
                    throw new Exception("Procedure did not return a valid integer value.");
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Error: ";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}
