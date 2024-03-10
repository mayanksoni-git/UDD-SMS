using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ReportClass
/// </summary>

[Serializable]
public class Bill_Info
{
    public string Organization_Name { get; set; }
    public string Organization_Address { get; set; }
    public string Organization_Mobile { get; set; }
    public string Organization_EmailID { get; set; }
    public string Organization_GSTIN { get; set; }
    public string Organization_TAN { get; set; }
    public string Organization_PAN { get; set; }
    public string Division_Name { get; set; }
    public string Circle_Name { get; set; }
    public string Zone_Name { get; set; }
    public string Division_Address { get; set; }
    public string Division_Mobile { get; set; }
    public string Division_EmailID { get; set; }
    public string Division_GSTIN { get; set; }
    public string Division_PAN { get; set; }
    public string Division_TAN { get; set; }
    public string Contractor_Name { get; set; }
    public string Contractor_Address { get; set; }
    public string Contractor_Mobile { get; set; }
    public string Contractor_EmailID { get; set; }
    public string Contractor_GSTIN { get; set; }
    public string Contractor_PAN { get; set; }
    public string Contractor_TAN { get; set; }
    public string Invoice_No { get; set; }
    public string Invoice_Date { get; set; }
    public string DBR_No { get; set; }
    public string SBR_No { get; set; }
    public string Narration { get; set; }
    public string EMB_Specification { get; set; }
    public string EMB_Unit_Name { get; set; }
    public decimal EMB_Qty { get; set; }
    public decimal EMB_Rate { get; set; }
    public decimal EMB_Amount { get; set; }
    public decimal EMB_Tax { get; set; }
    public string EMB_Amount_In_Words { get; set; }
    public string EMB_Date { get; set; }
    public string EMB_Ref_No { get; set; }
    public string Scheme_Name { get; set; }
    public string Agreement_No { get; set; }
    public string Start_Date { get; set; }
    public string End_Date { get; set; }
    public decimal Invoie_Amount_Final { get; set; }
    public string Additional_Data_1 { get; set; }
    public string Additional_Data_2 { get; set; }
    public string Additional_Data_3 { get; set; }
    public string Additional_Data_4 { get; set; }
    public string Additional_Data_5 { get; set; }
    public string Additional_Data_6 { get; set; }
    public string Additional_Data_7 { get; set; }
    public string Additional_Data_8 { get; set; }
    public string Additional_Data_9 { get; set; }
    public string Additional_Data_10 { get; set; }
    public decimal Global_CGST { get; set; }
    public decimal Global_SGST { get; set; }
    public decimal Global_Quoted_Rate { get; set; }
    public string Quoted_Rate_Text { get; set; }
    public string ProcessType { get; set; }
    public string Bill_Type { get; set; }
    public decimal Global_Quoted_Amount { get; set; }
    public decimal Global_Sub_Total { get; set; }
    public decimal PackageInvoiceItem_SincePrev { get; set; }
    public decimal PackageInvoiceItem_SincePrevAmount { get; set; }
    public decimal PackageInvoiceItem_UpToDate { get; set; }
    public decimal PackageInvoiceItem_PercentageToBeReleased { get; set; }
    public decimal PackageInvoiceItem_UpToDateAmount { get; set; }
    public decimal PackageInvoiceItem_Total_Qty_BOQ { get; set; }
    public decimal Total_Tax { get; set; }
    public decimal CurrentInvoice_Total_Amount { get; set; }
}

[Serializable]
public class PackageInvoiceAdditional
{
    public string Deduction_Name { get; set; }
    public decimal Deduction_Value { get; set; }
    public string Value_Type { get; set; }
}

[Serializable]
public class Invoice_View
{
    public List<Bill_Info> obj_Bill_Info_Li { get; set; }
    public List<PackageInvoiceAdditional> obj_PackageInvoiceAdditional_Li { get; set; }
}

