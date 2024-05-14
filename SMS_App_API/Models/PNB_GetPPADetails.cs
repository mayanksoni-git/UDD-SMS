
namespace ePayment_API.Models
{
    public class PNB_GetPPADetails_Req
    {
        public string Unique_Id { get; set; }
        public string PPA_Number { get; set; }
        public string Entity_Code { get; set; }
        /// <summary>
        /// Account Or Batch
        /// </summary>
        public string Req_Type { get; set; }
        public string UploadDate { get; set; }
    }

    public class PNB_GetPPADetails_Res
    {
        public string Unique_Id { get; set; }
        public string Payment_Product { get; set; }
        public string PFMS_Batch_Number { get; set; }
        public string PFMS_Entries_Number { get; set; }
        public string Batch_Amount { get; set; }
        public string Debit_Account_Number { get; set; }
        public string Debit_Account_Name { get; set; }
        public string File_Received_Date { get; set; }
        public string Expiry_Date { get; set; }
        public string Batch_Status { get; set; }
        public string Failure_Reason_Code { get; set; }
        public string Failure_Reason_Description { get; set; }
        public string Response_Status { get; set; }
        public string PPA_Number { get; set; }
        public string Status { get; set; }
        public string Batch_Id { get; set; }
        public string PPA_Receive_Date { get; set; }
        public decimal? Amount { get; set; }
        public string Error_Desc { get; set; }
        public string CBSTxnId { get; set; }
        public string CBSTranDate { get; set; }
    }
}

