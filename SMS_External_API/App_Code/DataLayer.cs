using SMS_External_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace SMS_External_API.App_Code
{
    public class DataLayer
    {
        private DataSet ExecuteSelectQuerywithTransaction(SqlConnection Con, string Sql, SqlTransaction trans)
        {
            DataSet set1 = new DataSet();
            SqlCommand command1 = new SqlCommand(Sql, Con, trans);
            command1.CommandTimeout = 7000;
            SqlDataAdapter adapter1 = new SqlDataAdapter(command1);
            adapter1.Fill(set1);
            return set1;
        }

        private DataSet ExecuteSelectQuery(string Sql)
        {
            string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            DataSet set1 = new DataSet();
            using (SqlConnection con = new SqlConnection(ConStr))
            {
                SqlCommand command1 = new SqlCommand(Sql, con);
                if (command1.Connection.State == ConnectionState.Closed)
                {
                    command1.Connection.Open();
                }
                command1.CommandTimeout = 7000;
                SqlDataAdapter adapter1 = new SqlDataAdapter(command1);

                adapter1.Fill(set1);
            }
            return set1;
        }

        public DataSet get_tbl_Scheme()
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            
            return ds;
        }
        public DataSet get_tbl_FinancialYear()
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            
            return ds;
        }
        public DataSet get_tbl_District()
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            
            return ds;
        }

        public DataSet get_tbl_ULB(int District_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            
            try
            {
                ds = ExecuteSelectQuery(strQuery);
            }
            catch
            {
                ds = null;
            }
            return ds;
        }

        public DataSet get_tbl_ProjectWorkInstallment(int Work_Id, int Project_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            
            try
            {
                ds = ExecuteSelectQuery(strQuery);
            }
            catch
            {
                ds = null;
            }
            return ds;
        }

        public DataSet get_tbl_ProjectWork(SearchCriteria obj_SearchCriteria)
        {
            string strQuery = "";
            
            DataSet ds = new DataSet();
            
            try
            {
                ds = ExecuteSelectQuery(strQuery);
            }
            catch
            {
                ds = null;
            }
            return ds;
        }

        public DataSet get_tbl_ProjectWorkInspection(int Work_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();

            try
            {
                ds = ExecuteSelectQuery(strQuery);
            }
            catch
            {
                ds = null;
            }
            return ds;
        }
    }
}