[Serializable]
public class ProjectSummery
{
    public string Project_Name { get; set; }
    public string Contractor_Name { get; set; }
    public decimal Project_Cost { get; set; }
    public decimal Work_Cost { get; set; }
    public string Work_Name { get; set; }
    public string Scheme_Name { get; set; }
    public decimal Sactioned_Cost { get; set; }
    public decimal Tender_Cost { get; set; }
    public decimal Tender_Cost_Less { get; set; }
    public decimal Total_Release { get; set; }
    public decimal Total_Centre_Share { get; set; }
    public decimal Total_State_Share { get; set; }
    public decimal Total_ULB_Share { get; set; }
    public decimal Total_Calculated { get; set; }
    public decimal Total_Payment_Earlier { get; set; }
    public decimal Total_Balance { get; set; }
    public decimal Total_Bill_Raised { get; set; }
    public decimal Total_Proposed_Payment_Jal_Nigam { get; set; }
    public decimal Total_With_Held_Amount { get; set; }
    public string Tender_Cost_Less_With_Text { get; set; }
    public string Installment_Condition { get; set; }
    public string Extra_Item_Condition { get; set; }
    public string Additional_Text_1 { get; set; }
    public string Additional_Text_2 { get; set; }
    public string Additional_Text_3 { get; set; }
    public string Additional_Text_4 { get; set; }
    public decimal Additional_Value_1 { get; set; }
    public decimal Additional_Value_2 { get; set; }
    public decimal Additional_Value_3 { get; set; }
    public decimal Additional_Value_4 { get; set; }
}

[Serializable]
public class Cover_Letter
{
    public string Place { get; set; }
    public string Financial_Year { get; set; }
    public string Project_Type { get; set; }
    public string Project_Id { get; set; }
    public string Project_Name { get; set; }
    public string Scheme_Name { get; set; }
    public string Work_Name { get; set; }
    public decimal Sanctioned_Amount_Without_Centage { get; set; }
    public decimal Total_Centre_Share { get; set; }
    public decimal Total_State_Share { get; set; }
    public decimal Total_ULB_Share { get; set; }
    public decimal Centage { get; set; }
    public decimal Tendred_Amount { get; set; }
    public decimal Release_To_Implementing_Agency { get; set; }
    public decimal Fund_Diverted { get; set; }
    public decimal Find_Received { get; set; }
    public decimal Amount_Received_To_Implementing_Agency_Including_Diversion { get; set; }
    public decimal Expenditure_Done_By_Implementing_Agency { get; set; }
    public decimal Balance_Amount_As_In_Bank_Statement { get; set; }
    public decimal Amount_Released_To_Division { get; set; }
    public decimal Expenditure_By_Division { get; set; }
    public string Contractor_Type { get; set; }
    public string Contractor_Firm_Name { get; set; }
    public string Contractor_Name { get; set; }
    public string Contractor_Address { get; set; }
    public string Contractor_Mobile { get; set; }
    public string Contractor_EmailID { get; set; }
    public string Contractor_GSTIN { get; set; }
    public string Contractor_PAN { get; set; }
    public string Contractor_TAN { get; set; }
    public string Contractor_Service_Tax { get; set; }
    public string Bank_Name { get; set; }
    public string Account_Number { get; set; }
    public string Account_Holder_Name { get; set; }
    public decimal Total_Amount_Paid_To_Contractor_Till_Date { get; set; }
    public decimal Total_Mobelization_Advance { get; set; }
    public decimal Total_Mobelization_Advance_Adjustment_Before_Bill { get; set; }
    public decimal Total_Mobelization_Advance_Adjustment_In_Current_Bill { get; set; }
    public decimal Total_Invoice_Value { get; set; }
    public string Project_Manager { get; set; }
    public string General_Manager { get; set; }
    public string Additional_Text_1 { get; set; }
    public string Additional_Text_2 { get; set; }
    public string Additional_Text_3 { get; set; }
    public string Additional_Text_4 { get; set; }
    public decimal Additional_Value_1 { get; set; }
    public decimal Additional_Value_2 { get; set; }
    public decimal Additional_Value_3 { get; set; }
    public decimal Additional_Value_4 { get; set; }
}

