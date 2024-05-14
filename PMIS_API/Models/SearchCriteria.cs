
namespace PMIS_API.Models
{
    public class SearchCriteria
    {
        public int FinancialYear_Id { get; set; }
        public int ProjectWork_Id { get; set; }
        public int Scheme_Id { get; set; }
        public int ProjectType_Id { get; set; }
        public int ULB_Id { get; set; }
        public int LokSabha_Id { get; set; }
        public int VidhanSabha_Id { get; set; }
        public int District_Id { get; set; }
        public string FromDate { get; set; }
        public string TillDate { get; set; }
        public string ProjectCode { get; set; }
        public int ULB_Type { get; set; }
    }
}
