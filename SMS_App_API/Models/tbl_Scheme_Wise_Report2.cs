
namespace ePayment_API.Models
{
    public class tbl_Scheme_Wise_Report2
    {
        public int Project_Id { get; set; }
        public int FinancialYear_Id { get; set; }
        public string Project_Name { get; set; }
        public int Total_ULB { get; set; }
        public string Project_Budget { get; set; }
        public int Total_Work { get; set; }
        public string BudgetAllocated { get; set; }
        public string Fund_Released { get; set; }
        public decimal Expenditure { get; set; }
        public decimal Balance { get; set; }
        public decimal Physical_Progress { get; set; }
        public decimal Financial_Progress { get; set; }
        public decimal Financial_Progress_Per { get; set; }
    }
}