[Serializable]
public class Decleration_Letter
{
    public string Subject { get; set; }
    public string Zone_Name { get; set; }
    public string Circle_Name { get; set; }
    public string Division_Name { get; set; }
    public string Project_Name { get; set; }
    public string Project_Code { get; set; }
    public string Package_Name { get; set; }
    public string Package_Code { get; set; }
    public string Content { get; set; }
    public int Last_RA_Bill_No { get; set; }
    public string Last_RA_Bill_Date { get; set; }
    public string Additional_1 { get; set; }
    public string Additional_2 { get; set; }
    public string Additional_3 { get; set; }
    public string Additional_4 { get; set; }
}
[Serializable]
public class tbl_Report_BOQ
{
    public int Serial_No { get; set; }
    public string BOQ_Description { get; set; }
    public decimal BOQ_Quantity { get; set; }
    public string Unit_Name { get; set; }
    public decimal Estimated_Rate { get; set; }
    public decimal Quoted_Rate { get; set; }
    public decimal Quantity_Paid { get; set; }
    public decimal Percentage_Paid { get; set; }
    public decimal Amount_Paid { get; set; }
    public string Scheme_Name { get; set; }
    public string Division_Name { get; set; }
    public string project_code { get; set; }
    public string Project_Name { get; set; }
}
[Serializable]
public class tbl_Report_BoQDivision
{
    public int Division_Id { get; set; }
    public string Scheme_Name { get; set; }
    public string Division_Name { get; set; }
    public string Project_Name { get; set; }
    public string project_Description { get; set; }
    public string project_code { get; set; }
    public decimal Project_budget { get; set; }
    public decimal project_Cost { get; set; }
    public string project_GoNumber { get; set; }
    public string project_GoDate { get; set; }

}

[Serializable]
public class tbl_Report_Payment_Order
{
    public int Invoice_Id { get; set; }
    public string Invoice_Date { get; set; }
    public string PPA_No { get; set; }
    public string MB_No { get; set; }
    public string Account_No { get; set; }
    public decimal Invoice_Amount { get; set; }
    public string Content_Line_1 { get; set; }
    public string Content_Line_2 { get; set; }
    public string Content_Line_3 { get; set; }
    public string Content_Line_4 { get; set; }
    public string Content_Line_5 { get; set; }
}

[Serializable]
public class tbl_Achivment
{
    public string Header_Text { get; set; }
    public int Zone_Id { get; set; }
    public int Circle_Id { get; set; }
    public string Zone_Name { get; set; }
    public string Circle_Name { get; set; }
    public string Zone_Circle_Name { get; set; }
    public decimal Target_Value { get; set; }
    public decimal EMB_Value { get; set; }
    public decimal Invoice_Value { get; set; }
    public decimal ADP_Value { get; set; }
    public decimal Deduction_Release_Value { get; set; }
    public decimal MA_Value { get; set; }
    public decimal Achivment_Value { get; set; }
    public decimal Achivment_Percentage { get; set; }
}

[Serializable]
public class tbl_Document_NA_Summery
{
    public string Header_Text { get; set; }
    public int Zone_Id { get; set; }
    public int Circle_Id { get; set; }
    public string Zone_Name { get; set; }
    public string Circle_Name { get; set; }
    public string Zone_Circle_Name { get; set; }
    public int Package_Count { get; set; }
    public int First_GO { get; set; }
    public int Second_GO { get; set; }
    public int Third_GO { get; set; }
    public int CB { get; set; }
    public int LOI { get; set; }
    public int CB_Front { get; set; }
    public int Schedule_G { get; set; }
    public int Agreement_Stamp { get; set; }
    public int BG { get; set; }
    public int PS { get; set; }
    public int MA { get; set; }
    public int LD { get; set; }
    public int TE { get; set; }
    public int Physical_Closure { get; set; }
    public int Financial_Closure { get; set; }
    public int Variation { get; set; }
}

