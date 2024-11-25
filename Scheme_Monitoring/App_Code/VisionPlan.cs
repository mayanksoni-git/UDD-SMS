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
using System.Collections.Generic;

public class VisionPlan
{
    DAL objDAL = new DAL();

    string ConStr = ConfigurationManager.AppSettings.Get("conn").ToString();

    public DataTable getTotalProjectFinancialYearWise()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@Action", "TotalProjectFinancialYearWise");
            return objDAL.GetDataByProcedure("sp_VisionPlanDashboard", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message); 
        }
    }

    public DataTable getTotalProjectsUlbWiseByFYID(int FYID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@Action", "TotalProjectsUlbWiseByFYID");
            param[1] = new SqlParameter("@FYID", FYID);
            return objDAL.GetDataByProcedure("sp_VisionPlanDashboard", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getProjectByFYIDandULB(int FYID, int ULBID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@Action", "ProjectByFYIDandULB");
            param[1] = new SqlParameter("@FYID", FYID);
            param[2] = new SqlParameter("@ULBID", ULBID);
            return objDAL.GetDataByProcedure("sp_VisionPlanDashboard", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getTotalULBFinancialYearWise()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@Action", "TotalULBReportedFinnacialYearWise");
            return objDAL.GetDataByProcedure("sp_VisionPlanDashboard", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getULBByFYID(int FYID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@Action", "ULBNamesByFYID");
            param[1] = new SqlParameter("@FYID", FYID);
            return objDAL.GetDataByProcedure("sp_VisionPlanDashboard", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getVisionPlanDocumentFYWise()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@Action", "VisionPlanDocumentFYWise");
            return objDAL.GetDataByProcedure("sp_VisionPlanDashboard", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getTotalDocumentsULBWiseByFYID(int FYID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@Action", "SelectForDashboard");
            param[1] = new SqlParameter("@FYID", FYID);
            return objDAL.GetDataByProcedure("sp_VisionPlanDashboard", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getGetULBPendingToUploadFYWise()
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@Action", "GetULBPendingToUploadFYWise");
            return objDAL.GetDataByProcedure("sp_VisionPlanDashboard", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable getUlBPendingToUpByFYID(int FYID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@Action", "UlBPendingToUpByFYID");
            param[1] = new SqlParameter("@FYID", FYID);
            return objDAL.GetDataByProcedure("sp_VisionPlanDashboard", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}