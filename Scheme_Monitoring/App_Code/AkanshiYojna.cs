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

public class AkanshiYojna
{
    DAL objDAL = new DAL();

    string ConStr = ConfigurationManager.AppSettings.Get("conn").ToString();

    public int InsertCMFellowDetail(tbl_CMFellowDetail obj)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[7];

            param[0] = new SqlParameter("@AddedBy", obj.AddedBy);
            param[1] = new SqlParameter("@CMFellowName", obj.CMFellowName);
            param[2] = new SqlParameter("@EducationalDetail", obj.EducationalDetail);
            param[3] = new SqlParameter("@ProfessionalDetail", obj.ProfessionalDetail);
            param[4] = new SqlParameter("@Experience", obj.Experience);
            param[5] = new SqlParameter("@CMFellowImagePath", obj.CMFellowImagePath);
            param[6] = new SqlParameter("@Division", obj.Division);

            return objDAL.ExecuteProcedure("sp_InsertCMFellowDetail", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getCMFellowDetailBySearch(int StateId, int MandalId, int CircleId, string ULBType, int ULBID, int FY)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@ULBID", ULBID);
            param[1] = new SqlParameter("@StateId", StateId);
            param[2] = new SqlParameter("@CircleId", CircleId);
            param[3] = new SqlParameter("@FYID", FY);
            param[4] = new SqlParameter("@ULBType", ULBType);
            param[5] = new SqlParameter("@MandalId", MandalId);

            return objDAL.GetDataByProcedure("sp_GetCMFellowDetail", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getGetCMFellowDetailById(int Id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Id", Id);

            return objDAL.GetDataByProcedure("sp_GetCMFellowDetailById", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int UpdateCMFellowDetail(tbl_CMFellowDetail obj, int Id)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[8];

            param[0] = new SqlParameter("@AddedBy", obj.AddedBy);
            param[1] = new SqlParameter("@CMFellowName", obj.CMFellowName);
            param[2] = new SqlParameter("@EducationalDetail", obj.EducationalDetail);
            param[3] = new SqlParameter("@ProfessionalDetail", obj.ProfessionalDetail);
            param[4] = new SqlParameter("@Experience", obj.Experience);
            param[5] = new SqlParameter("@CMFellowImagePath", obj.CMFellowImagePath);
            param[6] = new SqlParameter("@Division", obj.Division);
            param[7] = new SqlParameter("@Id", Id);


            return objDAL.ExecuteProcedure("sp_UpdateCMFellowDetail", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}