[Serializable]
public class tbl_Document_NA_Detail
{
    public string Header_Text { get; set; }
    public int Zone_Id { get; set; }
    public int Circle_Id { get; set; }
    public int Division_Id { get; set; }
    public string Zone_Name { get; set; }
    public string Circle_Name { get; set; }
    public string Division_Name { get; set; }
    public string ProjectWork_Name { get; set; }
    public string ProjectWork_Code { get; set; }
    public string Circle_Division_Name { get; set; }
    public int Package_Count { get; set; }
    public int First_GO { get; set; }
    public int Second_GO { get; set; }
    public int Third_GO { get; set; }
    public int CB { get; set; }
    public int LOI { get; set; }
    public int CB_Front { get; set; }
    public int Schedule_G { get; set; }
    public int Agreement_Stamp { get; set; }
    public int BG { get; set; }
    public int PS { get; set; }
    public int MA { get; set; }
    public int LD { get; set; }
    public int TE { get; set; }
    public int Physical_Closure { get; set; }
    public int Financial_Closure { get; set; }
    public int Variation { get; set; }
}

[Serializable]
public class tbl_Stagnant_Progress
{
    public string Header_Text { get; set; }
    public int Zone_Id { get; set; }
    public int Circle_Id { get; set; }
    public string Zone_Name { get; set; }
    public string Circle_Name { get; set; }
    public string Zone_Circle_Name { get; set; }
    public int OnGoing { get; set; }
    public int Completed { get; set; }
    public int Total { get; set; }
}

[Serializable]
public class tbl_Stagnant_Progress_Physical
{
    public string Header_Text { get; set; }
    public int Zone_Id { get; set; }
    public int Circle_Id { get; set; }
    public string Zone_Name { get; set; }
    public string Circle_Name { get; set; }
    public string Zone_Circle_Name { get; set; }
    public int Data_1 { get; set; }
    public int Data_2 { get; set; }
    public int Data_3 { get; set; }
    public int Data_4 { get; set; }
    public int Data_5 { get; set; }
    public int Total { get; set; }
}

[Serializable]
public class tbl_Stagnant_Progress_Detail
{
    public string Header_Text { get; set; }
    public int Zone_Id { get; set; }
    public int Circle_Id { get; set; }
    public int Division_Id { get; set; }
    public string Zone_Name { get; set; }
    public string Circle_Name { get; set; }
    public string Division_Name { get; set; }
    public string ProjectWork_Name { get; set; }
    public string ProjectWork_Code { get; set; }
    public string Circle_Division_Name { get; set; }
    public decimal Physical_Progress { get; set; }
    public decimal Financial_Progress { get; set; }
    public string Last_Invoice_Date { get; set; }
    public int Days_Since_Last_Invoice { get; set; }
    public string Issue_Reported { get; set; }
}

[Serializable]
public class tbl_TimeOverRun_Detail
{
    public string Header_Text { get; set; }
    public int Zone_Id { get; set; }
    public int Circle_Id { get; set; }
    public int Division_Id { get; set; }
    public string Zone_Name { get; set; }
    public string Circle_Name { get; set; }
    public string Division_Name { get; set; }
    public string ProjectWork_Name { get; set; }
    public string ProjectWork_Code { get; set; }
    public string Circle_Division_Name { get; set; }
    public decimal Physical_Progress { get; set; }
    public decimal Financial_Progress { get; set; }
    public string Agreement_Start_Date { get; set; }
    public string Agreement_End_Date { get; set; }
    public string Agreement_Extended_Date { get; set; }
}

[Serializable]
public class tbl_TimeOverRun
{
    public string Header_Text { get; set; }
    public int Zone_Id { get; set; }
    public int Circle_Id { get; set; }
    public string Zone_Name { get; set; }
    public string Circle_Name { get; set; }
    public string Zone_Circle_Name { get; set; }
    public int RequireExtention { get; set; }
    public int ExtentionExpired { get; set; }
}

[Serializable]
public class tbl_SNA_ChildAccount_Detail
{
    public string Header_Text { get; set; }
    public int Zone_Id { get; set; }
    public int Circle_Id { get; set; }
    public int Division_Id { get; set; }
    public string Zone_Name { get; set; }
    public string Circle_Name { get; set; }
    public string Division_Name { get; set; }
    public string ProjectWork_Name { get; set; }
    public string ProjectWork_Code { get; set; }
    public string Circle_Division_Name { get; set; }
    public int Pendency_Days { get; set; }
    public int Days_Since_Last_Limit_Assigned { get; set; }
    public string Oldest_Invoice_Date { get; set; }
    public int Total_Invoices { get; set; }
    public decimal Assigned_Limit { get; set; }
    public decimal Used_Limit { get; set; }
    public decimal Available_Limit { get; set; }
    public decimal Available_Limit_PNB { get; set; }
    public decimal Pipeline_Total { get; set; }
    public decimal Pipeline_Invoice { get; set; }
    public decimal Pipeline_MA { get; set; }
    public decimal Pipeline_DR { get; set; }
    public decimal Pipeline_ADP { get; set; }
}

