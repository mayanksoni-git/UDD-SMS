using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for ULBFund
/// </summary>
public class ULBFund
{
    DAL objDAL = new DAL();

    string ConStr = ConfigurationManager.AppSettings.Get("conn").ToString();

    public DataTable GetULBFundAction(string actions, int? ULBID, int? TaskId,int?  stateid, int? circleId,int? FY,decimal SFC, decimal CFC, decimal totalTax, int? personId )
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@action", actions);
            param[1] = new SqlParameter("@ULBId", ULBID);
            param[2] = new SqlParameter("@taskId", TaskId);
            param[3] = new SqlParameter("@statetId", stateid);

            param[4] = new SqlParameter("@DistrictId", circleId);
            param[5] = new SqlParameter("@FYId", FY);
            param[6] = new SqlParameter("@SFCFund", SFC);
            param[7] = new SqlParameter("@CFCFund", CFC);
            param[8] = new SqlParameter("@TotalTaxtCollection", totalTax);
            param[9] = new SqlParameter("@PersonBy", personId);
           

            return objDAL.GetDataByProcedure("ULBFunds", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

   
}