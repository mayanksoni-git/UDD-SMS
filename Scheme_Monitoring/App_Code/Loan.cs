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

    #region FundSanctionMaster
    public DataTable getFundSanctionBySearch(int? YearID, int? DistID, int? ULBID, int? SchemeID, int? PersonId, int? ULBTypeID)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@YearID", YearID);
            param[1] = new SqlParameter("@DistID", DistID);
            param[2] = new SqlParameter("@ULBID", ULBID);
            param[3] = new SqlParameter("@SchemeID", SchemeID);
            param[4] = new SqlParameter("@PersonId", PersonId);
            param[5] = new SqlParameter("@ULBTypeID", ULBTypeID);


            return objDAL.GetDataByProcedure("sp_GetFundSanctionBySearch", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getFundSanctionedById(int Id)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Id", Id);

            return objDAL.GetDataByProcedure("sp_GetFundSanctionedById", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int UpdateFundSanctioned(FundSanctionedMaster obj, Int32 Id)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[8];

            param[0] = new SqlParameter("@UpdatedBy", obj.AddedBy);
            param[1] = new SqlParameter("@SessionID", obj.SessionID);
            param[2] = new SqlParameter("@DistID", obj.DistID);
            param[3] = new SqlParameter("@ULBTypeID", obj.ULBTypeID);
            param[4] = new SqlParameter("@ULBID", obj.ULBID);
            param[5] = new SqlParameter("@SchemeID", obj.SchemeID);
            param[6] = new SqlParameter("@AmtInLac", obj.AmtInLac);
            param[7] = new SqlParameter("@Id", Id);

            return objDAL.ExecuteProcedure("sp_UpdateFundSanctioned", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public int InsertFundSanction(FundSanctionedMaster obj)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[7];

            param[0] = new SqlParameter("@AddedBy", obj.AddedBy);
            param[1] = new SqlParameter("@SessionID", obj.SessionID);
            param[2] = new SqlParameter("@DistID", obj.DistID);
            param[3] = new SqlParameter("@ULBTypeID", obj.ULBTypeID);
            param[4] = new SqlParameter("@ULBID", obj.ULBID);
            param[5] = new SqlParameter("@SchemeID", obj.SchemeID);
            param[6] = new SqlParameter("@AmtInLac", obj.AmtInLac);

            return objDAL.ExecuteProcedure("sp_InsertFundSanction", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region WorkProposal
    public DataTable InsertWorkProposal(tbl_WorkProposal obj)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[20];

            param[0] = new SqlParameter("@FY", obj.FY);
            param[1] = new SqlParameter("@Zone", obj.Zone);
            param[2] = new SqlParameter("@Circle", obj.Circle);
            param[3] = new SqlParameter("@Division", obj.Division);
            param[4] = new SqlParameter("@ZoneOfULB", obj.ZoneOfULB);
            param[5] = new SqlParameter("@Ward", obj.Ward);
            param[6] = new SqlParameter("@Scheme", obj.Scheme);
            param[7] = new SqlParameter("@WorkType", obj.WorkType);
            param[8] = new SqlParameter("@ExpectedAmount", obj.ExpectedAmount);
            param[9] = new SqlParameter("@ProposerType", obj.ProposerType);
            param[10] = new SqlParameter("@MPMLAid", obj.MPMLAid);
            param[11] = new SqlParameter("@ProposerName", obj.ProposerName);
            param[12] = new SqlParameter("@Mobile", obj.Mobile);
            param[13] = new SqlParameter("@Designation", obj.Designation);
            param[14] = new SqlParameter("@RecomendationLetter", obj.RecomendationLetter);
            param[15] = new SqlParameter("@AddedBy", obj.AddedBy);
            param[16] = new SqlParameter("@ProposalName", obj.ProposalName);
            param[17] = new SqlParameter("@ProposalDetail", obj.ProposalDetail);
            param[18] = new SqlParameter("@ProposalDate", obj.ProposalDate);
            param[19] = new SqlParameter("@SubSchemeId", obj.SubSchemeId);

            return objDAL.ExecuteProcedureReturnDataTable("sp_InsertWorkProposal", param);
            //return objDAL.ExecuteProcedureReturnDataTable("sp_InsertWorkProposal_Test", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Insert_WorkProposal_ProjectType(WorkProposal_ProjectType obj, SqlTransaction trans, SqlConnection cn)
    {
        string strQuery = "";

        strQuery = " set dateformat dmy; insert into WorkProposal_ProjectType (Proposal_Id, ProjectType_Id, AddedBy, AddedOn,  Status) values ('" + obj.Proposal_Id + "','" + obj.ProjectType_Id + "','" + obj.AddedBy + "', getdate(),'" + obj.Status + "')";
        if (trans == null)
        {
            try
            {
                objDAL.ExecuteSelectQuery(strQuery);
            }
            catch
            {
            }
        }
        else
        {
            objDAL.ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
        }
    }

    public DataTable getWorkProposalBySearch(tbl_WorkProposal objSearch)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];

            param[0] = new SqlParameter("@Zone", objSearch.Zone);
            param[1] = new SqlParameter("@Circle", objSearch.Circle);
            param[2] = new SqlParameter("@Division", objSearch.Division);
            param[3] = new SqlParameter("@FY", objSearch.FY);
            param[4] = new SqlParameter("@Scheme", objSearch.Scheme);
            param[5] = new SqlParameter("@ProposalStatus", objSearch.ProposalStatus);

            return objDAL.GetDataByProcedure("sp_SelectWorkProposalsBySearch", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getWorkProposalSectionWise(tbl_WorkProposal objSearch)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[7];

            param[0] = new SqlParameter("@Zone", objSearch.Zone);
            param[1] = new SqlParameter("@Circle", objSearch.Circle);
            param[2] = new SqlParameter("@Division", objSearch.Division);
            param[3] = new SqlParameter("@FY", objSearch.FY);
            param[4] = new SqlParameter("@Scheme", objSearch.Scheme);
            param[5] = new SqlParameter("@Section", objSearch.Section);
            param[6] = new SqlParameter("@ProposerType", objSearch.ProposerType);

            return objDAL.GetDataByProcedure("sp_WorkProposalsReport2", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getWorkProposalSummary(tbl_WorkProposal objSearch, int Mandal, string ULBType)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[9];

            param[0] = new SqlParameter("@Zone", objSearch.Zone);
            param[1] = new SqlParameter("@Circle", objSearch.Circle);
            param[2] = new SqlParameter("@Division", objSearch.Division);
            param[3] = new SqlParameter("@FY", objSearch.FY);
            param[4] = new SqlParameter("@Scheme", objSearch.Scheme);
            param[5] = new SqlParameter("@Section", objSearch.Section);
            param[6] = new SqlParameter("@ProposerType", objSearch.ProposerType);
            param[7] = new SqlParameter("@Mandal", Mandal);
            param[8] = new SqlParameter("@ULBType", ULBType);

            return objDAL.GetDataByProcedure("SpFinancialYearWisePropasalReport_Test", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getWorkTypeByProposal(int WorkProposalId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@WorkProposalId", WorkProposalId);

            return objDAL.GetDataByProcedure("sp_GetWorkTypeByProposal", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getWorkPlanWiseForChartBySearch(tbl_WorkProposal objSearch)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];

            param[0] = new SqlParameter("@Zone", objSearch.Zone);
            param[1] = new SqlParameter("@Circle", objSearch.Circle);
            param[2] = new SqlParameter("@Division", objSearch.Division);
            param[3] = new SqlParameter("@FY", objSearch.FY);
            param[4] = new SqlParameter("@Scheme", objSearch.Scheme);
            param[5] = new SqlParameter("@ProposalStatus", objSearch.ProposalStatus);

            return objDAL.GetDataByProcedure("sp_GetWorkPlanWiseForChart", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getWorkProposalById(int WorkProposalId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@WorkProposalId", WorkProposalId);

            return objDAL.GetDataByProcedure("sp_SelectWorkProposalById", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getWorkProposalByIdForAction(int WorkProposalId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@WorkProposalId", WorkProposalId);

            return objDAL.GetDataByProcedure("sp_SelectWorkProposalsBySearch2", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int UpdateWorkProposal(tbl_WorkProposal obj, Int32 WorkProposalId)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[21];

            param[0] = new SqlParameter("@FY", obj.FY);
            param[1] = new SqlParameter("@Zone", obj.Zone);
            param[2] = new SqlParameter("@Circle", obj.Circle);
            param[3] = new SqlParameter("@Division", obj.Division);
            param[4] = new SqlParameter("@ZoneOfULB", obj.ZoneOfULB);
            param[5] = new SqlParameter("@Ward", obj.Ward);
            param[6] = new SqlParameter("@Scheme", obj.Scheme);
            param[7] = new SqlParameter("@WorkType", obj.WorkType);
            param[8] = new SqlParameter("@ExpectedAmount", obj.ExpectedAmount);
            param[9] = new SqlParameter("@ProposerType", obj.ProposerType);
            param[10] = new SqlParameter("@MPMLAid", obj.MPMLAid);
            param[11] = new SqlParameter("@ProposerName", obj.ProposerName);
            param[12] = new SqlParameter("@Mobile", obj.Mobile);
            param[13] = new SqlParameter("@Designation", obj.Designation);
            param[14] = new SqlParameter("@RecomendationLetter", obj.RecomendationLetter);
            param[15] = new SqlParameter("@AddedBy", obj.AddedBy);
            param[16] = new SqlParameter("@WorkProposalId", WorkProposalId);
            param[17] = new SqlParameter("@ProposalName", obj.ProposalName);
            param[18] = new SqlParameter("@ProposalDetail", obj.ProposalDetail);
            param[19] = new SqlParameter("@ProposalDate", obj.ProposalDate);
            param[20] = new SqlParameter("@SubSchemeId", obj.SubSchemeId);

            return objDAL.ExecuteProcedure("sp_UpdateWorkProposal", param);
            //return objDAL.ExecuteProcedure("sp_UpdateWorkProposal_Test", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int ActionOnWorkProposal(string Remark, int Status, DateTime ActionDate,  int WorkProposalId, int ActionTakenBy)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@Remark", Remark);
            param[1] = new SqlParameter("@Status", Status);
            param[2] = new SqlParameter("@ActionDate", ActionDate);
            param[3] = new SqlParameter("@WorkProposalId", WorkProposalId);
            param[4] = new SqlParameter("@ActionBy", ActionTakenBy);

            return objDAL.ExecuteProcedure("sp_UpdateWorkProposalForAction", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string GetWorkProposalCode(int WorkProposalId)
    {
        string sqlQuery = "select top 1 ProposalCode from tbl_WorkProposal where WorkProposalId=@WorkProposalId";
        SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@WorkProposalId", WorkProposalId)
        };

        try
        {
            string ProposalCode = objDAL.ExecuteSqlReturnString(sqlQuery, parameters);
            return ProposalCode;
        }
        catch (Exception ex)
        {
            return "Error executing SQL statement: "+ ex.Message;
        }
    }

    public void DeleteWorkProposalProjectTypes(int workProposalId, SqlTransaction trans, SqlConnection connection)
    {
        using (SqlCommand command = new SqlCommand("DELETE FROM WorkProposal_ProjectType WHERE Proposal_Id = @Proposal_Id", connection, trans))
        {
            command.Parameters.AddWithValue("@Proposal_Id", workProposalId);
            command.ExecuteNonQuery();
        }
    }
    #endregion

    #region Decision Making Page
    public DataTable getFYWiseData(int WorkProposalId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@WorkProposalId", WorkProposalId);

            return objDAL.GetDataByProcedure("spGetFYWiseData", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getMPWiseData(int WorkProposalId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@WorkProposalId", WorkProposalId);

            return objDAL.GetDataByProcedure("SpMPviseSchemeAmountReport", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getMLAWiseData(int WorkProposalId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@WorkProposalId", WorkProposalId);

            return objDAL.GetDataByProcedure("SpMLAviseSchemeAmountReport", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getDivisionWiseData(int WorkProposalId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@WorkProposalId", WorkProposalId);

            return objDAL.GetDataByProcedure("SpDivisionWiseSchemeAmountReport", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getDistrictWiseData(int WorkProposalId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@WorkProposalId", WorkProposalId);

            return objDAL.GetDataByProcedure("SpDistrictWiseSchemeAmountReport", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getWorkPlanWiseData(int WorkProposalId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@WorkProposalId", WorkProposalId);

            return objDAL.GetDataByProcedure("SpFinancialYearWisePropasalReport", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getRecommendationWiseData(int WorkProposalId)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@WorkProposalId", WorkProposalId);

            return objDAL.GetDataByProcedure("SpRecommendationWisePropasalReport", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable getYearWiseData(int? WorkProposalId,int? parliamentID,string type)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@WorkProposalId", WorkProposalId);
            param[1] = new SqlParameter("@parliament", parliamentID); 
            param[2] = new SqlParameter("@action", type); 

            return objDAL.GetDataByProcedure("SpYealyFundReportOfMP", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getULBWiseData(int? WorkProposalId, int? parliamentID,string type)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@WorkProposalId", WorkProposalId);
            param[1] = new SqlParameter("@parliamentId", parliamentID);
            param[2] = new SqlParameter("@action", type);

            return objDAL.GetDataByProcedure("SpULBWiseData", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getAmountWiseData(int? WorkProposalId, int? parliamentID, string type)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@WorkProposalId", WorkProposalId);
            param[1] = new SqlParameter("@parliamentId", parliamentID);
            param[2] = new SqlParameter("@action", type);

            return objDAL.GetDataByProcedure("SpAMountWiseFundReportOfMPMLA", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    #endregion

    #region JeetApiData
    public DataTable get_JeetApi_Data(tbl_JeetApiData objSearch)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@District", objSearch.DISTRICT_NAME);
            param[1] = new SqlParameter("@FromDate", Convert.ToDateTime(objSearch.FromDate));
            param[2] = new SqlParameter("@ToDate", Convert.ToDateTime(objSearch.ToDate));
            param[3] = new SqlParameter("@LGTDCode", Convert.ToInt32(objSearch.LG_DT_Code));
            
            return objDAL.GetDataByProcedure("SpGetJeetReport", param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion
}