[Serializable]
public class tbl_SNA_ChildAccount
{
    public string Header_Text { get; set; }
    public int Zone_Id { get; set; }
    public int Circle_Id { get; set; }
    public string Zone_Name { get; set; }
    public string Circle_Name { get; set; }
    public string Zone_Circle_Name { get; set; }
    public int Total_Invoices { get; set; }
    public decimal Assigned_Limit { get; set; }
    public decimal Used_Limit { get; set; }
    public decimal Available_Limit { get; set; }
    public decimal Available_Limit_PNB { get; set; }
    public decimal Pipeline_Total { get; set; }
    public decimal Pipeline_Invoice { get; set; }
    public decimal Pipeline_MA { get; set; }
    public decimal Pipeline_DR { get; set; }
    public decimal Pipeline_ADP { get; set; }
}

[Serializable]
public class tbl_Invoice_Pending_Detail
{
    public string Header_Text { get; set; }
    public int Zone_Id { get; set; }
    public int Circle_Id { get; set; }
    public int Division_Id { get; set; }
    public string Zone_Name { get; set; }
    public string Circle_Name { get; set; }
    public string Division_Name { get; set; }
    public string ProjectWork_Name { get; set; }
    public string ProjectWork_Code { get; set; }
    public string Circle_Division_Name { get; set; }
    public int Pendency_Days { get; set; }
    public string Days_Since_Last_Action { get; set; }
    public string Pending_At_Designation { get; set; }
    public decimal Total_Invoice_Amount { get; set; }
    public string Pending_Reason_If_Any { get; set; }
}

[Serializable]
public class tbl_Invoice_Pending
{
    public string Header_Text { get; set; }
    public int ProcessConfigMaster_OrgId { get; set; }
    public int ProcessConfigMaster_Designation_Id { get; set; }
    public string Designation_DesignationName { get; set; }
    public int Agra_Zone_Invoice_Count { get; set; }
    public decimal Agra_Zone_Invoice_Total { get; set; }
    public int GZB_Zone_Invoice_Count { get; set; }
    public decimal GZB_Zone_Invoice_Total { get; set; }
    public int Lucknow_Zone_Invoice_Count { get; set; }
    public decimal Lucknow_Zone_Invoice_Total { get; set; }
    public int Prayagraj_Zone_Invoice_Count { get; set; }
    public decimal Prayagraj_Zone_Invoice_Total { get; set; }
    public int Total_Invoice_Count { get; set; }
    public decimal Invoice_Total { get; set; }
}

[Serializable]
public class tbl_Financial_Progress_Monthly
{
    public string Header_Text { get; set; }
    public int Zone_Id { get; set; }
    public int Circle_Id { get; set; }
    public string Zone_Name { get; set; }
    public string Circle_Name { get; set; }
    public int Division_Id { get; set; }
    public string Division_Name { get; set; }
    public string ProjectWork_Name { get; set; }
    public string ProjectWork_Code { get; set; }
    public decimal EMB_Value { get; set; }
    public decimal Invoice_Value { get; set; }
    public decimal ADP_Value { get; set; }
    public decimal Deduction_Release_Value { get; set; }
    public decimal MA_Value { get; set; }
    public decimal Achivment_Value { get; set; }
}

