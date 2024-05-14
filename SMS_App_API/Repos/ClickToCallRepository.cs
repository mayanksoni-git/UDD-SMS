using ePayment_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ePayment_API.Repos
{
    public class ClickToCallRepository : RepositoryAsyn
    {
        public ClickToCallRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>

        public async Task<Click_To_Call_Response> get_Click_To_Call(Click_To_Call_Request obj_Click_To_Call_Request)
        {
            Click_To_Call_Response obj_Click_To_Call_Response = get_tbl_Click_To_Call(obj_Click_To_Call_Request);
            return obj_Click_To_Call_Response;
        }

        private Click_To_Call_Response get_tbl_Click_To_Call(Click_To_Call_Request obj_Click_To_Call_Request)
        {
            return Click_To_Call(obj_Click_To_Call_Request);
        }

        [HttpPost]
        public Click_To_Call_Response Click_To_Call(Click_To_Call_Request obj_Click_To_Call_Request)
        {
            string responseString = string.Empty;
            Click_To_Call_Response obj_Click_To_Call_Response = new Click_To_Call_Response();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://voice.ivrs.solutions/api/v1-click-to-call?username=Himanshu1&password=89474561&customer_number=" + obj_Click_To_Call_Request.customer_number + "&agent_number=" + obj_Click_To_Call_Request.agent_number + "");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(obj_Click_To_Call_Request));
                var response = client.PostAsync(client.BaseAddress, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    responseString = response.Content.ReadAsStringAsync().Result;
                    obj_Click_To_Call_Response = response.Content.ReadAsAsync<Click_To_Call_Response>().Result;
                }
                else
                {
                    obj_Click_To_Call_Response.status = "error";
                    obj_Click_To_Call_Response.message = "Originate failed";
                }
            }
            return obj_Click_To_Call_Response;
        }
    }
}
