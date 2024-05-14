
namespace PMIS_API.Models
{
    public class tbl_DistrictWiseProjectWork
    {
        public int District_Id { get; set; }
        public string District_Name { get; set; }
        public int Total_Projects_Count { get; set; }
        public decimal Sanctioned_Cost { get; set; }
        public decimal Tender_Cost { get; set; }
        public decimal Total_Release { get; set; }
        public decimal Total_Expenditure { get; set; }
        public decimal Completed_Projects { get; set; }
        public decimal Ongoing_Projects { get; set; }
        public int Completed_Projects_Count { get; set; }
        public int Ongoing_Projects_Count { get; set; }
    }
}
