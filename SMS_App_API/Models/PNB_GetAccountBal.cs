
namespace ePayment_API.Models
{
    public class PNB_GetAccountBal_Req
    {
        public string Request_Id { get; set; }
        public string Account_Number { get; set; }
        public string Entity_Code { get; set; }
    }


    public class PNB_GetAccountBal_Res
    {
        public string Request_Id { get; set; }
        public decimal Amount { get; set; }
        public string DP_Limit { get; set; }
        public string Status { get; set; }
        public string Error_Desc { get; set; }
    }
}
