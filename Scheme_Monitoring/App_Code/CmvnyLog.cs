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

public class CmvnyLog
{
    DAL objDAL = new DAL();

    string ConStr = ConfigurationManager.AppSettings.Get("conn").ToString();

    #region Master Plan Proposal
    public DataTable getCmvnyLogBySearch(int StateId, int MandalId, int CircleId, string ULBType, int ULBID, int FY, int Status, int PersonId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@ULBID", ULBID);
            param[1] = new SqlParameter("@StateId", StateId);
            param[2] = new SqlParameter("@CircleId", CircleId);
            param[3] = new SqlParameter("@FYID", FY);
            param[4] = new SqlParameter("@ULBType", ULBType);
            param[5] = new SqlParameter("@MandalId", MandalId);
            param[6] = new SqlParameter("@Status", Status);
            param[7] = new SqlParameter("@PersonId", PersonId);

            return objDAL.GetDataByProcedure("sp_GetCmvnyLogsBySearch", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion



}