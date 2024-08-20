
namespace ePayment_API.Models
{
    public class Click_To_Call_Response
    {
        public string Agent_No { get; set; }
        public string customer_No { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public int Call_Id { get; set; }
    }

    public class Click_To_Call_Request
    {
        public string username { get; set; }
        public string password { get; set; }
        public string customer_number { get; set; }
        public string agent_number { get; set; }
    }
}
