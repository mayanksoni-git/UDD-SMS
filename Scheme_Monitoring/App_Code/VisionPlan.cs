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
}