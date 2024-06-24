using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public class Loan
{
    DAL objDAL = new DAL();

    string ConStr = ConfigurationManager.AppSettings.Get("conn").ToString();

    #region Loan Release
    public int InsertLoanRelease(tbl_LoanRelease obj_LoanRelease)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[7];

            param[0] = new SqlParameter("@AddedBy", obj_LoanRelease.AddedBy);
            param[1] = new SqlParameter("@ReleaseAmount", obj_LoanRelease.ReleaseAmount);
            param[2] = new SqlParameter("@InstNo", obj_LoanRelease.InstNo);
            param[3] = new SqlParameter("@Zone", obj_LoanRelease.Zone);
            param[4] = new SqlParameter("@Circle", obj_LoanRelease.Circle);
            param[5] = new SqlParameter("@Division", obj_LoanRelease.Division);
            param[6] = new SqlParameter("@ReleaseDate", obj_LoanRelease.ReleaseDate);

            return objDAL.ExecuteProcedure("sp_InsertLoanRelease", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getLoanReleaseBySearch(tbl_LoanRelease objSearch)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Zone", objSearch.Zone);
            param[1] = new SqlParameter("@Circle", objSearch.Circle);
            param[2] = new SqlParameter("@Division", objSearch.Division);


            return objDAL.GetDataByProcedure("sp_GetLoanReleaseBySearch", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getLoanReleaseById(int LoanRelease_Id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@LoanRelease_Id", LoanRelease_Id);

            return objDAL.GetDataByProcedure("sp_GetLoanReleaseById", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int UpdateLoanRelease(tbl_LoanRelease obj_LoanRelease, Int32 LoanRelease_Id)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[8];

            param[0] = new SqlParameter("@AddedBy", obj_LoanRelease.AddedBy);
            param[1] = new SqlParameter("@ReleaseAmount", obj_LoanRelease.ReleaseAmount);
            param[2] = new SqlParameter("@InstNo", obj_LoanRelease.InstNo);
            param[3] = new SqlParameter("@Zone", obj_LoanRelease.Zone);
            param[4] = new SqlParameter("@Circle", obj_LoanRelease.Circle);
            param[5] = new SqlParameter("@Division", obj_LoanRelease.Division);
            param[6] = new SqlParameter("@ReleaseDate", obj_LoanRelease.ReleaseDate);
            param[7] = new SqlParameter("@LoanRelease_Id", LoanRelease_Id);


            return objDAL.ExecuteProcedure("sp_UpdateLoanRelease", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region Loan Deposit

    public decimal GetTotalLoanByProjectId(int ProjectWork_Id)
    {
        decimal totalLoanAmount = 0;
        using (SqlConnection con = new SqlConnection(ConStr))
        {
            using (SqlCommand cmd = new SqlCommand("select ProjectWork_Budget from tbl_ProjectWork WHERE ProjectWork_Id = @ProjectWork_Id", con))
            {
                cmd.Parameters.AddWithValue("@ProjectWork_Id", ProjectWork_Id);
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    totalLoanAmount = (decimal)result;
                }
            }
        }

        return totalLoanAmount * 100000;
    }
    public decimal GetRemainingLoanAmount(int ProjectWork_Id)
    {
        decimal remainingLoanAmount = 0;

        using (SqlConnection con = new SqlConnection(ConStr))
        {
            using (SqlCommand cmd = new SqlCommand("GetRemainingLoanAmount", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectWork_Id", ProjectWork_Id);
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    remainingLoanAmount = (decimal)result;
                }
            }
        }

        return remainingLoanAmount;
    }
    public decimal GetTotalDueAmount(int ProjectWork_Id)
    {
        decimal totalDueAmount = 0;

        using (SqlConnection con = new SqlConnection(ConStr))
        {
            using (SqlCommand cmd = new SqlCommand(@"SELECT SUM(RemainingAmount) AS TotalDueAmount FROM tbl_ProjectWorkGO 
            WHERE ProjectWorkGO_Status = 1
            AND(IsPaid IS NULL OR IsPaid = 0)
            AND DATEADD(YEAR, 3, ProjectWorkGO_GO_Date) <= GETDATE() AND ProjectWorkGO_Work_Id = @ProjectWork_Id", con))
            {
                cmd.Parameters.AddWithValue("@ProjectWork_Id", ProjectWork_Id);
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    totalDueAmount = (decimal)result;
                }
            }
        }

        return totalDueAmount; ;
    }
    public DataTable GetLoanEMIsByProjectId(int ProjectWork_Id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ProjectWork_Id", ProjectWork_Id);


            return objDAL.GetDataByProcedure("GetLoanEMIsByProjectWork_Id", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable GetLoanDepositsByProjectId(int ProjectWork_Id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ProjectWork_Id", ProjectWork_Id);


            return objDAL.GetDataByProcedure("GetLoanDepositssByProjectWork_Id", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int InsertLoanDeposit(int ProjectWork_Id, decimal DepositAmount, DateTime DepositDate, int AddedBy)
    {
        int result;
        try
        {
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@ProjectWork_Id", ProjectWork_Id);
            param[1] = new SqlParameter("@DepositAmount", DepositAmount);
            param[2] = new SqlParameter("@DepositDate", DepositDate);
            param[3] = new SqlParameter("@AddedBy", AddedBy);


            result= objDAL.ExecuteProcedure("sp_InsertLoanDeposit", param);
            UpdateEMISStatus(ProjectWork_Id, DepositAmount);
            return result;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void UpdateEMISStatus(int ProjectWork_Id, decimal DepositAmount)
    {
        using (SqlConnection con = new SqlConnection(ConStr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT ProjectWorkGO_Id, ProjectWorkGO_TotalRelease, RemainingAmount, ISNULL(IsPaid, 0) AS IsPaid, DATEADD(YEAR, 3, ProjectWorkGO_GO_Date) AS DueDate  FROM tbl_ProjectWorkGO WHERE ProjectWorkGO_Work_Id = @ProjectWork_Id AND (IsPaid is null or IsPaid = 0) and ProjectWorkGO_Status=1 ORDER BY ProjectWorkGO_GO_Date", con))
            {
                cmd.Parameters.AddWithValue("@ProjectWork_Id", ProjectWork_Id);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read() && DepositAmount > 0)
                    {
                        int ProjectWorkGO_Id = reader.GetInt32(0);
                        decimal ProjectWorkGO_TotalRelease = reader.GetDecimal(1);
                        decimal RemainingAmount = reader.IsDBNull(2) ? ProjectWorkGO_TotalRelease : reader.GetDecimal(2);
                        bool isPaid = reader.GetBoolean(3);
                        DateTime dueDate = reader.GetDateTime(4);

                        if (DepositAmount >= RemainingAmount)
                        {
                            UpdateEMI(ProjectWorkGO_Id, true, 0);
                            DepositAmount -= RemainingAmount;
                        }
                        else
                        {
                            UpdateEMI(ProjectWorkGO_Id, false, RemainingAmount - DepositAmount);
                            DepositAmount = 0;
                        }
                    }
                }
            }
        }
    }
    private void UpdateEMI(int ProjectWorkGO_Id, bool isPaid, decimal RemainingAmount)
    {
        using (SqlConnection con = new SqlConnection(ConStr))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateEMIStatus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectWorkGO_Id", ProjectWorkGO_Id);
                cmd.Parameters.AddWithValue("@IsPaid", isPaid);
                cmd.Parameters.AddWithValue("@RemainingAmount", RemainingAmount);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    #endregion
}