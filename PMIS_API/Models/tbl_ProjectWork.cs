
namespace PMIS_API.Models
{
    public class tbl_ProjectWork
    {
        public int ProjectWork_Id { get; set; }
        public int ProjectWork_Scheme_Id { get; set; }
        public string ProjectWork_Code { get; set; }
        public string ProjectWork_Name { get; set; }
        public string ProjectWork_GO_Date { get; set; }
        public string ProjectWork_GO_Number { get; set; }
        public string ProjectWork_GO_Path { get; set; }
        public string ProjectWork_Agreement_Date { get; set; }
        public string ProjectWork_Completion_Date { get; set; }
        public string ProjectWork_Completion_Date_Extended { get; set; }
        public decimal ProjectWork_Sanctioned_Cost { get; set; }
        public decimal ProjectWork_Tender_Cost { get; set; }
        public decimal ProjectWork_Released_Amount { get; set; }
        public decimal ProjectWork_Expenditure_Amount { get; set; }
        public decimal ProjectWork_Physical_Progress_Per { get; set; }
        public decimal ProjectWork_Financial_Progress_Per { get; set; }
        public int ULB_Id { get; set; }
        public int District_Id { get; set; }
        public string ULB_Name { get; set; }
        public string ProjectType_Name { get; set; }
        public string District_Name { get; set; }
        public int LokSabha_Id { get; set; }
        public int VidhanSabha_Id { get; set; }
        public string LokSabha_Name { get; set; }
        public string VidhanSabha_Name { get; set; }
    }
}
