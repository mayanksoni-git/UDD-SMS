
namespace ePayment_API.Models
{
    public class PNB_GetPPATransactionDetails_Req
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

    public class PNB_GetPPATransactionDetails_Res
    {
        public string Unique_Id { get; set; }
        public string PFMS_Batch_Number { get; set; }
        public string PFMS_Tran_ID { get; set; }
        public string Payment_Product { get; set; }
        public string Agency_Account_Number { get; set; }
        public string Agency_Account_Name { get; set; }
        public string Beneficiary_Account_Number { get; set; }
        public string Beneficiary_Name { get; set; }
        public string Beneficiary_IFSC_Code { get; set; }
        public string Transaction_Amount { get; set; }
        public string Payment_Status { get; set; }
        public string Payment_Date { get; set; }
        public string Return_Reason { get; set; }
        public string Response_Status { get; set; }
        public string Error_Desc { get; set; }
    }
}

