using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class ApiDataPage2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string testJson = "{\"IsResponse\":\"Y\",\"Msg\":null,\"Count\":\"2799\",\"Data\":\"[{\\\"LetterRefNo\\\":11970,\\\"Complainant\\\":\\\"मो0 अकरम अध्यक्ष\\\",\\\"LetterSubject\\\":\\\"नगर पंचायत, शाहपुर में नालों का नवनिर्माण कराने हेतु नगरीय सीवरेज एवं जल निकासी योजनान्तर्गत रू0 300\\\",\\\"detail\\\":\\\"जनपद-मुजफ्फरनगर की नगर पंचायत, शाहपुर में नालों का नवनिर्माण कराने हेतु नगरीय सीवरेज एवं जल निकासी योजनान्तर्गत रू0 300.00 लाख की वित्तीय एवं प्रशासकीय स्वीकृति प्रदान किये जाने\\\",\\\"MemberName\\\":\\\"Mohd Akram - Aam Aadmi Party\\\",\\\"ReceivedDate\\\":\\\"01/05/2024\\\",\\\"AcknowledgeDate\\\":\\\"10/07/2024\\\",\\\"DISTRICT_NAME\\\":\\\"MUZAFFARNAGAR\\\",\\\"LG_DT_Code\\\":172}]\"}";

        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(testJson);
        var data = JsonConvert.DeserializeObject<List<ApiResponseModel>>(apiResponse.Data);

        //Console.WriteLine(data);
    }

    protected async void btnGetData_Click(object sender, EventArgs e)
    {
        string apiUrl = "https://jeet.net.in/Service/JeetService.svc/JeetVRDetail";
        string jsonBody = @"{
        ""SecretKey"": ""Jeet12-jBh2CcK2H@uf3S8o1222082024"",
        ""ClientId"": ""jeettosangam82024"",
        ""Date"": ""14/05/2024""
    }";

        var apiResponse = await GetApiData(apiUrl, jsonBody);

        if (apiResponse != null)
        {
            gvApiData.DataSource = apiResponse;
            gvApiData.DataBind();
        }
    }

    private async Task<List<ApiResponseModel>> GetApiData(string url, string jsonBody)
    {
        using (var client = new HttpClient())
        {
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the main response
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);

                // Check if Data field is not null and deserialize it
                if (apiResponse != null && !string.IsNullOrEmpty(apiResponse.Data))
                {
                    // Deserialize the Data field (which is a JSON string) into a list of ApiResponseModel
                    var data = JsonConvert.DeserializeObject<List<ApiResponseModel>>(apiResponse.Data);
                    return data;
                }
            }

            return null; // Return null if something goes wrong or no data
        }
    }



    public class ApiResponse
    {
        public string IsResponse { get; set; }
        public string Msg { get; set; }
        public string Count { get; set; }
        public string Data { get; set; } // Data is a JSON string
    }

    public class ApiResponseModel
    {
        public int LetterRefNo { get; set; }
        public string Complainant { get; set; }
        public string LetterSubject { get; set; }
        public string Detail { get; set; }
        public string MemberName { get; set; }
        public string ReceivedDate { get; set; }
        public string AcknowledgeDate { get; set; }
        public string DISTRICT_NAME { get; set; }
        public int LG_DT_Code { get; set; }
    }
}
