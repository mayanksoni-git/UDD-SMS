
namespace ePayment_API.Models
{
    public class tbl_SNAAccountDPLimit
    {
        public int SNAAccountDPLimit_AddedBy { get; set; }
        public string SNAAccountDPLimit_AddedOn { get; set; }
        public decimal? SNAAccountDPLimit_DPLimit { get; set; }
        public string SNAAccountDPLimit_ErrorCode { get; set; }
        public string SNAAccountDPLimit_ErrorDesc { get; set; }
        public int SNAAccountDPLimit_Id { get; set; }
        public int SNAAccountDPLimit_ModifiedBy { get; set; }
        public string SNAAccountDPLimit_ModifiedOn { get; set; }
        public string SNAAccountDPLimit_RefNum { get; set; }
        public int SNAAccountDPLimit_SNAAccountMaster_Id { get; set; }
        public int SNAAccountDPLimit_Status { get; set; }
        public string SNAAccountDPLimit_StatusPNB { get; set; }
        public string SNAAccountDPLimit_TranDate { get; set; }
        public string SNAAccountDPLimit_TranId { get; set; }
        public string SNAAccountDPLimit_TranParticular { get; set; }
        public string SNAAccountDPLimit_TranRmks { get; set; }
        public string SNAAccountDPLimit_Unique_Id { get; set; }
    }
}
