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

    #region Crematorium Detail
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
}