[Serializable]
public class Report_ProjectWorkPkgCert
{
    public int ProjectWorkPkgCert_AddedBy { get; set; }
    public string ProjectWorkPkgCert_AddedOn { get; set; }
    public string ProjectWorkPkgCert_AgreementDate { get; set; }
    public string ProjectWorkPkgCert_AgrrementNo { get; set; }
    public Decimal ProjectWorkPkgCert_AmountAwarded { get; set; }
    public string ProjectWorkPkgCert_Behavious { get; set; }
    public Decimal ProjectWorkPkgCert_ClaimAmount { get; set; }
    public string ProjectWorkPkgCert_EE_Name { get; set; }
    public string ProjectWorkPkgCert_EndDate { get; set; }
    public string ProjectWorkPkgCert_EndDateExtended { get; set; }
    public string ProjectWorkPkgCert_CompletedCommissionedDate { get; set; }
    public string ProjectWorkPkgCert_FinancialSoundness { get; set; }
    public Decimal ProjectWorkPkgCert_GrossAmountCompleted { get; set; }
    public int ProjectWorkPkgCert_Id { get; set; }
    public string ProjectWorkPkgCert_Is_Arbitration { get; set; }
    public string ProjectWorkPkgCert_Is_Qualified { get; set; }
    public string ProjectWorkPkgCert_LD_Details { get; set; }
    public string ProjectWorkPkgCert_Manpower { get; set; }
    public int ProjectWorkPkgCert_ModifiedBy { get; set; }
    public string ProjectWorkPkgCert_ModifiedOn { get; set; }
    public string ProjectWorkPkgCert_No { get; set; }
    public byte[] ProjectWorkPkgCert_No_BarCode { get; set; }
    public Decimal ProjectWorkPkgCert_PaidOnReducedRate { get; set; }
    public int ProjectWorkPkgCert_Pkg_Id { get; set; }
    public string ProjectWorkPkgCert_QualityOfWork { get; set; }
    public string ProjectWorkPkgCert_StartDate { get; set; }
    public int ProjectWorkPkgCert_Status { get; set; }
    public string ProjectWorkPkgCert_TechnicalProficiency { get; set; }
    public Decimal ProjectWorkPkgCert_TenderCost { get; set; }
    public string ProjectWorkPkgCert_TP { get; set; }
    public string ProjectWorkPkgCert_LetterNo { get; set; }
    public string ProjectWorkPkgCert_LetterDate { get; set; }
    public string ProjectWorkPkgCert_Comments { get; set; }
    public string ProjectWorkPkgCert_ExEngName { get; set; }
    public string ProjectWorkPkgCert_WorkName { get; set; }
    public string Division_Office_FullAddress { get; set; }
    public string Division_Office_ContactNo { get; set; }
    public string Division_Office_eMailID { get; set; }
    public string Division_Office_OfficeName { get; set; }
    public string ProjectWorkPkgCert_LeadContractor_Name { get; set; }
    public string ProjectWorkPkgCert_PartnerContractor_Name { get; set; }
    public decimal ProjectWorkPkgCert_LeadContractor_Share { get; set; }
    public decimal ProjectWorkPkgCert_PartnerContractor_Share { get; set; }
    public decimal ProjectWorkPkgCert_LeadContractor_Cost { get; set; }
    public decimal ProjectWorkPkgCert_PartnerContractor_Cost { get; set; }
}

[Serializable]
public class Certificate_Component
{
    public string Component_Name { get; set; }
    public string Unit_Name { get; set; }
    public decimal Total_Cost_As_Per_BOQ { get; set; }
    public decimal Total_Cost_As_Per_Actual { get; set; }
    public decimal Praposed_No_Origional { get; set; }
    public decimal Praposed_No_Actual { get; set; }
    public decimal Completed { get; set; }
    public decimal Functional { get; set; }
    public string Remarks { get; set; }
}

[Serializable]
public class Certificate_View
{
    public List<Report_ProjectWorkPkgCert> obj_Report_ProjectWorkPkgCert_Li { get; set; }
    public List<Certificate_Component> obj_Certificate_Component_Li { get; set; }
}

