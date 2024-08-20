
namespace ePayment_API.Models
{
    public class PPA_Details
    {
        public string PPA_Date { get; set; }
        public string PPA_Number { get; set; }
        public int PPA_Invoice_Id { get; set; }
        /// <summary>
        /// Account Or Batch
        /// </summary>
        public string Req_Type { get; set; }
    }
}
