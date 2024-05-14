
namespace ePayment_API.Models
{
    public class tbl_SNAAccountBalance
    {
        public int SNAAccountBalance_AddedBy { get; set; }
        public string SNAAccountBalance_AddedOn { get; set; }
        public decimal SNAAccountBalance_Balance { get; set; }
        public string SNAAccountBalance_Error_Desc { get; set; }
        public int SNAAccountBalance_Id { get; set; }
        public int SNAAccountBalance_ModifiedBy { get; set; }
        public string SNAAccountBalance_ModifiedOn { get; set; }
        public string SNAAccountBalance_Request_Id { get; set; }
        public int SNAAccountBalance_SNAAccountMaster_Id { get; set; }
        public int SNAAccountBalance_Status { get; set; }
        public string SNAAccountBalance_StatusPNB { get; set; }
    }
}
