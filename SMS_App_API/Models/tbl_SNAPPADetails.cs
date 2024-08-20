
namespace ePayment_API.Models
{
    public class tbl_SNAPPADetails
    {
        public int SNAPPADetails_AddedBy { get; set; }
        public string SNAPPADetails_AddedOn { get; set; }
        public decimal? SNAPPADetails_Amount { get; set; }
        public string SNAPPADetails_Batch_Id { get; set; }
        public string SNAPPADetails_Error_Desc { get; set; }
        public int SNAPPADetails_Id { get; set; }
        public int SNAPPADetails_ModifiedBy { get; set; }
        public string SNAPPADetails_ModifiedOn { get; set; }
        public string SNAPPADetails_PPA_Number { get; set; }
        public string SNAPPADetails_PPA_Receive_Date { get; set; }
        public int SNAPPADetails_SNAAccountMaster_Id { get; set; }
        public int SNAPPADetails_Status { get; set; }
        public string SNAPPADetails_StatusPNB { get; set; }
        public string SNAPPADetails_Unique_Id { get; set; }
        public string SNAPPADetails_Payment_Product { get; set; }
        public string SNAPPADetails_PFMS_Batch_Number { get; set; }
        public string SNAPPADetails_PFMS_Entries_Number { get; set; }
        public string SNAPPADetails_Batch_Amount { get; set; }
        public string SNAPPADetails_Debit_Account_Number { get; set; }
        public string SNAPPADetails_Debit_Account_Name { get; set; }
        public string SNAPPADetails_File_Received_Date { get; set; }
        public string SNAPPADetails_Expiry_Date { get; set; }
        public string SNAPPADetails_Batch_Status { get; set; }
        public string SNAPPADetails_Failure_Reason_Code { get; set; }
        public string SNAPPADetails_Failure_Reason_Description { get; set; }
        public string SNAPPADetails_Response_Status { get; set; }
        public int SNAPPADetails_Invoice_Id { get; set; }
        public string SNAPPADetails_CBSTxnId { get; set; }
        public string SNAPPADetails_CBSTranDate { get; set; }
    }
}
