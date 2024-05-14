using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.NetworkInformation;
using System.Web;
using ePayment_API.Models;

namespace ePayment_API.Repos
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

        public void Insert_tbl_SMS(tbl_SMS obj_tbl_SMS)
        {
            DataSet set1 = new DataSet();
            SqlCommand command1 = null;
            SqlDataAdapter adapter1 = null;
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_SMS (SMS_Mobile_No, SMS_Content, SMS_Response, SMS_AddedOn) values ('" + obj_tbl_SMS.SMS_Mobile_No + "', '" + obj_tbl_SMS.SMS_Content + "','" + obj_tbl_SMS.SMS_Response + "', getdate());Select @@Identity";
            try
            {
                string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    command1 = new SqlCommand(strQuery, con);
                    if (command1.Connection.State == ConnectionState.Closed)
                    {
                        command1.Connection.Open();
                    }
                    command1.CommandTimeout = 7000;
                    adapter1 = new SqlDataAdapter(command1);
                    adapter1.Fill(set1);
                }
            }
            catch
            {

            }
        }

        public DataSet get_M_Jurisdiction(int M_Level_Id, int Parent_Jurisdiction_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = " set dateformat dmy; select M_Jurisdiction.M_Jurisdiction_Id, M_Jurisdiction.M_Level_Id, M_Jurisdiction.Jurisdiction_Name_Eng, Jurisdiction_Name_With_Code = M_Jurisdiction.Jurisdiction_Name_Eng + ' - ' + isnull(M_Jurisdiction.Jurisdiction_Code, ''), M_Jurisdiction.Parent_Jurisdiction_Id, M_Jurisdiction.Jurisdiction_Code, Jurisdiction_Name_Eng_With_Parent = (case when isnull(Parent_M_Jurisdiction.Jurisdiction_Name_Eng, '') = '' then '' else isnull(Parent_M_Jurisdiction.Jurisdiction_Name_Eng, '-NA-') + '- ' end) + M_Jurisdiction.Jurisdiction_Name_Eng, M_Jurisdiction.Created_By, M_Jurisdiction.Created_Date, M_Jurisdiction.Is_Active, IsNULL(Parent_M_Jurisdiction.Jurisdiction_Name_Eng,'-NA-') Parent_Jurisdiction_Name_Eng, isnull(tbl_PersonDetail.Person_Name, 'Backend Entry') CreatedBy, M_Jurisdiction.Created_Date,tbl_PersonDetail1.Person_Name as ModifyBy,M_Jurisdiction.M_Jurisdiction_ModifiedOn from M_Jurisdiction left join M_Jurisdiction Parent_M_Jurisdiction on Parent_M_Jurisdiction.M_Jurisdiction_Id = M_Jurisdiction.Parent_Jurisdiction_Id left join tbl_PersonDetail on Person_Id = M_Jurisdiction.Created_By left join tbl_PersonDetail as tbl_PersonDetail1 on tbl_PersonDetail1.Person_Id = M_Jurisdiction.M_Jurisdiction_ModifiedBy where M_Jurisdiction.Is_Active = 1 ";
            if (M_Level_Id != 0)
            {
                strQuery += " and M_Jurisdiction.M_Level_Id = '" + M_Level_Id + "'";
            }
            if (Parent_Jurisdiction_Id != 0)
            {
                strQuery += " and M_Jurisdiction.Parent_Jurisdiction_Id = '" + Parent_Jurisdiction_Id + "'";
            }
            strQuery += " order by M_Jurisdiction.M_Level_Id, Parent_M_Jurisdiction.Jurisdiction_Name_Eng, M_Jurisdiction.Jurisdiction_Name_Eng";
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

        public DataSet getLoginDetails(string Mobile_No)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; 
                        select 
                            tbl_PersonDetail.Person_Id,
	                        tbl_PersonDetail.Person_Name,
                            tbl_PersonDetail.Person_FName,
	                        Person_Name_Full = isnull(tbl_PersonDetail.Person_Name, '') + ', ' + ', Mob: ' + isnull(tbl_PersonDetail.Person_Mobile1, ''),
	                        tbl_PersonDetail.Person_Mobile1,
	                        tbl_PersonDetail.Person_Mobile2,
	                        tbl_PersonJuridiction.M_Level_Id,
	                        tbl_PersonJuridiction.M_Jurisdiction_Id,
	                        tbl_PersonJuridiction.PersonJuridiction_UserTypeId,
	                        Level_Name,
	                        UserType_Desc_E,
                            CONVERT(char(10),getdate(),103) ServerDate, 
                            Zone_Id, 
                            Circle_Id, 
	                        Division_Id, 
	                        Zone_Name, 
	                        Circle_Name, 
	                        Division_Name, 
                            Department_Name, 
                            Designation_DesignationName
                        from tbl_PersonDetail
                        join tbl_PersonJuridiction on Person_Id = PersonJuridiction_PersonId
                        left join tbl_Department on Department_Id = PersonJuridiction_DepartmentId
                        left join tbl_Designation on Designation_Id = PersonJuridiction_DesignationId
                        left join M_Level on M_Level.M_Level_Id = tbl_PersonJuridiction.M_Level_Id
                        left join tbl_UserType on UserType_Id = PersonJuridiction_UserTypeId
                        left join tbl_UserLogin on Login_PersonId = Person_Id
                        left join tbl_Division on Division_Id = tbl_PersonJuridiction.PersonJuridiction_DivisionId
                        left join tbl_Circle on Circle_Id = tbl_PersonJuridiction.PersonJuridiction_CircleId
                        left join tbl_Zone on Zone_Id = tbl_PersonJuridiction.PersonJuridiction_ZoneId
                        where tbl_PersonDetail.Person_Status = 1 and PersonJuridiction_Status = 1 and (tbl_PersonDetail.Person_Mobile1 = '" + Mobile_No + "' or tbl_PersonDetail.Person_Mobile2 = '" + Mobile_No + "'); ";

            try
            {
                ds = ExecuteSelectQuery(strQuery);
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet getLoginDetails(string UserName, string Password)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; 
                        select 
                            tbl_PersonDetail.Person_Id,
	                        tbl_PersonDetail.Person_Name,
                            tbl_PersonDetail.Person_FName,
	                        Person_Name_Full = isnull(tbl_PersonDetail.Person_Name, '') + ', ' + ', Mob: ' + isnull(tbl_PersonDetail.Person_Mobile1, ''),
	                        tbl_PersonDetail.Person_Mobile1,
	                        tbl_PersonDetail.Person_Mobile2,
	                        tbl_PersonJuridiction.M_Level_Id,
	                        tbl_PersonJuridiction.M_Jurisdiction_Id,
	                        tbl_PersonJuridiction.PersonJuridiction_UserTypeId,
	                        Level_Name,
	                        UserType_Desc_E,
                            CONVERT(char(10),getdate(),103) ServerDate, 
                            Zone_Id, 
                            Circle_Id, 
	                        Division_Id, 
	                        Zone_Name, 
	                        Circle_Name, 
	                        Division_Name, 
                            Department_Name, 
                            Designation_DesignationName
                        from tbl_PersonDetail
                        join tbl_PersonJuridiction on Person_Id = PersonJuridiction_PersonId
                        left join tbl_Department on Department_Id = PersonJuridiction_DepartmentId
                        left join tbl_Designation on Designation_Id = PersonJuridiction_DesignationId
                        left join M_Level on M_Level.M_Level_Id = tbl_PersonJuridiction.M_Level_Id
                        left join tbl_UserType on UserType_Id = PersonJuridiction_UserTypeId
                        left join tbl_UserLogin on Login_PersonId = Person_Id
                        left join tbl_Division on Division_Id = tbl_PersonJuridiction.PersonJuridiction_DivisionId
                        left join tbl_Circle on Circle_Id = tbl_PersonJuridiction.PersonJuridiction_CircleId
                        left join tbl_Zone on Zone_Id = tbl_PersonJuridiction.PersonJuridiction_ZoneId
                        where tbl_PersonDetail.Person_Status = 1 and PersonJuridiction_Status = 1 and (Login_UserName = '" + UserName + "' or tbl_PersonDetail.Person_Mobile1 = '" + UserName + "' or tbl_PersonDetail.Person_Mobile2 = '" + UserName + "') and Login_password = '" + Password + "'; ";

            try
            {
                ds = ExecuteSelectQuery(strQuery);
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet get_tbl_Project()
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @" set dateformat dmy; 
                    select 
                        Project_Id, 
                        Project_Name, 
                        Project_Time, 
                        Project_Budget, 
                        Project_AddedOn, 
                        Project_AddedBy, 
                        Project_Status, 
                        isnull(tbl_PersonDetail.Person_Name, 'Backend Entry') CreatedBy, 
                        Created_Date = Project_AddedOn, 
                        tbl_PersonDetail1.Person_Name as ModifyBy, 
                        Modify_Date = Project_ModifiedOn, 
                        Project_GO_Path, 
                        Project_AllocationType
                      from tbl_Project
                      left join tbl_PersonDetail on Person_Id = Project_AddedBy
                      left join tbl_PersonDetail as tbl_PersonDetail1 on tbl_PersonDetail1.Person_Id = Project_ModifiedBy
                      where Project_Status = 1 order by Project_Name";
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
        public DataSet get_tbl_ProjectDPR_Upload(int Person_Id, string _Mode, int ULB_Id, int Scheme_Id, string _Status)
        {
            string sqlJoin = "";
            string strQuery = "";
            DataSet ds = new DataSet();

            strQuery = @"set dateformat dmy; 
                        select 
	                        ProjectDPR_Id,
	                        ProjectDPR_Project_Id,
	                        ProjectDPR_District_Jurisdiction_Id,
	                        ProjectDPR_NP_JurisdictionId,
	                        convert(char(10), ProjectDPR_SubmitionDate, 103) ProjectDPR_SubmitionDate,
	                        ProjectDPR_Comments,
	                        convert(char(10), ProjectDPR_AddedOn, 103) ProjectDPR_AddedOn, 
	                        ProjectDPR_AddedBy,
	                        ProjectDPR_FilePath1,
	                        ProjectDPR_FilePath2,
	                        convert(char(10), ProjectDPR_UploadedOn, 103) ProjectDPR_UploadedOn,
	                        ProjectDPR_UploadedBy,
	                        ProjectDPR_Verified_Comments,
	                        ProjectDPR_IsVerified,
	                        ProjectDPR_Upload_Comments,
	                        ProjectDPR_VerifiedBy,
	                        ProjectDPR_BudgetAllocated = convert(decimal(18,2), (isnull(ProjectDPR_BudgetAllocated, 0) / 100000)),
	                        ProjectDPR_BudgetAllocatedBy,
	                        convert(char(10), ProjectDPR_BudgetAllocatedOn, 103) ProjectDPR_BudgetAllocatedOn,
	                        ProjectDPR_BudgetAllocationComments,
	                        convert(char(10), ProjectDPR_VerifiedOn, 103) ProjectDPR_VerifiedOn,
	                        ProjectDPR_Work_Id,
	                        ProjectDPR_SubmitionDate1 = convert(char(10), ProjectDPR_SubmitionDate, 103), 
	                        ProjectDPR_BudgetAllocatedOn1 = convert(char(10), ProjectDPR_BudgetAllocatedOn, 103), 
	                        M_Jurisdiction_NP.ULB_Name NP_Jurisdiction_Name_Eng, 
                            M_Jurisdiction.Jurisdiction_Name_Eng Jurisdiction_Name_Eng, 
                            Fund_Allocated = convert(decimal(18,2), (isnull(tFinancialTrans.FinancialTrans_TransAmount, 0) / 100000)) , 
                            FinancialTrans_Id = convert(decimal(18,2), (isnull(tFinancialTrans.FinancialTrans_Id, 0) / 100000)) , 
                            Total_Release = convert(decimal(18,2), (isnull(tStatement.TransAmount_C, 0) / 100000)) , 
                            Total_Expenditure = convert(decimal(18,2), (isnull(tStatement.TransAmount_D, 0) / 100000)) ,
                            Total_Available = convert(decimal(18,2), (isnull(tStatement.TransAmount_C, 0) / 100000)) - convert(decimal(18,2), (isnull(tStatement.TransAmount_D, 0) / 100000)), 
                            Vendor_Name = tbl_ProjectDPRTenderInfo.Person_Name, 
	                        tbl_ProjectDPRTenderInfo.ProjectDPRTenderInfo_Id, 
                            tAdditional.ProjectDPRAdditionalInfo_Designation,
	                        tAdditional.ProjectDPRAdditionalInfo_RecomendatorMobile, 
	                        tAdditional.ProjectDPRAdditionalInfo_RecomendatorName, 
	                        ProjectWork_Id,
	                        ProjectWork_Project_Id,
	                        ProjectWork_Name,
	                        ProjectWork_Status,
	                        ProjectWork_AddedBy,
	                        ProjectWork_GO_Path,
	                        ProjectWork_Budget,
	                        ProjectWork_GO_Date = convert(char(10), ProjectWork_GO_Date, 103),
	                        ProjectWork_GO_Date1 = convert(char(10), ProjectWork_GO_Date, 103), 
	                        ProjectWork_GO_No,
	                        ProjectWork_ProjectType_Id,
	                        ProjectWork_Target_Date = convert(char(10), ProjectWork_Target_Date, 103),
	                        ProjectWork_IsVerified, 
	                        tVerificationStatus.ProjectDPRStatus_Date, 
	                        tVerificationStatus.ProjectDPRStatus_Comments, 
	                        tVerificationStatus.DPR_Status_DPR_StatusName, 
	                        tVerificationStatus.ProjectDPRStatus_DPR_StatusId, 
                            tbl_FinancialTransGO.List_FT_GO, 
                            Physical_Progress = isnull(TUCDetails.ProjectUC_PhysicalProgress, 0),
                            Financial_Progress = convert(decimal(18,2), isnull(TUCDetails.ProjectUC_BudgetUtilized, 0) / 100000),
                            Financial_Progress_Per = isnull(TUCDetails.ProjectUC_Achivment, 0), 
                            M_Jurisdiction.Jurisdiction_Name_Eng District_Name, 
                            M_Jurisdiction_NP.ULB_Name NP_Jurisdiction_Name_Eng, 
                            M_Jurisdiction_NP.ULB_Name ULB_Name, 
                            M_Jurisdiction.Jurisdiction_Name_Eng Jurisdiction_Name_Eng,                             
                            Vendor_Name = tbl_ProjectDPRTenderInfo.Person_Name, 
                            Vendor_Mobile = tbl_ProjectDPRTenderInfo.Person_Mobile1, 
	                        tbl_ProjectDPRTenderInfo.ProjectDPRTenderInfo_Id, 
                            tAdditional.ProjectDPRAdditionalInfo_Designation,
	                        tAdditional.ProjectDPRAdditionalInfo_RecomendatorMobile, 
	                        tAdditional.ProjectDPRAdditionalInfo_RecomendatorName, 
                            ProjectDPR_PhysicalProgressTrackingType
                        from tbl_ProjectWork 
                        join tbl_ProjectDPR on ProjectWork_Id = ProjectDPR_Work_Id and ProjectWork_Status = 1
                        join M_Jurisdiction on ProjectDPR_District_Jurisdiction_Id = M_Jurisdiction_Id 
                        join tbl_ULB M_Jurisdiction_NP on M_Jurisdiction_NP.ULB_Id = tbl_ProjectDPR.ProjectDPR_NP_JurisdictionId
                        
                        left join (select ROW_NUMBER() over (partition by ProjectDPRAdditionalInfo_ProjectDPR_Id order by ProjectDPRAdditionalInfo_Id desc) rrrr, ProjectDPRAdditionalInfo_Id, ProjectDPRAdditionalInfo_ProjectDPR_Id, ProjectDPRAdditionalInfo_RecomendatorMobile, ProjectDPRAdditionalInfo_RecomendatorName, ProjectDPRAdditionalInfo_Designation from tbl_ProjectDPRAdditionalInfo where ProjectDPRAdditionalInfo_Status = 1) tAdditional on ProjectDPRAdditionalInfo_ProjectDPR_Id = ProjectDPR_Id and tAdditional.rrrr = 1
                        left join (select ROW_NUMBER() over (partition by ProjectDPRStatus_ProjectDPR_Id order by ProjectDPRStatus_Id desc) rrrr, ProjectDPRStatus_DPR_StatusId, ProjectDPRStatus_ProjectDPR_Id, ProjectDPRStatus_Date = convert(char(10), ProjectDPRStatus_Date, 103), ProjectDPRStatus_Comments, DPR_Status_DPR_StatusName from tbl_ProjectDPRStatus join tbl_DPR_Status on DPR_Status_Id = ProjectDPRStatus_DPR_StatusId where ProjectDPRStatus_Status = 1) tVerificationStatus on tVerificationStatus.ProjectDPRStatus_ProjectDPR_Id = ProjectDPR_Id and tVerificationStatus.rrrr = 1
                        leftCondHere join (select ROW_NUMBER() over (partition by ProjectDPRTenderInfo_ProjectDPR_Id, ProjectDPRTenderInfo_ProjectWork_Id order by ProjectDPRTenderInfo_Id desc) rrrT, ProjectDPRTenderInfo_ProjectDPR_Id, ProjectDPRTenderInfo_ProjectWork_Id, ProjectDPRTenderInfo_Id, ProjectDPRTenderInfo_VendorPersonId, Person_Name, Person_Mobile1 from tbl_ProjectDPRTenderInfo join tbl_PersonDetail on Person_Id = ProjectDPRTenderInfo_VendorPersonId) tbl_ProjectDPRTenderInfo on tbl_ProjectDPRTenderInfo.ProjectDPRTenderInfo_ProjectDPR_Id = ProjectDPR_Id and tbl_ProjectDPRTenderInfo.ProjectDPRTenderInfo_ProjectWork_Id = ProjectWork_Id and tbl_ProjectDPRTenderInfo.rrrT = 1
                        left join (select ROW_NUMBER() over (partition by FinancialTrans_ProjectDPR_Id, FinancialTrans_Jurisdiction_Id, FinancialTrans_SchemeId, FinancialTrans_WorkId, FinancialTrans_TransType order by FinancialTrans_Id asc) rrr, FinancialTrans_Jurisdiction_Id, FinancialTrans_Id, FinancialTrans_TransAmount, FinancialTrans_SchemeId, FinancialTrans_WorkId, FinancialTrans_ProjectDPR_Id from tbl_FinancialTrans where FinancialTrans_Status = 1 and FinancialTrans_TransType = 'C') tFinancialTrans on tFinancialTrans.FinancialTrans_SchemeId = ProjectDPR_Project_Id and tFinancialTrans.FinancialTrans_Jurisdiction_Id = ProjectDPR_NP_JurisdictionId and FinancialTrans_ProjectDPR_Id = ProjectDPR_Id and FinancialTrans_WorkId = ProjectDPR_Work_Id and rrr = 1
                        left join (select FinancialTrans_Jurisdiction_Id, FinancialTrans_ProjectDPR_Id, FinancialTrans_SchemeId, FinancialTrans_WorkId, TransAmount_C = sum(case when FinancialTrans_TransType = 'C' then FinancialTrans_TransAmount else 0 end), TransAmount_D = sum(case when FinancialTrans_TransType = 'D' then FinancialTrans_TransAmount else 0 end) from tbl_FinancialTrans where FinancialTrans_Status = 1 group by FinancialTrans_Jurisdiction_Id, FinancialTrans_SchemeId, FinancialTrans_ProjectDPR_Id, FinancialTrans_WorkId) tStatement on tStatement.FinancialTrans_SchemeId = ProjectDPR_Project_Id and tStatement.FinancialTrans_Jurisdiction_Id = ProjectDPR_NP_JurisdictionId and tStatement.FinancialTrans_ProjectDPR_Id = ProjectDPR_Id and tStatement.FinancialTrans_WorkId = ProjectDPR_Work_Id
                        left join (select ROW_NUMBER() over (partition by ProjectUC_ProjectDPR_Id, ProjectUC_ProjectWork_Id order by ProjectUC_Id desc) rrUC, ProjectUC_ProjectDPR_Id, ProjectUC_ProjectWork_Id, ProjectUC_Achivment, convert(date, ProjectUC_SubmitionDate, 103) ProjectUC_SubmitionDate, ProjectUC_BudgetUtilized, ProjectUC_PhysicalProgress from tbl_ProjectUC where ProjectUC_Status = 1) TUCDetails on TUCDetails.ProjectUC_ProjectDPR_Id = ProjectDPR_Id  and TUCDetails.ProjectUC_ProjectWork_Id = ProjectDPR_Work_Id and rrUC = 1
                        left join 
                        (
	                        SELECT	FinancialTrans_ProjectDPR_Id, 
			                        FinancialTrans_WorkId,
			                        STUFF((SELECT ', ' + CAST(FinancialTrans_GO_Number AS NVARCHAR(100)) + ' Dt: ' + convert(char(10), FinancialTrans_GO_Date, 103) [text()]
                                    FROM tbl_FinancialTrans 
                                    WHERE FinancialTrans_ProjectDPR_Id = t.FinancialTrans_ProjectDPR_Id and FinancialTrans_WorkId = t.FinancialTrans_WorkId and tbl_FinancialTrans.FinancialTrans_Status = 1 and tbl_FinancialTrans.FinancialTrans_TransType = 'C'
                                    FOR XML PATH(''), TYPE)
                                .value('.','NVARCHAR(MAX)'),1,2,' ') List_FT_GO
	                        FROM tbl_FinancialTrans t
                            where t.FinancialTrans_Status = 1 and t.FinancialTrans_TransType = 'C'
	                        GROUP BY FinancialTrans_ProjectDPR_Id, FinancialTrans_WorkId
                        ) tbl_FinancialTransGO on tbl_FinancialTransGO.FinancialTrans_ProjectDPR_Id = ProjectDPR_Id and tbl_FinancialTransGO.FinancialTrans_WorkId = ProjectWork_Id
                        AddJoinCondition
                        where ProjectDPR_Status = 1 and isnull(tFinancialTrans.FinancialTrans_TransAmount, 0) > 0 ";

            if (_Mode == "ULB")
            {
                strQuery = strQuery.Replace("AddJoinCondition", "");
                strQuery = strQuery.Replace("leftCondHere", "left");
                strQuery += " and (ProjectDPR_NP_JurisdictionId in (select PersonJuridiction_ULB_Id from tbl_PersonJuridiction where PersonJuridiction_PersonId = '" + Person_Id.ToString() + "') or ProjectDPR_NP_JurisdictionId in (select PersonAdditionalULB_ULB_Id from tbl_PersonAdditionalULB where PersonAdditionalULB_Person_Id = '" + Person_Id.ToString() + "'))";
            }
            else if (_Mode == "Vendor")
            {
                strQuery = strQuery.Replace("leftCondHere", "");
                strQuery = strQuery.Replace("AddJoinCondition", "");
                strQuery += "and tbl_ProjectDPRTenderInfo.ProjectDPRTenderInfo_VendorPersonId = " + Person_Id.ToString();
            }
            else if (_Mode == "Inspection")
            {
                sqlJoin += Environment.NewLine;
                sqlJoin += "join tbl_ProjectDPRInspectionInfo on ProjectDPRInspectionInfo_ProjectDPR_Id = ProjectDPR_Id and ProjectDPRInspectionInfo_ProjectWork_Id = ProjectWork_Id";
                strQuery = strQuery.Replace("AddJoinCondition", sqlJoin);
                strQuery = strQuery.Replace("leftCondHere", "left");
                strQuery += "and ProjectDPRInspectionInfo_InspectionPersonId = " + Person_Id.ToString();
            }
            else
            {
                strQuery = strQuery.Replace("AddJoinCondition", "");
                strQuery = strQuery.Replace("leftCondHere", "left");
            }
            if (ULB_Id > 0)
            {
                // strQuery += " and PersonJuridiction_PersonId = '" + ULB_Id.ToString() + "'";
                strQuery += " and (ProjectDPR_NP_JurisdictionId in (select PersonJuridiction_ULB_Id from tbl_PersonJuridiction where PersonJuridiction_PersonId = '" + ULB_Id.ToString() + "') or ProjectDPR_NP_JurisdictionId in (select PersonAdditionalULB_ULB_Id from tbl_PersonAdditionalULB where PersonAdditionalULB_Person_Id = '" + ULB_Id.ToString() + "')) ";
            }
            if (Scheme_Id > 0)
            {
                strQuery += " and ProjectDPR_Project_Id = '" + Scheme_Id.ToString() + "'";
            }
            if (_Status == "Completed")
            {
                strQuery += " and isnull(TUCDetails.ProjectUC_PhysicalProgress, 0)>=100 and isnull(TUCDetails.ProjectUC_Achivment, 0)>=100 ";
            }
            else
            {
                strQuery += " and (isnull(TUCDetails.ProjectUC_PhysicalProgress, 0) < 100 or isnull(TUCDetails.ProjectUC_Achivment, 0) < 100) ";
            }
            strQuery += " order by M_Jurisdiction.Jurisdiction_Name_Eng, M_Jurisdiction_NP.ULB_Name";
            try
            {
                ds = ExecuteSelectQuery(strQuery);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet get_tbl_ProjectDPR_JalNigam_Upload(SearchCriteria obj_SearchCriteria, int Zone_Id_Search)
        {
            int Zone_Id = 0;
            int Circle_Id = 0;
            int Division_Id = 0;

            string strQuery = "";
            string sql = "";
            DataSet ds = new DataSet();
            if (obj_SearchCriteria.Person_Id > 0)
            {
                sql = @"select PersonJuridiction_DivisionId, PersonJuridiction_CircleId, PersonJuridiction_ZoneId from tbl_PersonDetail join tbl_PersonJuridiction on Person_Id = PersonJuridiction_PersonId where Person_Id = '" + obj_SearchCriteria.Person_Id + "' ";
                ds = ExecuteSelectQuery(sql);
                if (Utility.CheckDataSet(ds))
                {
                    try
                    {
                        if (Zone_Id_Search > 0)
                        {
                            Zone_Id = Zone_Id_Search;
                        }
                        else
                        {
                            Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_ZoneId"].ToString());
                        }
                    }
                    catch
                    {
                        Zone_Id = 0;
                    }
                    try
                    {
                        Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                    try
                    {
                        Division_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_DivisionId"].ToString());
                    }
                    catch
                    {
                        Division_Id = 0;
                    }
                }
            }
            strQuery = @"set dateformat dmy; 
                        select 
	                        *, Expenditure = convert(decimal(18, 2), (tender_cost * Financial_Progress) / 100) 
                        from (
                        select 
                            ProjectWork_Id, 
                            ProjectWork_Project_Id, 
                            Project_Name, 
                            ProjectWork_ProjectCode, 
                            ProjectWork_Name, 
                            ProjectWork_GO_Path,
                            ProjectWork_GO_Date = convert(char(10), ProjectWork_GO_Date, 103), 
	                        ProjectWork_StartDate = convert(char(10), ProjectWork_StartDate, 103), 
	                        ProjectWork_EndDate = convert(char(10), ProjectWork_EndDate, 103), 
	                        ProjectWorkPkg_Agreement_Date,
	                        ProjectWorkPkg_Due_Date, 
	                        ProjectWorkPkg_Start_Date, 
	                        Target_Date_Agreement_Extended,
                            ProjectWork_GO_No,
                            ProjectWork_Budget = isnull(ProjectWork_Budget, 0) + isnull(ProjectWork_Centage, 0),
                            ProjectWork_ProjectType_Id,
	                        ULB_Name, 
	                        Jurisdiction_Name_Eng, 
	                        Division_Name, 
	                        Circle_Name, 
	                        Zone_Name, 
	                        ProjectWork_DistrictId, 
                            ProjectWork_BlockId,
	                        ProjectWork_ULB_Id, 
	                        ProjectWork_DivisionId, 
	                        Division_CircleId, 
                            Division_Id, 
	                        Circle_Id, 
	                        Zone_Id, 
	                        tender_cost,
	                        Financial_Progress = convert(decimal(18, 2), (case when isnull(tProjectWorkPkg.tender_cost, 0) > 0 then (((isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tPrevInvoice.Amount, 0)) * 100) / isnull(tProjectWorkPkg.tender_cost, 0)) else 0 end)), 
                            Physical_Progress = convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0))
                        from tbl_ProjectWork
                        join tbl_Project on Project_Id = ProjectWork_Project_Id
                        join M_Jurisdiction on M_Jurisdiction_Id = ProjectWork_DistrictId
                        left join tbl_ULB on ULB_Id = ProjectWork_ULB_Id
                        left join tbl_Division on Division_Id = ProjectWork_DivisionId
                        left join tbl_Circle on Circle_Id = Division_CircleId
                        left join tbl_Zone on Zone_Id = Circle_ZoneId
                        left join 
                        (
                        select 
	                        ProjectWorkPkg_Work_Id,
	                        tender_cost=sum(cast(ISNULL(ProjectWorkPkg_AgreementAmount,0) as decimal(18,2))), 
                            tender_cost_1 = convert(decimal(18,2), (sum(ISNULL(ProjectWorkPkg_AgreementAmount, 0)) / (1 + (max(isnull(ProjectWorkPkg_Percent, 12)) / 100)))), 
	                        ProjectWorkPkg_Due_Date = convert(char(10), max(ProjectWorkPkg_Due_Date), 103),
	                        ProjectWorkPkg_Start_Date = convert(char(10), min(ProjectWorkPkg_Start_Date), 103),
	                        ProjectWorkPkg_Agreement_Date = convert(char(10), min(ProjectWorkPkg_Agreement_Date), 103), 
	                        Target_Date_Agreement_Extended = convert(char(10), max(case when ProjectWorkPkg_ExtendDate is null then ProjectWorkPkg_Due_Date else ProjectWorkPkg_ExtendDate end), 103)
                        from (
	                        select 
		                        ProjectWorkPkg_Work_Id,
		                        ProjectWorkPkg_AgreementAmount = case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then (ProjectWorkPkg_AgreementAmount + ProjectWorkPkg_AgreementAmount * isnull(ProjectWorkPkg_Percent, 12) / 100) else ProjectWorkPkg_AgreementAmount end, 
		                        ProjectWorkPkg_Due_Date,
		                        ProjectWorkPkg_Start_Date,
		                        ProjectWorkPkg_Agreement_Date, 
		                        ProjectWorkPkg_ExtendDate, 
		                        ProjectWorkPkg_Percent
	                        from tbl_ProjectWorkPkg 
	                        where 	ProjectWorkPkg_Status=1	
	                        ) tData
	                        group by ProjectWorkPkg_Work_Id
                        )tProjectWorkPkg  on tProjectWorkPkg.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id
                        left join
                        (
	                        select 
		                        ProjectWorkPkg_Work_Id, 
		                        Total_Amount = sum(cast((ISNULL(FinancialTrans_TransAmount,0) / 100000) as decimal(18,2))), 
		                        Amount = sum(cast((ISNULL(FinancialTrans_Amount,0) / 100000) as decimal(18,2))), 
		                        GST = sum(cast((ISNULL(FinancialTrans_GST,0) / 100000) as decimal(18,2))) 
	                        from tbl_PackageInvoice
	                        join tbl_ProjectWorkPkg on ProjectWorkPkg_Id = PackageInvoice_Package_Id
	                        inner join tbl_FinancialTrans on FinancialTrans_Invoice_Id=PackageInvoice_Id
	                        where PackageInvoice_Status = 1 and FinancialTrans_EntryType = 'Fund Allocated' and FinancialTrans_TransType = 'C' and PackageInvoice_Id not in (select PackageInvoiceEMBMasterLink_Invoice_Id from tbl_PackageInvoiceEMBMasterLink where PackageInvoiceEMBMasterLink_Status=1) 
	                        group by ProjectWorkPkg_Work_Id
                        ) tPrevInvoice on tPrevInvoice.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id
                        left join
                        (
                            select 
		                        ProjectWorkPkg_Work_Id,
		                        Amount = sum(cast((ISNULL(FinancialTrans_Amount,0) / 100000) as decimal(18,4))) 
	                        from tbl_Package_ADP
	                        inner join tbl_FinancialTrans on FinancialTrans_Invoice_Id = Package_ADP_Id 
	                        join tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
	                        where Package_ADP_Status = 1 and FinancialTrans_EntryType = 'ADP' and FinancialTrans_TransType = 'C' 
	                        group by ProjectWorkPkg_Work_Id
                        ) tPrevInvoiceADP on tPrevInvoiceADP.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id
                        left join 
                        (
	                        select 
		                        ProjectWorkPkg_Work_Id,
		                        Total_Invoice = count(*), 
		                        Total_Invoice_Value = sum(isnull(InvoiceAmount, 0)) / 100000, 
		                        Deffered_Value = sum(case when (isnull(PackageInvoiceApproval_Next_Designation_Id, 0) = 0 and isnull(PackageInvoiceApproval_Next_Organisation_Id, 0) = 0 and  PackageInvoiceApproval_Status_Id not in (1, 6)) then isnull(InvoiceAmount, 0) else 0 end) / 100000
	                        from tbl_PackageInvoice 
	                        join tbl_ProjectWorkPkg on ProjectWorkPkg_Id = PackageInvoice_Package_Id
	                        join 
	                        (
		                        select 
			                        ROW_NUMBER() over (partition by PackageInvoiceApproval_PackageInvoice_Id order by PackageInvoiceApproval_Id desc) rrrrr,
			                        PackageInvoiceApproval_Next_Designation_Id,
			                        PackageInvoiceApproval_Next_Organisation_Id,
			                        PackageInvoiceApproval_Comments,
			                        PackageInvoiceApproval_AddedBy,
			                        PackageInvoiceApproval_AddedOn,
			                        PackageInvoiceApproval_Status_Id,
			                        PackageInvoiceApproval_Package_Id,
			                        PackageInvoiceApproval_PackageInvoice_Id,
			                        InvoiceStatus_Name,
			                        PackageInvoiceApproval_Date = convert(char(10), PackageInvoiceApproval_Date, 103),
			                        PackageInvoiceApproval_Id
		                        from tbl_PackageInvoiceApproval
		                        left join tbl_InvoiceStatus on InvoiceStatus_Id = PackageInvoiceApproval_Status_Id
		                        where PackageInvoiceApproval_Status = 1
	                        ) tInvoiceApproval on tInvoiceApproval.PackageInvoiceApproval_PackageInvoice_Id = PackageInvoice_Id and tInvoiceApproval.rrrrr = 1
	                        where PackageInvoice_Status = 1 and ProjectWorkPkg_Status = 1  
	                        group by ProjectWorkPkg_Work_Id
                        ) tInvoice on tInvoice.ProjectWorkPkg_Work_Id = ProjectWork_Id 
                        left join 
                        (
                            select 
                                row_number() over (partition by ProjectWorkFinancialTarget_ProjectWork_Id order by ProjectWorkFinancialTarget_Id desc) rrT,
                                ProjectWorkFinancialTarget_ProjectWork_Id, 
                                ProjectWorkFinancialTarget_Target, 
                                ProjectWorkPhysicalTarget_Target, 
		                        ProjectWorkFinancialTarget_AddedOn,  
		                        ProjectWorkFinancialTarget_Month, 
		                        ProjectWorkFinancialTarget_Year,
		                        DaysDiff = DATEDIFF(DD, ProjectWorkFinancialTarget_AddedOn, getdate())
                            from tbl_ProjectWorkFinancialTarget
                            where ProjectWorkFinancialTarget_Status = 1   
                        ) tTarget on tTarget.ProjectWorkFinancialTarget_ProjectWork_Id = tbl_ProjectWork.ProjectWork_Id and rrT = 1
                        where ProjectWork_Status = 1 ";

            if (obj_SearchCriteria.Reporting_Mode == "ULB")
            {
                if (Zone_Id > 0)
                {
                    strQuery += " and Circle_ZoneId = '" + Zone_Id + "'";
                }
                if (Circle_Id > 0)
                {
                    strQuery += " and Division_CircleId = '" + Circle_Id + "'";
                }
                if (Division_Id > 0)
                {
                    strQuery += " and ProjectWork_DivisionId = '" + Division_Id + "'";
                }
            }
            else if (obj_SearchCriteria.Reporting_Mode == "Vendor")
            {
                strQuery = strQuery.Replace("leftCondHere", "");
                strQuery = strQuery.Replace("AddJoinCondition", "");
                strQuery += "and tbl_ProjectDPR_JalNigamTenderInfo.ProjectDPRTenderInfo_VendorPersonId = " + obj_SearchCriteria.Person_Id.ToString();
            }
            else if (obj_SearchCriteria.Reporting_Mode == "Inspection")
            {
                strQuery += "and ProjectDPRInspectionInfo_InspectionPersonId = " + obj_SearchCriteria.Person_Id.ToString();
            }
            else
            {
                strQuery = strQuery.Replace("AddJoinCondition", "");
                strQuery = strQuery.Replace("leftCondHere", "left");
            }
            if (Zone_Id > 0)
            {
                strQuery += " and Circle_ZoneId = '" + Zone_Id + "'";
            }
            if (Circle_Id > 0)
            {
                strQuery += " and Division_CircleId = '" + Circle_Id + "'";
            }
            if (Division_Id > 0)
            {
                strQuery += " and ProjectWork_DivisionId = '" + Division_Id + "'";
            }
            if (obj_SearchCriteria.Project_Id > 0)
            {
                strQuery += " and ProjectWork_Project_Id = '" + obj_SearchCriteria.Project_Id.ToString() + "'";
            }
            if (obj_SearchCriteria.Project_Status == "Completed")
            {
                strQuery += " and isnull(convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0)), 0) >= 100 ";
            }
            else if (obj_SearchCriteria.Project_Status == "Running")
            {
                strQuery += " and isnull(convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0)), 0) < 100 ";
            }
            else
            {

            }
            strQuery += " ) tData order by Zone_Name, Circle_Name, Division_Name";
            try
            {
                ds = ExecuteSelectQuery(strQuery);
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet get_tbl_ProjectQuestionnaire(int Project_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @" set dateformat dmy; 
                        select 
                            ProjectQuestionnaire_Id,
                            ProjectQuestionnaire_ProjectId,
                            ProjectQuestionnaire_Name,
                            ProjectQuestionnaire_AddedBy,
                            ProjectQuestionnaire_AddedOn,
                            ProjectQuestionnaire_Status
                        from tbl_ProjectQuestionnaire 
                        where ProjectQuestionnaire_Status = 1 ";
            if (Project_Id > 0)
            {
                strQuery += " and ProjectQuestionnaire_ProjectId = '" + Project_Id + "'";
            }
            strQuery += " order by ProjectQuestionnaire_Name";
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
        public DataSet get_tbl_ProjectAnswer(int Questionnaire_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @" set dateformat dmy; 
                    select 
                        ProjectAnswer_Id,
                        ProjectAnswer_ProjectQuestionnaireId,
                        ProjectAnswer_Name,
                        ProjectAnswer_AddedBy,
                        ProjectAnswer_AddedOn,
                        ProjectAnswer_Status
                    from tbl_ProjectAnswer 
                    where ProjectAnswer_Status = 1 ";
            if (Questionnaire_Id > 0)
            {
                strQuery += " and ProjectAnswer_ProjectQuestionnaireId = '" + Questionnaire_Id + "'";
            }
            strQuery += " order by ProjectAnswer_Name";
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

        private bool saveImageData(string file_Name, string base_64_Data, string UploadPath_Full, string lat, string lon, string TimeStamp)
        {
            try
            {
                string ext = file_Name.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[1];
                byte[] bytes = Convert.FromBase64String(base_64_Data);
                if (bytes != null && bytes.Length > 0)
                {
                    Image image = Image.FromStream(new MemoryStream(bytes));
                    if (!string.IsNullOrEmpty(lat))
                    {
                        image = Write_Info_On_Image(image, lat, lon, TimeStamp);
                    }
                    if (!Directory.Exists(UploadPath_Full))
                    {
                        Directory.CreateDirectory(UploadPath_Full);
                    }
                    if (ext.ToLower() == "jpg" || ext.ToLower() == "jpeg")
                    {
                        image.Save(UploadPath_Full + file_Name, ImageFormat.Jpeg);
                    }
                    else if (ext.ToLower() == "bmp")
                    {
                        image.Save(UploadPath_Full + file_Name, ImageFormat.Bmp);
                    }
                    else if (ext.ToLower() == "gif")
                    {
                        image.Save(UploadPath_Full + file_Name, ImageFormat.Gif);
                    }
                    else if (ext.ToLower() == "icon")
                    {
                        image.Save(UploadPath_Full + file_Name, ImageFormat.Icon);
                    }
                    else if (ext.ToLower() == "png")
                    {
                        image.Save(UploadPath_Full + file_Name, ImageFormat.Png);
                    }
                    else if (ext.ToLower() == "tiff")
                    {
                        image.Save(UploadPath_Full + file_Name, ImageFormat.Tiff);
                    }
                    else if (ext.ToLower() == "wmf")
                    {
                        image.Save(UploadPath_Full + file_Name, ImageFormat.Wmf);
                    }
                    else
                    {
                        image.Save(UploadPath_Full + file_Name, ImageFormat.Jpeg);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private Image Write_Info_On_Image(Image bmp, string lat, string lon, string TimeStamp)
        {
            Bitmap overlayImage;
            try
            {
                overlayImage = (Bitmap)Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "logo.png");
                overlayImage = new Bitmap(overlayImage, new Size(100, 100));
            }
            catch
            {
                overlayImage = null;
            }
            try
            {
                float YLocation = bmp.Height;

                PointF firstLocation = new PointF(10f, YLocation - 90);
                PointF secondLocation = new PointF(10f, YLocation - 60);
                PointF thirdLocation = new PointF(10f, YLocation - 30);

                Graphics graphics = Graphics.FromImage(bmp);

                //Pen p = new Pen(System.Drawing.Color.White, 5);
                //Rectangle rectangle = new Rectangle(0, Convert.ToInt32(YLocation - 120), bmp.Width, 110);
                //graphics.DrawRectangle(p, rectangle);
                //SolidBrush myBrush = new SolidBrush(Color.LightGray);
                //graphics.FillRectangle(myBrush, rectangle);

                using (Font arialFont = new Font("Arial", 15, FontStyle.Bold))
                {
                    graphics.DrawString("Lat: " + lat, arialFont, Brushes.White, firstLocation);
                    graphics.DrawString("Lon: " + lon, arialFont, Brushes.White, secondLocation);
                    graphics.DrawString(TimeStamp, arialFont, Brushes.White, thirdLocation);
                }
                if (overlayImage != null)
                {
                    graphics.DrawImage(overlayImage, bmp.Width - 100, bmp.Height - 100);
                }
            }
            catch { }
            return bmp;
        }

        public bool Update_tbl_ProjectDPR_WorkStatus(tbl_ProjectUC obj_tbl_ProjectUC, tbl_FinancialTrans obj_tbl_FinancialTrans, List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li, List<tbl_ProjectDPRSitePics> obj_tbl_ProjectDPRSitePics_Li, List<tbl_ProjectUC_PhysicalProgress> obj_tbl_ProjectUC_PhysicalProgress_Li, List<tbl_ProjectUC_Deliverables> obj_tbl_ProjectUC_Deliverables_Li, string Project_Status)
        {
            DataSet ds = new DataSet();
            bool iResult = false;
            string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            using (SqlConnection cn = new SqlConnection(ConStr))
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlTransaction trans = cn.BeginTransaction();
                try
                {
                    if (!Directory.Exists(System.Configuration.ConfigurationManager.AppSettings["BasePath"] + "Gallery\\" + obj_tbl_ProjectUC.ProjectUC_ProjectWork_Id.ToString()))
                    {
                        Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings["BasePath"] + "Gallery\\" + obj_tbl_ProjectUC.ProjectUC_ProjectWork_Id.ToString());
                    }
                    string sql = "";
                    if (obj_tbl_ProjectDPRSitePics_Li != null && obj_tbl_ProjectDPRSitePics_Li.Count > 0)
                    {
                        for (int i = 0; i < obj_tbl_ProjectDPRSitePics_Li.Count; i++)
                        {
                            if (saveImageData(obj_tbl_ProjectDPRSitePics_Li[i].ProjectDPRSitePics_SitePic_Path1, obj_tbl_ProjectDPRSitePics_Li[i].ProjectDPRSitePics_SitePic_Bytes1, System.Configuration.ConfigurationManager.AppSettings["BasePath"] + "Gallery\\" + obj_tbl_ProjectUC.ProjectUC_ProjectWork_Id.ToString() + "\\", obj_tbl_ProjectUC.ProjectUC_Latitude.ToString(), obj_tbl_ProjectUC.ProjectUC_Longitude.ToString(), DateTime.Now.ToLongDateString()))
                            {
                                obj_tbl_ProjectDPRSitePics_Li[i].ProjectDPRSitePics_UCDetails_Id = obj_tbl_ProjectUC.ProjectUC_Id;
                                obj_tbl_ProjectDPRSitePics_Li[i].ProjectDPRSitePics_SitePic_Path1 = "\\Downloads\\Gallery\\" + obj_tbl_ProjectUC.ProjectUC_ProjectWork_Id.ToString() + "\\" + obj_tbl_ProjectDPRSitePics_Li[i].ProjectDPRSitePics_SitePic_Path1;

                                sql = "set dateformat dmy; insert into tbl_ProjectWorkGallery (ProjectWorkGallery_Work_Id, ProjectWorkGallery_Path, ProjectWorkGallery_AddedBy, ProjectWorkGallery_AddedOn, ProjectWorkGallery_Status, ProjectWorkGallery_Comments, ProjectWorkGallery_Latitude, ProjectWorkGallery_Longitude) values ('" + obj_tbl_ProjectUC.ProjectUC_ProjectWork_Id + "', '" + obj_tbl_ProjectDPRSitePics_Li[i].ProjectDPRSitePics_SitePic_Path1 + "', '" + obj_tbl_ProjectUC.ProjectUC_AddedBy + "', getdate(), 1, '" + obj_tbl_ProjectDPRSitePics_Li[i].ProjectDPRSitePics_Comments + "', '" + obj_tbl_ProjectUC.ProjectUC_Latitude + "', '" + obj_tbl_ProjectUC.ProjectUC_Longitude + "')";
                                ExecuteSelectQuerywithTransaction(cn, sql, trans);
                            }
                        }
                    }

                    iResult = true;
                    trans.Commit();
                    cn.Close();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    cn.Close();
                    iResult = false;
                }
            }
            return iResult;
        }
        private void Insert_tbl_ProjectUC_PhysicalProgress(tbl_ProjectUC_PhysicalProgress obj_tbl_ProjectUC_PhysicalProgress, int DPR_Id, int Work_Id, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_ProjectUC_PhysicalProgress (ProjectUC_PhysicalProgress_ProjectWork_Id, ProjectUC_PhysicalProgress_ProjectDPR_Id, ProjectUC_PhysicalProgress_PhysicalProgressComponent_Id, ProjectUC_PhysicalProgress_PhysicalProgress, ProjectUC_PhysicalProgress_AddedOn, ProjectUC_PhysicalProgress_AddedBy, ProjectUC_PhysicalProgress_Status, ProjectUC_PhysicalProgress_UC_Id) values ('" + Work_Id + "','" + DPR_Id + "','" + obj_tbl_ProjectUC_PhysicalProgress.PhysicalProgressComponent_Id + "','" + obj_tbl_ProjectUC_PhysicalProgress.PhysicalProgress_Current + "', getdate(), 1, 1, '" + obj_tbl_ProjectUC_PhysicalProgress.ProjectUC_PhysicalProgress_UC_Id + "');Select @@Identity";
            if (trans == null)
            {
                try
                {
                    ExecuteSelectQuery(strQuery);
                }
                catch
                {
                }
            }
            else
            {
                ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
            }
        }
        private void Insert_tbl_ProjectUC_Deliverables(tbl_ProjectUC_Deliverables obj_tbl_ProjectUC_Deliverables, int DPR_Id, int Work_Id, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_ProjectUC_Deliverables (ProjectUC_Deliverables_ProjectWork_Id, ProjectUC_Deliverables_ProjectDPR_Id, ProjectUC_Deliverables_Deliverables_Id, ProjectUC_Deliverables_Deliverables, ProjectUC_Deliverables_AddedOn, ProjectUC_Deliverables_AddedBy, ProjectUC_Deliverables_Status, ProjectUC_Deliverables_UC_Id) values ('" + Work_Id + "','" + DPR_Id + "','" + obj_tbl_ProjectUC_Deliverables.Deliverables_Id + "','" + obj_tbl_ProjectUC_Deliverables.DeliverablesTotal_Current + "', getdate(), 1, 1, '" + obj_tbl_ProjectUC_Deliverables.ProjectUC_Deliverables_UC_Id + "');Select @@Identity";
            if (trans == null)
            {
                try
                {
                    ExecuteSelectQuery(strQuery);
                }
                catch
                {
                }
            }
            else
            {
                ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
            }
        }
        private void Insert_tbl_ProjectDPRSitePics(tbl_ProjectDPRSitePics obj_tbl_ProjectDPRSitePics, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_ProjectDPRSitePics ([ProjectDPRSitePics_AddedBy],[ProjectDPRSitePics_AddedOn],[ProjectDPRSitePics_ProjectDPR_Id],[ProjectDPRSitePics_ProjectWork_Id],[ProjectDPRSitePics_ReportSubmitted],[ProjectDPRSitePics_ReportSubmittedBy_PersonId],[ProjectDPRSitePics_SitePic_Path1],[ProjectDPRSitePics_SitePic_Type],[ProjectDPRSitePics_Status], [ProjectDPRSitePics_UCDetails_Id], [ProjectDPRSitePics_Comments]) values ('" + obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_AddedBy + "',getdate(),'" + obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ProjectDPR_Id + "','" + obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ProjectWork_Id + "',N'" + obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ReportSubmitted + "','" + obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ReportSubmittedBy_PersonId + "',N'" + obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_SitePic_Path1 + "',N'" + obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_SitePic_Type + "','" + obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_Status + "', '" + obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_UCDetails_Id + "', '" + obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_Comments + "');Select @@Identity";
            if (trans == null)
            {
                try
                {
                    ExecuteSelectQuery(strQuery);
                }
                catch
                {
                }
            }
            else
            {
                ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
            }
        }
        public bool Update_tbl_ProjectDPR_JalNigam_WorkStatus(tbl_ProjectUC obj_tbl_ProjectUC, tbl_FinancialTrans obj_tbl_FinancialTrans, List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li, tbl_ProjectDPRSitePicsB1 obj_tbl_ProjectDPR_JalNigamSitePics_B1, tbl_ProjectDPRSitePicsB2 obj_tbl_ProjectDPR_JalNigamSitePics_B2, tbl_ProjectDPRSitePicsA1 obj_tbl_ProjectDPR_JalNigamSitePics_A1, tbl_ProjectDPRSitePicsA2 obj_tbl_ProjectDPR_JalNigamSitePics_A2)
        {
            DataSet ds = new DataSet();
            bool iResult = false;
            string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            using (SqlConnection cn = new SqlConnection(ConStr))
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlTransaction trans = cn.BeginTransaction();
                try
                {
                    obj_tbl_ProjectUC.ProjectUC_Id = Insert_tbl_ProjectUC(obj_tbl_ProjectUC, trans, cn);

                    if (obj_tbl_ProjectUC_Concent_Li != null)
                    {
                        for (int i = 0; i < obj_tbl_ProjectUC_Concent_Li.Count; i++)
                        {
                            obj_tbl_ProjectUC_Concent_Li[i].ProjectUC_Concent_ProjectUC_Id = obj_tbl_ProjectUC.ProjectUC_Id;
                            Insert_tbl_ProjectUC_Concent(obj_tbl_ProjectUC_Concent_Li[i], trans, cn);
                        }
                    }

                    if (obj_tbl_ProjectDPR_JalNigamSitePics_B1.ProjectDPRSitePics_SitePic_Bytes1 != null && obj_tbl_ProjectDPR_JalNigamSitePics_B1.ProjectDPRSitePics_SitePic_Bytes1.Length > 0)
                    {
                        if (saveImageData(obj_tbl_ProjectDPR_JalNigamSitePics_B1.ProjectDPRSitePics_SitePic_Path1, obj_tbl_ProjectDPR_JalNigamSitePics_B1.ProjectDPRSitePics_SitePic_Bytes1, System.Configuration.ConfigurationManager.AppSettings["BasePath"] + "Work\\", obj_tbl_ProjectUC.ProjectUC_Latitude.ToString(), obj_tbl_ProjectUC.ProjectUC_Longitude.ToString(), DateTime.Now.ToLongDateString()))
                        {
                            obj_tbl_ProjectDPR_JalNigamSitePics_B1.ProjectDPRSitePics_SitePic_Path1 = "\\Downloads\\Work\\" + obj_tbl_ProjectDPR_JalNigamSitePics_B1.ProjectDPRSitePics_SitePic_Path1;
                            Insert_tblProjectDPRSitePicsB1(obj_tbl_ProjectDPR_JalNigamSitePics_B1, obj_tbl_ProjectUC.ProjectUC_Id, trans, cn);
                        }
                    }

                    if (obj_tbl_ProjectDPR_JalNigamSitePics_B2.ProjectDPRSitePics_SitePic_Bytes1 != null && obj_tbl_ProjectDPR_JalNigamSitePics_B2.ProjectDPRSitePics_SitePic_Bytes1.Length > 0)
                    {
                        if (saveImageData(obj_tbl_ProjectDPR_JalNigamSitePics_B2.ProjectDPRSitePics_SitePic_Path1, obj_tbl_ProjectDPR_JalNigamSitePics_B2.ProjectDPRSitePics_SitePic_Bytes1, System.Configuration.ConfigurationManager.AppSettings["BasePath"] + "Work\\", obj_tbl_ProjectUC.ProjectUC_Latitude.ToString(), obj_tbl_ProjectUC.ProjectUC_Longitude.ToString(), DateTime.Now.ToLongDateString()))
                        {
                            obj_tbl_ProjectDPR_JalNigamSitePics_B2.ProjectDPRSitePics_SitePic_Path1 = "\\Downloads\\Work\\" + obj_tbl_ProjectDPR_JalNigamSitePics_B2.ProjectDPRSitePics_SitePic_Path1;
                            Insert_tblProjectDPRSitePicsB2(obj_tbl_ProjectDPR_JalNigamSitePics_B2, obj_tbl_ProjectUC.ProjectUC_Id, trans, cn);
                        }
                    }

                    if (obj_tbl_ProjectDPR_JalNigamSitePics_A1.ProjectDPRSitePics_SitePic_Bytes1 != null && obj_tbl_ProjectDPR_JalNigamSitePics_A1.ProjectDPRSitePics_SitePic_Bytes1.Length > 0)
                    {
                        if (saveImageData(obj_tbl_ProjectDPR_JalNigamSitePics_A1.ProjectDPRSitePics_SitePic_Path1, obj_tbl_ProjectDPR_JalNigamSitePics_A1.ProjectDPRSitePics_SitePic_Bytes1, System.Configuration.ConfigurationManager.AppSettings["BasePath"] + "Work\\", obj_tbl_ProjectUC.ProjectUC_Latitude.ToString(), obj_tbl_ProjectUC.ProjectUC_Longitude.ToString(), DateTime.Now.ToLongDateString()))
                        {
                            obj_tbl_ProjectDPR_JalNigamSitePics_A1.ProjectDPRSitePics_SitePic_Path1 = "\\Downloads\\Work\\" + obj_tbl_ProjectDPR_JalNigamSitePics_A1.ProjectDPRSitePics_SitePic_Path1;
                            Insert_tblProjectDPRSitePicsA1(obj_tbl_ProjectDPR_JalNigamSitePics_A1, obj_tbl_ProjectUC.ProjectUC_Id, trans, cn);
                        }
                    }

                    if (obj_tbl_ProjectDPR_JalNigamSitePics_A2.ProjectDPRSitePics_SitePic_Bytes1 != null && obj_tbl_ProjectDPR_JalNigamSitePics_A2.ProjectDPRSitePics_SitePic_Bytes1.Length > 0)
                    {
                        if (saveImageData(obj_tbl_ProjectDPR_JalNigamSitePics_A2.ProjectDPRSitePics_SitePic_Path1, obj_tbl_ProjectDPR_JalNigamSitePics_A2.ProjectDPRSitePics_SitePic_Bytes1, System.Configuration.ConfigurationManager.AppSettings["BasePath"] + "Work\\", obj_tbl_ProjectUC.ProjectUC_Latitude.ToString(), obj_tbl_ProjectUC.ProjectUC_Longitude.ToString(), DateTime.Now.ToLongDateString()))
                        {
                            obj_tbl_ProjectDPR_JalNigamSitePics_A2.ProjectDPRSitePics_SitePic_Path1 = "\\Downloads\\Work\\" + obj_tbl_ProjectDPR_JalNigamSitePics_A2.ProjectDPRSitePics_SitePic_Path1;
                            Insert_tblProjectDPRSitePicsA2(obj_tbl_ProjectDPR_JalNigamSitePics_A2, obj_tbl_ProjectUC.ProjectUC_Id, trans, cn);
                        }
                    }

                    obj_tbl_FinancialTrans.FinancialTrans_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
                    obj_tbl_FinancialTrans.FinancialTrans_GO_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
                    Insert_tbl_FinancialTrans(obj_tbl_FinancialTrans, trans, cn);

                    iResult = true;
                    trans.Commit();
                    cn.Close();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    cn.Close();
                    iResult = false;
                }
            }
            return iResult;
        }

        private void Insert_tblProjectDPRSitePicsA1(tbl_ProjectDPRSitePicsA1 obj_tbl_ProjectDPR_JalNigamSitePics, int UCDetails_Id, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_ProjectDPR_JalNigamSitePics ( [ProjectDPRSitePics_AddedBy],[ProjectDPRSitePics_AddedOn],[ProjectDPRSitePics_ProjectDPR_Id],[ProjectDPRSitePics_ProjectWork_Id],[ProjectDPRSitePics_ReportSubmitted],[ProjectDPRSitePics_ReportSubmittedBy_PersonId],[ProjectDPRSitePics_SitePic_Path1],[ProjectDPRSitePics_SitePic_Type],[ProjectDPRSitePics_Status], [ProjectDPRSitePics_UCDetails_Id]) values ('" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_AddedBy + "',getdate(),'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ProjectDPR_Id + "','" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ProjectWork_Id + "',N'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ReportSubmitted + "','" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ReportSubmittedBy_PersonId + "',N'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_SitePic_Path1 + "',N'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_SitePic_Type + "','" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_Status + "', '" + UCDetails_Id + "');Select @@Identity";
            if (trans == null)
            {
                try
                {
                    ExecuteSelectQuery(strQuery);
                }
                catch
                {
                }
            }
            else
            {
                ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
            }
        }

        private void Insert_tblProjectDPRSitePicsA2(tbl_ProjectDPRSitePicsA2 obj_tbl_ProjectDPR_JalNigamSitePics, int UCDetails_Id, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_ProjectDPR_JalNigamSitePics ( [ProjectDPRSitePics_AddedBy],[ProjectDPRSitePics_AddedOn],[ProjectDPRSitePics_ProjectDPR_Id],[ProjectDPRSitePics_ProjectWork_Id],[ProjectDPRSitePics_ReportSubmitted],[ProjectDPRSitePics_ReportSubmittedBy_PersonId],[ProjectDPRSitePics_SitePic_Path1],[ProjectDPRSitePics_SitePic_Type],[ProjectDPRSitePics_Status], [ProjectDPRSitePics_UCDetails_Id]) values ('" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_AddedBy + "',getdate(),'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ProjectDPR_Id + "','" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ProjectWork_Id + "',N'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ReportSubmitted + "','" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ReportSubmittedBy_PersonId + "',N'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_SitePic_Path1 + "',N'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_SitePic_Type + "','" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_Status + "', '" + UCDetails_Id + "');Select @@Identity";
            if (trans == null)
            {
                try
                {
                    ExecuteSelectQuery(strQuery);
                }
                catch
                {
                }
            }
            else
            {
                ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
            }
        }

        private void Insert_tblProjectDPRSitePicsB1(tbl_ProjectDPRSitePicsB1 obj_tbl_ProjectDPR_JalNigamSitePics, int UCDetails_Id, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_ProjectDPR_JalNigamSitePics ( [ProjectDPRSitePics_AddedBy],[ProjectDPRSitePics_AddedOn],[ProjectDPRSitePics_ProjectDPR_Id],[ProjectDPRSitePics_ProjectWork_Id],[ProjectDPRSitePics_ReportSubmitted],[ProjectDPRSitePics_ReportSubmittedBy_PersonId],[ProjectDPRSitePics_SitePic_Path1],[ProjectDPRSitePics_SitePic_Type],[ProjectDPRSitePics_Status], [ProjectDPRSitePics_UCDetails_Id]) values ('" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_AddedBy + "',getdate(),'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ProjectDPR_Id + "','" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ProjectWork_Id + "',N'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ReportSubmitted + "','" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ReportSubmittedBy_PersonId + "',N'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_SitePic_Path1 + "',N'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_SitePic_Type + "','" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_Status + "', '" + UCDetails_Id + "');Select @@Identity";
            if (trans == null)
            {
                try
                {
                    ExecuteSelectQuery(strQuery);
                }
                catch
                {
                }
            }
            else
            {
                ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
            }
        }

        private void Insert_tblProjectDPRSitePicsB2(tbl_ProjectDPRSitePicsB2 obj_tbl_ProjectDPR_JalNigamSitePics, int UCDetails_Id, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_ProjectDPR_JalNigamSitePics ( [ProjectDPRSitePics_AddedBy],[ProjectDPRSitePics_AddedOn],[ProjectDPRSitePics_ProjectDPR_Id],[ProjectDPRSitePics_ProjectWork_Id],[ProjectDPRSitePics_ReportSubmitted],[ProjectDPRSitePics_ReportSubmittedBy_PersonId],[ProjectDPRSitePics_SitePic_Path1],[ProjectDPRSitePics_SitePic_Type],[ProjectDPRSitePics_Status], [ProjectDPRSitePics_UCDetails_Id]) values ('" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_AddedBy + "',getdate(),'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ProjectDPR_Id + "','" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ProjectWork_Id + "',N'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ReportSubmitted + "','" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_ReportSubmittedBy_PersonId + "',N'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_SitePic_Path1 + "',N'" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_SitePic_Type + "','" + obj_tbl_ProjectDPR_JalNigamSitePics.ProjectDPRSitePics_Status + "', '" + UCDetails_Id + "');Select @@Identity";
            if (trans == null)
            {
                try
                {
                    ExecuteSelectQuery(strQuery);
                }
                catch
                {
                }
            }
            else
            {
                ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
            }
        }

        private string Insert_tbl_FinancialTrans(tbl_FinancialTrans obj_tbl_FinancialTrans, SqlTransaction trans, SqlConnection cn)
        {
            DataSet ds = new DataSet();
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_FinancialTrans ( [FinancialTrans_AddedBy],[FinancialTrans_Jurisdiction_Id],[FinancialTrans_Status],[FinancialTrans_TransAmount],[FinancialTrans_TransType],[FinancialTrans_SchemeId], [FinancialTrans_AddedOn], [FinancialTrans_Date], [FinancialTrans_EntryType], [FinancialTrans_Comments], [FinancialTrans_FinancialYear_Id], [FinancialTrans_ProjectDPR_Id], [FinancialTrans_WorkId], [FinancialTrans_FilePath1], [FinancialTrans_GO_Date], [FinancialTrans_GO_Number]) values ('" + obj_tbl_FinancialTrans.FinancialTrans_AddedBy + "','" + obj_tbl_FinancialTrans.FinancialTrans_Jurisdiction_Id + "','" + obj_tbl_FinancialTrans.FinancialTrans_Status + "','" + obj_tbl_FinancialTrans.FinancialTrans_TransAmount + "','" + obj_tbl_FinancialTrans.FinancialTrans_TransType + "','" + obj_tbl_FinancialTrans.FinancialTrans_SchemeId + "', getdate(), convert(date, '" + obj_tbl_FinancialTrans.FinancialTrans_Date + "', 103), '" + obj_tbl_FinancialTrans.FinancialTrans_EntryType + "', '" + obj_tbl_FinancialTrans.FinancialTrans_Comments + "', '" + obj_tbl_FinancialTrans.FinancialTrans_FinancialYear_Id + "', '" + obj_tbl_FinancialTrans.FinancialTrans_ProjectDPR_Id + "', '" + obj_tbl_FinancialTrans.FinancialTrans_WorkId + "', '" + obj_tbl_FinancialTrans.FinancialTrans_FilePath1 + "', convert(date, '" + obj_tbl_FinancialTrans.FinancialTrans_GO_Date + "', 103), N'" + obj_tbl_FinancialTrans.FinancialTrans_GO_Number + "'); Select @@Identity";
            if (trans == null)
            {
                ds = ExecuteSelectQuery(strQuery);
            }
            else
            {
                ds = ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
            }
            return ds.Tables[0].Rows[0][0].ToString();
        }

        private int Insert_tbl_ProjectUC(tbl_ProjectUC obj_tbl_ProjectUC, SqlTransaction trans, SqlConnection cn)
        {
            DataSet ds = new DataSet();
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_ProjectUC ([ProjectUC_Achivment],[ProjectUC_AddedBy],[ProjectUC_AddedOn],[ProjectUC_BudgetUtilized],[ProjectUC_Comments],[ProjectUC_ProjectDPR_Id],[ProjectUC_ProjectWork_Id],[ProjectUC_Status],[ProjectUC_SubmitionDate], [ProjectUC_PhysicalProgress], [ProjectUC_Latitude], [ProjectUC_Longitude], [ProjectUC_FinancialYear_Id], [ProjectUC_Total_Allocated], [ProjectUC_Centage]) values ('" + obj_tbl_ProjectUC.ProjectUC_Achivment + "','" + obj_tbl_ProjectUC.ProjectUC_AddedBy + "', getdate(),'" + obj_tbl_ProjectUC.ProjectUC_BudgetUtilized + "',N'" + obj_tbl_ProjectUC.ProjectUC_Comments + "','" + obj_tbl_ProjectUC.ProjectUC_ProjectDPR_Id + "','" + obj_tbl_ProjectUC.ProjectUC_ProjectWork_Id + "','" + obj_tbl_ProjectUC.ProjectUC_Status + "', convert(date, '" + obj_tbl_ProjectUC.ProjectUC_SubmitionDate + "', 103), '" + obj_tbl_ProjectUC.ProjectUC_PhysicalProgress + "', '" + obj_tbl_ProjectUC.ProjectUC_Latitude + "', '" + obj_tbl_ProjectUC.ProjectUC_Longitude + "', '" + obj_tbl_ProjectUC.ProjectUC_FinancialYear_Id + "', '" + obj_tbl_ProjectUC.ProjectUC_Total_Allocated + "', '" + obj_tbl_ProjectUC.ProjectUC_Centage + "'); select @@IDENTITY";
            if (trans == null)
            {
                try
                {
                    ds = ExecuteSelectQuery(strQuery);
                }
                catch
                {
                }
            }
            else
            {
                ds = ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
            }
            return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        }

        private void Insert_tbl_ProjectUC_Concent(tbl_ProjectUC_Concent objtbl_ProjectUC_Concent, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_ProjectUC_Concent (ProjectUC_Concent_ProjectUC_Id, ProjectUC_Concent_Questionire_Id, ProjectUC_Concent_Answer_Id, ProjectUC_Concent_AddedOn, ProjectUC_Concent_AddedBy, ProjectUC_Concent_Status, ProjectUC_Concent_Comments, ProjectUC_Concent_Answer) values ('" + objtbl_ProjectUC_Concent.ProjectUC_Concent_ProjectUC_Id + "','" + objtbl_ProjectUC_Concent.ProjectUC_Concent_Questionire_Id + "','" + objtbl_ProjectUC_Concent.ProjectUC_Concent_Answer_Id + "', getdate(), '" + objtbl_ProjectUC_Concent.ProjectUC_Concent_AddedBy + "','" + objtbl_ProjectUC_Concent.ProjectUC_Concent_Status + "','" + objtbl_ProjectUC_Concent.ProjectUC_Concent_Comments + "', N'" + objtbl_ProjectUC_Concent.ProjectUC_Concent_Answer + "')";
            if (trans == null)
            {
                try
                {
                    ExecuteSelectQuery(strQuery);
                }
                catch
                {
                }
            }
            else
            {
                ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
            }
        }

        public DataSet get_Dashboard_Data(int Person_Id, int Zone_Id, int Circle_Id, int Division_Id)
        {
            string strQuery = "";
            string sql = "";
            DataSet ds = new DataSet();
            if (Person_Id > 0)
            {
                sql = @"select PersonJuridiction_DivisionId, PersonJuridiction_CircleId, PersonJuridiction_ZoneId from tbl_PersonDetail join tbl_PersonJuridiction on Person_Id = PersonJuridiction_PersonId where Person_Id = '" + Person_Id + "' ";
                ds = ExecuteSelectQuery(sql);
                if (Utility.CheckDataSet(ds))
                {
                    try
                    {
                        Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_ZoneId"].ToString());
                    }
                    catch
                    {
                        Zone_Id = 0;
                    }
                    try
                    {
                        Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                    try
                    {
                        Division_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_DivisionId"].ToString());
                    }
                    catch
                    {
                        Division_Id = 0;
                    }
                }
            }
            strQuery = @"set dateformat dmy; 
                        select 
                            Total_Projects = count(*), 
							Running_Projects = sum(case when convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0)) < 100 then 1 else 0 end),
							Completed_Projects = sum(case when convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0)) >= 100 then 1 else 0 end)
                        from tbl_ProjectWork
                        join tbl_Project on Project_Id = ProjectWork_Project_Id
                        join M_Jurisdiction on M_Jurisdiction_Id = ProjectWork_DistrictId
                        left join tbl_ULB on ULB_Id = ProjectWork_ULB_Id
                        left join tbl_Division on Division_Id = ProjectWork_DivisionId
                        left join tbl_Circle on Circle_Id = Division_CircleId
                        left join tbl_Zone on Zone_Id = Circle_ZoneId
                        left join 
                        (
                            select 
                                row_number() over (partition by ProjectWorkFinancialTarget_ProjectWork_Id order by ProjectWorkFinancialTarget_Id desc) rrT,
                                ProjectWorkFinancialTarget_ProjectWork_Id, 
                                ProjectWorkFinancialTarget_Target, 
                                ProjectWorkPhysicalTarget_Target, 
		                        ProjectWorkFinancialTarget_AddedOn,  
		                        ProjectWorkFinancialTarget_Month, 
		                        ProjectWorkFinancialTarget_Year,
		                        DaysDiff = DATEDIFF(DD, ProjectWorkFinancialTarget_AddedOn, getdate())
                            from tbl_ProjectWorkFinancialTarget
                            where ProjectWorkFinancialTarget_Status = 1   
                        ) tTarget on tTarget.ProjectWorkFinancialTarget_ProjectWork_Id = tbl_ProjectWork.ProjectWork_Id and rrT = 1
                        where ProjectWork_Status = 1 ";
            if (Zone_Id > 0)
            {
                strQuery += " and Circle_ZoneId = '" + Zone_Id + "'";
            }
            if (Circle_Id > 0)
            {
                strQuery += " and Division_CircleId = '" + Circle_Id + "'";
            }
            if (Division_Id > 0)
            {
                strQuery += " and ProjectWork_DivisionId = '" + Division_Id + "'";
            }

            strQuery += Environment.NewLine;

            strQuery += @"set dateformat dmy; 
                        select 
	                        count(distinct ProjectWorkPkg_Id) Total_Running_Vendor
                        from tbl_ProjectWorkPkg
                        join tbl_ProjectWork on ProjectWork_Id = ProjectWorkPkg_Work_Id and ProjectWork_Status = 1
                        where ProjectWorkPkg_Status = 1 and ProjectWorkPkg_Vendor_Id = '" + Person_Id + "'; ";

            strQuery += Environment.NewLine;

            strQuery += @"set dateformat dmy; 
                        select 
	                        count(distinct ProjectWorkPkg_Id) Total_Running_Inspection
                        from tbl_ProjectWorkPkg
                        join tbl_ProjectWork on ProjectWork_Id = ProjectWorkPkg_Work_Id and ProjectWork_Status = 1
                        join tbl_ProjectPKGInspectionInfo on ProjectPKGInspectionInfo_ProjectPkg_Id = ProjectWorkPkg_Id and ProjectPKGInspectionInfo_ProjectWork_Id = ProjectWork_Id
                        where ProjectWorkPkg_Status = 1 and ProjectPKGInspectionInfo_Status = 1 and ProjectPKGInspectionInfo_InspectionPersonId = '" + Person_Id + "'; ";

            try
            {
                ds = ExecuteSelectQuery(strQuery);
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet get_Dashboard_Data2(int Person_Id, int Zone_Id, int Circle_Id, int Division_Id)
        {
            string strQuery = "";
            string sql = "";
            DataSet ds = new DataSet();
            if (Person_Id > 0)
            {
                sql = @"select PersonJuridiction_DivisionId, PersonJuridiction_CircleId, PersonJuridiction_ZoneId from tbl_PersonDetail join tbl_PersonJuridiction on Person_Id = PersonJuridiction_PersonId where Person_Id = '" + Person_Id + "' ";
                ds = ExecuteSelectQuery(sql);
                if (Utility.CheckDataSet(ds))
                {
                    try
                    {
                        Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_ZoneId"].ToString());
                    }
                    catch
                    {
                        Zone_Id = 0;
                    }
                    try
                    {
                        Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                    try
                    {
                        Division_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_DivisionId"].ToString());
                    }
                    catch
                    {
                        Division_Id = 0;
                    }
                }
            }
            strQuery = @"select 
                            ProjectWork_Id, 
                            ProjectWork_Project_Id, 
                            Project_Name, 
                            ProjectWork_ProjectCode, 
                            ProjectWork_Name, 
                            ProjectWork_GO_Path,
                            ProjectWork_GO_Date = convert(char(10), ProjectWork_GO_Date, 103), 
	                        ProjectWork_StartDate = convert(char(10), ProjectWork_StartDate, 103), 
	                        ProjectWork_EndDate = convert(char(10), ProjectWork_EndDate, 103), 
	                        ProjectWorkPkg_Agreement_Date,
	                        ProjectWorkPkg_Due_Date, 
	                        ProjectWorkPkg_Start_Date, 
	                        Target_Date_Agreement_Extended,
                            ProjectWork_GO_No,
                            ProjectWork_Budget = isnull(ProjectWork_Budget, 0) + isnull(ProjectWork_Centage, 0),
                            ProjectWork_ProjectType_Id,
	                        ULB_Name, 
	                        Jurisdiction_Name_Eng, 
	                        Division_Name, 
	                        Circle_Name, 
	                        Zone_Name, 
	                        ProjectWork_DistrictId, 
                            ProjectWork_BlockId,
	                        ProjectWork_ULB_Id, 
	                        ProjectWork_DivisionId, 
	                        Division_CircleId, 
                            Division_Id, 
	                        Circle_Id, 
	                        Zone_Id, 
	                        tender_cost, 
	                        ProjectWorkPkg_Agreement_Date,
	                        Financial_Progress = convert(decimal(18, 2), (case when isnull(tProjectWorkPkg.tender_cost, 0) > 0 then (((isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tPrevInvoice.Amount, 0)) * 100) / isnull(tProjectWorkPkg.tender_cost, 0)) else 0 end)), 
                            Physical_Progress = convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0))
                        from tbl_ProjectWork
                        join tbl_Project on Project_Id = ProjectWork_Project_Id
                        join M_Jurisdiction on M_Jurisdiction_Id = ProjectWork_DistrictId
                        left join tbl_ULB on ULB_Id = ProjectWork_ULB_Id
                        left join tbl_Division on Division_Id = ProjectWork_DivisionId
                        left join tbl_Circle on Circle_Id = Division_CircleId
                        left join tbl_Zone on Zone_Id = Circle_ZoneId
                        left join 
                        (
                        select 
	                        ProjectWorkPkg_Work_Id,
	                        tender_cost=sum(cast(ISNULL(ProjectWorkPkg_AgreementAmount,0) as decimal(18,2))), 
                            tender_cost_1 = convert(decimal(18,2), (sum(ISNULL(ProjectWorkPkg_AgreementAmount, 0)) / (1 + (max(isnull(ProjectWorkPkg_Percent, 12)) / 100)))), 
	                        ProjectWorkPkg_Due_Date = convert(char(10), max(ProjectWorkPkg_Due_Date), 103),
	                        ProjectWorkPkg_Start_Date = convert(char(10), min(ProjectWorkPkg_Start_Date), 103),
	                        ProjectWorkPkg_Agreement_Date = convert(char(10), min(ProjectWorkPkg_Agreement_Date), 103), 
	                        Target_Date_Agreement_Extended = convert(char(10), max(case when ProjectWorkPkg_ExtendDate is null then ProjectWorkPkg_Due_Date else ProjectWorkPkg_ExtendDate end), 103)
                        from (
	                        select 
		                        ProjectWorkPkg_Work_Id,
		                        ProjectWorkPkg_AgreementAmount = case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then (ProjectWorkPkg_AgreementAmount + ProjectWorkPkg_AgreementAmount * isnull(ProjectWorkPkg_Percent, 12) / 100) else ProjectWorkPkg_AgreementAmount end, 
		                        ProjectWorkPkg_Due_Date,
		                        ProjectWorkPkg_Start_Date,
		                        ProjectWorkPkg_Agreement_Date, 
		                        ProjectWorkPkg_ExtendDate, 
		                        ProjectWorkPkg_Percent
	                        from tbl_ProjectWorkPkg 
	                        where 	ProjectWorkPkg_Status=1	
	                        ) tData
	                        group by ProjectWorkPkg_Work_Id
                        )tProjectWorkPkg  on tProjectWorkPkg.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id
                        left join
                        (
	                        select 
		                        ProjectWorkPkg_Work_Id, 
		                        Total_Amount = sum(cast((ISNULL(FinancialTrans_TransAmount,0) / 100000) as decimal(18,2))), 
		                        Amount = sum(cast((ISNULL(FinancialTrans_Amount,0) / 100000) as decimal(18,2))), 
		                        GST = sum(cast((ISNULL(FinancialTrans_GST,0) / 100000) as decimal(18,2))) 
	                        from tbl_PackageInvoice
	                        join tbl_ProjectWorkPkg on ProjectWorkPkg_Id = PackageInvoice_Package_Id
	                        inner join tbl_FinancialTrans on FinancialTrans_Invoice_Id=PackageInvoice_Id
	                        where PackageInvoice_Status = 1 and FinancialTrans_EntryType = 'Fund Allocated' and FinancialTrans_TransType = 'C' and PackageInvoice_Id not in (select PackageInvoiceEMBMasterLink_Invoice_Id from tbl_PackageInvoiceEMBMasterLink where PackageInvoiceEMBMasterLink_Status=1) 
	                        group by ProjectWorkPkg_Work_Id
                        ) tPrevInvoice on tPrevInvoice.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id
                        left join
                        (
                            select 
		                        ProjectWorkPkg_Work_Id,
		                        Amount = sum(cast((ISNULL(FinancialTrans_Amount,0) / 100000) as decimal(18,4))) 
	                        from tbl_Package_ADP
	                        inner join tbl_FinancialTrans on FinancialTrans_Invoice_Id = Package_ADP_Id 
	                        join tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
	                        where Package_ADP_Status = 1 and FinancialTrans_EntryType = 'ADP' and FinancialTrans_TransType = 'C' 
	                        group by ProjectWorkPkg_Work_Id
                        ) tPrevInvoiceADP on tPrevInvoiceADP.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id
                        left join 
                        (
	                        select 
		                        ProjectWorkPkg_Work_Id,
		                        Total_Invoice = count(*), 
		                        Total_Invoice_Value = sum(isnull(InvoiceAmount, 0)) / 100000, 
		                        Deffered_Value = sum(case when (isnull(PackageInvoiceApproval_Next_Designation_Id, 0) = 0 and isnull(PackageInvoiceApproval_Next_Organisation_Id, 0) = 0 and  PackageInvoiceApproval_Status_Id not in (1, 6)) then isnull(InvoiceAmount, 0) else 0 end) / 100000
	                        from tbl_PackageInvoice 
	                        join tbl_ProjectWorkPkg on ProjectWorkPkg_Id = PackageInvoice_Package_Id
	                        join 
	                        (
		                        select 
			                        ROW_NUMBER() over (partition by PackageInvoiceApproval_PackageInvoice_Id order by PackageInvoiceApproval_Id desc) rrrrr,
			                        PackageInvoiceApproval_Next_Designation_Id,
			                        PackageInvoiceApproval_Next_Organisation_Id,
			                        PackageInvoiceApproval_Comments,
			                        PackageInvoiceApproval_AddedBy,
			                        PackageInvoiceApproval_AddedOn,
			                        PackageInvoiceApproval_Status_Id,
			                        PackageInvoiceApproval_Package_Id,
			                        PackageInvoiceApproval_PackageInvoice_Id,
			                        InvoiceStatus_Name,
			                        PackageInvoiceApproval_Date = convert(char(10), PackageInvoiceApproval_Date, 103),
			                        PackageInvoiceApproval_Id
		                        from tbl_PackageInvoiceApproval
		                        left join tbl_InvoiceStatus on InvoiceStatus_Id = PackageInvoiceApproval_Status_Id
		                        where PackageInvoiceApproval_Status = 1
	                        ) tInvoiceApproval on tInvoiceApproval.PackageInvoiceApproval_PackageInvoice_Id = PackageInvoice_Id and tInvoiceApproval.rrrrr = 1
	                        where PackageInvoice_Status = 1 and ProjectWorkPkg_Status = 1  
	                        group by ProjectWorkPkg_Work_Id
                        ) tInvoice on tInvoice.ProjectWorkPkg_Work_Id = ProjectWork_Id 
                        left join 
                        (
                            select 
                                row_number() over (partition by ProjectWorkFinancialTarget_ProjectWork_Id order by ProjectWorkFinancialTarget_Id desc) rrT,
                                ProjectWorkFinancialTarget_ProjectWork_Id, 
                                ProjectWorkFinancialTarget_Target, 
                                ProjectWorkPhysicalTarget_Target, 
		                        ProjectWorkFinancialTarget_AddedOn,  
		                        ProjectWorkFinancialTarget_Month, 
		                        ProjectWorkFinancialTarget_Year,
		                        DaysDiff = DATEDIFF(DD, ProjectWorkFinancialTarget_AddedOn, getdate())
                            from tbl_ProjectWorkFinancialTarget
                            where ProjectWorkFinancialTarget_Status = 1   
                        ) tTarget on tTarget.ProjectWorkFinancialTarget_ProjectWork_Id = tbl_ProjectWork.ProjectWork_Id and rrT = 1
                        where ProjectWork_Status = 1 ";
            if (Zone_Id > 0)
            {
                strQuery += " and Circle_ZoneId = '" + Zone_Id + "'";
            }
            if (Circle_Id > 0)
            {
                strQuery += " and Division_CircleId = '" + Circle_Id + "'";
            }
            if (Division_Id > 0)
            {
                strQuery += " and ProjectWork_DivisionId = '" + Division_Id + "'";
            }

            try
            {
                ds = ExecuteSelectQuery(strQuery);
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet get_ComiteeMember(int ProjectDPR_Id, int ProjectWork_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @" set dateformat dmy; 
                        select 
	                        Person_Id,
	                        UserType_Desc_E, 
	                        Person_Name, 
	                        Person_Mobile1, 
	                        Person_Mobile2,
	                        Designation_DesignationName
                        from tbl_ProjectDPR_JalNigamInspectionInfo
                        join tbl_PersonDetail on Person_Id = ProjectDPRInspectionInfo_InspectionPersonId
                        join tbl_PersonJuridiction on PersonJuridiction_PersonId = ProjectDPRInspectionInfo_InspectionPersonId
                        left join tbl_Designation on Designation_Id = PersonJuridiction_DesignationId
                        join tbl_UserType on UserType_Id = PersonJuridiction_UserTypeId
                        where ProjectDPRInspectionInfo_Status = 1 and ProjectDPRInspectionInfo_ProjectDPR_Id = '" + ProjectDPR_Id + "' and ProjectDPRInspectionInfo_ProjectWork_Id = '" + ProjectWork_Id + "' ";
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

        public DataSet get_FinancialRelease_Breakup(int ProjectDPR_Id, int ProjectWork_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; 
                    select 
	                    ProjectWorkGO_Id,
                        ProjectWorkGO_Work_Id,
						ProjectWork_GO_Count = isnull(ProjectWork_GO_Count, 0),
                        ProjectWorkGO_GO_Date = convert(char(10), ProjectWorkGO_GO_Date, 103),
                        ProjectWorkGO_GO_Number,
                        ProjectWorkGO_TotalRelease = convert(decimal(18,2), (isnull(ProjectWorkGO_TotalRelease,0) / 100000)),
	                    ProjectWorkGO_CentralShare = convert(decimal(18,2), (isnull(ProjectWorkGO_CentralShare,0) / 100000)),
	                    ProjectWorkGO_StateShare = convert(decimal(18,2), (isnull(ProjectWorkGO_StateShare,0) / 100000)),
	                    ProjectWorkGO_ULBShare = convert(decimal(18,2), (isnull(ProjectWorkGO_ULBShare,0) / 100000)), 
	                    ProjectWorkGO_Centage = convert(decimal(18,2), (isnull(ProjectWorkGO_Centage,0) / 100000)), 
                        ProjectWorkGO_Document_Path, 
                        ProjectWorkGO_IssuingAuthority, 
                        ProjectWorkGO_ULB_Id
                        ULBName, 
                        GO_Total = convert(decimal(18,2), (isnull(ProjectWorkGO_CentralShare,0) / 100000)) + convert(decimal(18,2), (isnull(ProjectWorkGO_StateShare,0) / 100000)) + convert(decimal(18,2), (isnull(ProjectWorkGO_ULBShare,0) / 100000)), 
						ProjectWorkGO_EntryType
                    from tbl_ProjectWorkGO
					join tbl_ProjectWork on ProjectWork_Id = ProjectWorkGO_Work_Id 
					left join tbl_ULB on ULB_Id = ProjectWorkGO_ULB_Id
                    where ProjectWorkGO_Status = 1 and ProjectWorkGO_Work_Id = '" + ProjectDPR_Id + "' and ProjectWorkGO_EntryType in ('S', 'U') order by convert(date, ProjectWorkGO_GO_Date, 103) ";
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

        public DataSet get_Site_Pics(int ProjectDPR_Id, int ProjectWork_Id, int ProjectUC_Id, string Date)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; 
                        select 
	                        Person_Name,
	                        UserType_Desc_E = Designation_DesignationName,
	                        ProjectDPRSitePics_Id = ProjectWorkGallery_Id,
	                        ProjectDPRSitePics_ProjectDPR_Id = ProjectWorkGallery_Work_Id,
	                        ProjectDPRSitePics_ProjectWork_Id = ProjectWorkGallery_Work_Id,
	                        ProjectDPRSitePics_ReportSubmittedBy_PersonId = ProjectWorkGallery_AddedBy,
	                        ProjectDPRSitePics_ReportSubmitted = isnull(Person_Name, '') + ', Mob: ' + isnull(Person_Mobile1, ''),
	                        ProjectDPRSitePics_SitePic_Path1 = ProjectWorkGallery_Path,
	                        ProjectDPRSitePics_SitePic_Type = 'Gallery Photos', 
                            ProjectDPRSitePics_AddedOn = convert(char(10), convert(date, ProjectWorkGallery_AddedOn, 103), 103), 
                            ProjectWorkGallery_Comments
                        from tbl_ProjectWorkGallery 
                        left join tbl_PersonDetail on Person_Id = ProjectWorkGallery_AddedBy
                        join tbl_PersonJuridiction on Person_Id = PersonJuridiction_PersonId
                        left join tbl_Designation on Designation_Id = PersonJuridiction_DesignationId
                        left join tbl_UserType on UserType_Id = PersonJuridiction_UserTypeId
                        where ProjectWorkGallery_Status = 1 ";
            if (ProjectDPR_Id > 0)
            {
                strQuery += " and ProjectWorkGallery_Work_Id = '" + ProjectDPR_Id + "'";
            }
            if (ProjectUC_Id > 0)
            {
                //strQuery += " and ProjectDPRSitePics_UCDetails_Id = '" + ProjectUC_Id + "'";
            }
            if (Date != "")
            {
                strQuery += " and convert(date, ProjectWorkGallery_AddedOn, 103) = convert(date, '" + Date + "', 103)";
            }
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

        public DataSet get_DPR_UC_Details(int ProjectDPR_Id, int ProjectWork_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; 
                        select 
	                        ProjectUC_Id = ROW_NUMBER() over (order by convert(date, ProjectWorkGallery_AddedOn, 103), ProjectWorkGallery_AddedBy, Designation_DesignationName),
	                        ProjectUC_ProjectWork_Id = ProjectWorkGallery_Work_Id,
	                        ProjectUC_ProjectDPR_Id = ProjectWorkGallery_Work_Id,
	                        ProjectUC_Achivment = 0,
	                        ProjectUC_SubmitionDate = convert(char(10), convert(date, ProjectWorkGallery_AddedOn, 103), 103), 
	                        ProjectUC_BudgetUtilized = 0,
	                        ProjectUC_Comments = max(ProjectWorkGallery_Comments),
	                        ProjectUC_PhysicalProgress = 0,
	                        ProjectUC_Total_Allocated = 0,
	                        ProjectUC_Latitude = max(ProjectWorkGallery_Latitude),
	                        ProjectUC_Longitude = max(ProjectWorkGallery_Longitude), 
	                        Person_Name, 
	                        Person_Mobile1, 
	                        Level_Name = Designation_DesignationName,
	                        ProjectUC_AddedBy = ProjectWorkGallery_AddedBy, 
	                        count(*) TotalPic
                        from tbl_ProjectWorkGallery 
                        left join tbl_PersonDetail on Person_Id = ProjectWorkGallery_AddedBy
                        join tbl_PersonJuridiction on Person_Id = PersonJuridiction_PersonId
						left join tbl_Designation on Designation_Id = PersonJuridiction_DesignationId
                        left join tbl_UserType on UserType_Id = PersonJuridiction_UserTypeId
                        where ProjectWorkGallery_Status = 1 and ProjectWorkGallery_Work_Id = '" + ProjectDPR_Id + "' group by convert(date, ProjectWorkGallery_AddedOn, 103), ProjectWorkGallery_AddedBy, ProjectWorkGallery_Work_Id, Person_Name, Person_Mobile1, UserType_Desc_E, PersonJuridiction_DesignationId, Designation_DesignationName ";
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

        public DataSet get_tbl_DPRQuestionnaire(int Project_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @" set dateformat dmy; 
                    select 
                        DPRQuestionnaire_Id,
                        DPRQuestionnaire_ProjectId,
                        DPRQuestionnaire_Name,
                        DPRQuestionnaire_AddedBy,
                        DPRQuestionnaire_AddedOn,
                        DPRQuestionnaire_Status
                    from tbl_DPRQuestionnaire 
                    where DPRQuestionnaire_Status = 1 ";
            if (Project_Id > 0)
            {
                strQuery += " and DPRQuestionnaire_ProjectId = '" + Project_Id + "'";
            }
            strQuery += " order by DPRQuestionnaire_Name";
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

        public DataSet get_Scheme_Wise_Report(SearchCriteria obj_SearchCriteria, int Zone_Id_Search)
        {
            string strQuery = "";
            string sql = "";
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; 
                        select 
	                        *, Expenditure = convert(decimal(18, 2), (Fund_Released * Financial_Progress) / 100) 
                        from (
                        select 
                            Project_Id, 
	                        Project_Name, 
	                        Project_Budget = sum(isnull(ProjectWork_Budget, 0) + isnull(ProjectWork_Centage, 0)),
	                        Total_ULB = count(distinct ProjectWork_DivisionId),
	                        Total_Work = count(ProjectWork_Id),
	                        ProjectDPR_BudgetAllocated = sum(isnull(tender_cost, 0)),
	                        Fund_Released = sum(isnull(tender_cost, 0)),
	                        Financial_Progress = convert(decimal(18, 2), AVG(convert(decimal(18, 2), (case when isnull(tProjectWorkPkg.tender_cost, 0) > 0 then (((isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tPrevInvoice.Amount, 0)) * 100) / isnull(tProjectWorkPkg.tender_cost, 0)) else 0 end)))), 
                            Physical_Progress = convert(decimal(18, 2), AVG(convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0))))
                        from tbl_ProjectWork
                        join tbl_Project on Project_Id = ProjectWork_Project_Id
                        join M_Jurisdiction on M_Jurisdiction_Id = ProjectWork_DistrictId
                        left join tbl_ULB on ULB_Id = ProjectWork_ULB_Id
                        left join tbl_Division on Division_Id = ProjectWork_DivisionId
                        left join tbl_Circle on Circle_Id = Division_CircleId
                        left join tbl_Zone on Zone_Id = Circle_ZoneId
                        left join 
                        (
                        select 
	                        ProjectWorkPkg_Work_Id,
	                        tender_cost=sum(cast(ISNULL(ProjectWorkPkg_AgreementAmount,0) as decimal(18,2))), 
                            tender_cost_1 = convert(decimal(18,2), (sum(ISNULL(ProjectWorkPkg_AgreementAmount, 0)) / (1 + (max(isnull(ProjectWorkPkg_Percent, 12)) / 100)))), 
	                        ProjectWorkPkg_Due_Date = convert(char(10), max(ProjectWorkPkg_Due_Date), 103),
	                        ProjectWorkPkg_Start_Date = convert(char(10), min(ProjectWorkPkg_Start_Date), 103),
	                        ProjectWorkPkg_Agreement_Date = convert(char(10), min(ProjectWorkPkg_Agreement_Date), 103), 
	                        Target_Date_Agreement_Extended = convert(char(10), max(case when ProjectWorkPkg_ExtendDate is null then ProjectWorkPkg_Due_Date else ProjectWorkPkg_ExtendDate end), 103)
                        from (
	                        select 
		                        ProjectWorkPkg_Work_Id,
		                        ProjectWorkPkg_AgreementAmount = case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then (ProjectWorkPkg_AgreementAmount + ProjectWorkPkg_AgreementAmount * isnull(ProjectWorkPkg_Percent, 12) / 100) else ProjectWorkPkg_AgreementAmount end, 
		                        ProjectWorkPkg_Due_Date,
		                        ProjectWorkPkg_Start_Date,
		                        ProjectWorkPkg_Agreement_Date, 
		                        ProjectWorkPkg_ExtendDate, 
		                        ProjectWorkPkg_Percent
	                        from tbl_ProjectWorkPkg 
	                        where 	ProjectWorkPkg_Status=1	
	                        ) tData
	                        group by ProjectWorkPkg_Work_Id
                        )tProjectWorkPkg  on tProjectWorkPkg.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id
                        left join
                        (
	                        select 
		                        ProjectWorkPkg_Work_Id, 
		                        Total_Amount = sum(cast((ISNULL(FinancialTrans_TransAmount,0) / 100000) as decimal(18,2))), 
		                        Amount = sum(cast((ISNULL(FinancialTrans_Amount,0) / 100000) as decimal(18,2))), 
		                        GST = sum(cast((ISNULL(FinancialTrans_GST,0) / 100000) as decimal(18,2))) 
	                        from tbl_PackageInvoice
	                        join tbl_ProjectWorkPkg on ProjectWorkPkg_Id = PackageInvoice_Package_Id
	                        inner join tbl_FinancialTrans on FinancialTrans_Invoice_Id=PackageInvoice_Id
	                        where PackageInvoice_Status = 1 and FinancialTrans_EntryType = 'Fund Allocated' and FinancialTrans_TransType = 'C' and PackageInvoice_Id not in (select PackageInvoiceEMBMasterLink_Invoice_Id from tbl_PackageInvoiceEMBMasterLink where PackageInvoiceEMBMasterLink_Status=1) 
	                        group by ProjectWorkPkg_Work_Id
                        ) tPrevInvoice on tPrevInvoice.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id
                        left join
                        (
                            select 
		                        ProjectWorkPkg_Work_Id,
		                        Amount = sum(cast((ISNULL(FinancialTrans_Amount,0) / 100000) as decimal(18,4))) 
	                        from tbl_Package_ADP
	                        inner join tbl_FinancialTrans on FinancialTrans_Invoice_Id = Package_ADP_Id 
	                        join tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
	                        where Package_ADP_Status = 1 and FinancialTrans_EntryType = 'ADP' and FinancialTrans_TransType = 'C' 
	                        group by ProjectWorkPkg_Work_Id
                        ) tPrevInvoiceADP on tPrevInvoiceADP.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id
                        left join 
                        (
	                        select 
		                        ProjectWorkPkg_Work_Id,
		                        Total_Invoice = count(*), 
		                        Total_Invoice_Value = sum(isnull(InvoiceAmount, 0)) / 100000, 
		                        Deffered_Value = sum(case when (isnull(PackageInvoiceApproval_Next_Designation_Id, 0) = 0 and isnull(PackageInvoiceApproval_Next_Organisation_Id, 0) = 0 and  PackageInvoiceApproval_Status_Id not in (1, 6)) then isnull(InvoiceAmount, 0) else 0 end) / 100000
	                        from tbl_PackageInvoice 
	                        join tbl_ProjectWorkPkg on ProjectWorkPkg_Id = PackageInvoice_Package_Id
	                        join 
	                        (
		                        select 
			                        ROW_NUMBER() over (partition by PackageInvoiceApproval_PackageInvoice_Id order by PackageInvoiceApproval_Id desc) rrrrr,
			                        PackageInvoiceApproval_Next_Designation_Id,
			                        PackageInvoiceApproval_Next_Organisation_Id,
			                        PackageInvoiceApproval_Comments,
			                        PackageInvoiceApproval_AddedBy,
			                        PackageInvoiceApproval_AddedOn,
			                        PackageInvoiceApproval_Status_Id,
			                        PackageInvoiceApproval_Package_Id,
			                        PackageInvoiceApproval_PackageInvoice_Id,
			                        InvoiceStatus_Name,
			                        PackageInvoiceApproval_Date = convert(char(10), PackageInvoiceApproval_Date, 103),
			                        PackageInvoiceApproval_Id
		                        from tbl_PackageInvoiceApproval
		                        left join tbl_InvoiceStatus on InvoiceStatus_Id = PackageInvoiceApproval_Status_Id
		                        where PackageInvoiceApproval_Status = 1
	                        ) tInvoiceApproval on tInvoiceApproval.PackageInvoiceApproval_PackageInvoice_Id = PackageInvoice_Id and tInvoiceApproval.rrrrr = 1
	                        where PackageInvoice_Status = 1 and ProjectWorkPkg_Status = 1  
	                        group by ProjectWorkPkg_Work_Id
                        ) tInvoice on tInvoice.ProjectWorkPkg_Work_Id = ProjectWork_Id 
                        left join 
                        (
                            select 
                                row_number() over (partition by ProjectWorkFinancialTarget_ProjectWork_Id order by ProjectWorkFinancialTarget_Id desc) rrT,
                                ProjectWorkFinancialTarget_ProjectWork_Id, 
                                ProjectWorkFinancialTarget_Target, 
                                ProjectWorkPhysicalTarget_Target, 
		                        ProjectWorkFinancialTarget_AddedOn,  
		                        ProjectWorkFinancialTarget_Month, 
		                        ProjectWorkFinancialTarget_Year,
		                        DaysDiff = DATEDIFF(DD, ProjectWorkFinancialTarget_AddedOn, getdate())
                            from tbl_ProjectWorkFinancialTarget
                            where ProjectWorkFinancialTarget_Status = 1   
                        ) tTarget on tTarget.ProjectWorkFinancialTarget_ProjectWork_Id = tbl_ProjectWork.ProjectWork_Id and rrT = 1
                        where ProjectWork_Status = 1 Zone_IdCond Circle_IdCond Division_IdCond Project_StatusCond
                        group by Project_Id, Project_Name
                        ) tData order by Project_Name";
            if (obj_SearchCriteria.Project_Status == "Completed")
            {
                strQuery = strQuery.Replace("Project_StatusCond", " and isnull(convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0)), 0) >= 100 ");
            }
            else if (obj_SearchCriteria.Project_Status == "Running")
            {
                strQuery = strQuery.Replace("Project_StatusCond", " and isnull(convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0)), 0) < 100 ");
            }
            else
            {
                strQuery = strQuery.Replace("Project_StatusCond", "");
            }
            if (obj_SearchCriteria.Person_Id > 0)
            {
                sql = @"select PersonJuridiction_DivisionId, PersonJuridiction_CircleId, PersonJuridiction_ZoneId from tbl_PersonDetail join tbl_PersonJuridiction on Person_Id = PersonJuridiction_PersonId where Person_Id = '" + obj_SearchCriteria.Person_Id + "' ";
                ds = ExecuteSelectQuery(sql);
                if (Utility.CheckDataSet(ds))
                {
                    try
                    {
                        if (Zone_Id_Search > 0)
                        {
                            obj_SearchCriteria.Zone_Id = Zone_Id_Search;
                        }
                        else
                        {
                            obj_SearchCriteria.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_ZoneId"].ToString());
                        }
                    }
                    catch
                    {
                        obj_SearchCriteria.Zone_Id = 0;
                    }
                    try
                    {
                        obj_SearchCriteria.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        obj_SearchCriteria.Circle_Id = 0;
                    }
                    try
                    {
                        obj_SearchCriteria.Division_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_DivisionId"].ToString());
                    }
                    catch
                    {
                        obj_SearchCriteria.Division_Id = 0;
                    }
                }
            }

            if (obj_SearchCriteria.Zone_Id > 0)
            {
                strQuery = strQuery.Replace("Zone_IdCond", " and Circle_ZoneId = " + obj_SearchCriteria.Zone_Id.ToString());
            }
            else
            {
                strQuery = strQuery.Replace("Zone_IdCond", "");
            }
            if (obj_SearchCriteria.Circle_Id > 0)
            {
                strQuery = strQuery.Replace("Circle_IdCond", " and Division_CircleId = " + obj_SearchCriteria.Circle_Id.ToString());
            }
            else
            {
                strQuery = strQuery.Replace("Circle_IdCond", "");
            }
            if (obj_SearchCriteria.Division_Id > 0)
            {
                strQuery = strQuery.Replace("Division_IdCond", " and ProjectWork_DivisionId = " + obj_SearchCriteria.Division_Id.ToString());
            }
            else
            {
                strQuery = strQuery.Replace("Division_IdCond", "");
            }
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

        public DataSet get_tbl_TicketCategory()
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; 
                    select 
                        TicketCategory_Id, 
                        TicketCategory_Name, 
                        TicketCategory_AddedOn, 
                        TicketCategory_AddedBy, 
                        TicketCategory_Status, 
                        isnull(tbl_PersonDetail.Person_Name, 'Backend Entry') CreatedBy, 
                        Created_Date = TicketCategory_AddedOn,
                        tbl_PersonDetail1.Person_Name as ModifyBy, 
                        Modify_Date = TicketCategory_ModifiedOn 
                      from tbl_TicketCategory
                      left join tbl_PersonDetail on Person_Id = TicketCategory_AddedBy
                      left join tbl_PersonDetail as tbl_PersonDetail1 on tbl_PersonDetail1.Person_Id = TicketCategory_ModifiedBy
                      where TicketCategory_Status = 1 order by TicketCategory_Name";
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

        public DataSet insert_tbl_DeviceInfo(tbl_DeviceInfo obj_tbl_DeviceInfo)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = " set dateformat dmy; insert into tbl_DeviceInfo ([BRAND],[CPU_ABI],[CPU_ABI2],[Created_Date],[DEVICE],[DEVICE_ID],[DISPLAY],[HARDWARE],[HOST],[ID],[Is_Active],[MANUFACTURER],[MODEL],[PRODUCT],[RELEASE],[SERIAL],[UNKNOWN],[USER], [Person_Id], [Mobile_No], [Latitude], [Longitude], [App_Version]) values ('" + obj_tbl_DeviceInfo.BRAND + "','" + obj_tbl_DeviceInfo.CPU_ABI + "','" + obj_tbl_DeviceInfo.CPU_ABI2 + "',getdate(),'" + obj_tbl_DeviceInfo.DEVICE + "','" + obj_tbl_DeviceInfo.DEVICE_ID + "','" + obj_tbl_DeviceInfo.DISPLAY + "','" + obj_tbl_DeviceInfo.HARDWARE + "','" + obj_tbl_DeviceInfo.HOST + "','" + obj_tbl_DeviceInfo.ID + "',1,'" + obj_tbl_DeviceInfo.MANUFACTURER + "','" + obj_tbl_DeviceInfo.MODEL + "','" + obj_tbl_DeviceInfo.PRODUCT + "','" + obj_tbl_DeviceInfo.RELEASE + "','" + obj_tbl_DeviceInfo.SERIAL + "','" + obj_tbl_DeviceInfo.UNKNOWN + "','" + obj_tbl_DeviceInfo.USER + "', '" + obj_tbl_DeviceInfo.Person_Id + "', '" + obj_tbl_DeviceInfo.Mobile_No + "', '" + obj_tbl_DeviceInfo.Latitude + "', '" + obj_tbl_DeviceInfo.Longitude + "', '" + obj_tbl_DeviceInfo.App_Version + "');Select @@Identity";
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

        public DataSet get_tbl_Zone(int Zone_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @" set dateformat dmy; 
                        select 
                            Zone_Id, 
                            Zone_Name, 
                            Zone_AddedOn, 
                            Zone_AddedBy, 
                            Zone_Status
                          from tbl_Zone
                          where Zone_Status = 1 ";
            if (Zone_Id > 0)
            {
                strQuery += " and Zone_Id = '" + Zone_Id + "'";
            }
            strQuery += " order by Zone_Name";

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

        public DataSet get_tbl_Circle(int Zone_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @" set dateformat dmy; 
                    select 
                        Circle_Id, 
                        Circle_ZoneId, 
                        Zone_Name, 
                        Circle_Name, 
                        Circle_AddedOn, 
                        Circle_AddedBy, 
                        Circle_Status 
                    from tbl_Circle
                    join tbl_Zone on Zone_Id = Circle_ZoneId
                    where Circle_Status = 1";
            if (Zone_Id != 0)
            {
                strQuery += " and Circle_ZoneId = '" + Zone_Id + "'";
            }
            strQuery += " order by Zone_Name, Circle_Name";
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

        public DataSet get_tbl_Division(int Circle_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @" set dateformat dmy; 
                    select 
                        Division_Id, 
                        Division_CircleId, 
                        Circle_Name, 
                        Division_Name, 
                        Division_AddedOn, 
                        Division_AddedBy, 
                        Division_Status 
                    from tbl_Division
                    join tbl_Circle on Circle_Id = Division_CircleId
                    where Division_Status = 1";
            if (Circle_Id != 0)
            {
                strQuery += " and Division_CircleId = '" + Circle_Id + "'";
            }
            strQuery += " order by Circle_Name, Division_Name";
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

        public DataSet get_tbl_Profile(int Person_Id)
        {
            DataSet dt = new DataSet();
            string strQuery = @"set dateformat dmy; 
                                select 
	                                Person_Id,
							        Person_Name,
							        Person_FName,
							        Person_Mobile1,
							        Person_Mobile2,
							        Person_TelePhone,
							        Person_EmailId,
							        Person_AddressLine1,
							        Person_AddressLine2,
                                    Person_EmailId,
							        Person_ProfilePIC, 
	                                Designation_DesignationName, 
							        Department_Name, 
							        Zone_Name, 
							        Circle_Name, 
							        Division_Name
                                from tbl_PersonDetail 
						        join tbl_PersonJuridiction on Person_Id = PersonJuridiction_PersonId
						        left join tbl_Designation on Designation_Id = PersonJuridiction_DesignationId
						        left join tbl_Department on Department_Id = PersonJuridiction_DepartmentId
						        left join tbl_UserType on UserType_Id = PersonJuridiction_UserTypeId
						        left join tbl_Zone on Zone_Id = PersonJuridiction_ZoneId
						        left join tbl_Circle on Zone_Id = PersonJuridiction_CircleId
						        left join tbl_Division on Zone_Id = PersonJuridiction_DivisionId
						        where Person_Status = 1 and Person_Id = " + Person_Id.ToString();
            dt = ExecuteSelectQuery(strQuery);

            return dt;
        }

        public bool Update_tbl_Profile(tbl_Profile obj_tbl_Profile)
        {
            string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            using (SqlConnection cn = new SqlConnection(ConStr))
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlTransaction trans = cn.BeginTransaction();
                try
                {
                    string baseURL = ConfigurationManager.AppSettings.Get("BaseURL");
                    if (saveImageData(obj_tbl_Profile.Profile_Pic_File, obj_tbl_Profile.Profile_Base_64, baseURL + "\\Downloads\\" + obj_tbl_Profile.Profile_Pic_File, null, null, null))
                    {
                        obj_tbl_Profile.Profile_Pic_File = "\\Downloads\\" + obj_tbl_Profile.Profile_Pic_File;
                    }
                    else
                    {
                        obj_tbl_Profile.Profile_Pic_File = "";
                    }
                    string sql = "set dateformat dmy; update tbl_PersonDetail set Person_Name = '" + obj_tbl_Profile.Profile_Name + "', Person_Mobile1 = '" + obj_tbl_Profile.Profile_Mobile + "', Person_EmailId = '" + obj_tbl_Profile.Profile_Email + "', Person_AddressLine1 = '" + obj_tbl_Profile.Profile_Address + "', Person_ProfilePIC = '" + obj_tbl_Profile.Profile_Pic_File + "', Person_ModifiedBy = '" + obj_tbl_Profile.Person_Id + "', Person_ModifiedOn = getdate() where Person_Id = '" + obj_tbl_Profile.Person_Id + "'";
                    ExecuteSelectQuerywithTransaction(cn, sql, trans);

                    trans.Commit();
                    cn.Close();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    cn.Close();
                    return false;
                }
            }
        }

        public DataSet get_ProjectPkg_PhysicalProgress(int ProjectWork_Id, int Package_Id)
        {
            DataSet dt = new DataSet();
            string strQuery = @"set dateformat dmy; 
                                select 
	                                PhysicalProgressComponent_Id,
                                    PhysicalProgressComponent_Component,
                                    Unit_Name,
                                    PhysicalProgress_Total = isnull(tbl_ProjectUC_PhysicalProgressTotal.ProjectUC_PhysicalProgress_PhysicalProgress,0)
                                from tbl_ProjectPkg_PhysicalProgress
                                left join tbl_PhysicalProgressComponent on ProjectPkg_PhysicalProgress_PhysicalProgressComponent_Id=PhysicalProgressComponent_Id  
                                inner join tbl_Unit on Unit_Id=PhysicalProgressComponent_Unit_Id

                                left join 
                                (
	                                select 
		                                ROW_NUMBER() Over(Partition by ProjectUC_PhysicalProgress_ProjectWork_Id,ProjectUC_PhysicalProgress_ProjectPkg_Id,ProjectUC_PhysicalProgress_PhysicalProgressComponent_Id order by ProjectUC_PhysicalProgress_Id desc) as rr,* 
	                                from tbl_ProjectUC_PhysicalProgress 
	                                where ProjectUC_PhysicalProgress_Status=1
                                ) as tbl_ProjectUC_PhysicalProgressTotal on tbl_ProjectUC_PhysicalProgressTotal.rr=1 and tbl_ProjectUC_PhysicalProgressTotal.ProjectUC_PhysicalProgress_ProjectWork_Id = ProjectPkg_PhysicalProgress_PrjectWork_Id and tbl_ProjectUC_PhysicalProgressTotal.ProjectUC_PhysicalProgress_ProjectPkg_Id = ProjectPkg_PhysicalProgress_ProjectPkg_Id and tbl_ProjectUC_PhysicalProgressTotal.ProjectUC_PhysicalProgress_PhysicalProgressComponent_Id = ProjectPkg_PhysicalProgress_PhysicalProgressComponent_Id
                                where ProjectPkg_PhysicalProgress_Status = 1 and ProjectPkg_PhysicalProgress_PrjectWork_Id = '" + ProjectWork_Id + "' and ProjectPkg_PhysicalProgress_ProjectPkg_Id='" + Package_Id + "' ";
            dt = ExecuteSelectQuery(strQuery);

            return dt;
        }

        public DataSet get_ProjectPkg_Deliverables(int ProjectWork_Id, int Package_Id)
        {
            DataSet dt = new DataSet();
            string strQuery = @"set dateformat dmy; 
                                select 
	                                Deliverables_Id,
                                    Deliverables_Deliverables,
                                    Unit_Name,
                                    Deliverables_Total = isnull(tbl_ProjectUC_DeliverablesTotal.ProjectUC_Deliverables_Deliverables,0)
                                from tbl_ProjectPkg_Deliverables
                                left join tbl_Deliverables on ProjectPkg_Deliverables_Deliverables_Id=Deliverables_Id  
                                inner join tbl_Unit on Unit_Id=Deliverables_Unit_Id
                                left join 
                                (
	                                select 
		                                ROW_NUMBER() Over(Partition by ProjectUC_Deliverables_ProjectWork_Id,ProjectUC_Deliverables_ProjectPkg_Id,ProjectUC_Deliverables_Deliverables_Id order by ProjectUC_Deliverables_Id desc) as rr,* 
	                                from tbl_ProjectUC_Deliverables 
	                                where ProjectUC_Deliverables_Status=1
                                ) as tbl_ProjectUC_DeliverablesTotal on tbl_ProjectUC_DeliverablesTotal.rr = 1 and tbl_ProjectUC_DeliverablesTotal.ProjectUC_Deliverables_ProjectWork_Id = ProjectPkg_Deliverables_ProjectWork_Id and tbl_ProjectUC_DeliverablesTotal.ProjectUC_Deliverables_ProjectPkg_Id = ProjectPkg_Deliverables_ProjectPkg_Id and tbl_ProjectUC_DeliverablesTotal.ProjectUC_Deliverables_Deliverables_Id = ProjectPkg_Deliverables_Deliverables_Id
                                where ProjectPkg_Deliverables_Status=1 and ProjectPkg_Deliverables_ProjectWork_Id='" + ProjectWork_Id + "' and ProjectPkg_Deliverables_ProjectPkg_Id='" + Package_Id + "' ";
            dt = ExecuteSelectQuery(strQuery);

            return dt;
        }

        public DataSet get_tbl_Department()
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; 
                        select 
                            Department_Id, 
                            Department_Name, 
                            Department_AddedOn, 
                            Department_AddedBy, 
                            Department_Status
                          from tbl_Department
                          where Department_Status = 1 order by Department_Name";
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

        public DataSet get_tbl_Designation()
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @" set dateformat dmy; 
                        select 
                            Designation_Id, 
                            Designation_DesignationName, 
                            Designation_AddedBy, 
                            Designation_AddedOn, 
                            Designation_Status, 
                            Designation_Level
                        from tbl_Designation
                        where Designation_Status = 1 order by Designation_DesignationName";
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

        public DataSet get_tbl_SNAAccountMaster(int SNAAccountMaster_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; 
                    select 
						SNAAccountMaster_Id,
						SNAAccountMaster_SrNo,
						SNAAccountMaster_ACCT_NO,
						SNAAccountMaster_ProjectCode,
						SNAAccountMaster_ACCT_NAME,
						SNAAccountMaster_Mode_of_Operation,
						SNAAccountMaster_ProjectWotk_Id,
						tbl_ProjectWork.ProjectWork_Id, 
                        ProjectWork_Project_Id, 
                        Project_Name, 
                        ProjectWork_ProjectCode, 
                        ProjectWork_Name, 
						Project_Name_C = Project_Name + ' / ' + ProjectWork_ProjectCode,
                        ProjectWork_Description, 
                        ProjectWork_Status, 
						ULB_Name, 
						Jurisdiction_Name_Eng, 
						Division_Name, 
						Circle_Name, 
						Zone_Name, 
						ProjectWork_DistrictId, 
                        ProjectWork_BlockId,
						ProjectWork_ULB_Id, 
						ProjectWork_DivisionId, 
						Division_CircleId, 
						SNAAccountLimit_AssignedLimit = isnull(tLimit.SNAAccountLimit_AssignedLimit, 0),
						SNAAccountLimitUsed_UsedLimit = isnull(tLimitUsed.SNAAccountLimitUsed_UsedLimit, 0),
						SNAAccountAvailableLimit = isnull(tLimit.SNAAccountLimit_AssignedLimit, 0) - isnull(tLimitUsed.SNAAccountLimitUsed_UsedLimit, 0), 
						Status = case when isnull(ProjectWork_Id, 0) > 0 then 'Mapped' else 'Not Mapped' end
                    from tbl_SNAAccountMaster 
					left join tbl_ProjectWork on SNAAccountMaster_ProjectWotk_Id = ProjectWork_Id and ProjectWork_Status = 1
                    left join tbl_Project on Project_Id = ProjectWork_Project_Id
					left join M_Jurisdiction on M_Jurisdiction_Id = ProjectWork_DistrictId
					left join tbl_ULB on ULB_Id = ProjectWork_ULB_Id
					left join tbl_Division on Division_Id = ProjectWork_DivisionId
					left join tbl_Circle on Circle_Id = Division_CircleId
					left join tbl_Zone on Zone_Id = Circle_ZoneId
					left join 
					(
						select 
							SNAAccountLimit_ProjectWotk_Id,
							SNAAccountLimit_AssignedLimit = sum(isnull(SNAAccountLimit_AssignedLimit, 0))
						from tbl_SNAAccountLimit
						where SNAAccountLimit_Status = 1
						group by SNAAccountLimit_ProjectWotk_Id
					) tLimit on tLimit.SNAAccountLimit_ProjectWotk_Id = ProjectWork_Id 
					left join 
					(
						select 
							SNAAccountLimitUsed_ProjectWotk_Id,
							SNAAccountLimitUsed_UsedLimit = sum(isnull(SNAAccountLimitUsed_UsedLimit, 0))
						from tbl_SNAAccountLimitUsed
						where SNAAccountLimitUsed_Status = 1
						group by SNAAccountLimitUsed_ProjectWotk_Id
					) tLimitUsed on tLimitUsed.SNAAccountLimitUsed_ProjectWotk_Id = ProjectWork_Id 
                    where SNAAccountMaster_Status = 1 and SNAAccountMaster_Id = '" + SNAAccountMaster_Id + "' order by Project_Name, ProjectWork_Name";
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
        public string get_tbl_SNAAccountParentMaster(int SNAAccountParentMaster_Id)
        {
            string Account_No = "";
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; 
                        select 
						    SNAAccountParentMaster_ACCT_NO
                        from tbl_SNAAccountParentMaster 
                        where SNAAccountParentMaster_Status = 1 and SNAAccountParentMaster_Id = '" + SNAAccountParentMaster_Id + "'";
            try
            {
                ds = ExecuteSelectQuery(strQuery);
                if (Utility.CheckDataSet(ds))
                {
                    Account_No = ds.Tables[0].Rows[0]["SNAAccountParentMaster_ACCT_NO"].ToString();
                }
                else
                {
                    Account_No = "";
                }
            }
            catch
            {
                Account_No = "";
            }
            return Account_No;
        }
        public bool Update_SNA_Account_Balance(tbl_SNA_API_History obj_tbl_SNA_API_History, tbl_SNAAccountBalance obj_tbl_SNAAccountBalance)
        {
            DataSet ds = new DataSet();
            bool iResult = false;
            string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            using (SqlConnection cn = new SqlConnection(ConStr))
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlTransaction trans = cn.BeginTransaction();
                try
                {
                    Insert_tbl_SNA_API_History(obj_tbl_SNA_API_History, trans, cn);
                    if (obj_tbl_SNAAccountBalance != null)
                    {
                        Insert_tbl_SNAAccountBalance(obj_tbl_SNAAccountBalance, trans, cn);
                    }
                    iResult = true;
                    trans.Commit();
                    cn.Close();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    cn.Close();
                    iResult = false;
                }
            }
            return iResult;
        }

        public bool Update_tbl_DeletePhoto(int ProjectUC_Id)
        {
            DataSet ds = new DataSet();
            bool iResult = false;
            string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            using (SqlConnection cn = new SqlConnection(ConStr))
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlTransaction trans = cn.BeginTransaction();
                try
                {
                    string sql = "set dateformat dmy; update tbl_ProjectWorkGallery set ProjectWorkGallery_ModifiedOn = getdate(), ProjectWorkGallery_ModifiedBy = -1, ProjectWorkGallery_Status = 0 where ProjectWorkGallery_Status = 1 and ProjectWorkGallery_Id = '" + ProjectUC_Id + "'";
                    ExecuteSelectQuerywithTransaction(cn, sql, trans);
                    iResult = true;
                    trans.Commit();
                    cn.Close();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    cn.Close();
                    iResult = false;
                }
            }
            return iResult;
        }

        private void Insert_tbl_SNA_API_History(tbl_SNA_API_History obj_tbl_SNA_API_History, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_SNA_API_History ( [SNA_API_History_AddedBy],[SNA_API_History_AddedOn],[SNA_API_History_AES_Key],[SNA_API_History_API_Type],[SNA_API_History_JSON_Request_Encrypted],[SNA_API_History_JSON_Request_Plain],[SNA_API_History_JSON_Response_Encrypted],[SNA_API_History_JSON_Response_Plain],[SNA_API_History_Status] ) values ('" + obj_tbl_SNA_API_History.SNA_API_History_AddedBy + "', getdate(), N'" + obj_tbl_SNA_API_History.SNA_API_History_AES_Key + "','" + obj_tbl_SNA_API_History.SNA_API_History_API_Type + "',N'" + obj_tbl_SNA_API_History.SNA_API_History_JSON_Request_Encrypted + "',N'" + obj_tbl_SNA_API_History.SNA_API_History_JSON_Request_Plain + "',N'" + obj_tbl_SNA_API_History.SNA_API_History_JSON_Response_Encrypted + "',N'" + obj_tbl_SNA_API_History.SNA_API_History_JSON_Response_Plain + "','" + obj_tbl_SNA_API_History.SNA_API_History_Status + "');Select @@Identity";
            if (trans == null)
            {
                try
                {
                    ExecuteSelectQuery(strQuery);
                }
                catch
                {
                }
            }
            else
            {
                ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
            }
        }

        private void Insert_tbl_SNAAccountBalance(tbl_SNAAccountBalance obj_tbl_SNAAccountBalance, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_SNAAccountBalance ( [SNAAccountBalance_AddedBy],[SNAAccountBalance_AddedOn],[SNAAccountBalance_Balance],[SNAAccountBalance_Error_Desc],[SNAAccountBalance_Request_Id],[SNAAccountBalance_SNAAccountMaster_Id],[SNAAccountBalance_Status],[SNAAccountBalance_StatusPNB] ) values ('" + obj_tbl_SNAAccountBalance.SNAAccountBalance_AddedBy + "', getdate(), '" + obj_tbl_SNAAccountBalance.SNAAccountBalance_Balance + "',N'" + obj_tbl_SNAAccountBalance.SNAAccountBalance_Error_Desc + "', N'" + obj_tbl_SNAAccountBalance.SNAAccountBalance_Request_Id + "','" + obj_tbl_SNAAccountBalance.SNAAccountBalance_SNAAccountMaster_Id + "','" + obj_tbl_SNAAccountBalance.SNAAccountBalance_Status + "',N'" + obj_tbl_SNAAccountBalance.SNAAccountBalance_StatusPNB + "');Select @@Identity";
            if (trans == null)
            {
                try
                {
                    ExecuteSelectQuery(strQuery);
                }
                catch
                {
                }
            }
            else
            {
                ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
            }
        }

        public bool Update_SNA_Account_DP_Limit(tbl_SNA_API_History obj_tbl_SNA_API_History, tbl_SNAAccountDPLimit obj_tbl_SNAAccountDPLimit)
        {
            DataSet ds = new DataSet();
            bool iResult = false;
            string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            using (SqlConnection cn = new SqlConnection(ConStr))
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlTransaction trans = cn.BeginTransaction();
                try
                {
                    Insert_tbl_SNA_API_History(obj_tbl_SNA_API_History, trans, cn);
                    if (obj_tbl_SNAAccountDPLimit != null)
                    {
                        Insert_tbl_SNAAccountDPLimit(obj_tbl_SNAAccountDPLimit, trans, cn);
                    }
                    iResult = true;
                    trans.Commit();
                    cn.Close();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    cn.Close();
                    iResult = false;
                }
            }
            return iResult;
        }

        private void Insert_tbl_SNAAccountDPLimit(tbl_SNAAccountDPLimit obj_tbl_SNAAccountDPLimit, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            strQuery = " set dateformat dmy;insert into tbl_SNAAccountDPLimit ( [SNAAccountDPLimit_AddedBy],[SNAAccountDPLimit_AddedOn],[SNAAccountDPLimit_DPLimit],[SNAAccountDPLimit_ErrorCode],[SNAAccountDPLimit_ErrorDesc],[SNAAccountDPLimit_RefNum],[SNAAccountDPLimit_SNAAccountMaster_Id],[SNAAccountDPLimit_Status],[SNAAccountDPLimit_StatusPNB],[SNAAccountDPLimit_TranDate],[SNAAccountDPLimit_TranId],[SNAAccountDPLimit_TranParticular],[SNAAccountDPLimit_TranRmks],[SNAAccountDPLimit_Unique_Id] ) values ('" + obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_AddedBy + "', getdate(),'" + obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_DPLimit + "',N'" + obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_ErrorCode + "',N'" + obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_ErrorDesc + "',N'" + obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_RefNum + "','" + obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_SNAAccountMaster_Id + "','" + obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_Status + "',N'" + obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_StatusPNB + "',N'" + obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_TranDate + "',N'" + obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_TranId + "',N'" + obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_TranParticular + "',N'" + obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_TranRmks + "',N'" + obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_Unique_Id + "');Select @@Identity";
            if (trans == null)
            {
                try
                {
                    ExecuteSelectQuery(strQuery);
                }
                catch
                {
                }
            }
            else
            {
                ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
            }
        }

        public bool Update_SNA_PPA_Details(tbl_SNA_API_History obj_tbl_SNA_API_History, tbl_SNAPPADetails obj_tbl_SNAPPADetails)
        {
            DataSet ds = new DataSet();
            bool iResult = false;
            string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            using (SqlConnection cn = new SqlConnection(ConStr))
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlTransaction trans = cn.BeginTransaction();
                try
                {
                    Insert_tbl_SNA_API_History(obj_tbl_SNA_API_History, trans, cn);
                    if (obj_tbl_SNAPPADetails != null)
                    {
                        Insert_tbl_SNAPPADetails(obj_tbl_SNAPPADetails, trans, cn);
                    }
                    iResult = true;
                    trans.Commit();
                    cn.Close();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    cn.Close();
                    iResult = false;
                }
            }
            return iResult;
        }

        private void Insert_tbl_SNAPPADetails(tbl_SNAPPADetails obj_tbl_SNAPPADetails, SqlTransaction trans, SqlConnection cn)
        {
            string strQuery = "";
            if (obj_tbl_SNAPPADetails.SNAPPADetails_Amount == null)
            {
                strQuery = " set dateformat dmy;insert into tbl_SNAPPADetails ( [SNAPPADetails_AddedBy],[SNAPPADetails_AddedOn],[SNAPPADetails_Batch_Id],[SNAPPADetails_Error_Desc],[SNAPPADetails_PPA_Number],[SNAPPADetails_PPA_Receive_Date],[SNAPPADetails_SNAAccountMaster_Id],[SNAPPADetails_Status],[SNAPPADetails_StatusPNB],[SNAPPADetails_Unique_Id]) values ('" + obj_tbl_SNAPPADetails.SNAPPADetails_AddedBy + "', getdate(),N'" + obj_tbl_SNAPPADetails.SNAPPADetails_Batch_Id + "',N'" + obj_tbl_SNAPPADetails.SNAPPADetails_Error_Desc + "',N'" + obj_tbl_SNAPPADetails.SNAPPADetails_PPA_Number + "',N'" + obj_tbl_SNAPPADetails.SNAPPADetails_PPA_Receive_Date + "','" + obj_tbl_SNAPPADetails.SNAPPADetails_SNAAccountMaster_Id + "','" + obj_tbl_SNAPPADetails.SNAPPADetails_Status + "',N'" + obj_tbl_SNAPPADetails.SNAPPADetails_StatusPNB + "',N'" + obj_tbl_SNAPPADetails.SNAPPADetails_Unique_Id + "'); Select @@Identity";
            }
            else
            {
                strQuery = " set dateformat dmy;insert into tbl_SNAPPADetails ( [SNAPPADetails_AddedBy],[SNAPPADetails_AddedOn],[SNAPPADetails_Amount],[SNAPPADetails_Batch_Id],[SNAPPADetails_Error_Desc],[SNAPPADetails_PPA_Number],[SNAPPADetails_PPA_Receive_Date],[SNAPPADetails_SNAAccountMaster_Id],[SNAPPADetails_Status],[SNAPPADetails_StatusPNB],[SNAPPADetails_Unique_Id] ) values ('" + obj_tbl_SNAPPADetails.SNAPPADetails_AddedBy + "', getdate(),'" + obj_tbl_SNAPPADetails.SNAPPADetails_Amount + "',N'" + obj_tbl_SNAPPADetails.SNAPPADetails_Batch_Id + "',N'" + obj_tbl_SNAPPADetails.SNAPPADetails_Error_Desc + "',N'" + obj_tbl_SNAPPADetails.SNAPPADetails_PPA_Number + "',N'" + obj_tbl_SNAPPADetails.SNAPPADetails_PPA_Receive_Date + "','" + obj_tbl_SNAPPADetails.SNAPPADetails_SNAAccountMaster_Id + "','" + obj_tbl_SNAPPADetails.SNAPPADetails_Status + "',N'" + obj_tbl_SNAPPADetails.SNAPPADetails_StatusPNB + "',N'" + obj_tbl_SNAPPADetails.SNAPPADetails_Unique_Id + "');Select @@Identity";
            }
            if (trans == null)
            {
                try
                {
                    ExecuteSelectQuery(strQuery);
                }
                catch
                {
                }
            }
            else
            {
                ExecuteSelectQuerywithTransaction(cn, strQuery, trans);
            }
        }

        public DataSet get_EngineeringWing(int Person_Id, int Zone_Id, int Circle_Id, int Division_Id, int ProjectDPR_Id, int ProjectWork_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            if (ProjectDPR_Id > 0)
            {
                strQuery = @"select Circle_ZoneId, Division_CircleId, ProjectWork_DivisionId from tbl_ProjectWork join tbl_Division on Division_Id = ProjectWork_DivisionId join tbl_Circle on Circle_Id = Division_CircleId where ProjectWork_Id = '" + ProjectDPR_Id + "'";
                ds = ExecuteSelectQuery(strQuery);
                if (Utility.CheckDataSet(ds))
                {
                    try
                    {
                        Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Circle_ZoneId"].ToString());
                    }
                    catch
                    {
                        Zone_Id = 0;
                    }
                    try
                    {
                        Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Division_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                    try
                    {
                        Division_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProjectWork_DivisionId"].ToString());
                    }
                    catch
                    {
                        Division_Id = 0;
                    }
                }
            }
            else if (ProjectWork_Id > 0)
            {
                strQuery = @"select Circle_ZoneId, Division_CircleId, ProjectWork_DivisionId from tbl_ProjectWork join tbl_Division on Division_Id = ProjectWork_DivisionId join tbl_Circle on Circle_Id = Division_CircleId where ProjectWork_Id = '" + ProjectDPR_Id + "'";
                ds = ExecuteSelectQuery(strQuery);
                if (Utility.CheckDataSet(ds))
                {
                    try
                    {
                        Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Circle_ZoneId"].ToString());
                    }
                    catch
                    {
                        Zone_Id = 0;
                    }
                    try
                    {
                        Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Division_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                    try
                    {
                        Division_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProjectWork_DivisionId"].ToString());
                    }
                    catch
                    {
                        Division_Id = 0;
                    }
                }
            }
            else if (Person_Id > 0)
            {
                strQuery = @"select PersonJuridiction_DivisionId, PersonJuridiction_CircleId, PersonJuridiction_ZoneId from tbl_PersonDetail join tbl_PersonJuridiction on Person_Id = PersonJuridiction_PersonId where Person_Id = '" + Person_Id + "' ";
                ds = ExecuteSelectQuery(strQuery);
                if (Utility.CheckDataSet(ds))
                {
                    try
                    {
                        Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_ZoneId"].ToString());
                    }
                    catch
                    {
                        Zone_Id = 0;
                    }
                    try
                    {
                        Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                    try
                    {
                        Division_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PersonJuridiction_DivisionId"].ToString());
                    }
                    catch
                    {
                        Division_Id = 0;
                    }
                }
            }

            strQuery = @"set dateformat dmy; 
                        select 
	                        Person_Id,
	                        UserType_Desc_E, 
	                        Person_Name, 
	                        Person_Mobile1, 
	                        Person_Mobile2,
	                        Designation_DesignationName
                        from tbl_PersonDetail 
						join tbl_PersonJuridiction on Person_Id = PersonJuridiction_PersonId
						left join tbl_Designation on Designation_Id = PersonJuridiction_DesignationId
						left join tbl_UserType on UserType_Id = PersonJuridiction_UserTypeId
						where Person_Status = 1 and PersonJuridiction_Status = 1 and PersonJuridiction_UserTypeId = 7 and PersonJuridiction_DivisionId = '" + Division_Id + "' order by Designation_DesignationName";
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

        public bool Update_tbl_ProjectRoadReinst(tbl_ProjectRoadReinst obj_tbl_ProjectRoadReinst)
        {
            DataSet ds = new DataSet();
            bool iResult = false;
            string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
            using (SqlConnection cn = new SqlConnection(ConStr))
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
                SqlTransaction trans = cn.BeginTransaction();
                try
                {
                    if (!Directory.Exists(System.Configuration.ConfigurationManager.AppSettings["BasePath"] + "Road\\"))
                    {
                        Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings["BasePath"] + "Road\\");
                    }
                    if (!Directory.Exists(System.Configuration.ConfigurationManager.AppSettings["BasePath"] + "Road\\" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_ProjectWork_Id.ToString()))
                    {
                        Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings["BasePath"] + "Road\\" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_ProjectWork_Id.ToString());
                    }
                    string sql = "";
                    if (saveImageData(obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Path1, obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Bytes1, System.Configuration.ConfigurationManager.AppSettings["BasePath"] + "Road\\" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_ProjectWork_Id.ToString() + "\\", obj_tbl_ProjectRoadReinst.ProjectRoadReinst_Latitude.ToString(), obj_tbl_ProjectRoadReinst.ProjectRoadReinst_Longitude.ToString(), DateTime.Now.ToLongDateString()))
                    {
                        obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Path1 = "\\Downloads\\Road\\" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_ProjectWork_Id.ToString() + "\\" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Path1;
                    }
                    if (saveImageData(obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Path2, obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Bytes2, System.Configuration.ConfigurationManager.AppSettings["BasePath"] + "Road\\" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_ProjectWork_Id.ToString() + "\\", obj_tbl_ProjectRoadReinst.ProjectRoadReinst_Latitude.ToString(), obj_tbl_ProjectRoadReinst.ProjectRoadReinst_Longitude.ToString(), DateTime.Now.ToLongDateString()))
                    {
                        obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Path2 = "\\Downloads\\Road\\" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_ProjectWork_Id.ToString() + "\\" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Path2;
                    }
                    if (saveImageData(obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Path3, obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Bytes3, System.Configuration.ConfigurationManager.AppSettings["BasePath"] + "Road\\" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_ProjectWork_Id.ToString() + "\\", obj_tbl_ProjectRoadReinst.ProjectRoadReinst_Latitude.ToString(), obj_tbl_ProjectRoadReinst.ProjectRoadReinst_Longitude.ToString(), DateTime.Now.ToLongDateString()))
                    {
                        obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Path3 = "\\Downloads\\Road\\" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_ProjectWork_Id.ToString() + "\\" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Path3;
                    }
                    sql = " set dateformat dmy;insert into tbl_ProjectRoadReinst ( [ProjectRoadReinst_AddedBy],[ProjectRoadReinst_AddedOn],[ProjectRoadReinst_Comments],[ProjectRoadReinst_Latitude],[ProjectRoadReinst_Longitude],[ProjectRoadReinst_ProjectWork_Id],[ProjectRoadReinst_SitePic_Path1],[ProjectRoadReinst_SitePic_Path2],[ProjectRoadReinst_SitePic_Path3],[ProjectRoadReinst_Status],[ProjectRoadReinst_TotalLengthLaid],[ProjectRoadReinst_TotalLengthLaid_Dismental],[ProjectRoadReinst_TotalLengthLaid_Moterable],[ProjectRoadReinst_TotalLengthLaid_Pending],[ProjectRoadReinst_TotalLengthLaid_Reinstatement],[ProjectRoadReinst_TotalLengthLaid_Reinstatement_And_Moterable],[ProjectRoadReinst_TotalLengthLaid_TillDate] ) values ('" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_AddedBy + "', getdate(), N'" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_Comments + "','" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_Latitude + "','" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_Longitude + "','" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_ProjectWork_Id + "',N'" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Path1 + "',N'" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Path2 + "',N'" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_SitePic_Path3 + "','" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_Status + "','" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_TotalLengthLaid + "','" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_TotalLengthLaid_Dismental + "','" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_TotalLengthLaid_Moterable + "','" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_TotalLengthLaid_Pending + "','" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_TotalLengthLaid_Reinstatement + "','" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_TotalLengthLaid_Reinstatement_And_Moterable + "','" + obj_tbl_ProjectRoadReinst.ProjectRoadReinst_TotalLengthLaid_TillDate + "');Select @@Identity";

                    ExecuteSelectQuerywithTransaction(cn, sql, trans);
                    iResult = true;
                    trans.Commit();
                    cn.Close();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    cn.Close();
                    iResult = false;
                }
            }
            return iResult;
        }

        public DataSet get_tbl_DownloadsOther()
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @" set dateformat dmy; 
                        select 
                            DownloadsOther_Id,  
                            DownloadsOther_Name, 
                            DownloadsOther_URL, 
                            DownloadsOther_Status, 
                            DownloadsOther_AddedBy, 
                            DownloadsOther_Ref_No, 
                            DownloadsOther_Date = convert(char(10), DownloadsOther_Date, 103)
                        from tbl_DownloadsOther  
                        where DownloadsOther_Status = 1 
                        order by DownloadsOther_Name";
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
