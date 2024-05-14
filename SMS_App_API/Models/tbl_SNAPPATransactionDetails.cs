
namespace ePayment_API.Models
{
    public class tbl_SNAPPATransactionDetails
    {
        public int SNAPPATransactionDetails_AddedBy { get; set; }
        public string SNAPPATransactionDetails_AddedOn { get; set; }
        public int SNAPPATransactionDetails_Id { get; set; }
        public int SNAPPATransactionDetails_ModifiedBy { get; set; }
        public string SNAPPATransactionDetails_ModifiedOn { get; set; }
        public int SNAPPATransactionDetails_Status { get; set; }
        public string SNAPPATransactionDetails_Unique_Id { get; set; }
        public string SNAPPATransactionDetails_PFMS_Batch_Number { get; set; }
        public string SNAPPATransactionDetails_PFMS_Tran_ID { get; set; }
        public string SNAPPATransactionDetails_Payment_Product { get; set; }
        public string SNAPPATransactionDetails_Agency_Account_Number { get; set; }
        public string SNAPPATransactionDetails_Agency_Account_Name { get; set; }
        public string SNAPPATransactionDetails_Beneficiary_Account_Number { get; set; }
        public string SNAPPATransactionDetails_Beneficiary_Name { get; set; }
        public string SNAPPATransactionDetails_Beneficiary_IFSC_Code { get; set; }
        public string SNAPPATransactionDetails_Transaction_Amount { get; set; }
        public string SNAPPATransactionDetails_Payment_Status { get; set; }
        public string SNAPPATransactionDetails_Payment_Date { get; set; }
        public string SNAPPATransactionDetails_Return_Reason { get; set; }
        public string SNAPPATransactionDetails_Response_Status { get; set; }
        public string SNAPPATransactionDetails_Error_Desc { get; set; }
        public int SNAPPATransactionDetails_Invoice_Id { get; set; }
    }
}
