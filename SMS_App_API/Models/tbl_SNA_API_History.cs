
namespace ePayment_API.Models
{
    public class tbl_SNA_API_History
    {
        public int SNA_API_History_AddedBy { get; set; }
        public string SNA_API_History_AddedOn { get; set; }
        public string SNA_API_History_AES_Key { get; set; }
        public string SNA_API_History_API_Type { get; set; }
        public int SNA_API_History_Id { get; set; }
        public string SNA_API_History_JSON_Request_Encrypted { get; set; }
        public string SNA_API_History_JSON_Request_Plain { get; set; }
        public string SNA_API_History_JSON_Response_Encrypted { get; set; }
        public string SNA_API_History_JSON_Response_Plain { get; set; }
        public string SNA_API_History_StatusCode { get; set; }
        public string SNA_API_History_ReasonPhrase { get; set; }
        public int SNA_API_History_Status { get; set; }
    }
}
