
namespace ePayment_API.Models
{
    public class PNB_Assign_DP_Limit_Req
    {
        public string Account_Number { get; set; }
        public int DP_Limit { get; set; }
        public string Unique_Id { get; set; }
        public string Entity_Code { get; set; }
    }
    public class PNB_Assign_DP_Limit_Res
    {
        public string RefNum { get; set; }
        public string Status { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDesc { get; set; }
        public decimal? TranAmt { get; set; }
        public string TranDate { get; set; }
        public string TranId { get; set; }
        public string TranParticular { get; set; }
        public string TranRmks { get; set; }
        public string Unique_Id { get; set; }
    }
}
