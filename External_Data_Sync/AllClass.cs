using System;
using System.Data;

namespace External_Data_Sync
{
    public class AllClass
    {
        public AllClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        // Method use to Check DataSet  
        public static bool CheckDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        // Method use to Check DataTable  
        public static bool CheckDataTable(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        
    }


    public class tbl_District
    {
        public int District_Id { get; set; }
        public string District_Name { get; set; }
        public int District_Status { get; set; }
    }


    public class tbl_FinancialYear
    {
        public string FinancialYear_EndYear { get; set; }
        public int FinancialYear_Id { get; set; }
        public string FinancialYear_Name { get; set; }
        public string FinancialYear_StartYear { get; set; }
    }


    public class tbl_ProjectWork
    {
        public int District_Id { get; set; }
        public string District_Name { get; set; }
        public string LokSabha_Name { get; set; }
        public string ProjectType_Name { get; set; }
        public string ProjectWork_Agreement_Date { get; set; }
        public string ProjectWork_Code { get; set; }
        public string ProjectWork_Completion_Date { get; set; }
        public string ProjectWork_Completion_Date_Extended { get; set; }
        public Decimal ProjectWork_Expenditure_Amount { get; set; }
        public Decimal ProjectWork_Financial_Progress_Per { get; set; }
        public int ProjectWork_FinancialYear_Id { get; set; }
        public string ProjectWork_GO_Date { get; set; }
        public string ProjectWork_GO_No { get; set; }
        public string ProjectWork_GO_Path { get; set; }
        public int ProjectWork_Id { get; set; }
        public string ProjectWork_Name { get; set; }
        public Decimal ProjectWork_Physical_Progress_Per { get; set; }
        public Decimal ProjectWork_Released_Amount { get; set; }
        public Decimal ProjectWork_Sanctioned_Cost { get; set; }
        public int ProjectWork_Scheme_Id { get; set; }
        public int ProjectWork_Status { get; set; }
        public Decimal ProjectWork_Tender_Cost { get; set; }
        public int ULB_Id { get; set; }
        public string ULB_Name { get; set; }
        public string VidhanSabha_Name { get; set; }
        public string Ward_Name { get; set; }
    }


    public class tbl_Scheme
    {
        public Decimal Scheme_Budget { get; set; }
        public int Scheme_Id { get; set; }
        public string Scheme_Name { get; set; }
        public int Scheme_Status { get; set; }
    }


    public class tbl_ULB
    {
        public string District_Name { get; set; }
        public int ULB_DistrictId { get; set; }
        public int ULB_Id { get; set; }
        public string ULB_Name { get; set; }
        public int ULB_Status { get; set; }
        public string ULB_Type { get; set; }
    }

    public class SearchCriteria
    {
        public int FinancialYear_Id { get; set; }
        public int ProjectWork_Id { get; set; }
        public int Scheme_Id { get; set; }
        public int ProjectType_Id { get; set; }
        public int ULB_Id { get; set; }
        public int District_Id { get; set; }
        public string FromDate { get; set; }
        public string TillDate { get; set; }
        public string ProjectCode { get; set; }
        public int ULB_Type { get; set; }
    }
}