[Serializable]
public class One_Pager_Lite
{
    public string Project_Name { get; set; }
    public string Project_Code { get; set; }
    public string Project_Type { get; set; }
    public string District { get; set; }
    public string Person_CE { get; set; }
    public string Person_SE { get; set; }
    public string Person_EE { get; set; }
    public string Sanctioned_Cost { get; set; }
    public string Work_Cost { get; set; }
    public string Centage { get; set; }
    public string ULB_Share_GO { get; set; }
    public string Agreement_Date { get; set; }
    public string End_Date_Agreement { get; set; }
    public string Extended_Date { get; set; }
    public string Tender_Cost { get; set; }
    public string Tender_Cost_GST { get; set; }
    public string ULB_Share_Tender_Cost { get; set; }
    public string Installment_1_Central_State { get; set; }
    public string Installment_2_Central_State { get; set; }
    public string Installment_3_Central_State { get; set; }
    public string Installment_ULB { get; set; }
    public string Installment_Central_State { get; set; }
    public string Total_Funds_Available { get; set; }
    public string ULB_Share_Balance { get; set; }
    public string Total_Expenditure { get; set; }
    public string Physical_Progress { get; set; }
    public string Financial_Progress { get; set; }
    public string Physical_Closure { get; set; }
    public string Financial_closure { get; set; }
    public string Vendor { get; set; }
    public string Issues { get; set; }
    public string Additional_Field_1 { get; set; }
    public string Additional_Field_2 { get; set; }
    public string Additional_Field_3 { get; set; }
    public string Additional_Field_4 { get; set; }
    public string Additional_Field_5 { get; set; }
    public string Additional_Field_6 { get; set; }
    public string Additional_Field_7 { get; set; }
    public string Additional_Field_8 { get; set; }
    public string Additional_Field_9 { get; set; }
    public string Additional_Field_10 { get; set; }
}

[Serializable]
public class MPR_Detailed
{
    public string Project_Name { get; set; }
    public string Project_Code { get; set; }
    public string Zone_Name { get; set; }
    public string Circle_Name { get; set; }
    public string Division_Name { get; set; }
    public string Project_Type { get; set; }
    public string District { get; set; }
    public string Person_CE { get; set; }
    public string Person_SE { get; set; }
    public string Person_EE { get; set; }
    public decimal Sanctioned_Cost { get; set; }
    public decimal Work_Cost { get; set; }
    public decimal Centage { get; set; }
    public decimal Total_Cost_With_Centage { get; set; }
    public decimal Tender_Cost_With_GST { get; set; }
    public decimal Tender_Cost_WithOut_GST { get; set; }
    public string Agreement_Date { get; set; }
    public string End_Date_Agreement { get; set; }
    public string Extended_Date { get; set; }
    public decimal Installment_1_Central_State { get; set; }
    public decimal Installment_2_Central_State { get; set; }
    public decimal Installment_3_Central_State { get; set; }
    public decimal Installment_1_Centage { get; set; }
    public decimal Installment_2_Centage { get; set; }
    public decimal Installment_3_Centage { get; set; }
    public decimal Installment_Central_State { get; set; }
    public decimal Installment_Centage { get; set; }
    public decimal Installment_ULB { get; set; }
    public decimal Installment_Total_With_ULB { get; set; }
    public decimal Installment_Total_Centage { get; set; }
    public decimal Total_Expenditure { get; set; }
    public decimal Physical_Progress { get; set; }
    public decimal Financial_Progress { get; set; }
    public string Physical_Closure { get; set; }
    public string Financial_closure { get; set; }
    public string Vendor { get; set; }
    public string Issues { get; set; }
    public string Additional_Field_1 { get; set; }
    public string Additional_Field_2 { get; set; }
    public string Additional_Field_3 { get; set; }
    public string Additional_Field_4 { get; set; }
    public string Additional_Field_5 { get; set; }
    public string Additional_Field_6 { get; set; }
    public string Additional_Field_7 { get; set; }
    public string Additional_Field_8 { get; set; }
    public string Additional_Field_9 { get; set; }
    public string Additional_Field_10 { get; set; }
}
[Serializable]
public class MPR_Component
{
    public string Component_Name { get; set; }
    public string Unit_Name { get; set; }
    public string Capacity { get; set; }
    public decimal Praposed_No_Origional { get; set; }
    public decimal Praposed_No_Actual { get; set; }
    public decimal Completed { get; set; }
    public decimal Functional { get; set; }
    public string Remarks { get; set; }
    public string Additional_Field_1 { get; set; }
    public string Additional_Field_2 { get; set; }
    public string Additional_Field_3 { get; set; }
    public string Additional_Field_4 { get; set; }
    public string Additional_Field_5 { get; set; }
}