using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ePayment_API.Models;

namespace ePayment_API.Repos
{
    public class DPRWorkUpdateRepository : RepositoryAsyn
    {
        public DPRWorkUpdateRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<bool> update_ProjectDPRWorkStatus(DPRWorkUpdate obj_DPRWorkUpdate)
        {
            tbl_ProjectUC obj_tbl_ProjectUC = new tbl_ProjectUC();
            obj_tbl_ProjectUC.ProjectUC_AddedBy = obj_DPRWorkUpdate.Person_Id;
            obj_tbl_ProjectUC.ProjectUC_BudgetUtilized = obj_DPRWorkUpdate.BudgetUtilized * 100000;
            try
            {
                obj_tbl_ProjectUC.ProjectUC_Centage = obj_DPRWorkUpdate.Total_Centage;
            }
            catch
            {
                obj_tbl_ProjectUC.ProjectUC_Centage = 0;
            }
            obj_tbl_ProjectUC.ProjectUC_Comments = obj_DPRWorkUpdate.Comments;
            obj_tbl_ProjectUC.ProjectUC_Latitude = obj_DPRWorkUpdate.Latitude;
            obj_tbl_ProjectUC.ProjectUC_Longitude = obj_DPRWorkUpdate.Longitude;
            obj_tbl_ProjectUC.ProjectUC_PhysicalProgress = obj_DPRWorkUpdate.PhysicalProgress;
            obj_tbl_ProjectUC.ProjectUC_ProjectDPR_Id = obj_DPRWorkUpdate.ProjectDPR_Id;
            obj_tbl_ProjectUC.ProjectUC_ProjectWork_Id= obj_DPRWorkUpdate.ProjectWork_Id;
            obj_tbl_ProjectUC.ProjectUC_Status = 1;
            obj_tbl_ProjectUC.ProjectUC_SubmitionDate = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            obj_tbl_ProjectUC.ProjectUC_Total_Allocated = obj_DPRWorkUpdate.Total_Allocated * 100000;
            try
            {
                obj_tbl_ProjectUC.ProjectUC_Achivment = decimal.Round(((obj_tbl_ProjectUC.ProjectUC_BudgetUtilized * 100) / obj_tbl_ProjectUC.ProjectUC_Total_Allocated), 2, MidpointRounding.AwayFromZero);
            }
            catch
            {
                obj_tbl_ProjectUC.ProjectUC_Achivment = 0;
            }


            tbl_FinancialTrans obj_tbl_FinancialTrans = new tbl_FinancialTrans();
            obj_tbl_FinancialTrans.FinancialTrans_AddedBy = obj_DPRWorkUpdate.Person_Id;
            obj_tbl_FinancialTrans.FinancialTrans_Comments = obj_DPRWorkUpdate.Comments;
            obj_tbl_FinancialTrans.FinancialTrans_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            obj_tbl_FinancialTrans.FinancialTrans_EntryType = "Fund Utilized";
            obj_tbl_FinancialTrans.FinancialTrans_Jurisdiction_Id = obj_DPRWorkUpdate.ULB_Id;
            obj_tbl_FinancialTrans.FinancialTrans_ProjectDPR_Id = obj_DPRWorkUpdate.ProjectDPR_Id;
            obj_tbl_FinancialTrans.FinancialTrans_SchemeId = obj_DPRWorkUpdate.SchemeId;
            obj_tbl_FinancialTrans.FinancialTrans_Status = 1;
            obj_tbl_FinancialTrans.FinancialTrans_TransAmount = obj_DPRWorkUpdate.BudgetUtilized * 100000;
            obj_tbl_FinancialTrans.FinancialTrans_TransType = "D";
            obj_tbl_FinancialTrans.FinancialTrans_WorkId = obj_DPRWorkUpdate.ProjectWork_Id;

            List<tbl_ProjectDPRSitePics> obj_tbl_ProjectDPRSitePics_Li = new List<tbl_ProjectDPRSitePics>();
            if (obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_BytesA1 != null && obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_BytesA1.Length > 0)
            {
                tbl_ProjectDPRSitePics obj_tbl_ProjectDPRSitePics = new tbl_ProjectDPRSitePics();
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_AddedBy = obj_DPRWorkUpdate.Person_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ProjectDPR_Id = obj_DPRWorkUpdate.ProjectDPR_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ProjectWork_Id = obj_DPRWorkUpdate.ProjectWork_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ReportSubmittedBy_PersonId = obj_DPRWorkUpdate.Person_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_SitePic_Bytes1 = obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_BytesA1;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_SitePic_Path1 = obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_PathA1;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_SitePic_Type = "A";
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_Status = 1;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_Comments = obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_CommentsA1;
                obj_tbl_ProjectDPRSitePics_Li.Add(obj_tbl_ProjectDPRSitePics);
            }
            if (obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_BytesA2 != null && obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_BytesA2.Length > 0)
            {
                tbl_ProjectDPRSitePics obj_tbl_ProjectDPRSitePics = new tbl_ProjectDPRSitePics();
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_AddedBy = obj_DPRWorkUpdate.Person_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ProjectDPR_Id = obj_DPRWorkUpdate.ProjectDPR_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ProjectWork_Id = obj_DPRWorkUpdate.ProjectWork_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ReportSubmittedBy_PersonId = obj_DPRWorkUpdate.Person_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_SitePic_Bytes1 = obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_BytesA2;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_SitePic_Path1 = obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_PathA2;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_SitePic_Type = "A";
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_Status = 1;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_Comments = obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_CommentsA2;
                obj_tbl_ProjectDPRSitePics_Li.Add(obj_tbl_ProjectDPRSitePics);
            }
            if (obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_BytesB1 != null && obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_BytesB1.Length > 0)
            {
                tbl_ProjectDPRSitePics obj_tbl_ProjectDPRSitePics = new tbl_ProjectDPRSitePics();
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_AddedBy = obj_DPRWorkUpdate.Person_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ProjectDPR_Id = obj_DPRWorkUpdate.ProjectDPR_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ProjectWork_Id = obj_DPRWorkUpdate.ProjectWork_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ReportSubmittedBy_PersonId = obj_DPRWorkUpdate.Person_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_SitePic_Bytes1 = obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_BytesB1;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_SitePic_Path1 = obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_PathB1;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_SitePic_Type = "B";
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_Status = 1;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_Comments = obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_CommentsB1;
                obj_tbl_ProjectDPRSitePics_Li.Add(obj_tbl_ProjectDPRSitePics);
            }
            if (obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_BytesB2 != null && obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_BytesB2.Length > 0)
            {
                tbl_ProjectDPRSitePics obj_tbl_ProjectDPRSitePics = new tbl_ProjectDPRSitePics();
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_AddedBy = obj_DPRWorkUpdate.Person_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ProjectDPR_Id = obj_DPRWorkUpdate.ProjectDPR_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ProjectWork_Id = obj_DPRWorkUpdate.ProjectWork_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_ReportSubmittedBy_PersonId = obj_DPRWorkUpdate.Person_Id;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_SitePic_Bytes1 = obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_BytesB2;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_SitePic_Path1 = obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_PathB2;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_SitePic_Type = "B";
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_Status = 1;
                obj_tbl_ProjectDPRSitePics.ProjectDPRSitePics_Comments = obj_DPRWorkUpdate.ProjectDPRSitePics_SitePic_CommentsB1;
                obj_tbl_ProjectDPRSitePics_Li.Add(obj_tbl_ProjectDPRSitePics);
            }
            return new DataLayer().Update_tbl_ProjectDPR_WorkStatus(obj_tbl_ProjectUC, obj_tbl_FinancialTrans, obj_DPRWorkUpdate.obj_tbl_ProjectUC_Concent_Li, obj_tbl_ProjectDPRSitePics_Li, obj_DPRWorkUpdate.obj_tbl_ProjectUC_PhysicalProgress_Li, obj_DPRWorkUpdate.obj_tbl_ProjectUC_Deliverables_Li, obj_DPRWorkUpdate.Project_Status);
        }
    }
}
