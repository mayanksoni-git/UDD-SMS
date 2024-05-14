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
using PMIS_API.Models;

namespace PMIS_API.App_Code
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
        public DataSet get_tbl_FinancialYear()
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; 
                        select 
                            FinancialYear_Id,
	                        convert(char(10), FinancialYear_StartYear, 103) FinancialYear_StartYear,
	                        convert(char(10), FinancialYear_EndYear, 103) FinancialYear_EndYear,
	                        FinancialYear_Comments,
	                        FinancialYear_Status,
	                        FinancialYear_Active
                        from tbl_FinancialYear
                        where FinancialYear_Status = 1
                        order by FinancialYear_Order desc";
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
        public DataSet get_tbl_District()
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
                    where Circle_Status = 1
                    order by Zone_Name, Circle_Name";
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
        public DataSet get_tbl_LokSabha()
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @" set dateformat dmy; 
                    select 
                        LokSabha_Id, 
                        LokSabha_Name, 
                        LokSabha_DistrictId = tbl_LokSabhaDistrictLink.List_District_Id, 
                        District_Name = tbl_LokSabhaDistrictLinkName.List_District_Name 
                    from tbl_LokSabha
                    left join 
                    (
                        SELECT	
                            LokSabhaDistrictLink_LokSabhaId,
                            STUFF((SELECT ', ' + CAST(Circle_Name AS VARCHAR(1000)) [text()]
                        FROM tbl_LokSabhaDistrictLink
	                    join tbl_Circle on Circle_Id = LokSabhaDistrictLink_DistrictId and Circle_Status = 1
                        WHERE LokSabhaDistrictLink_LokSabhaId = t.LokSabhaDistrictLink_LokSabhaId and tbl_LokSabhaDistrictLink.LokSabhaDistrictLink_Status = 1
                        FOR XML PATH(''), TYPE)
                        .value('.','NVARCHAR(MAX)'),1,2,' ') as List_District_Name
                        FROM tbl_LokSabhaDistrictLink t
                        where t.LokSabhaDistrictLink_Status = 1 
                        GROUP BY LokSabhaDistrictLink_LokSabhaId
                    ) tbl_LokSabhaDistrictLinkName on tbl_LokSabhaDistrictLinkName.LokSabhaDistrictLink_LokSabhaId = LokSabha_Id
                    left join 
                    (
                        SELECT	
                            LokSabhaDistrictLink_LokSabhaId,
                            STUFF((SELECT ', ' + CAST(LokSabhaDistrictLink_DistrictId AS VARCHAR(100)) [text()]
                        FROM tbl_LokSabhaDistrictLink
                        WHERE LokSabhaDistrictLink_LokSabhaId = t.LokSabhaDistrictLink_LokSabhaId and tbl_LokSabhaDistrictLink.LokSabhaDistrictLink_Status = 1
                        FOR XML PATH(''), TYPE)
                        .value('.','NVARCHAR(MAX)'),1,2,' ') as List_District_Id
                        FROM tbl_LokSabhaDistrictLink t
                        where t.LokSabhaDistrictLink_Status = 1 
                        GROUP BY LokSabhaDistrictLink_LokSabhaId
                    ) tbl_LokSabhaDistrictLink on tbl_LokSabhaDistrictLink.LokSabhaDistrictLink_LokSabhaId = LokSabha_Id
                    left join tbl_PersonDetail on Person_Id = LokSabha_AddedBy
                    left join tbl_PersonDetail as tbl_PersonDetail1 on tbl_PersonDetail1.Person_Id = LokSabha_ModifiedBy
                    where LokSabha_Status = 1 
                    order by LokSabha_Name";
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
        public DataSet get_tbl_VidhanSabha(int LokSabha_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @" set dateformat dmy; 
                    select 
                        VidhanSabha_Id, 
                        VidhanSabha_LokSabhaId, 
                        LokSabha_Name, 
                        VidhanSabha_Name, 
                        VidhanSabha_AddedOn, 
                        VidhanSabha_AddedBy, 
                        VidhanSabha_ModifiedOn, 
                        VidhanSabha_ModifiedBy, 
                        VidhanSabha_Status 
                    from tbl_VidhanSabha
                    join tbl_LokSabha on LokSabha_Id = VidhanSabha_LokSabhaId
                    where VidhanSabha_Status = 1";
            if (LokSabha_Id != 0)
            {
                strQuery += " and VidhanSabha_LokSabhaId = '" + LokSabha_Id + "'";
            }
            strQuery += " order by LokSabha_Name, VidhanSabha_Name";
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
        public DataSet get_tbl_ULB(int District_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            strQuery = @" set dateformat dmy; 
                    select 
                        Division_Id, 
                        Division_CircleId, 
                        Circle_Name, 
						Division_Type,
                        Division_Name, 
                        Division_AddedOn, 
                        Division_AddedBy, 
                        Division_Status 
                    from tbl_Division
                    join tbl_Circle on Circle_Id = Division_CircleId
                    where Division_Status = 1";
            if (District_Id != 0)
            {
                strQuery += " and Division_CircleId = '" + District_Id + "'";
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

        public DataSet get_tbl_ProjectWorkGO(int Work_Id, int Project_Id)
        {
            string strQuery = "";
            DataSet ds = new DataSet();
            if (Project_Id == 12 || Project_Id == 1013 || Project_Id == 1016)
            {
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
                                ULB_Name, 
                                GO_Total = convert(decimal(18,2), (isnull(ProjectWorkGO_CentralShare,0) / 100000)) + convert(decimal(18,2), (isnull(ProjectWorkGO_StateShare,0) / 100000)) + convert(decimal(18,2), (isnull(ProjectWorkGO_ULBShare,0) / 100000))
                            from ePayment.dbo.tbl_ProjectWorkGO
					        join ePayment.dbo.tbl_ProjectWork on ProjectWork_Id = ProjectWorkGO_Work_Id 
					        left join ePayment.dbo.tbl_ULB on ULB_Id = ProjectWorkGO_ULB_Id
                            where ProjectWorkGO_Status = 1 and ProjectWorkGO_Work_Id = '" + Work_Id + "' ";
            }
            else
            {
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
                                ULB_Name, 
                                GO_Total = convert(decimal(18,2), (isnull(ProjectWorkGO_CentralShare,0) / 100000)) + convert(decimal(18,2), (isnull(ProjectWorkGO_StateShare,0) / 100000)) + convert(decimal(18,2), (isnull(ProjectWorkGO_ULBShare,0) / 100000))
                            from tbl_ProjectWorkGO
					        join tbl_ProjectWork on ProjectWork_Id = ProjectWorkGO_Work_Id 
					        left join tbl_ULB on ULB_Id = ProjectWorkGO_ULB_Id
                            where ProjectWorkGO_Status = 1 and ProjectWorkGO_Work_Id = '" + Work_Id + "' ";
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

        public DataSet get_tbl_ProjectWork(SearchCriteria obj_SearchCriteria)
        {
            string strQuery = "";
            if (obj_SearchCriteria.FinancialYear_Id > 0)
            {
                string strQueryFY = "";
                DataSet dsFY = new DataSet();
                strQueryFY = @"set dateformat dmy; 
                            select 
                                FinancialYear_Id,
	                            convert(char(10), FinancialYear_StartYear, 103) FinancialYear_StartYear,
	                            convert(char(10), FinancialYear_EndYear, 103) FinancialYear_EndYear,
	                            FinancialYear_Comments,
	                            FinancialYear_Status,
	                            FinancialYear_Active
                            from tbl_FinancialYear
                            where FinancialYear_Status = 1 and FinancialYear_Id = '" + obj_SearchCriteria.FinancialYear_Id + "'";
                dsFY = ExecuteSelectQuery(strQueryFY);
                if (Utility.CheckDataSet(dsFY))
                {
                    obj_SearchCriteria.TillDate = dsFY.Tables[0].Rows[0]["FinancialYear_EndYear"].ToString();
                    obj_SearchCriteria.FromDate = dsFY.Tables[0].Rows[0]["FinancialYear_StartYear"].ToString();
                }
            }
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; ";
            strQuery += Environment.NewLine;
            strQuery += "select * from ( ";
            strQuery += @"select 
                            ProjectWork_Id, 
                            ProjectWork_Project_Id, 
                            Project_Name, 
                            ProjectWork_ProjectCode, 
                            ProjectWork_Name = REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_Name_Code = isnull(ProjectWork_ProjectCode, '') + ' - ' + REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''),
                            ProjectWork_Description = REPLACE(REPLACE(ProjectWork_Description, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_GO_Path,
                            ProjectWork_GO_Date = convert(char(10), ProjectWork_GO_Date, 103), 
						    ProjectWork_StartDate = convert(char(10), ProjectWork_StartDate, 103), 
						    ProjectWork_EndDate = convert(char(10), ProjectWork_EndDate, 103), 
                            ProjectWork_GO_No,
                            ProjectType_Name,
                            ProjectWork_Budget = convert(decimal(18, 3), isnull(ProjectWork_Budget, 0) + isnull(ProjectWork_Centage, 0)),
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
						    tender_cost = convert(decimal(18, 3), isnull(tender_cost, 0)),
						    tender_cost_1 = convert(decimal(18, 3), isnull(tender_cost_1, 0)),
                            Total_Release = convert(decimal(18, 3), isnull(Total_Release, 0)),
                            Total_Expenditure = convert(decimal(18, 3), isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)),
						    ProjectWorkPkg_Due_Date, 
                            ProjectWorkPkg_Start_Date,
						    ProjectWorkPkg_Agreement_Date,
                            Target_Date_Agreement_Extended = convert(char(10), ProjectWorkPkg_End_Date_Extended, 103), 
						    Financial_Progress = convert(decimal(18, 2), (case when isnull(tProjectWorkPkg.tender_cost, 0) > 0 then (((isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)) * 100) / (isnull(tProjectWorkPkg.tender_cost, 0) + isnull(ProjectWork_ADPCost, 0))) else 0 end)),
                            Physical_Progress = convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0)), 
                            Total_Photo = isnull(tGallery.Total_Photo, 0),
                            LokSabha_Name, 
                            VidhanSabha_Name, 
                            LokSabha_Id, 
                            VidhanSabha_Id,
                            Last_Updated
                        from tbl_ProjectWork
                        left join tbl_Project on Project_Id = ProjectWork_Project_Id
                        left join tbl_ProjectType on ProjectType_Id = ProjectWork_ProjectType_Id
					    left join M_Jurisdiction on M_Jurisdiction_Id = ProjectWork_DistrictId
					    left join tbl_ULB on ULB_Id = ProjectWork_ULB_Id
					    left join tbl_Division on Division_Id = ProjectWork_DivisionId
					    left join tbl_Circle on Circle_Id = Division_CircleId
					    left join tbl_Zone on Zone_Id = Circle_ZoneId
                        left join tbl_VidhanSabha on VidhanSabha_Id = ProjectWork_VidhanSabha_Id
                        left join tbl_LokSabha on LokSabha_Id = VidhanSabha_LokSabhaId
                        left join
                        (
	                        select 
		                        ProjectWorkGO_Work_Id, 
                                Total_Release = sum(isnull(ProjectWorkGO_TotalRelease, 0)) / 100000 
	                        from tbl_ProjectWorkGO 
	                        where ProjectWorkGO_Status = 1 
	                        group by ProjectWorkGO_Work_Id
                        ) tGO on tGO.ProjectWorkGO_Work_Id = ProjectWork_Id
					    left join 
					    (
                            select 
	                            ProjectWorkPkg_Work_Id,
	                            tender_cost = sum(isnull(tender_cost_With_GST, 0)), 
	                            tender_cost_1 = sum(isnull(tender_cost_WithOut_GST, 0)), 
	                            ProjectWorkPkg_Due_Date = convert(char(10), max(ProjectWorkPkg_Due_Date), 103),
	                            ProjectWorkPkg_Start_Date = convert(char(10), min(ProjectWorkPkg_Start_Date), 103),
	                            ProjectWorkPkg_Agreement_Date = convert(char(10), min(ProjectWorkPkg_Agreement_Date), 103), 
	                            ProjectWorkPkg_End_Date_Extended = convert(char(10), max(case when ProjectWorkPkg_ExtendDate is null then ProjectWorkPkg_Due_Date else ProjectWorkPkg_ExtendDate end), 103)
                            from (
                            select 
                                ProjectWorkPkg_Work_Id,
                                tender_cost_With_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then (isnull(ProjectWorkPkg_AgreementAmount, 0) * (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) else ProjectWorkPkg_AgreementAmount end) else isnull(Tender_Cost_Include_GST, 0) end, 
	                            tender_cost_WithOut_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then ISNULL(ProjectWorkPkg_AgreementAmount, 0) else (isnull(ProjectWorkPkg_AgreementAmount, 0) / (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) end) else isnull(Tender_Cost_Exclude_GST, 0) end,
	                            ProjectWorkPkg_Due_Date,
                                ProjectWorkPkg_Start_Date, 
	                            ProjectWorkPkg_Agreement_Date, 
                                ProjectWorkPkg_ExtendDate, 
                                ProjectWorkPkg_Percent, 
	                            ProjectWorkPkg_Id_TC = isnull(ProjectWorkPkg_Id_TC, 0), 
	                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
	                            Tender_Cost_Include_GST = isnull(Tender_Cost_Include_GST, 0)
                            from tbl_ProjectWorkPkg 
                            left join 
                            (
	                            select 
		                            ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id, 
		                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
		                            Tender_Cost_Include_GST = isnull(Final_Tender_Cost, 0)
	                            from tbl_Tender_Cost_Pkg_Wise_Automated
                            ) tTenderCost_Auto on tTenderCost_Auto.ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id
                            where ProjectWorkPkg_Status = 1	
                            ) tData
                            group by ProjectWorkPkg_Work_Id
					    ) tProjectWorkPkg  on tProjectWorkPkg.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id
					    left join 
                        (
                            select 
                                row_number() over (partition by ProjectWorkFinancialTarget_ProjectWork_Id order by ProjectWorkFinancialTarget_Id desc) rrT,
                                ProjectWorkFinancialTarget_ProjectWork_Id, 
                                ProjectWorkFinancialTarget_Target, 
                                ProjectWorkPhysicalTarget_Target
                            from tbl_ProjectWorkFinancialTarget
                            where ProjectWorkFinancialTarget_Status = 1   
                        ) tTarget on tTarget.ProjectWorkFinancialTarget_ProjectWork_Id = tbl_ProjectWork.ProjectWork_Id and rrT = 1
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
						    where PackageInvoice_Status = 1 and FinancialTrans_EntryType = 'Fund Allocated' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1 and PackageInvoice_Id not in (select PackageInvoiceEMBMasterLink_Invoice_Id from tbl_PackageInvoiceEMBMasterLink where PackageInvoiceEMBMasterLink_Status=1) 
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
						    where Package_ADP_Status = 1 and FinancialTrans_EntryType = 'ADP' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1
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
							    ProjectWorkPkg_Work_Id,
							    Total_Invoice_ADP = count(*), 
							    Total_Value_ADP = sum(isnull(Package_ADP_ADPTotalAmount, 0)) / 100000
						    from tbl_Package_ADP 
                            join
                            (
                                select
                                    Package_ADP_Item_Package_ADP_Id,
		                            Total_Line_Items = count(*),
		                            Total_Amount = sum(isnull(Package_ADP_Item_TotalAmount, 0))
                                from tbl_Package_ADP_Item
                                where Package_ADP_Item_Status = 1
                                group by Package_ADP_Item_Package_ADP_Id
                            ) tPackageInvoiceItem on Package_ADP_Item_Package_ADP_Id = Package_ADP_Id
						    join tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
						    join 
						    (
							    select 
								    ROW_NUMBER() over (partition by PackageADPApproval_Package_ADP_Id order by PackageADPApproval_Id desc) rrrrr,
								    PackageADPApproval_Next_Designation_Id,
								    PackageADPApproval_Next_Organisation_Id,
								    PackageADPApproval_Comments,
								    PackageADPApproval_AddedBy,
								    PackageADPApproval_AddedOn = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Status_Id,
								    PackageADPApproval_Package_Id,
								    PackageADPApproval_Package_ADP_Id,
								    InvoiceStatus_Name,
								    PackageADPApproval_Date = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Id
							    from tbl_PackageADPApproval
							    left join tbl_InvoiceStatus on InvoiceStatus_Id = PackageADPApproval_Status_Id
							    where PackageADPApproval_Status = 1
						    ) tADPApproval on tADPApproval.PackageADPApproval_Package_ADP_Id = Package_ADP_Id and tADPApproval.rrrrr = 1
						    where Package_ADP_Status = 1 and ProjectWorkPkg_Status = 1  
						    group by ProjectWorkPkg_Work_Id
					    ) tADP on tADP.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id 
                        left join 
                        (
	                        select 
		                        ProjectWorkGallery_Work_Id,
		                        Total_Photo = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) not in ('mov', 'mp4') then 1 else 0 end),
		                        Total_Video = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) in ('mov', 'mp4') then 1 else 0 end), 
                                Last_Updated = max(ProjectWorkGallery_AddedOn)
	                        from tbl_ProjectWorkGallery 
	                        where ProjectWorkGallery_Status = 1  
	                        group by ProjectWorkGallery_Work_Id
                        ) tGallery on tGallery.ProjectWorkGallery_Work_Id = tbl_ProjectWork.ProjectWork_Id 
                        where ProjectWork_Status = 1 and ProjectWork_Project_Id not in (12, 19, 1013, 1016) ULBTypeCond1 ";

            strQuery += Environment.NewLine;
            strQuery += "UNION ALL";
            strQuery += Environment.NewLine;

            strQuery += @"select 
                            ProjectWork_Id, 
                            ProjectWork_Project_Id, 
                            Project_Name, 
                            ProjectWork_ProjectCode, 
                            ProjectWork_Name = REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_Name_Code = isnull(ProjectWork_ProjectCode, '') + ' - ' + REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''),
                            ProjectWork_Description = REPLACE(REPLACE(ProjectWork_Description, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_GO_Path,
                            ProjectWork_GO_Date = convert(char(10), ProjectWork_GO_Date, 103), 
						    ProjectWork_StartDate = convert(char(10), ProjectWork_StartDate, 103), 
						    ProjectWork_EndDate = convert(char(10), ProjectWork_EndDate, 103), 
                            ProjectWork_GO_No,
                            ProjectType_Name,
                            ProjectWork_Budget = convert(decimal(18, 3), isnull(ProjectWork_Budget, 0) + isnull(ProjectWork_Centage, 0)),
                            ProjectWork_ProjectType_Id,
						    ULB_Name, 
						    Jurisdiction_Name_Eng, 
						    Division_Name = ULB_Name, 
						    Circle_Name = Jurisdiction_Name_Eng, 
						    Zone_Name = 'Uttar Pradesh', 
						    ProjectWork_DistrictId = ePayment_ULB_Circle_Id, 
                            ProjectWork_BlockId,
                            ProjectWork_ULB_Id = ePayment_ULB_Division_Id, 
						    ProjectWork_DivisionId = ePayment_ULB_Division_Id, 
						    Division_CircleId = ePayment_ULB_Circle_Id, 
						    tender_cost = convert(decimal(18, 3), isnull(tender_cost, 0)),
						    tender_cost_1 = convert(decimal(18, 3), isnull(tender_cost_1, 0)),
                            Total_Release = convert(decimal(18, 3), isnull(Total_Release, 0)),
                            Total_Expenditure = convert(decimal(18, 3), isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)),
						    ProjectWorkPkg_Due_Date, 
                            ProjectWorkPkg_Start_Date,
						    ProjectWorkPkg_Agreement_Date,
                            Target_Date_Agreement_Extended = convert(char(10), ProjectWorkPkg_End_Date_Extended, 103), 
						    Financial_Progress = convert(decimal(18, 2), (case when isnull(tProjectWorkPkg.tender_cost, 0) > 0 then (((isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)) * 100) / (isnull(tProjectWorkPkg.tender_cost, 0) + isnull(ProjectWork_ADPCost, 0))) else 0 end)),
                            Physical_Progress = convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0)), 
                            Total_Photo = isnull(tGallery.Total_Photo, 0),
	                        LokSabha_Name, 
	                        VidhanSabha_Name, 
	                        LokSabha_Id, 
	                        VidhanSabha_Id,
                            Last_Updated
                        from ePayment.dbo.tbl_ProjectWork
                        left join ePayment.dbo.tbl_Project on Project_Id = ProjectWork_Project_Id
                        left join ePayment.dbo.tbl_ProjectType on ProjectType_Id = ProjectWork_ProjectType_Id
					    left join ePayment.dbo.M_Jurisdiction on M_Jurisdiction_Id = ProjectWork_DistrictId
					    left join ePayment.dbo.tbl_ULB on ULB_Id = ProjectWork_ULB_Id
					    left join ePayment.dbo.tbl_Division on Division_Id = ProjectWork_DivisionId
					    left join ePayment.dbo.tbl_Circle on Circle_Id = Division_CircleId
					    left join ePayment.dbo.tbl_Zone on Zone_Id = Circle_ZoneId
                        left join ePayment.dbo.tbl_VidhanSabha on VidhanSabha_Id = ProjectWork_VidhanSabha_Id
                        left join ePayment.dbo.tbl_LokSabha on LokSabha_Id = VidhanSabha_LokSabhaId
                        left join
                        (
	                        select 
		                        ProjectWorkGO_Work_Id, 
                                Total_Release = sum(isnull(ProjectWorkGO_TotalRelease, 0)) / 100000 
	                        from ePayment.dbo.tbl_ProjectWorkGO 
	                        where ProjectWorkGO_Status = 1 
	                        group by ProjectWorkGO_Work_Id
                        ) tGO on tGO.ProjectWorkGO_Work_Id = ProjectWork_Id
					    left join 
					    (
                            select 
	                            ProjectWorkPkg_Work_Id,
	                            tender_cost = sum(isnull(tender_cost_With_GST, 0)), 
	                            tender_cost_1 = sum(isnull(tender_cost_WithOut_GST, 0)), 
	                            ProjectWorkPkg_Due_Date = convert(char(10), max(ProjectWorkPkg_Due_Date), 103),
	                            ProjectWorkPkg_Start_Date = convert(char(10), min(ProjectWorkPkg_Start_Date), 103),
	                            ProjectWorkPkg_Agreement_Date = convert(char(10), min(ProjectWorkPkg_Agreement_Date), 103), 
	                            ProjectWorkPkg_End_Date_Extended = convert(char(10), max(case when ProjectWorkPkg_ExtendDate is null then ProjectWorkPkg_Due_Date else ProjectWorkPkg_ExtendDate end), 103)
                            from (
                            select 
                                ProjectWorkPkg_Work_Id,
                                tender_cost_With_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then (isnull(ProjectWorkPkg_AgreementAmount, 0) * (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) else ProjectWorkPkg_AgreementAmount end) else isnull(Tender_Cost_Include_GST, 0) end, 
	                            tender_cost_WithOut_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then ISNULL(ProjectWorkPkg_AgreementAmount, 0) else (isnull(ProjectWorkPkg_AgreementAmount, 0) / (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) end) else isnull(Tender_Cost_Exclude_GST, 0) end,
	                            ProjectWorkPkg_Due_Date,
                                ProjectWorkPkg_Start_Date, 
	                            ProjectWorkPkg_Agreement_Date, 
                                ProjectWorkPkg_ExtendDate, 
                                ProjectWorkPkg_Percent, 
	                            ProjectWorkPkg_Id_TC = isnull(ProjectWorkPkg_Id_TC, 0), 
	                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
	                            Tender_Cost_Include_GST = isnull(Tender_Cost_Include_GST, 0)
                            from ePayment.dbo.tbl_ProjectWorkPkg 
                            left join 
                            (
	                            select 
		                            ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id, 
		                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
		                            Tender_Cost_Include_GST = isnull(Final_Tender_Cost, 0)
	                            from ePayment.dbo.tbl_Tender_Cost_Pkg_Wise_Automated
                            ) tTenderCost_Auto on tTenderCost_Auto.ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id
                            where ProjectWorkPkg_Status = 1	
                            ) tData
                            group by ProjectWorkPkg_Work_Id
					    ) tProjectWorkPkg  on tProjectWorkPkg.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id
					    left join 
                        (
                            select 
                                row_number() over (partition by ProjectWorkFinancialTarget_ProjectWork_Id order by ProjectWorkFinancialTarget_Id desc) rrT,
                                ProjectWorkFinancialTarget_ProjectWork_Id, 
                                ProjectWorkFinancialTarget_Target, 
                                ProjectWorkPhysicalTarget_Target
                            from ePayment.dbo.tbl_ProjectWorkFinancialTarget
                            where ProjectWorkFinancialTarget_Status = 1   
                        ) tTarget on tTarget.ProjectWorkFinancialTarget_ProjectWork_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id and rrT = 1
                        left join
					    (
						    select 
							    ProjectWorkPkg_Work_Id, 
							    Total_Amount = sum(cast((ISNULL(FinancialTrans_TransAmount,0) / 100000) as decimal(18,2))), 
							    Amount = sum(cast((ISNULL(FinancialTrans_Amount,0) / 100000) as decimal(18,2))), 
							    GST = sum(cast((ISNULL(FinancialTrans_GST,0) / 100000) as decimal(18,2))) 
						    from ePayment.dbo.tbl_PackageInvoice
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = PackageInvoice_Package_Id
						    inner join ePayment.dbo.tbl_FinancialTrans on FinancialTrans_Invoice_Id = PackageInvoice_Id
						    where PackageInvoice_Status = 1 and FinancialTrans_EntryType = 'Fund Allocated' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1 and PackageInvoice_Id not in (select PackageInvoiceEMBMasterLink_Invoice_Id from ePayment.dbo.tbl_PackageInvoiceEMBMasterLink where PackageInvoiceEMBMasterLink_Status=1) 
						    group by ProjectWorkPkg_Work_Id
					    ) tPrevInvoice on tPrevInvoice.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id
                        left join
                        (
                            select 
							    ProjectWorkPkg_Work_Id,
							    Amount = sum(cast((ISNULL(FinancialTrans_Amount,0) / 100000) as decimal(18,4))) 
						    from ePayment.dbo.tbl_Package_ADP
						    inner join ePayment.dbo.tbl_FinancialTrans on FinancialTrans_Invoice_Id = Package_ADP_Id 
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
						    where Package_ADP_Status = 1 and FinancialTrans_EntryType = 'ADP' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1
						    group by ProjectWorkPkg_Work_Id
                        ) tPrevInvoiceADP on tPrevInvoiceADP.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id
					    left join 
					    (
						    select 
							    ProjectWorkPkg_Work_Id,
							    Total_Invoice = count(*), 
							    Total_Invoice_Value = sum(isnull(InvoiceAmount, 0)) / 100000, 
							    Deffered_Value = sum(case when (isnull(PackageInvoiceApproval_Next_Designation_Id, 0) = 0 and isnull(PackageInvoiceApproval_Next_Organisation_Id, 0) = 0 and  PackageInvoiceApproval_Status_Id not in (1, 6)) then isnull(InvoiceAmount, 0) else 0 end) / 100000
						    from ePayment.dbo.tbl_PackageInvoice 
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = PackageInvoice_Package_Id
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
							    from ePayment.dbo.tbl_PackageInvoiceApproval
							    left join ePayment.dbo.tbl_InvoiceStatus on InvoiceStatus_Id = PackageInvoiceApproval_Status_Id
							    where PackageInvoiceApproval_Status = 1
						    ) tInvoiceApproval on tInvoiceApproval.PackageInvoiceApproval_PackageInvoice_Id = PackageInvoice_Id and tInvoiceApproval.rrrrr = 1
						    where PackageInvoice_Status = 1 and ProjectWorkPkg_Status = 1  
						    group by ProjectWorkPkg_Work_Id
					    ) tInvoice on tInvoice.ProjectWorkPkg_Work_Id = ProjectWork_Id 
                        left join 
					    (
						    select 
							    ProjectWorkPkg_Work_Id,
							    Total_Invoice_ADP = count(*), 
							    Total_Value_ADP = sum(isnull(Package_ADP_ADPTotalAmount, 0)) / 100000
						    from ePayment.dbo.tbl_Package_ADP 
                            join
                            (
                                select
                                    Package_ADP_Item_Package_ADP_Id,
		                            Total_Line_Items = count(*),
		                            Total_Amount = sum(isnull(Package_ADP_Item_TotalAmount, 0))
                                from ePayment.dbo.tbl_Package_ADP_Item
                                where Package_ADP_Item_Status = 1
                                group by Package_ADP_Item_Package_ADP_Id
                            ) tPackageInvoiceItem on Package_ADP_Item_Package_ADP_Id = Package_ADP_Id
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
						    join 
						    (
							    select 
								    ROW_NUMBER() over (partition by PackageADPApproval_Package_ADP_Id order by PackageADPApproval_Id desc) rrrrr,
								    PackageADPApproval_Next_Designation_Id,
								    PackageADPApproval_Next_Organisation_Id,
								    PackageADPApproval_Comments,
								    PackageADPApproval_AddedBy,
								    PackageADPApproval_AddedOn = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Status_Id,
								    PackageADPApproval_Package_Id,
								    PackageADPApproval_Package_ADP_Id,
								    InvoiceStatus_Name,
								    PackageADPApproval_Date = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Id
							    from ePayment.dbo.tbl_PackageADPApproval
							    left join ePayment.dbo.tbl_InvoiceStatus on InvoiceStatus_Id = PackageADPApproval_Status_Id
							    where PackageADPApproval_Status = 1
						    ) tADPApproval on tADPApproval.PackageADPApproval_Package_ADP_Id = Package_ADP_Id and tADPApproval.rrrrr = 1
						    where Package_ADP_Status = 1 and ProjectWorkPkg_Status = 1  
						    group by ProjectWorkPkg_Work_Id
					    ) tADP on tADP.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id 
                        left join 
                        (
	                        select 
		                        ProjectWorkGallery_Work_Id,
		                        Total_Photo = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) not in ('mov', 'mp4') then 1 else 0 end),
		                        Total_Video = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) in ('mov', 'mp4') then 1 else 0 end), 
                                Last_Updated = max(ProjectWorkGallery_AddedOn)
	                        from ePayment.dbo.tbl_ProjectWorkGallery 
	                        where ProjectWorkGallery_Status = 1  
	                        group by ProjectWorkGallery_Work_Id
                        ) tGallery on tGallery.ProjectWorkGallery_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id 
                        where ProjectWork_Status = 1 and ProjectWork_Project_Id in (12, 1013, 1016) ULBTypeCond2 ";

            strQuery += ") tData where 1=1 ";

            if (obj_SearchCriteria.ULB_Type == 1)
            {//NN
                strQuery = strQuery.Replace("ULBTypeCond1", " and Division_Type = 'NN'");
                strQuery = strQuery.Replace("ULBTypeCond2", " and ULB_Type = 'NN'");
            }
            else if (obj_SearchCriteria.ULB_Type == 1)
            {//NPP
                strQuery = strQuery.Replace("ULBTypeCond1", " and Division_Type = 'NPP'");
                strQuery = strQuery.Replace("ULBTypeCond2", " and ULB_Type = 'NPP'");
            }
            else if (obj_SearchCriteria.ULB_Type == 1)
            {//NP
                strQuery = strQuery.Replace("ULBTypeCond1", " and Division_Type = 'NP'");
                strQuery = strQuery.Replace("ULBTypeCond2", " and ULB_Type = 'NP'");
            }
            else
            {
                strQuery = strQuery.Replace("ULBTypeCond1", " ");
                strQuery = strQuery.Replace("ULBTypeCond2", " ");
            }

            if (obj_SearchCriteria.Scheme_Id > 0)
            {
                strQuery += " and ProjectWork_Project_Id = " + obj_SearchCriteria.Scheme_Id + "";
            }
            if (obj_SearchCriteria.ProjectWork_Id > 0)
            {
                strQuery += " and ProjectWork_Id = " + obj_SearchCriteria.ProjectWork_Id + "";
            }
            if (obj_SearchCriteria.FromDate != "")
            {
                strQuery += " and convert(date, convert(char(10), ProjectWork_GO_Date, 103), 103) >= convert(date, convert(char(10), '" + obj_SearchCriteria.FromDate + "', 103), 103)";
            }
            if (obj_SearchCriteria.TillDate != "")
            {
                strQuery += " and convert(date, convert(char(10), ProjectWork_GO_Date, 103), 103) <= convert(date, convert(char(10), '" + obj_SearchCriteria.TillDate + "', 103), 103)";
            }
            if (obj_SearchCriteria.District_Id != 0)
            {
                strQuery += " and Division_CircleId = '" + obj_SearchCriteria.District_Id + "'";
            }
            if (obj_SearchCriteria.ULB_Id != 0)
            {
                strQuery += " and ProjectWork_DivisionId = '" + obj_SearchCriteria.ULB_Id + "'";
            }
            if (obj_SearchCriteria.LokSabha_Id != 0)
            {
                strQuery += " and LokSabha_Id = '" + obj_SearchCriteria.LokSabha_Id + "'";
            }
            if (obj_SearchCriteria.VidhanSabha_Id != 0)
            {
                strQuery += " and VidhanSabha_Id = '" + obj_SearchCriteria.VidhanSabha_Id + "'";
            }
            if (obj_SearchCriteria.ProjectType_Id != 0)
            {
                strQuery += " and ProjectWork_ProjectType_Id = '" + obj_SearchCriteria.ProjectType_Id + "'";
            }
            if (obj_SearchCriteria.ProjectCode != "")
            {
                strQuery += " and ProjectWork_ProjectCode like '%" + obj_SearchCriteria.ProjectCode + "%'";
            }
            strQuery += " order by Zone_Name, Circle_Name, Division_Name, ProjectWork_ProjectCode";
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

        public DataSet get_tbl_ProjectWork_Scheme_Wise(SearchCriteria obj_SearchCriteria)
        {
            string strQuery = "";
            if (obj_SearchCriteria.FinancialYear_Id > 0)
            {
                string strQueryFY = "";
                DataSet dsFY = new DataSet();
                strQueryFY = @"set dateformat dmy; 
                            select 
                                FinancialYear_Id,
	                            convert(char(10), FinancialYear_StartYear, 103) FinancialYear_StartYear,
	                            convert(char(10), FinancialYear_EndYear, 103) FinancialYear_EndYear,
	                            FinancialYear_Comments,
	                            FinancialYear_Status,
	                            FinancialYear_Active
                            from tbl_FinancialYear
                            where FinancialYear_Status = 1 and FinancialYear_Id = '" + obj_SearchCriteria.FinancialYear_Id + "'";
                dsFY = ExecuteSelectQuery(strQueryFY);
                if (Utility.CheckDataSet(dsFY))
                {
                    obj_SearchCriteria.TillDate = dsFY.Tables[0].Rows[0]["FinancialYear_EndYear"].ToString();
                    obj_SearchCriteria.FromDate = dsFY.Tables[0].Rows[0]["FinancialYear_StartYear"].ToString();
                }
            }
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; ";
            strQuery += Environment.NewLine;
            strQuery += @"select 
                            Project_Id, 
                            Project_Name, 
                            Total_Projects_Count = count(*),
                            Sanctioned_Cost = sum(isnull(ProjectWork_Budget, 0)),
                            Tender_Cost = sum(isnull(tender_cost, 0)),
                            Total_Release = sum(isnull(Total_Release, 0)),
                            Total_Expenditure = sum(isnull(Total_Expenditure, 0)),
                            Completed_Projects_Count = sum(case when isnull(Physical_Progress, 0) >= 100 then 1 else 0 end), 
                            Ongoing_Projects_Count = sum(case when isnull(Physical_Progress, 0) < 100 then 1 else 0 end), 
                            Completed_Projects = sum(case when isnull(Physical_Progress, 0) >= 100 then isnull(ProjectWork_Budget, 0) else 0 end), 
                            Ongoing_Projects = sum(case when isnull(Physical_Progress, 0) < 100 then isnull(ProjectWork_Budget, 0) else 0 end)
                            from 
                            ( 
                        select 
                            ProjectWork_Id, 
                            ProjectWork_Project_Id, 
                            Project_Name, 
                            Project_Id, 
                            ProjectWork_ProjectCode, 
                            ProjectWork_Name = REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_Name_Code = isnull(ProjectWork_ProjectCode, '') + ' - ' + REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''),
                            ProjectWork_Description = REPLACE(REPLACE(ProjectWork_Description, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_GO_Path,
                            ProjectWork_GO_Date = convert(char(10), ProjectWork_GO_Date, 103), 
						    ProjectWork_StartDate = convert(char(10), ProjectWork_StartDate, 103), 
						    ProjectWork_EndDate = convert(char(10), ProjectWork_EndDate, 103), 
                            ProjectWork_GO_No,
                            ProjectType_Name,
                            ProjectWork_Budget = convert(decimal(18, 3), isnull(ProjectWork_Budget, 0) + isnull(ProjectWork_Centage, 0)),
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
						    tender_cost = convert(decimal(18, 3), isnull(tender_cost, 0)),
						    tender_cost_1 = convert(decimal(18, 3), isnull(tender_cost_1, 0)),
                            Total_Release = convert(decimal(18, 3), isnull(Total_Release, 0)),
                            Total_Expenditure = convert(decimal(18, 3), isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)),
						    ProjectWorkPkg_Due_Date, 
                            ProjectWorkPkg_Start_Date,
						    ProjectWorkPkg_Agreement_Date,
                            Target_Date_Agreement_Extended = convert(char(10), ProjectWorkPkg_End_Date_Extended, 103), 
						    Financial_Progress = convert(decimal(18, 2), (case when isnull(tProjectWorkPkg.tender_cost, 0) > 0 then (((isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)) * 100) / (isnull(tProjectWorkPkg.tender_cost, 0) + isnull(ProjectWork_ADPCost, 0))) else 0 end)),
                            Physical_Progress = convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0)), 
                            Total_Photo = isnull(tGallery.Total_Photo, 0),
	                        LokSabha_Name, 
	                        VidhanSabha_Name, 
	                        LokSabha_Id, 
	                        VidhanSabha_Id,
                            Last_Updated
                        from tbl_ProjectWork
                        left join tbl_Project on Project_Id = ProjectWork_Project_Id
                        left join tbl_ProjectType on ProjectType_Id = ProjectWork_ProjectType_Id
					    left join M_Jurisdiction on M_Jurisdiction_Id = ProjectWork_DistrictId
					    left join tbl_ULB on ULB_Id = ProjectWork_ULB_Id
					    left join tbl_Division on Division_Id = ProjectWork_DivisionId
					    left join tbl_Circle on Circle_Id = Division_CircleId
					    left join tbl_Zone on Zone_Id = Circle_ZoneId
                        left join tbl_VidhanSabha on VidhanSabha_Id = ProjectWork_VidhanSabha_Id
                        left join tbl_LokSabha on LokSabha_Id = VidhanSabha_LokSabhaId
                        left join
                        (
	                        select 
		                        ProjectWorkGO_Work_Id, 
                                Total_Release = sum(isnull(ProjectWorkGO_TotalRelease, 0)) / 100000 
	                        from tbl_ProjectWorkGO 
	                        where ProjectWorkGO_Status = 1 
	                        group by ProjectWorkGO_Work_Id
                        ) tGO on tGO.ProjectWorkGO_Work_Id = ProjectWork_Id
					    left join 
					    (
                            select 
	                            ProjectWorkPkg_Work_Id,
	                            tender_cost = sum(isnull(tender_cost_With_GST, 0)), 
	                            tender_cost_1 = sum(isnull(tender_cost_WithOut_GST, 0)), 
	                            ProjectWorkPkg_Due_Date = convert(char(10), max(ProjectWorkPkg_Due_Date), 103),
	                            ProjectWorkPkg_Start_Date = convert(char(10), min(ProjectWorkPkg_Start_Date), 103),
	                            ProjectWorkPkg_Agreement_Date = convert(char(10), min(ProjectWorkPkg_Agreement_Date), 103), 
	                            ProjectWorkPkg_End_Date_Extended = convert(char(10), max(case when ProjectWorkPkg_ExtendDate is null then ProjectWorkPkg_Due_Date else ProjectWorkPkg_ExtendDate end), 103)
                            from (
                            select 
                                ProjectWorkPkg_Work_Id,
                                tender_cost_With_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then (isnull(ProjectWorkPkg_AgreementAmount, 0) * (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) else ProjectWorkPkg_AgreementAmount end) else isnull(Tender_Cost_Include_GST, 0) end, 
	                            tender_cost_WithOut_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then ISNULL(ProjectWorkPkg_AgreementAmount, 0) else (isnull(ProjectWorkPkg_AgreementAmount, 0) / (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) end) else isnull(Tender_Cost_Exclude_GST, 0) end,
	                            ProjectWorkPkg_Due_Date,
                                ProjectWorkPkg_Start_Date, 
	                            ProjectWorkPkg_Agreement_Date, 
                                ProjectWorkPkg_ExtendDate, 
                                ProjectWorkPkg_Percent, 
	                            ProjectWorkPkg_Id_TC = isnull(ProjectWorkPkg_Id_TC, 0), 
	                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
	                            Tender_Cost_Include_GST = isnull(Tender_Cost_Include_GST, 0)
                            from tbl_ProjectWorkPkg 
                            left join 
                            (
	                            select 
		                            ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id, 
		                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
		                            Tender_Cost_Include_GST = isnull(Final_Tender_Cost, 0)
	                            from tbl_Tender_Cost_Pkg_Wise_Automated
                            ) tTenderCost_Auto on tTenderCost_Auto.ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id
                            where ProjectWorkPkg_Status = 1	
                            ) tData
                            group by ProjectWorkPkg_Work_Id
					    ) tProjectWorkPkg  on tProjectWorkPkg.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id
					    left join 
                        (
                            select 
                                row_number() over (partition by ProjectWorkFinancialTarget_ProjectWork_Id order by ProjectWorkFinancialTarget_Id desc) rrT,
                                ProjectWorkFinancialTarget_ProjectWork_Id, 
                                ProjectWorkFinancialTarget_Target, 
                                ProjectWorkPhysicalTarget_Target
                            from tbl_ProjectWorkFinancialTarget
                            where ProjectWorkFinancialTarget_Status = 1   
                        ) tTarget on tTarget.ProjectWorkFinancialTarget_ProjectWork_Id = tbl_ProjectWork.ProjectWork_Id and rrT = 1
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
						    where PackageInvoice_Status = 1 and FinancialTrans_EntryType = 'Fund Allocated' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1 and PackageInvoice_Id not in (select PackageInvoiceEMBMasterLink_Invoice_Id from tbl_PackageInvoiceEMBMasterLink where PackageInvoiceEMBMasterLink_Status=1) 
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
						    where Package_ADP_Status = 1 and FinancialTrans_EntryType = 'ADP' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1
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
							    ProjectWorkPkg_Work_Id,
							    Total_Invoice_ADP = count(*), 
							    Total_Value_ADP = sum(isnull(Package_ADP_ADPTotalAmount, 0)) / 100000
						    from tbl_Package_ADP 
                            join
                            (
                                select
                                    Package_ADP_Item_Package_ADP_Id,
		                            Total_Line_Items = count(*),
		                            Total_Amount = sum(isnull(Package_ADP_Item_TotalAmount, 0))
                                from tbl_Package_ADP_Item
                                where Package_ADP_Item_Status = 1
                                group by Package_ADP_Item_Package_ADP_Id
                            ) tPackageInvoiceItem on Package_ADP_Item_Package_ADP_Id = Package_ADP_Id
						    join tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
						    join 
						    (
							    select 
								    ROW_NUMBER() over (partition by PackageADPApproval_Package_ADP_Id order by PackageADPApproval_Id desc) rrrrr,
								    PackageADPApproval_Next_Designation_Id,
								    PackageADPApproval_Next_Organisation_Id,
								    PackageADPApproval_Comments,
								    PackageADPApproval_AddedBy,
								    PackageADPApproval_AddedOn = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Status_Id,
								    PackageADPApproval_Package_Id,
								    PackageADPApproval_Package_ADP_Id,
								    InvoiceStatus_Name,
								    PackageADPApproval_Date = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Id
							    from tbl_PackageADPApproval
							    left join tbl_InvoiceStatus on InvoiceStatus_Id = PackageADPApproval_Status_Id
							    where PackageADPApproval_Status = 1
						    ) tADPApproval on tADPApproval.PackageADPApproval_Package_ADP_Id = Package_ADP_Id and tADPApproval.rrrrr = 1
						    where Package_ADP_Status = 1 and ProjectWorkPkg_Status = 1  
						    group by ProjectWorkPkg_Work_Id
					    ) tADP on tADP.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id 
                        left join 
                        (
	                        select 
		                        ProjectWorkGallery_Work_Id,
		                        Total_Photo = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) not in ('mov', 'mp4') then 1 else 0 end),
		                        Total_Video = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) in ('mov', 'mp4') then 1 else 0 end), 
                                Last_Updated = max(ProjectWorkGallery_AddedOn)
	                        from tbl_ProjectWorkGallery 
	                        where ProjectWorkGallery_Status = 1  
	                        group by ProjectWorkGallery_Work_Id
                        ) tGallery on tGallery.ProjectWorkGallery_Work_Id = tbl_ProjectWork.ProjectWork_Id 
                        where ProjectWork_Status = 1 and ProjectWork_Project_Id not in (12, 19, 1013, 1016) ULBTypeCond1 ";

            strQuery += Environment.NewLine;
            strQuery += "UNION ALL";
            strQuery += Environment.NewLine;

            strQuery += @"select 
                            ProjectWork_Id, 
                            ProjectWork_Project_Id, 
                            Project_Name, 
                            Project_Id, 
                            ProjectWork_ProjectCode, 
                            ProjectWork_Name = REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_Name_Code = isnull(ProjectWork_ProjectCode, '') + ' - ' + REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''),
                            ProjectWork_Description = REPLACE(REPLACE(ProjectWork_Description, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_GO_Path,
                            ProjectWork_GO_Date = convert(char(10), ProjectWork_GO_Date, 103), 
						    ProjectWork_StartDate = convert(char(10), ProjectWork_StartDate, 103), 
						    ProjectWork_EndDate = convert(char(10), ProjectWork_EndDate, 103), 
                            ProjectWork_GO_No,
                            ProjectType_Name,
                            ProjectWork_Budget = convert(decimal(18, 3), isnull(ProjectWork_Budget, 0) + isnull(ProjectWork_Centage, 0)),
                            ProjectWork_ProjectType_Id,
						    ULB_Name, 
						    Jurisdiction_Name_Eng, 
						    Division_Name = ULB_Name, 
						    Circle_Name = Jurisdiction_Name_Eng, 
						    Zone_Name = 'Uttar Pradesh', 
						    ProjectWork_DistrictId = ePayment_ULB_Circle_Id, 
                            ProjectWork_BlockId,
                            ProjectWork_ULB_Id = ePayment_ULB_Division_Id, 
						    ProjectWork_DivisionId = ePayment_ULB_Division_Id, 
						    Division_CircleId = ePayment_ULB_Circle_Id, 
						    tender_cost = convert(decimal(18, 3), isnull(tender_cost, 0)),
						    tender_cost_1 = convert(decimal(18, 3), isnull(tender_cost_1, 0)),
                            Total_Release = convert(decimal(18, 3), isnull(Total_Release, 0)),
                            Total_Expenditure = convert(decimal(18, 3), isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)),
						    ProjectWorkPkg_Due_Date, 
                            ProjectWorkPkg_Start_Date,
						    ProjectWorkPkg_Agreement_Date,
                            Target_Date_Agreement_Extended = convert(char(10), ProjectWorkPkg_End_Date_Extended, 103), 
						    Financial_Progress = convert(decimal(18, 2), (case when isnull(tProjectWorkPkg.tender_cost, 0) > 0 then (((isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)) * 100) / (isnull(tProjectWorkPkg.tender_cost, 0) + isnull(ProjectWork_ADPCost, 0))) else 0 end)),
                            Physical_Progress = convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0)), 
                            Total_Photo = isnull(tGallery.Total_Photo, 0),
	                        LokSabha_Name, 
	                        VidhanSabha_Name, 
	                        LokSabha_Id, 
	                        VidhanSabha_Id,
                            Last_Updated
                        from ePayment.dbo.tbl_ProjectWork
                        left join ePayment.dbo.tbl_Project on Project_Id = ProjectWork_Project_Id
                        left join ePayment.dbo.tbl_ProjectType on ProjectType_Id = ProjectWork_ProjectType_Id
					    left join ePayment.dbo.M_Jurisdiction on M_Jurisdiction_Id = ProjectWork_DistrictId
					    left join ePayment.dbo.tbl_ULB on ULB_Id = ProjectWork_ULB_Id
					    left join ePayment.dbo.tbl_Division on Division_Id = ProjectWork_DivisionId
					    left join ePayment.dbo.tbl_Circle on Circle_Id = Division_CircleId
					    left join ePayment.dbo.tbl_Zone on Zone_Id = Circle_ZoneId
                        left join ePayment.dbo.tbl_VidhanSabha on VidhanSabha_Id = ProjectWork_VidhanSabha_Id
                        left join ePayment.dbo.tbl_LokSabha on LokSabha_Id = VidhanSabha_LokSabhaId
                        left join
                        (
	                        select 
		                        ProjectWorkGO_Work_Id, 
                                Total_Release = sum(isnull(ProjectWorkGO_TotalRelease, 0)) / 100000 
	                        from ePayment.dbo.tbl_ProjectWorkGO 
	                        where ProjectWorkGO_Status = 1 
	                        group by ProjectWorkGO_Work_Id
                        ) tGO on tGO.ProjectWorkGO_Work_Id = ProjectWork_Id
					    left join 
					    (
                            select 
	                            ProjectWorkPkg_Work_Id,
	                            tender_cost = sum(isnull(tender_cost_With_GST, 0)), 
	                            tender_cost_1 = sum(isnull(tender_cost_WithOut_GST, 0)), 
	                            ProjectWorkPkg_Due_Date = convert(char(10), max(ProjectWorkPkg_Due_Date), 103),
	                            ProjectWorkPkg_Start_Date = convert(char(10), min(ProjectWorkPkg_Start_Date), 103),
	                            ProjectWorkPkg_Agreement_Date = convert(char(10), min(ProjectWorkPkg_Agreement_Date), 103), 
	                            ProjectWorkPkg_End_Date_Extended = convert(char(10), max(case when ProjectWorkPkg_ExtendDate is null then ProjectWorkPkg_Due_Date else ProjectWorkPkg_ExtendDate end), 103)
                            from (
                            select 
                                ProjectWorkPkg_Work_Id,
                                tender_cost_With_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then (isnull(ProjectWorkPkg_AgreementAmount, 0) * (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) else ProjectWorkPkg_AgreementAmount end) else isnull(Tender_Cost_Include_GST, 0) end, 
	                            tender_cost_WithOut_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then ISNULL(ProjectWorkPkg_AgreementAmount, 0) else (isnull(ProjectWorkPkg_AgreementAmount, 0) / (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) end) else isnull(Tender_Cost_Exclude_GST, 0) end,
	                            ProjectWorkPkg_Due_Date,
                                ProjectWorkPkg_Start_Date, 
	                            ProjectWorkPkg_Agreement_Date, 
                                ProjectWorkPkg_ExtendDate, 
                                ProjectWorkPkg_Percent, 
	                            ProjectWorkPkg_Id_TC = isnull(ProjectWorkPkg_Id_TC, 0), 
	                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
	                            Tender_Cost_Include_GST = isnull(Tender_Cost_Include_GST, 0)
                            from ePayment.dbo.tbl_ProjectWorkPkg 
                            left join 
                            (
	                            select 
		                            ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id, 
		                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
		                            Tender_Cost_Include_GST = isnull(Final_Tender_Cost, 0)
	                            from ePayment.dbo.tbl_Tender_Cost_Pkg_Wise_Automated
                            ) tTenderCost_Auto on tTenderCost_Auto.ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id
                            where ProjectWorkPkg_Status = 1	
                            ) tData
                            group by ProjectWorkPkg_Work_Id
					    ) tProjectWorkPkg  on tProjectWorkPkg.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id
					    left join 
                        (
                            select 
                                row_number() over (partition by ProjectWorkFinancialTarget_ProjectWork_Id order by ProjectWorkFinancialTarget_Id desc) rrT,
                                ProjectWorkFinancialTarget_ProjectWork_Id, 
                                ProjectWorkFinancialTarget_Target, 
                                ProjectWorkPhysicalTarget_Target
                            from ePayment.dbo.tbl_ProjectWorkFinancialTarget
                            where ProjectWorkFinancialTarget_Status = 1   
                        ) tTarget on tTarget.ProjectWorkFinancialTarget_ProjectWork_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id and rrT = 1
                        left join
					    (
						    select 
							    ProjectWorkPkg_Work_Id, 
							    Total_Amount = sum(cast((ISNULL(FinancialTrans_TransAmount,0) / 100000) as decimal(18,2))), 
							    Amount = sum(cast((ISNULL(FinancialTrans_Amount,0) / 100000) as decimal(18,2))), 
							    GST = sum(cast((ISNULL(FinancialTrans_GST,0) / 100000) as decimal(18,2))) 
						    from ePayment.dbo.tbl_PackageInvoice
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = PackageInvoice_Package_Id
						    inner join ePayment.dbo.tbl_FinancialTrans on FinancialTrans_Invoice_Id = PackageInvoice_Id
						    where PackageInvoice_Status = 1 and FinancialTrans_EntryType = 'Fund Allocated' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1 and PackageInvoice_Id not in (select PackageInvoiceEMBMasterLink_Invoice_Id from ePayment.dbo.tbl_PackageInvoiceEMBMasterLink where PackageInvoiceEMBMasterLink_Status=1) 
						    group by ProjectWorkPkg_Work_Id
					    ) tPrevInvoice on tPrevInvoice.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id
                        left join
                        (
                            select 
							    ProjectWorkPkg_Work_Id,
							    Amount = sum(cast((ISNULL(FinancialTrans_Amount,0) / 100000) as decimal(18,4))) 
						    from ePayment.dbo.tbl_Package_ADP
						    inner join ePayment.dbo.tbl_FinancialTrans on FinancialTrans_Invoice_Id = Package_ADP_Id 
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
						    where Package_ADP_Status = 1 and FinancialTrans_EntryType = 'ADP' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1
						    group by ProjectWorkPkg_Work_Id
                        ) tPrevInvoiceADP on tPrevInvoiceADP.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id
					    left join 
					    (
						    select 
							    ProjectWorkPkg_Work_Id,
							    Total_Invoice = count(*), 
							    Total_Invoice_Value = sum(isnull(InvoiceAmount, 0)) / 100000, 
							    Deffered_Value = sum(case when (isnull(PackageInvoiceApproval_Next_Designation_Id, 0) = 0 and isnull(PackageInvoiceApproval_Next_Organisation_Id, 0) = 0 and  PackageInvoiceApproval_Status_Id not in (1, 6)) then isnull(InvoiceAmount, 0) else 0 end) / 100000
						    from ePayment.dbo.tbl_PackageInvoice 
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = PackageInvoice_Package_Id
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
							    from ePayment.dbo.tbl_PackageInvoiceApproval
							    left join ePayment.dbo.tbl_InvoiceStatus on InvoiceStatus_Id = PackageInvoiceApproval_Status_Id
							    where PackageInvoiceApproval_Status = 1
						    ) tInvoiceApproval on tInvoiceApproval.PackageInvoiceApproval_PackageInvoice_Id = PackageInvoice_Id and tInvoiceApproval.rrrrr = 1
						    where PackageInvoice_Status = 1 and ProjectWorkPkg_Status = 1  
						    group by ProjectWorkPkg_Work_Id
					    ) tInvoice on tInvoice.ProjectWorkPkg_Work_Id = ProjectWork_Id 
                        left join 
					    (
						    select 
							    ProjectWorkPkg_Work_Id,
							    Total_Invoice_ADP = count(*), 
							    Total_Value_ADP = sum(isnull(Package_ADP_ADPTotalAmount, 0)) / 100000
						    from ePayment.dbo.tbl_Package_ADP 
                            join
                            (
                                select
                                    Package_ADP_Item_Package_ADP_Id,
		                            Total_Line_Items = count(*),
		                            Total_Amount = sum(isnull(Package_ADP_Item_TotalAmount, 0))
                                from ePayment.dbo.tbl_Package_ADP_Item
                                where Package_ADP_Item_Status = 1
                                group by Package_ADP_Item_Package_ADP_Id
                            ) tPackageInvoiceItem on Package_ADP_Item_Package_ADP_Id = Package_ADP_Id
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
						    join 
						    (
							    select 
								    ROW_NUMBER() over (partition by PackageADPApproval_Package_ADP_Id order by PackageADPApproval_Id desc) rrrrr,
								    PackageADPApproval_Next_Designation_Id,
								    PackageADPApproval_Next_Organisation_Id,
								    PackageADPApproval_Comments,
								    PackageADPApproval_AddedBy,
								    PackageADPApproval_AddedOn = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Status_Id,
								    PackageADPApproval_Package_Id,
								    PackageADPApproval_Package_ADP_Id,
								    InvoiceStatus_Name,
								    PackageADPApproval_Date = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Id
							    from ePayment.dbo.tbl_PackageADPApproval
							    left join ePayment.dbo.tbl_InvoiceStatus on InvoiceStatus_Id = PackageADPApproval_Status_Id
							    where PackageADPApproval_Status = 1
						    ) tADPApproval on tADPApproval.PackageADPApproval_Package_ADP_Id = Package_ADP_Id and tADPApproval.rrrrr = 1
						    where Package_ADP_Status = 1 and ProjectWorkPkg_Status = 1  
						    group by ProjectWorkPkg_Work_Id
					    ) tADP on tADP.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id 
                        left join 
                        (
	                        select 
		                        ProjectWorkGallery_Work_Id,
		                        Total_Photo = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) not in ('mov', 'mp4') then 1 else 0 end),
		                        Total_Video = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) in ('mov', 'mp4') then 1 else 0 end), 
                                Last_Updated = max(ProjectWorkGallery_AddedOn)
	                        from ePayment.dbo.tbl_ProjectWorkGallery 
	                        where ProjectWorkGallery_Status = 1  
	                        group by ProjectWorkGallery_Work_Id
                        ) tGallery on tGallery.ProjectWorkGallery_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id 
                        where ProjectWork_Status = 1 and ProjectWork_Project_Id in (12, 1013, 1016) ULBTypeCond2 ";

            strQuery += ") tData where 1=1 ";
            if (obj_SearchCriteria.ULB_Type == 1)
            {//NN
                strQuery = strQuery.Replace("ULBTypeCond1", " and Division_Type = 'NN'");
                strQuery = strQuery.Replace("ULBTypeCond2", " and ULB_Type = 'NN'");
            }
            else if (obj_SearchCriteria.ULB_Type == 1)
            {//NPP
                strQuery = strQuery.Replace("ULBTypeCond1", " and Division_Type = 'NPP'");
                strQuery = strQuery.Replace("ULBTypeCond2", " and ULB_Type = 'NPP'");
            }
            else if (obj_SearchCriteria.ULB_Type == 1)
            {//NP
                strQuery = strQuery.Replace("ULBTypeCond1", " and Division_Type = 'NP'");
                strQuery = strQuery.Replace("ULBTypeCond2", " and ULB_Type = 'NP'");
            }
            else
            {
                strQuery = strQuery.Replace("ULBTypeCond1", " ");
                strQuery = strQuery.Replace("ULBTypeCond2", " ");
            }
            if (obj_SearchCriteria.Scheme_Id > 0)
            {
                strQuery += " and ProjectWork_Project_Id = " + obj_SearchCriteria.Scheme_Id + "";
            }
            if (obj_SearchCriteria.ProjectWork_Id > 0)
            {
                strQuery += " and ProjectWork_Id = " + obj_SearchCriteria.ProjectWork_Id + "";
            }
            if (obj_SearchCriteria.FromDate != "")
            {
                strQuery += " and convert(date, convert(char(10), ProjectWork_GO_Date, 103), 103) >= convert(date, convert(char(10), '" + obj_SearchCriteria.FromDate + "', 103), 103)";
            }
            if (obj_SearchCriteria.TillDate != "")
            {
                strQuery += " and convert(date, convert(char(10), ProjectWork_GO_Date, 103), 103) <= convert(date, convert(char(10), '" + obj_SearchCriteria.TillDate + "', 103), 103)";
            }
            if (obj_SearchCriteria.District_Id != 0)
            {
                strQuery += " and Division_CircleId = '" + obj_SearchCriteria.District_Id + "'";
            }
            if (obj_SearchCriteria.ULB_Id != 0)
            {
                strQuery += " and ProjectWork_DivisionId = '" + obj_SearchCriteria.ULB_Id + "'";
            }
            if (obj_SearchCriteria.LokSabha_Id != 0)
            {
                strQuery += " and LokSabha_Id = '" + obj_SearchCriteria.LokSabha_Id + "'";
            }
            if (obj_SearchCriteria.VidhanSabha_Id != 0)
            {
                strQuery += " and VidhanSabha_Id = '" + obj_SearchCriteria.VidhanSabha_Id + "'";
            }
            if (obj_SearchCriteria.ProjectType_Id != 0)
            {
                strQuery += " and ProjectWork_ProjectType_Id = '" + obj_SearchCriteria.ProjectType_Id + "'";
            }
            if (obj_SearchCriteria.ProjectCode != "")
            {
                strQuery += " and ProjectWork_ProjectCode like '%" + obj_SearchCriteria.ProjectCode + "%'";
            }
            strQuery += " group by Project_Id, Project_Name order by Project_Name ";
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

        public DataSet get_tbl_ProjectWork_District_Wise(SearchCriteria obj_SearchCriteria)
        {
            string strQuery = "";
            if (obj_SearchCriteria.FinancialYear_Id > 0)
            {
                string strQueryFY = "";
                DataSet dsFY = new DataSet();
                strQueryFY = @"set dateformat dmy; 
                            select 
                                FinancialYear_Id,
	                            convert(char(10), FinancialYear_StartYear, 103) FinancialYear_StartYear,
	                            convert(char(10), FinancialYear_EndYear, 103) FinancialYear_EndYear,
	                            FinancialYear_Comments,
	                            FinancialYear_Status,
	                            FinancialYear_Active
                            from tbl_FinancialYear
                            where FinancialYear_Status = 1 and FinancialYear_Id = '" + obj_SearchCriteria.FinancialYear_Id + "'";
                dsFY = ExecuteSelectQuery(strQueryFY);
                if (Utility.CheckDataSet(dsFY))
                {
                    obj_SearchCriteria.TillDate = dsFY.Tables[0].Rows[0]["FinancialYear_EndYear"].ToString();
                    obj_SearchCriteria.FromDate = dsFY.Tables[0].Rows[0]["FinancialYear_StartYear"].ToString();
                }
            }
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; ";
            strQuery += Environment.NewLine;
            strQuery += @"select 
                            Circle_Name, 
                            Division_CircleId, 
                            Total_Projects_Count = count(*),
                            Sanctioned_Cost = sum(isnull(ProjectWork_Budget, 0)),
                            Tender_Cost = sum(isnull(tender_cost, 0)),
                            Total_Release = sum(isnull(Total_Release, 0)),
                            Total_Expenditure = sum(isnull(Total_Expenditure, 0)),
                            Completed_Projects_Count = sum(case when isnull(Physical_Progress, 0) >= 100 then 1 else 0 end), 
                            Ongoing_Projects_Count = sum(case when isnull(Physical_Progress, 0) < 100 then 1 else 0 end), 
                            Completed_Projects = sum(case when isnull(Physical_Progress, 0) >= 100 then isnull(ProjectWork_Budget, 0) else 0 end), 
                            Ongoing_Projects = sum(case when isnull(Physical_Progress, 0) < 100 then isnull(ProjectWork_Budget, 0) else 0 end)
                            from 
                            ( 
                        select 
                            ProjectWork_Id, 
                            ProjectWork_Project_Id, 
                            Project_Name, 
                            Project_Id, 
                            ProjectWork_ProjectCode, 
                            ProjectWork_Name = REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_Name_Code = isnull(ProjectWork_ProjectCode, '') + ' - ' + REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''),
                            ProjectWork_Description = REPLACE(REPLACE(ProjectWork_Description, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_GO_Path,
                            ProjectWork_GO_Date = convert(char(10), ProjectWork_GO_Date, 103), 
						    ProjectWork_StartDate = convert(char(10), ProjectWork_StartDate, 103), 
						    ProjectWork_EndDate = convert(char(10), ProjectWork_EndDate, 103), 
                            ProjectWork_GO_No,
                            ProjectType_Name,
                            ProjectWork_Budget = convert(decimal(18, 3), isnull(ProjectWork_Budget, 0) + isnull(ProjectWork_Centage, 0)),
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
						    tender_cost = convert(decimal(18, 3), isnull(tender_cost, 0)),
						    tender_cost_1 = convert(decimal(18, 3), isnull(tender_cost_1, 0)),
                            Total_Release = convert(decimal(18, 3), isnull(Total_Release, 0)),
                            Total_Expenditure = convert(decimal(18, 3), isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)),
						    ProjectWorkPkg_Due_Date, 
                            ProjectWorkPkg_Start_Date,
						    ProjectWorkPkg_Agreement_Date,
                            Target_Date_Agreement_Extended = convert(char(10), ProjectWorkPkg_End_Date_Extended, 103), 
						    Financial_Progress = convert(decimal(18, 2), (case when isnull(tProjectWorkPkg.tender_cost, 0) > 0 then (((isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)) * 100) / (isnull(tProjectWorkPkg.tender_cost, 0) + isnull(ProjectWork_ADPCost, 0))) else 0 end)),
                            Physical_Progress = convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0)), 
                            Total_Photo = isnull(tGallery.Total_Photo, 0),
	                        LokSabha_Name, 
	                        VidhanSabha_Name, 
	                        LokSabha_Id, 
	                        VidhanSabha_Id,
                            Last_Updated
                        from tbl_ProjectWork
                        left join tbl_Project on Project_Id = ProjectWork_Project_Id
                        left join tbl_ProjectType on ProjectType_Id = ProjectWork_ProjectType_Id
					    left join M_Jurisdiction on M_Jurisdiction_Id = ProjectWork_DistrictId
					    left join tbl_ULB on ULB_Id = ProjectWork_ULB_Id
					    left join tbl_Division on Division_Id = ProjectWork_DivisionId
					    left join tbl_Circle on Circle_Id = Division_CircleId
					    left join tbl_Zone on Zone_Id = Circle_ZoneId
                        left join tbl_VidhanSabha on VidhanSabha_Id = ProjectWork_VidhanSabha_Id
                        left join tbl_LokSabha on LokSabha_Id = VidhanSabha_LokSabhaId
                        left join
                        (
	                        select 
		                        ProjectWorkGO_Work_Id, 
                                Total_Release = sum(isnull(ProjectWorkGO_TotalRelease, 0)) / 100000 
	                        from tbl_ProjectWorkGO 
	                        where ProjectWorkGO_Status = 1 
	                        group by ProjectWorkGO_Work_Id
                        ) tGO on tGO.ProjectWorkGO_Work_Id = ProjectWork_Id
					    left join 
					    (
                            select 
	                            ProjectWorkPkg_Work_Id,
	                            tender_cost = sum(isnull(tender_cost_With_GST, 0)), 
	                            tender_cost_1 = sum(isnull(tender_cost_WithOut_GST, 0)), 
	                            ProjectWorkPkg_Due_Date = convert(char(10), max(ProjectWorkPkg_Due_Date), 103),
	                            ProjectWorkPkg_Start_Date = convert(char(10), min(ProjectWorkPkg_Start_Date), 103),
	                            ProjectWorkPkg_Agreement_Date = convert(char(10), min(ProjectWorkPkg_Agreement_Date), 103), 
	                            ProjectWorkPkg_End_Date_Extended = convert(char(10), max(case when ProjectWorkPkg_ExtendDate is null then ProjectWorkPkg_Due_Date else ProjectWorkPkg_ExtendDate end), 103)
                            from (
                            select 
                                ProjectWorkPkg_Work_Id,
                                tender_cost_With_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then (isnull(ProjectWorkPkg_AgreementAmount, 0) * (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) else ProjectWorkPkg_AgreementAmount end) else isnull(Tender_Cost_Include_GST, 0) end, 
	                            tender_cost_WithOut_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then ISNULL(ProjectWorkPkg_AgreementAmount, 0) else (isnull(ProjectWorkPkg_AgreementAmount, 0) / (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) end) else isnull(Tender_Cost_Exclude_GST, 0) end,
	                            ProjectWorkPkg_Due_Date,
                                ProjectWorkPkg_Start_Date, 
	                            ProjectWorkPkg_Agreement_Date, 
                                ProjectWorkPkg_ExtendDate, 
                                ProjectWorkPkg_Percent, 
	                            ProjectWorkPkg_Id_TC = isnull(ProjectWorkPkg_Id_TC, 0), 
	                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
	                            Tender_Cost_Include_GST = isnull(Tender_Cost_Include_GST, 0)
                            from tbl_ProjectWorkPkg 
                            left join 
                            (
	                            select 
		                            ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id, 
		                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
		                            Tender_Cost_Include_GST = isnull(Final_Tender_Cost, 0)
	                            from tbl_Tender_Cost_Pkg_Wise_Automated
                            ) tTenderCost_Auto on tTenderCost_Auto.ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id
                            where ProjectWorkPkg_Status = 1	
                            ) tData
                            group by ProjectWorkPkg_Work_Id
					    ) tProjectWorkPkg  on tProjectWorkPkg.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id
					    left join 
                        (
                            select 
                                row_number() over (partition by ProjectWorkFinancialTarget_ProjectWork_Id order by ProjectWorkFinancialTarget_Id desc) rrT,
                                ProjectWorkFinancialTarget_ProjectWork_Id, 
                                ProjectWorkFinancialTarget_Target, 
                                ProjectWorkPhysicalTarget_Target
                            from tbl_ProjectWorkFinancialTarget
                            where ProjectWorkFinancialTarget_Status = 1   
                        ) tTarget on tTarget.ProjectWorkFinancialTarget_ProjectWork_Id = tbl_ProjectWork.ProjectWork_Id and rrT = 1
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
						    where PackageInvoice_Status = 1 and FinancialTrans_EntryType = 'Fund Allocated' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1 and PackageInvoice_Id not in (select PackageInvoiceEMBMasterLink_Invoice_Id from tbl_PackageInvoiceEMBMasterLink where PackageInvoiceEMBMasterLink_Status=1) 
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
						    where Package_ADP_Status = 1 and FinancialTrans_EntryType = 'ADP' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1
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
							    ProjectWorkPkg_Work_Id,
							    Total_Invoice_ADP = count(*), 
							    Total_Value_ADP = sum(isnull(Package_ADP_ADPTotalAmount, 0)) / 100000
						    from tbl_Package_ADP 
                            join
                            (
                                select
                                    Package_ADP_Item_Package_ADP_Id,
		                            Total_Line_Items = count(*),
		                            Total_Amount = sum(isnull(Package_ADP_Item_TotalAmount, 0))
                                from tbl_Package_ADP_Item
                                where Package_ADP_Item_Status = 1
                                group by Package_ADP_Item_Package_ADP_Id
                            ) tPackageInvoiceItem on Package_ADP_Item_Package_ADP_Id = Package_ADP_Id
						    join tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
						    join 
						    (
							    select 
								    ROW_NUMBER() over (partition by PackageADPApproval_Package_ADP_Id order by PackageADPApproval_Id desc) rrrrr,
								    PackageADPApproval_Next_Designation_Id,
								    PackageADPApproval_Next_Organisation_Id,
								    PackageADPApproval_Comments,
								    PackageADPApproval_AddedBy,
								    PackageADPApproval_AddedOn = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Status_Id,
								    PackageADPApproval_Package_Id,
								    PackageADPApproval_Package_ADP_Id,
								    InvoiceStatus_Name,
								    PackageADPApproval_Date = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Id
							    from tbl_PackageADPApproval
							    left join tbl_InvoiceStatus on InvoiceStatus_Id = PackageADPApproval_Status_Id
							    where PackageADPApproval_Status = 1
						    ) tADPApproval on tADPApproval.PackageADPApproval_Package_ADP_Id = Package_ADP_Id and tADPApproval.rrrrr = 1
						    where Package_ADP_Status = 1 and ProjectWorkPkg_Status = 1  
						    group by ProjectWorkPkg_Work_Id
					    ) tADP on tADP.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id 
                        left join 
                        (
	                        select 
		                        ProjectWorkGallery_Work_Id,
		                        Total_Photo = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) not in ('mov', 'mp4') then 1 else 0 end),
		                        Total_Video = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) in ('mov', 'mp4') then 1 else 0 end), 
                                Last_Updated = max(ProjectWorkGallery_AddedOn)
	                        from tbl_ProjectWorkGallery 
	                        where ProjectWorkGallery_Status = 1  
	                        group by ProjectWorkGallery_Work_Id
                        ) tGallery on tGallery.ProjectWorkGallery_Work_Id = tbl_ProjectWork.ProjectWork_Id 
                        where ProjectWork_Status = 1 and ProjectWork_Project_Id not in (12, 19, 1013, 1016) ULBTypeCond1 ";

            strQuery += Environment.NewLine;
            strQuery += "UNION ALL";
            strQuery += Environment.NewLine;

            strQuery += @"select 
                            ProjectWork_Id, 
                            ProjectWork_Project_Id, 
                            Project_Name, 
                            Project_Id, 
                            ProjectWork_ProjectCode, 
                            ProjectWork_Name = REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_Name_Code = isnull(ProjectWork_ProjectCode, '') + ' - ' + REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''),
                            ProjectWork_Description = REPLACE(REPLACE(ProjectWork_Description, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_GO_Path,
                            ProjectWork_GO_Date = convert(char(10), ProjectWork_GO_Date, 103), 
						    ProjectWork_StartDate = convert(char(10), ProjectWork_StartDate, 103), 
						    ProjectWork_EndDate = convert(char(10), ProjectWork_EndDate, 103), 
                            ProjectWork_GO_No,
                            ProjectType_Name,
                            ProjectWork_Budget = convert(decimal(18, 3), isnull(ProjectWork_Budget, 0) + isnull(ProjectWork_Centage, 0)),
                            ProjectWork_ProjectType_Id,
						    ULB_Name, 
						    Jurisdiction_Name_Eng, 
						    Division_Name = ULB_Name, 
						    Circle_Name = Jurisdiction_Name_Eng, 
						    Zone_Name = 'Uttar Pradesh', 
						    ProjectWork_DistrictId = ePayment_ULB_Circle_Id, 
                            ProjectWork_BlockId,
                            ProjectWork_ULB_Id = ePayment_ULB_Division_Id, 
						    ProjectWork_DivisionId = ePayment_ULB_Division_Id, 
						    Division_CircleId = ePayment_ULB_Circle_Id, 
						    tender_cost = convert(decimal(18, 3), isnull(tender_cost, 0)),
						    tender_cost_1 = convert(decimal(18, 3), isnull(tender_cost_1, 0)),
                            Total_Release = convert(decimal(18, 3), isnull(Total_Release, 0)),
                            Total_Expenditure = convert(decimal(18, 3), isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)),
						    ProjectWorkPkg_Due_Date, 
                            ProjectWorkPkg_Start_Date,
						    ProjectWorkPkg_Agreement_Date,
                            Target_Date_Agreement_Extended = convert(char(10), ProjectWorkPkg_End_Date_Extended, 103), 
						    Financial_Progress = convert(decimal(18, 2), (case when isnull(tProjectWorkPkg.tender_cost, 0) > 0 then (((isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)) * 100) / (isnull(tProjectWorkPkg.tender_cost, 0) + isnull(ProjectWork_ADPCost, 0))) else 0 end)),
                            Physical_Progress = convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0)), 
                            Total_Photo = isnull(tGallery.Total_Photo, 0),
	                        LokSabha_Name, 
	                        VidhanSabha_Name, 
	                        LokSabha_Id, 
	                        VidhanSabha_Id,
                            Last_Updated
                        from ePayment.dbo.tbl_ProjectWork
                        left join ePayment.dbo.tbl_Project on Project_Id = ProjectWork_Project_Id
                        left join ePayment.dbo.tbl_ProjectType on ProjectType_Id = ProjectWork_ProjectType_Id
					    left join ePayment.dbo.M_Jurisdiction on M_Jurisdiction_Id = ProjectWork_DistrictId
					    left join ePayment.dbo.tbl_ULB on ULB_Id = ProjectWork_ULB_Id
					    left join ePayment.dbo.tbl_Division on Division_Id = ProjectWork_DivisionId
					    left join ePayment.dbo.tbl_Circle on Circle_Id = Division_CircleId
					    left join ePayment.dbo.tbl_Zone on Zone_Id = Circle_ZoneId
                        left join ePayment.dbo.tbl_VidhanSabha on VidhanSabha_Id = ProjectWork_VidhanSabha_Id
                        left join ePayment.dbo.tbl_LokSabha on LokSabha_Id = VidhanSabha_LokSabhaId
                        left join
                        (
	                        select 
		                        ProjectWorkGO_Work_Id, 
                                Total_Release = sum(isnull(ProjectWorkGO_TotalRelease, 0)) / 100000 
	                        from ePayment.dbo.tbl_ProjectWorkGO 
	                        where ProjectWorkGO_Status = 1 
	                        group by ProjectWorkGO_Work_Id
                        ) tGO on tGO.ProjectWorkGO_Work_Id = ProjectWork_Id
					    left join 
					    (
                            select 
	                            ProjectWorkPkg_Work_Id,
	                            tender_cost = sum(isnull(tender_cost_With_GST, 0)), 
	                            tender_cost_1 = sum(isnull(tender_cost_WithOut_GST, 0)), 
	                            ProjectWorkPkg_Due_Date = convert(char(10), max(ProjectWorkPkg_Due_Date), 103),
	                            ProjectWorkPkg_Start_Date = convert(char(10), min(ProjectWorkPkg_Start_Date), 103),
	                            ProjectWorkPkg_Agreement_Date = convert(char(10), min(ProjectWorkPkg_Agreement_Date), 103), 
	                            ProjectWorkPkg_End_Date_Extended = convert(char(10), max(case when ProjectWorkPkg_ExtendDate is null then ProjectWorkPkg_Due_Date else ProjectWorkPkg_ExtendDate end), 103)
                            from (
                            select 
                                ProjectWorkPkg_Work_Id,
                                tender_cost_With_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then (isnull(ProjectWorkPkg_AgreementAmount, 0) * (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) else ProjectWorkPkg_AgreementAmount end) else isnull(Tender_Cost_Include_GST, 0) end, 
	                            tender_cost_WithOut_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then ISNULL(ProjectWorkPkg_AgreementAmount, 0) else (isnull(ProjectWorkPkg_AgreementAmount, 0) / (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) end) else isnull(Tender_Cost_Exclude_GST, 0) end,
	                            ProjectWorkPkg_Due_Date,
                                ProjectWorkPkg_Start_Date, 
	                            ProjectWorkPkg_Agreement_Date, 
                                ProjectWorkPkg_ExtendDate, 
                                ProjectWorkPkg_Percent, 
	                            ProjectWorkPkg_Id_TC = isnull(ProjectWorkPkg_Id_TC, 0), 
	                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
	                            Tender_Cost_Include_GST = isnull(Tender_Cost_Include_GST, 0)
                            from ePayment.dbo.tbl_ProjectWorkPkg 
                            left join 
                            (
	                            select 
		                            ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id, 
		                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
		                            Tender_Cost_Include_GST = isnull(Final_Tender_Cost, 0)
	                            from ePayment.dbo.tbl_Tender_Cost_Pkg_Wise_Automated
                            ) tTenderCost_Auto on tTenderCost_Auto.ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id
                            where ProjectWorkPkg_Status = 1	
                            ) tData
                            group by ProjectWorkPkg_Work_Id
					    ) tProjectWorkPkg  on tProjectWorkPkg.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id
					    left join 
                        (
                            select 
                                row_number() over (partition by ProjectWorkFinancialTarget_ProjectWork_Id order by ProjectWorkFinancialTarget_Id desc) rrT,
                                ProjectWorkFinancialTarget_ProjectWork_Id, 
                                ProjectWorkFinancialTarget_Target, 
                                ProjectWorkPhysicalTarget_Target
                            from ePayment.dbo.tbl_ProjectWorkFinancialTarget
                            where ProjectWorkFinancialTarget_Status = 1   
                        ) tTarget on tTarget.ProjectWorkFinancialTarget_ProjectWork_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id and rrT = 1
                        left join
					    (
						    select 
							    ProjectWorkPkg_Work_Id, 
							    Total_Amount = sum(cast((ISNULL(FinancialTrans_TransAmount,0) / 100000) as decimal(18,2))), 
							    Amount = sum(cast((ISNULL(FinancialTrans_Amount,0) / 100000) as decimal(18,2))), 
							    GST = sum(cast((ISNULL(FinancialTrans_GST,0) / 100000) as decimal(18,2))) 
						    from ePayment.dbo.tbl_PackageInvoice
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = PackageInvoice_Package_Id
						    inner join ePayment.dbo.tbl_FinancialTrans on FinancialTrans_Invoice_Id = PackageInvoice_Id
						    where PackageInvoice_Status = 1 and FinancialTrans_EntryType = 'Fund Allocated' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1 and PackageInvoice_Id not in (select PackageInvoiceEMBMasterLink_Invoice_Id from ePayment.dbo.tbl_PackageInvoiceEMBMasterLink where PackageInvoiceEMBMasterLink_Status=1) 
						    group by ProjectWorkPkg_Work_Id
					    ) tPrevInvoice on tPrevInvoice.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id
                        left join
                        (
                            select 
							    ProjectWorkPkg_Work_Id,
							    Amount = sum(cast((ISNULL(FinancialTrans_Amount,0) / 100000) as decimal(18,4))) 
						    from ePayment.dbo.tbl_Package_ADP
						    inner join ePayment.dbo.tbl_FinancialTrans on FinancialTrans_Invoice_Id = Package_ADP_Id 
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
						    where Package_ADP_Status = 1 and FinancialTrans_EntryType = 'ADP' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1
						    group by ProjectWorkPkg_Work_Id
                        ) tPrevInvoiceADP on tPrevInvoiceADP.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id
					    left join 
					    (
						    select 
							    ProjectWorkPkg_Work_Id,
							    Total_Invoice = count(*), 
							    Total_Invoice_Value = sum(isnull(InvoiceAmount, 0)) / 100000, 
							    Deffered_Value = sum(case when (isnull(PackageInvoiceApproval_Next_Designation_Id, 0) = 0 and isnull(PackageInvoiceApproval_Next_Organisation_Id, 0) = 0 and  PackageInvoiceApproval_Status_Id not in (1, 6)) then isnull(InvoiceAmount, 0) else 0 end) / 100000
						    from ePayment.dbo.tbl_PackageInvoice 
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = PackageInvoice_Package_Id
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
							    from ePayment.dbo.tbl_PackageInvoiceApproval
							    left join ePayment.dbo.tbl_InvoiceStatus on InvoiceStatus_Id = PackageInvoiceApproval_Status_Id
							    where PackageInvoiceApproval_Status = 1
						    ) tInvoiceApproval on tInvoiceApproval.PackageInvoiceApproval_PackageInvoice_Id = PackageInvoice_Id and tInvoiceApproval.rrrrr = 1
						    where PackageInvoice_Status = 1 and ProjectWorkPkg_Status = 1  
						    group by ProjectWorkPkg_Work_Id
					    ) tInvoice on tInvoice.ProjectWorkPkg_Work_Id = ProjectWork_Id 
                        left join 
					    (
						    select 
							    ProjectWorkPkg_Work_Id,
							    Total_Invoice_ADP = count(*), 
							    Total_Value_ADP = sum(isnull(Package_ADP_ADPTotalAmount, 0)) / 100000
						    from ePayment.dbo.tbl_Package_ADP 
                            join
                            (
                                select
                                    Package_ADP_Item_Package_ADP_Id,
		                            Total_Line_Items = count(*),
		                            Total_Amount = sum(isnull(Package_ADP_Item_TotalAmount, 0))
                                from ePayment.dbo.tbl_Package_ADP_Item
                                where Package_ADP_Item_Status = 1
                                group by Package_ADP_Item_Package_ADP_Id
                            ) tPackageInvoiceItem on Package_ADP_Item_Package_ADP_Id = Package_ADP_Id
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
						    join 
						    (
							    select 
								    ROW_NUMBER() over (partition by PackageADPApproval_Package_ADP_Id order by PackageADPApproval_Id desc) rrrrr,
								    PackageADPApproval_Next_Designation_Id,
								    PackageADPApproval_Next_Organisation_Id,
								    PackageADPApproval_Comments,
								    PackageADPApproval_AddedBy,
								    PackageADPApproval_AddedOn = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Status_Id,
								    PackageADPApproval_Package_Id,
								    PackageADPApproval_Package_ADP_Id,
								    InvoiceStatus_Name,
								    PackageADPApproval_Date = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Id
							    from ePayment.dbo.tbl_PackageADPApproval
							    left join ePayment.dbo.tbl_InvoiceStatus on InvoiceStatus_Id = PackageADPApproval_Status_Id
							    where PackageADPApproval_Status = 1
						    ) tADPApproval on tADPApproval.PackageADPApproval_Package_ADP_Id = Package_ADP_Id and tADPApproval.rrrrr = 1
						    where Package_ADP_Status = 1 and ProjectWorkPkg_Status = 1  
						    group by ProjectWorkPkg_Work_Id
					    ) tADP on tADP.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id 
                        left join 
                        (
	                        select 
		                        ProjectWorkGallery_Work_Id,
		                        Total_Photo = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) not in ('mov', 'mp4') then 1 else 0 end),
		                        Total_Video = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) in ('mov', 'mp4') then 1 else 0 end), 
                                Last_Updated = max(ProjectWorkGallery_AddedOn)
	                        from ePayment.dbo.tbl_ProjectWorkGallery 
	                        where ProjectWorkGallery_Status = 1  
	                        group by ProjectWorkGallery_Work_Id
                        ) tGallery on tGallery.ProjectWorkGallery_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id 
                        where ProjectWork_Status = 1 and ProjectWork_Project_Id in (12, 1013, 1016) ULBTypeCond2 ";

            strQuery += ") tData where 1=1 ";

            if (obj_SearchCriteria.ULB_Type == 1)
            {//NN
                strQuery = strQuery.Replace("ULBTypeCond1", " and Division_Type = 'NN'");
                strQuery = strQuery.Replace("ULBTypeCond2", " and ULB_Type = 'NN'");
            }
            else if (obj_SearchCriteria.ULB_Type == 1)
            {//NPP
                strQuery = strQuery.Replace("ULBTypeCond1", " and Division_Type = 'NPP'");
                strQuery = strQuery.Replace("ULBTypeCond2", " and ULB_Type = 'NPP'");
            }
            else if (obj_SearchCriteria.ULB_Type == 1)
            {//NP
                strQuery = strQuery.Replace("ULBTypeCond1", " and Division_Type = 'NP'");
                strQuery = strQuery.Replace("ULBTypeCond2", " and ULB_Type = 'NP'");
            }
            else
            {
                strQuery = strQuery.Replace("ULBTypeCond1", " ");
                strQuery = strQuery.Replace("ULBTypeCond2", " ");
            }

            if (obj_SearchCriteria.Scheme_Id > 0)
            {
                strQuery += " and ProjectWork_Project_Id = " + obj_SearchCriteria.Scheme_Id + "";
            }
            if (obj_SearchCriteria.ProjectWork_Id > 0)
            {
                strQuery += " and ProjectWork_Id = " + obj_SearchCriteria.ProjectWork_Id + "";
            }
            if (obj_SearchCriteria.FromDate != "")
            {
                strQuery += " and convert(date, convert(char(10), ProjectWork_GO_Date, 103), 103) >= convert(date, convert(char(10), '" + obj_SearchCriteria.FromDate + "', 103), 103)";
            }
            if (obj_SearchCriteria.TillDate != "")
            {
                strQuery += " and convert(date, convert(char(10), ProjectWork_GO_Date, 103), 103) <= convert(date, convert(char(10), '" + obj_SearchCriteria.TillDate + "', 103), 103)";
            }
            if (obj_SearchCriteria.District_Id != 0)
            {
                strQuery += " and Division_CircleId = '" + obj_SearchCriteria.District_Id + "'";
            }
            if (obj_SearchCriteria.ULB_Id != 0)
            {
                strQuery += " and ProjectWork_DivisionId = '" + obj_SearchCriteria.ULB_Id + "'";
            }
            if (obj_SearchCriteria.LokSabha_Id != 0)
            {
                strQuery += " and LokSabha_Id = '" + obj_SearchCriteria.LokSabha_Id + "'";
            }
            if (obj_SearchCriteria.VidhanSabha_Id != 0)
            {
                strQuery += " and VidhanSabha_Id = '" + obj_SearchCriteria.VidhanSabha_Id + "'";
            }
            if (obj_SearchCriteria.ProjectType_Id != 0)
            {
                strQuery += " and ProjectWork_ProjectType_Id = '" + obj_SearchCriteria.ProjectType_Id + "'";
            }
            if (obj_SearchCriteria.ProjectCode != "")
            {
                strQuery += " and ProjectWork_ProjectCode like '%" + obj_SearchCriteria.ProjectCode + "%'";
            }
            strQuery += " group by Circle_Name, Division_CircleId order by Circle_Name ";
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

        public DataSet get_tbl_ProjectWork_ULB_Wise(SearchCriteria obj_SearchCriteria)
        {
            string strQuery = "";
            if (obj_SearchCriteria.FinancialYear_Id > 0)
            {
                string strQueryFY = "";
                DataSet dsFY = new DataSet();
                strQueryFY = @"set dateformat dmy; 
                            select 
                                FinancialYear_Id,
	                            convert(char(10), FinancialYear_StartYear, 103) FinancialYear_StartYear,
	                            convert(char(10), FinancialYear_EndYear, 103) FinancialYear_EndYear,
	                            FinancialYear_Comments,
	                            FinancialYear_Status,
	                            FinancialYear_Active
                            from tbl_FinancialYear
                            where FinancialYear_Status = 1 and FinancialYear_Id = '" + obj_SearchCriteria.FinancialYear_Id + "'";
                dsFY = ExecuteSelectQuery(strQueryFY);
                if (Utility.CheckDataSet(dsFY))
                {
                    obj_SearchCriteria.TillDate = dsFY.Tables[0].Rows[0]["FinancialYear_EndYear"].ToString();
                    obj_SearchCriteria.FromDate = dsFY.Tables[0].Rows[0]["FinancialYear_StartYear"].ToString();
                }
            }
            DataSet ds = new DataSet();
            strQuery = @"set dateformat dmy; ";
            strQuery += Environment.NewLine;
            strQuery += @"select 
                            Division_CircleId, 
                            Circle_Name,
                            Division_Name, 
                            ProjectWork_DivisionId, 
                            Total_Projects_Count = count(*),
                            Sanctioned_Cost = sum(isnull(ProjectWork_Budget, 0)),
                            Tender_Cost = sum(isnull(tender_cost, 0)),
                            Total_Release = sum(isnull(Total_Release, 0)),
                            Total_Expenditure = sum(isnull(Total_Expenditure, 0)),
                            Completed_Projects_Count = sum(case when isnull(Physical_Progress, 0) >= 100 then 1 else 0 end), 
                            Ongoing_Projects_Count = sum(case when isnull(Physical_Progress, 0) < 100 then 1 else 0 end), 
                            Completed_Projects = sum(case when isnull(Physical_Progress, 0) >= 100 then isnull(ProjectWork_Budget, 0) else 0 end), 
                            Ongoing_Projects = sum(case when isnull(Physical_Progress, 0) < 100 then isnull(ProjectWork_Budget, 0) else 0 end)
                            from 
                            ( 
                        select 
                            ProjectWork_Id, 
                            ProjectWork_Project_Id, 
                            Project_Name, 
                            Project_Id, 
                            ProjectWork_ProjectCode, 
                            ProjectWork_Name = REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_Name_Code = isnull(ProjectWork_ProjectCode, '') + ' - ' + REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''),
                            ProjectWork_Description = REPLACE(REPLACE(ProjectWork_Description, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_GO_Path,
                            ProjectWork_GO_Date = convert(char(10), ProjectWork_GO_Date, 103), 
						    ProjectWork_StartDate = convert(char(10), ProjectWork_StartDate, 103), 
						    ProjectWork_EndDate = convert(char(10), ProjectWork_EndDate, 103), 
                            ProjectWork_GO_No,
                            ProjectType_Name,
                            ProjectWork_Budget = convert(decimal(18, 3), isnull(ProjectWork_Budget, 0) + isnull(ProjectWork_Centage, 0)),
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
						    tender_cost = convert(decimal(18, 3), isnull(tender_cost, 0)),
						    tender_cost_1 = convert(decimal(18, 3), isnull(tender_cost_1, 0)),
                            Total_Release = convert(decimal(18, 3), isnull(Total_Release, 0)),
                            Total_Expenditure = convert(decimal(18, 3), isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)),
						    ProjectWorkPkg_Due_Date, 
                            ProjectWorkPkg_Start_Date,
						    ProjectWorkPkg_Agreement_Date,
                            Target_Date_Agreement_Extended = convert(char(10), ProjectWorkPkg_End_Date_Extended, 103), 
						    Financial_Progress = convert(decimal(18, 2), (case when isnull(tProjectWorkPkg.tender_cost, 0) > 0 then (((isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)) * 100) / (isnull(tProjectWorkPkg.tender_cost, 0) + isnull(ProjectWork_ADPCost, 0))) else 0 end)),
                            Physical_Progress = convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0)), 
                            Total_Photo = isnull(tGallery.Total_Photo, 0),
	                        LokSabha_Name, 
	                        VidhanSabha_Name, 
	                        LokSabha_Id, 
	                        VidhanSabha_Id,
                            Last_Updated
                        from tbl_ProjectWork
                        left join tbl_Project on Project_Id = ProjectWork_Project_Id
                        left join tbl_ProjectType on ProjectType_Id = ProjectWork_ProjectType_Id
					    left join M_Jurisdiction on M_Jurisdiction_Id = ProjectWork_DistrictId
					    left join tbl_ULB on ULB_Id = ProjectWork_ULB_Id
					    left join tbl_Division on Division_Id = ProjectWork_DivisionId
					    left join tbl_Circle on Circle_Id = Division_CircleId
					    left join tbl_Zone on Zone_Id = Circle_ZoneId
                        left join tbl_VidhanSabha on VidhanSabha_Id = ProjectWork_VidhanSabha_Id
                        left join tbl_LokSabha on LokSabha_Id = VidhanSabha_LokSabhaId
                        left join
                        (
	                        select 
		                        ProjectWorkGO_Work_Id, 
                                Total_Release = sum(isnull(ProjectWorkGO_TotalRelease, 0)) / 100000 
	                        from tbl_ProjectWorkGO 
	                        where ProjectWorkGO_Status = 1 
	                        group by ProjectWorkGO_Work_Id
                        ) tGO on tGO.ProjectWorkGO_Work_Id = ProjectWork_Id
					    left join 
					    (
                            select 
	                            ProjectWorkPkg_Work_Id,
	                            tender_cost = sum(isnull(tender_cost_With_GST, 0)), 
	                            tender_cost_1 = sum(isnull(tender_cost_WithOut_GST, 0)), 
	                            ProjectWorkPkg_Due_Date = convert(char(10), max(ProjectWorkPkg_Due_Date), 103),
	                            ProjectWorkPkg_Start_Date = convert(char(10), min(ProjectWorkPkg_Start_Date), 103),
	                            ProjectWorkPkg_Agreement_Date = convert(char(10), min(ProjectWorkPkg_Agreement_Date), 103), 
	                            ProjectWorkPkg_End_Date_Extended = convert(char(10), max(case when ProjectWorkPkg_ExtendDate is null then ProjectWorkPkg_Due_Date else ProjectWorkPkg_ExtendDate end), 103)
                            from (
                            select 
                                ProjectWorkPkg_Work_Id,
                                tender_cost_With_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then (isnull(ProjectWorkPkg_AgreementAmount, 0) * (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) else ProjectWorkPkg_AgreementAmount end) else isnull(Tender_Cost_Include_GST, 0) end, 
	                            tender_cost_WithOut_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then ISNULL(ProjectWorkPkg_AgreementAmount, 0) else (isnull(ProjectWorkPkg_AgreementAmount, 0) / (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) end) else isnull(Tender_Cost_Exclude_GST, 0) end,
	                            ProjectWorkPkg_Due_Date,
                                ProjectWorkPkg_Start_Date, 
	                            ProjectWorkPkg_Agreement_Date, 
                                ProjectWorkPkg_ExtendDate, 
                                ProjectWorkPkg_Percent, 
	                            ProjectWorkPkg_Id_TC = isnull(ProjectWorkPkg_Id_TC, 0), 
	                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
	                            Tender_Cost_Include_GST = isnull(Tender_Cost_Include_GST, 0)
                            from tbl_ProjectWorkPkg 
                            left join 
                            (
	                            select 
		                            ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id, 
		                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
		                            Tender_Cost_Include_GST = isnull(Final_Tender_Cost, 0)
	                            from tbl_Tender_Cost_Pkg_Wise_Automated
                            ) tTenderCost_Auto on tTenderCost_Auto.ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id
                            where ProjectWorkPkg_Status = 1	
                            ) tData
                            group by ProjectWorkPkg_Work_Id
					    ) tProjectWorkPkg  on tProjectWorkPkg.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id
					    left join 
                        (
                            select 
                                row_number() over (partition by ProjectWorkFinancialTarget_ProjectWork_Id order by ProjectWorkFinancialTarget_Id desc) rrT,
                                ProjectWorkFinancialTarget_ProjectWork_Id, 
                                ProjectWorkFinancialTarget_Target, 
                                ProjectWorkPhysicalTarget_Target
                            from tbl_ProjectWorkFinancialTarget
                            where ProjectWorkFinancialTarget_Status = 1   
                        ) tTarget on tTarget.ProjectWorkFinancialTarget_ProjectWork_Id = tbl_ProjectWork.ProjectWork_Id and rrT = 1
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
						    where PackageInvoice_Status = 1 and FinancialTrans_EntryType = 'Fund Allocated' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1 and PackageInvoice_Id not in (select PackageInvoiceEMBMasterLink_Invoice_Id from tbl_PackageInvoiceEMBMasterLink where PackageInvoiceEMBMasterLink_Status=1) 
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
						    where Package_ADP_Status = 1 and FinancialTrans_EntryType = 'ADP' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1
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
							    ProjectWorkPkg_Work_Id,
							    Total_Invoice_ADP = count(*), 
							    Total_Value_ADP = sum(isnull(Package_ADP_ADPTotalAmount, 0)) / 100000
						    from tbl_Package_ADP 
                            join
                            (
                                select
                                    Package_ADP_Item_Package_ADP_Id,
		                            Total_Line_Items = count(*),
		                            Total_Amount = sum(isnull(Package_ADP_Item_TotalAmount, 0))
                                from tbl_Package_ADP_Item
                                where Package_ADP_Item_Status = 1
                                group by Package_ADP_Item_Package_ADP_Id
                            ) tPackageInvoiceItem on Package_ADP_Item_Package_ADP_Id = Package_ADP_Id
						    join tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
						    join 
						    (
							    select 
								    ROW_NUMBER() over (partition by PackageADPApproval_Package_ADP_Id order by PackageADPApproval_Id desc) rrrrr,
								    PackageADPApproval_Next_Designation_Id,
								    PackageADPApproval_Next_Organisation_Id,
								    PackageADPApproval_Comments,
								    PackageADPApproval_AddedBy,
								    PackageADPApproval_AddedOn = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Status_Id,
								    PackageADPApproval_Package_Id,
								    PackageADPApproval_Package_ADP_Id,
								    InvoiceStatus_Name,
								    PackageADPApproval_Date = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Id
							    from tbl_PackageADPApproval
							    left join tbl_InvoiceStatus on InvoiceStatus_Id = PackageADPApproval_Status_Id
							    where PackageADPApproval_Status = 1
						    ) tADPApproval on tADPApproval.PackageADPApproval_Package_ADP_Id = Package_ADP_Id and tADPApproval.rrrrr = 1
						    where Package_ADP_Status = 1 and ProjectWorkPkg_Status = 1  
						    group by ProjectWorkPkg_Work_Id
					    ) tADP on tADP.ProjectWorkPkg_Work_Id = tbl_ProjectWork.ProjectWork_Id 
                        left join 
                        (
	                        select 
		                        ProjectWorkGallery_Work_Id,
		                        Total_Photo = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) not in ('mov', 'mp4') then 1 else 0 end),
		                        Total_Video = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) in ('mov', 'mp4') then 1 else 0 end), 
                                Last_Updated = max(ProjectWorkGallery_AddedOn)
	                        from tbl_ProjectWorkGallery 
	                        where ProjectWorkGallery_Status = 1  
	                        group by ProjectWorkGallery_Work_Id
                        ) tGallery on tGallery.ProjectWorkGallery_Work_Id = tbl_ProjectWork.ProjectWork_Id 
                        where ProjectWork_Status = 1 and ProjectWork_Project_Id not in (12, 19, 1013, 1016) ULBTypeCond1 ";

            strQuery += Environment.NewLine;
            strQuery += "UNION ALL";
            strQuery += Environment.NewLine;

            strQuery += @"select 
                            ProjectWork_Id, 
                            ProjectWork_Project_Id, 
                            Project_Name, 
                            Project_Id, 
                            ProjectWork_ProjectCode, 
                            ProjectWork_Name = REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_Name_Code = isnull(ProjectWork_ProjectCode, '') + ' - ' + REPLACE(REPLACE(ProjectWork_Name, CHAR(13),''), CHAR(10),''),
                            ProjectWork_Description = REPLACE(REPLACE(ProjectWork_Description, CHAR(13),''), CHAR(10),''), 
                            ProjectWork_GO_Path,
                            ProjectWork_GO_Date = convert(char(10), ProjectWork_GO_Date, 103), 
						    ProjectWork_StartDate = convert(char(10), ProjectWork_StartDate, 103), 
						    ProjectWork_EndDate = convert(char(10), ProjectWork_EndDate, 103), 
                            ProjectWork_GO_No,
                            ProjectType_Name,
                            ProjectWork_Budget = convert(decimal(18, 3), isnull(ProjectWork_Budget, 0) + isnull(ProjectWork_Centage, 0)),
                            ProjectWork_ProjectType_Id,
						    ULB_Name, 
						    Jurisdiction_Name_Eng, 
						    Division_Name = ULB_Name, 
						    Circle_Name = Jurisdiction_Name_Eng, 
						    Zone_Name = 'Uttar Pradesh', 
						    ProjectWork_DistrictId = ePayment_ULB_Circle_Id, 
                            ProjectWork_BlockId,
                            ProjectWork_ULB_Id = ePayment_ULB_Division_Id, 
						    ProjectWork_DivisionId = ePayment_ULB_Division_Id, 
						    Division_CircleId = ePayment_ULB_Circle_Id, 
						    tender_cost = convert(decimal(18, 3), isnull(tender_cost, 0)),
						    tender_cost_1 = convert(decimal(18, 3), isnull(tender_cost_1, 0)),
                            Total_Release = convert(decimal(18, 3), isnull(Total_Release, 0)),
                            Total_Expenditure = convert(decimal(18, 3), isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)),
						    ProjectWorkPkg_Due_Date, 
                            ProjectWorkPkg_Start_Date,
						    ProjectWorkPkg_Agreement_Date,
                            Target_Date_Agreement_Extended = convert(char(10), ProjectWorkPkg_End_Date_Extended, 103), 
						    Financial_Progress = convert(decimal(18, 2), (case when isnull(tProjectWorkPkg.tender_cost, 0) > 0 then (((isnull(tInvoice.Total_Invoice_Value, 0) + isnull(tPrevInvoiceADP.Amount, 0) + isnull(tADP.Total_Value_ADP, 0) + isnull(tPrevInvoice.Amount, 0)) * 100) / (isnull(tProjectWorkPkg.tender_cost, 0) + isnull(ProjectWork_ADPCost, 0))) else 0 end)),
                            Physical_Progress = convert(decimal(18, 2), isnull(tTarget.ProjectWorkPhysicalTarget_Target, 0)), 
                            Total_Photo = isnull(tGallery.Total_Photo, 0),
	                        LokSabha_Name, 
	                        VidhanSabha_Name, 
	                        LokSabha_Id, 
	                        VidhanSabha_Id,
                            Last_Updated
                        from ePayment.dbo.tbl_ProjectWork
                        left join ePayment.dbo.tbl_Project on Project_Id = ProjectWork_Project_Id
                        left join ePayment.dbo.tbl_ProjectType on ProjectType_Id = ProjectWork_ProjectType_Id
					    left join ePayment.dbo.M_Jurisdiction on M_Jurisdiction_Id = ProjectWork_DistrictId
					    left join ePayment.dbo.tbl_ULB on ULB_Id = ProjectWork_ULB_Id
					    left join ePayment.dbo.tbl_Division on Division_Id = ProjectWork_DivisionId
					    left join ePayment.dbo.tbl_Circle on Circle_Id = Division_CircleId
					    left join ePayment.dbo.tbl_Zone on Zone_Id = Circle_ZoneId
                        left join ePayment.dbo.tbl_VidhanSabha on VidhanSabha_Id = ProjectWork_VidhanSabha_Id
                        left join ePayment.dbo.tbl_LokSabha on LokSabha_Id = VidhanSabha_LokSabhaId
                        left join
                        (
	                        select 
		                        ProjectWorkGO_Work_Id, 
                                Total_Release = sum(isnull(ProjectWorkGO_TotalRelease, 0)) / 100000 
	                        from ePayment.dbo.tbl_ProjectWorkGO 
	                        where ProjectWorkGO_Status = 1 
	                        group by ProjectWorkGO_Work_Id
                        ) tGO on tGO.ProjectWorkGO_Work_Id = ProjectWork_Id
					    left join 
					    (
                            select 
	                            ProjectWorkPkg_Work_Id,
	                            tender_cost = sum(isnull(tender_cost_With_GST, 0)), 
	                            tender_cost_1 = sum(isnull(tender_cost_WithOut_GST, 0)), 
	                            ProjectWorkPkg_Due_Date = convert(char(10), max(ProjectWorkPkg_Due_Date), 103),
	                            ProjectWorkPkg_Start_Date = convert(char(10), min(ProjectWorkPkg_Start_Date), 103),
	                            ProjectWorkPkg_Agreement_Date = convert(char(10), min(ProjectWorkPkg_Agreement_Date), 103), 
	                            ProjectWorkPkg_End_Date_Extended = convert(char(10), max(case when ProjectWorkPkg_ExtendDate is null then ProjectWorkPkg_Due_Date else ProjectWorkPkg_ExtendDate end), 103)
                            from (
                            select 
                                ProjectWorkPkg_Work_Id,
                                tender_cost_With_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then (isnull(ProjectWorkPkg_AgreementAmount, 0) * (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) else ProjectWorkPkg_AgreementAmount end) else isnull(Tender_Cost_Include_GST, 0) end, 
	                            tender_cost_WithOut_GST = case when isnull(ProjectWorkPkg_Id_TC, 0) = 0 then (case when isnull(ProjectWorkPkg_GST, 'Exclude GST') = 'Exclude GST' then ISNULL(ProjectWorkPkg_AgreementAmount, 0) else (isnull(ProjectWorkPkg_AgreementAmount, 0) / (1 + isnull(ProjectWorkPkg_Percent, 18) / 100)) end) else isnull(Tender_Cost_Exclude_GST, 0) end,
	                            ProjectWorkPkg_Due_Date,
                                ProjectWorkPkg_Start_Date, 
	                            ProjectWorkPkg_Agreement_Date, 
                                ProjectWorkPkg_ExtendDate, 
                                ProjectWorkPkg_Percent, 
	                            ProjectWorkPkg_Id_TC = isnull(ProjectWorkPkg_Id_TC, 0), 
	                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
	                            Tender_Cost_Include_GST = isnull(Tender_Cost_Include_GST, 0)
                            from ePayment.dbo.tbl_ProjectWorkPkg 
                            left join 
                            (
	                            select 
		                            ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id, 
		                            Tender_Cost_Exclude_GST = isnull(Tender_Cost_Exclude_GST, 0),
		                            Tender_Cost_Include_GST = isnull(Final_Tender_Cost, 0)
	                            from ePayment.dbo.tbl_Tender_Cost_Pkg_Wise_Automated
                            ) tTenderCost_Auto on tTenderCost_Auto.ProjectWorkPkg_Id_TC = ProjectWorkPkg_Id
                            where ProjectWorkPkg_Status = 1	
                            ) tData
                            group by ProjectWorkPkg_Work_Id
					    ) tProjectWorkPkg  on tProjectWorkPkg.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id
					    left join 
                        (
                            select 
                                row_number() over (partition by ProjectWorkFinancialTarget_ProjectWork_Id order by ProjectWorkFinancialTarget_Id desc) rrT,
                                ProjectWorkFinancialTarget_ProjectWork_Id, 
                                ProjectWorkFinancialTarget_Target, 
                                ProjectWorkPhysicalTarget_Target
                            from ePayment.dbo.tbl_ProjectWorkFinancialTarget
                            where ProjectWorkFinancialTarget_Status = 1   
                        ) tTarget on tTarget.ProjectWorkFinancialTarget_ProjectWork_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id and rrT = 1
                        left join
					    (
						    select 
							    ProjectWorkPkg_Work_Id, 
							    Total_Amount = sum(cast((ISNULL(FinancialTrans_TransAmount,0) / 100000) as decimal(18,2))), 
							    Amount = sum(cast((ISNULL(FinancialTrans_Amount,0) / 100000) as decimal(18,2))), 
							    GST = sum(cast((ISNULL(FinancialTrans_GST,0) / 100000) as decimal(18,2))) 
						    from ePayment.dbo.tbl_PackageInvoice
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = PackageInvoice_Package_Id
						    inner join ePayment.dbo.tbl_FinancialTrans on FinancialTrans_Invoice_Id = PackageInvoice_Id
						    where PackageInvoice_Status = 1 and FinancialTrans_EntryType = 'Fund Allocated' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1 and PackageInvoice_Id not in (select PackageInvoiceEMBMasterLink_Invoice_Id from ePayment.dbo.tbl_PackageInvoiceEMBMasterLink where PackageInvoiceEMBMasterLink_Status=1) 
						    group by ProjectWorkPkg_Work_Id
					    ) tPrevInvoice on tPrevInvoice.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id
                        left join
                        (
                            select 
							    ProjectWorkPkg_Work_Id,
							    Amount = sum(cast((ISNULL(FinancialTrans_Amount,0) / 100000) as decimal(18,4))) 
						    from ePayment.dbo.tbl_Package_ADP
						    inner join ePayment.dbo.tbl_FinancialTrans on FinancialTrans_Invoice_Id = Package_ADP_Id 
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
						    where Package_ADP_Status = 1 and FinancialTrans_EntryType = 'ADP' and FinancialTrans_TransType = 'C' and ProjectWorkPkg_Status = 1
						    group by ProjectWorkPkg_Work_Id
                        ) tPrevInvoiceADP on tPrevInvoiceADP.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id
					    left join 
					    (
						    select 
							    ProjectWorkPkg_Work_Id,
							    Total_Invoice = count(*), 
							    Total_Invoice_Value = sum(isnull(InvoiceAmount, 0)) / 100000, 
							    Deffered_Value = sum(case when (isnull(PackageInvoiceApproval_Next_Designation_Id, 0) = 0 and isnull(PackageInvoiceApproval_Next_Organisation_Id, 0) = 0 and  PackageInvoiceApproval_Status_Id not in (1, 6)) then isnull(InvoiceAmount, 0) else 0 end) / 100000
						    from ePayment.dbo.tbl_PackageInvoice 
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = PackageInvoice_Package_Id
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
							    from ePayment.dbo.tbl_PackageInvoiceApproval
							    left join ePayment.dbo.tbl_InvoiceStatus on InvoiceStatus_Id = PackageInvoiceApproval_Status_Id
							    where PackageInvoiceApproval_Status = 1
						    ) tInvoiceApproval on tInvoiceApproval.PackageInvoiceApproval_PackageInvoice_Id = PackageInvoice_Id and tInvoiceApproval.rrrrr = 1
						    where PackageInvoice_Status = 1 and ProjectWorkPkg_Status = 1  
						    group by ProjectWorkPkg_Work_Id
					    ) tInvoice on tInvoice.ProjectWorkPkg_Work_Id = ProjectWork_Id 
                        left join 
					    (
						    select 
							    ProjectWorkPkg_Work_Id,
							    Total_Invoice_ADP = count(*), 
							    Total_Value_ADP = sum(isnull(Package_ADP_ADPTotalAmount, 0)) / 100000
						    from ePayment.dbo.tbl_Package_ADP 
                            join
                            (
                                select
                                    Package_ADP_Item_Package_ADP_Id,
		                            Total_Line_Items = count(*),
		                            Total_Amount = sum(isnull(Package_ADP_Item_TotalAmount, 0))
                                from ePayment.dbo.tbl_Package_ADP_Item
                                where Package_ADP_Item_Status = 1
                                group by Package_ADP_Item_Package_ADP_Id
                            ) tPackageInvoiceItem on Package_ADP_Item_Package_ADP_Id = Package_ADP_Id
						    join ePayment.dbo.tbl_ProjectWorkPkg on ProjectWorkPkg_Id = Package_ADP_Package_Id
						    join 
						    (
							    select 
								    ROW_NUMBER() over (partition by PackageADPApproval_Package_ADP_Id order by PackageADPApproval_Id desc) rrrrr,
								    PackageADPApproval_Next_Designation_Id,
								    PackageADPApproval_Next_Organisation_Id,
								    PackageADPApproval_Comments,
								    PackageADPApproval_AddedBy,
								    PackageADPApproval_AddedOn = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Status_Id,
								    PackageADPApproval_Package_Id,
								    PackageADPApproval_Package_ADP_Id,
								    InvoiceStatus_Name,
								    PackageADPApproval_Date = convert(char(10), PackageADPApproval_AddedOn, 103),
								    PackageADPApproval_Id
							    from ePayment.dbo.tbl_PackageADPApproval
							    left join ePayment.dbo.tbl_InvoiceStatus on InvoiceStatus_Id = PackageADPApproval_Status_Id
							    where PackageADPApproval_Status = 1
						    ) tADPApproval on tADPApproval.PackageADPApproval_Package_ADP_Id = Package_ADP_Id and tADPApproval.rrrrr = 1
						    where Package_ADP_Status = 1 and ProjectWorkPkg_Status = 1  
						    group by ProjectWorkPkg_Work_Id
					    ) tADP on tADP.ProjectWorkPkg_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id 
                        left join 
                        (
	                        select 
		                        ProjectWorkGallery_Work_Id,
		                        Total_Photo = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) not in ('mov', 'mp4') then 1 else 0 end),
		                        Total_Video = sum(case when REVERSE(substring(REVERSE(ProjectWorkGallery_Path), 1, 3)) in ('mov', 'mp4') then 1 else 0 end), 
                                Last_Updated = max(ProjectWorkGallery_AddedOn)
	                        from ePayment.dbo.tbl_ProjectWorkGallery 
	                        where ProjectWorkGallery_Status = 1  
	                        group by ProjectWorkGallery_Work_Id
                        ) tGallery on tGallery.ProjectWorkGallery_Work_Id = ePayment.dbo.tbl_ProjectWork.ProjectWork_Id 
                        where ProjectWork_Status = 1 and ProjectWork_Project_Id in (12, 1013, 1016) ULBTypeCond2 ";

            strQuery += ") tData where 1=1 ";

            if (obj_SearchCriteria.ULB_Type == 1)
            {//NN
                strQuery = strQuery.Replace("ULBTypeCond1", " and Division_Type = 'NN'");
                strQuery = strQuery.Replace("ULBTypeCond2", " and ULB_Type = 'NN'");
            }
            else if (obj_SearchCriteria.ULB_Type == 1)
            {//NPP
                strQuery = strQuery.Replace("ULBTypeCond1", " and Division_Type = 'NPP'");
                strQuery = strQuery.Replace("ULBTypeCond2", " and ULB_Type = 'NPP'");
            }
            else if (obj_SearchCriteria.ULB_Type == 1)
            {//NP
                strQuery = strQuery.Replace("ULBTypeCond1", " and Division_Type = 'NP'");
                strQuery = strQuery.Replace("ULBTypeCond2", " and ULB_Type = 'NP'");
            }
            else
            {
                strQuery = strQuery.Replace("ULBTypeCond1", " ");
                strQuery = strQuery.Replace("ULBTypeCond2", " ");
            }

            if (obj_SearchCriteria.Scheme_Id > 0)
            {
                strQuery += " and ProjectWork_Project_Id = " + obj_SearchCriteria.Scheme_Id + "";
            }
            if (obj_SearchCriteria.ProjectWork_Id > 0)
            {
                strQuery += " and ProjectWork_Id = " + obj_SearchCriteria.ProjectWork_Id + "";
            }
            if (obj_SearchCriteria.FromDate != "")
            {
                strQuery += " and convert(date, convert(char(10), ProjectWork_GO_Date, 103), 103) >= convert(date, convert(char(10), '" + obj_SearchCriteria.FromDate + "', 103), 103)";
            }
            if (obj_SearchCriteria.TillDate != "")
            {
                strQuery += " and convert(date, convert(char(10), ProjectWork_GO_Date, 103), 103) <= convert(date, convert(char(10), '" + obj_SearchCriteria.TillDate + "', 103), 103)";
            }
            if (obj_SearchCriteria.District_Id != 0)
            {
                strQuery += " and Division_CircleId = '" + obj_SearchCriteria.District_Id + "'";
            }
            if (obj_SearchCriteria.ULB_Id != 0)
            {
                strQuery += " and ProjectWork_DivisionId = '" + obj_SearchCriteria.ULB_Id + "'";
            }
            if (obj_SearchCriteria.LokSabha_Id != 0)
            {
                strQuery += " and LokSabha_Id = '" + obj_SearchCriteria.LokSabha_Id + "'";
            }
            if (obj_SearchCriteria.VidhanSabha_Id != 0)
            {
                strQuery += " and VidhanSabha_Id = '" + obj_SearchCriteria.VidhanSabha_Id + "'";
            }
            if (obj_SearchCriteria.ProjectType_Id != 0)
            {
                strQuery += " and ProjectWork_ProjectType_Id = '" + obj_SearchCriteria.ProjectType_Id + "'";
            }
            if (obj_SearchCriteria.ProjectCode != "")
            {
                strQuery += " and ProjectWork_ProjectCode like '%" + obj_SearchCriteria.ProjectCode + "%'";
            }
            strQuery += " group by Division_CircleId, Circle_Name, Division_Name, ProjectWork_DivisionId order by Circle_Name, Division_Name ";
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
