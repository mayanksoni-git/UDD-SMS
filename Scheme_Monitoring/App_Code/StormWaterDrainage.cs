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

public class StormWaterDrainage
{
    DAL objDAL = new DAL();

    string ConStr = ConfigurationManager.AppSettings.Get("conn").ToString();

    #region Master Plan
    public DataTable getMasterPlanReportBySearch(int StateId, int MandalId, int CircleId, string ULBType, int ULBID, int FY)
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

            return objDAL.GetDataByProcedure("sp_GetMasterPlan", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getGetMasterPlanById(int MasterPlanId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@MasterPlanId", MasterPlanId);

            return objDAL.GetDataByProcedure("sp_GetMasterPlanById", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int UpdateMasterPlan(tbl_MasterPlan obj, int MasterPlanId)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[7];

            param[0] = new SqlParameter("@AddedBy", obj.AddedBy);
            param[1] = new SqlParameter("@FY", obj.FY);
            param[2] = new SqlParameter("@Division", obj.Division);
            param[3] = new SqlParameter("@Population", obj.Population);
            param[4] = new SqlParameter("@Area", obj.Area);
            param[5] = new SqlParameter("@MasterPlanId", MasterPlanId);
            param[6] = new SqlParameter("@MasterPlanFilePath", obj.MasterPlanFilePath);

            return objDAL.ExecuteProcedure("sp_UpdateMasterPlan", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int InsertMasterPlan(tbl_MasterPlan obj_MasterPlan)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[6];

            param[0] = new SqlParameter("@AddedBy", obj_MasterPlan.AddedBy);
            param[1] = new SqlParameter("@FY", obj_MasterPlan.FY);
            param[2] = new SqlParameter("@Division", obj_MasterPlan.Division);
            param[3] = new SqlParameter("@Population", obj_MasterPlan.Population);
            param[4] = new SqlParameter("@Area", obj_MasterPlan.Area);
            param[5] = new SqlParameter("@MasterPlanFilePath", obj_MasterPlan.MasterPlanFilePath);

            return objDAL.ExecuteProcedure("sp_InsertMasterPlan", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion


    #region Master Plan Proposal
    public DataTable getMasterPlanProposalReportBySearch(int StateId, int MandalId, int CircleId, string ULBType, int ULBID, int FY)
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

            return objDAL.GetDataByProcedure("sp_GetMasterPlanProposal", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getGetMasterPlanProposalById(int MasterPlanProposalId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@MasterPlanProposalId", MasterPlanProposalId);

            return objDAL.GetDataByProcedure("sp_GetMasterProposalPlanById", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int UpdateMasterPlanProposal(tbl_MasterPlanProposal obj, int MasterPlanProposalId)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[9];

            param[0] = new SqlParameter("@AddedBy", obj.AddedBy);
            param[1] = new SqlParameter("@FY", obj.FY);
            param[2] = new SqlParameter("@Division", obj.Division);
            param[3] = new SqlParameter("@ProposalName", obj.ProposalName);
            param[4] = new SqlParameter("@ProposalDetail", obj.ProposalDetail);
            param[5] = new SqlParameter("@MasterPlanProposalId", MasterPlanProposalId);
            param[6] = new SqlParameter("@MasterPlanProposalFilePath", obj.MasterPlanProposalFilePath);
            param[7] = new SqlParameter("@ExpAmt", obj.ExpAmt);
            param[8] = new SqlParameter("@MobileNo", obj.MobileNo);

            return objDAL.ExecuteProcedure("sp_UpdateMasterPlanProposal", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int InsertMasterPlanProposal(tbl_MasterPlanProposal obj)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[8];

            param[0] = new SqlParameter("@AddedBy", obj.AddedBy);
            param[1] = new SqlParameter("@FY", obj.FY);
            param[2] = new SqlParameter("@Division", obj.Division);
            param[3] = new SqlParameter("@ProposalName", obj.ProposalName);
            param[4] = new SqlParameter("@ProposalDetail", obj.ProposalDetail);
            param[5] = new SqlParameter("@MasterPlanProposalFilePath", obj.MasterPlanProposalFilePath);
            param[6] = new SqlParameter("@ExpAmt", obj.ExpAmt);
            param[7] = new SqlParameter("@MobileNo", obj.MobileNo);

            return objDAL.ExecuteProcedure("sp_InsertMasterPlanProposal", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion



}