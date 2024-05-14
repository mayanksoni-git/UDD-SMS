
using System;
using System.Collections.Generic;

namespace ePayment_API.Models
{
    public class DPRWorkUpdate
    {
        public int User_Role_Id { get; set; }
        public int Person_Id { get; set; }
        public decimal BudgetUtilized { get; set; }
        public decimal PhysicalProgress { get; set; }
        public decimal Total_Allocated { get; set; }
        public decimal Total_Centage { get; set; }
        public string Comments { get; set; }
        public int ProjectDPR_Id { get; set; }
        public int ProjectWork_Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int ULB_Id { get; set; }
        public int Division_Id { get; set; }
        public int SchemeId { get; set; }
        public string Person_AE_Name { get; set; }
        public string Person_JE_Name { get; set; }
        public string Person_Vendor_Name { get; set; }
        public int Person_AE_Id { get; set; }
        public int Person_JE_Id { get; set; }
        public int Person_Vendor_Id { get; set; }
        public string Project_Completion_Date { get; set; }
        public string Project_Status { get; set; }
        public string ProjectDPRSitePics_SitePic_CommentsB1 { get; set; }
        public string ProjectDPRSitePics_SitePic_PathB1 { get; set; }
        public string ProjectDPRSitePics_SitePic_BytesB1 { get; set; }
        public string ProjectDPRSitePics_SitePic_CommentsB2 { get; set; }
        public string ProjectDPRSitePics_SitePic_PathB2 { get; set; }
        public string ProjectDPRSitePics_SitePic_BytesB2 { get; set; }
        public string ProjectDPRSitePics_SitePic_CommentsA1 { get; set; }
        public string ProjectDPRSitePics_SitePic_PathA1 { get; set; }
        public string ProjectDPRSitePics_SitePic_BytesA1 { get; set; }
        public string ProjectDPRSitePics_SitePic_CommentsA2 { get; set; }
        public string ProjectDPRSitePics_SitePic_PathA2 { get; set; }
        public string ProjectDPRSitePics_SitePic_BytesA2 { get; set; }
        public List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li { get; set; }
        public List<tbl_ProjectUC_Deliverables> obj_tbl_ProjectUC_Deliverables_Li { get; set; }
        public List<tbl_ProjectUC_PhysicalProgress> obj_tbl_ProjectUC_PhysicalProgress_Li { get; set; }
    }

    public class DPRWorkUpdate2
    {
        public int User_Role_Id { get; set; }
        public int Person_Id { get; set; }
        public int ProjectUC_FinancialYear_Id { get; set; }
        public decimal BudgetUtilized { get; set; }
        public decimal PhysicalProgress { get; set; }
        public decimal Total_Centage { get; set; }
        public decimal Total_Allocated { get; set; }
        public string Comments { get; set; }
        public int ProjectDPR_Id { get; set; }
        public int ProjectWork_Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int NP_JurisdictionId { get; set; }
        public int SchemeId { get; set; }
        public List<tbl_ProjectUC_Concent> obj_tbl_ProjectUC_Concent_Li { get; set; }
        public List<tbl_ProjectPkg_PhysicalProgress> obj_tbl_ProjectPkg_PhysicalProgress_Li { get; set; }
        public List<tbl_ProjectPkg_Deliverables> obj_tbl_ProjectPkg_Deliverables_Li { get; set; }
        public string ProjectDPRSitePics_SitePic_PathB1 { get; set; }
        public string ProjectDPRSitePics_SitePic_Base64_B1 { get; set; }
        public string ProjectDPRSitePics_SitePic_PathB2 { get; set; }
        public string ProjectDPRSitePics_SitePic_Base64_B2 { get; set; }
        public string ProjectDPRSitePics_SitePic_PathA1 { get; set; }
        public string ProjectDPRSitePics_SitePic_Base64_A1 { get; set; }
        public string ProjectDPRSitePics_SitePic_PathA2 { get; set; }
        public string ProjectDPRSitePics_SitePic_Base64_A2 { get; set; }
        public string  Person_AE { get; set; }
        public string  Person_JE { get; set; }
        public string Person_Vendor { get; set; }
        public string Project_Completion_Date { get; set; }
        public string Project_Status { get; set; }
    }
}
