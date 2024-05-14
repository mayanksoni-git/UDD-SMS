
namespace ePayment_API.Models
{
    public class SearchCriteria
    {
        public int Person_Id { get; set; }
        public int Designation_Id { get; set; }
        public int UserType_Id { get; set; }
        public int Role_ULB { get; set; }
        public int Role_Inspection { get; set; }
        public int Role_Vendor { get; set; }
        public int FinancialYear_Id { get; set; }
        public string Reporting_Mode { get; set; } //Breakup // Combined 
        public int ProjectDPR_Id { get; set; }
        public int ProjectWork_Id { get; set; }
        public int ProjectUC_Id { get; set; }
        public string Project_Status { get; set; }    //Running // Closed
        public int Project_Id { get; set; }
        public int ULB_Id { get; set; }
        public int Zone_Id { get; set; }
        public int Circle_Id { get; set; }
        public int Division_Id { get; set; }
        public int District_Id { get; set; }
        public string Client_Code { get; set; }
        public string Inspection_Date { get; set; }
    }
}
