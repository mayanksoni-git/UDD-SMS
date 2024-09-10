using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

public partial class ApiDataPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected async void btnFetchData_Click(object sender, EventArgs e)
    {
        await FetchAndBindDataAsync();
    }


    private async Task FetchAndBindDataAsync()
    {
        string apiUrl = "https://jeet.net.in/Service/JeetService.svc/JeetVRDetail";

        // Get the selected date from the TextBox
        string selectedDate = txtDate.Text; // Assuming the date format is "yyyy-MM-dd"
        if (string.IsNullOrEmpty(selectedDate))
        {
            Response.Write("<script>alert('Please select a date.');</script>");
            return;
        }

        // Use string.Format to construct the JSON body
        string jsonBody = string.Format(@"
        {{
            ""SecretKey"": ""Jeet12-jBh2CcK2H@uf3S8o1222082024"",
            ""ClientId"": ""jeettosangam82024"",
            ""Date"": ""{0}""
        }}", DateTime.Parse(selectedDate).ToString("dd/MM/yyyy"));

        try
        {
            var apiData = await FetchDataFromApiAsync(apiUrl, jsonBody);
            if (apiData != null)
            {
                gvApiData.DataSource = apiData.Data;
                gvApiData.DataBind();

                InsertDataIntoSql(apiData.Data);
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
        }
    }

    //private async Task FetchAndBindDataAsync()
    //{
    //    string apiUrl = "https://jeet.net.in/Service/JeetService.svc/JeetVRDetail";
    //    string jsonBody = @"
    //    {
    //        ""SecretKey"": ""Jeet12-jBh2CcK2H@uf3S8o1222082024"",
    //        ""ClientId"": ""jeettosangam82024"",
    //        ""Date"": ""01/07/2024""
    //    }";

    //    try
    //    {
    //        var apiData = await FetchDataFromApiAsync(apiUrl, jsonBody);
    //        if (apiData != null)
    //        {
    //            gvApiData.DataSource = apiData.Data;
    //            gvApiData.DataBind();

    //            InsertDataIntoSql(apiData.Data);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write("<script>alert('Test" + ex.Message + "');</script>");
    //    }
    //}


    private void InsertDataIntoSql(List<List_Data> apiData)
    {
        string connectionString = ConfigurationManager.AppSettings.Get("conn");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            foreach (var item in apiData)
            {
                DateTime receivedDate, acknowledgeDate;
                DateTime.TryParseExact((string)item.ReceivedDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out receivedDate);
                DateTime.TryParseExact((string)item.AcknowledgeDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out acknowledgeDate);

                using (SqlCommand command = new SqlCommand("sp_InsertOrUpdateJeetApiWorkProposalData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@LetterRefNo", item.LetterRefNo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Complainant", item.Complainant ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LetterSubject", item.LetterSubject ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Detail", item.detail ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@MemberName", item.MemberName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ReceivedDate", receivedDate != DateTime.MinValue ? (object)receivedDate : DBNull.Value);
                    command.Parameters.AddWithValue("@AcknowledgeDate", acknowledgeDate != DateTime.MinValue ? (object)acknowledgeDate : DBNull.Value);
                    command.Parameters.AddWithValue("@DistrictName", item.DISTRICT_NAME ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LGDTCode", item.LG_DT_Code ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }
    }


    //private void InsertDataIntoSql(List<List_Data> apiData)
    //{
    //    string connectionString = ConfigurationManager.AppSettings.Get("conn");

    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();

    //        foreach (var item in apiData)
    //        {
    //            DateTime receivedDate, acknowledgeDate;
    //            DateTime.TryParseExact((string)item.ReceivedDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out receivedDate);
    //            DateTime.TryParseExact((string)item.AcknowledgeDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out acknowledgeDate);

    //            using (SqlCommand command = new SqlCommand(
    //                @"INSERT INTO ApiData 
    //            (LetterRefNo, Complainant, LetterSubject, Detail, MemberName, ReceivedDate, AcknowledgeDate, DISTRICT_NAME, LG_DT_Code)
    //            VALUES (@LetterRefNo, @Complainant, @LetterSubject, @Detail, @MemberName, @ReceivedDate, @AcknowledgeDate, @DistrictName, @LGDTCode)", connection))
    //            {
    //                command.Parameters.AddWithValue("@LetterRefNo", item.LetterRefNo ?? (object)DBNull.Value);
    //                command.Parameters.AddWithValue("@Complainant", item.Complainant ?? (object)DBNull.Value);
    //                command.Parameters.AddWithValue("@LetterSubject", item.LetterSubject ?? (object)DBNull.Value);
    //                command.Parameters.AddWithValue("@Detail", item.detail ?? (object)DBNull.Value);
    //                command.Parameters.AddWithValue("@MemberName", item.MemberName ?? (object)DBNull.Value);
    //                command.Parameters.AddWithValue("@ReceivedDate", receivedDate);
    //                command.Parameters.AddWithValue("@AcknowledgeDate", acknowledgeDate);
    //                command.Parameters.AddWithValue("@DistrictName", item.DISTRICT_NAME ?? (object)DBNull.Value);
    //                command.Parameters.AddWithValue("@LGDTCode", item.LG_DT_Code ?? (object)DBNull.Value);

    //                command.ExecuteNonQuery();
    //            }
    //        }
    //    }
    //}

    private async Task<ApiResponse> FetchDataFromApiAsync(string apiUrl, string jsonBody)
    {
        using (var client = new HttpClient())
        {
            // Prepare the JSON body for the POST request
            var content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");

            // Send the POST request to the API
            var response = await client.PostAsync(apiUrl, content);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Read the response as a string
            var jsonResponse = await response.Content.ReadAsStringAsync();

            jsonResponse = jsonResponse.Trim('"').Replace("\\\"", "\"");
            jsonResponse = jsonResponse.Replace("\\\\", "\\");
            jsonResponse = jsonResponse.Replace("\"[", "[");
            jsonResponse = jsonResponse.Replace("]\"", "]");
            jsonResponse = jsonResponse.Trim('"').Replace("\\\"", "\"");
            jsonResponse = jsonResponse.Trim('"').Replace("\\\\\"", "'");

            try
            {
                Response.Write(jsonResponse.Length);
                var dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                return dataTable;
            }
            catch (Exception Ex)
            {
                Response.Write("Exppp :: " + Ex.Message);
            }
            return null;
        }
    }

    public class ApiResponse
    {
        public string IsResponse { get; set; }
        public string Msg { get; set; }
        public string Count { get; set; }
        public List<List_Data> Data { get; set; } // Data is a JSON string
    }

    public class List_Data
    {
        public string LetterRefNo { get; set; }
        public string Complainant { get; set; }
        public string LetterSubject { get; set; }
        public string detail { get; set; } // Data is a JSON string
        public string MemberName { get; set; } // Data is a JSON string
        public string ReceivedDate { get; set; } // Data is a JSON string
        public string AcknowledgeDate { get; set; } // Data is a JSON string
        public string DISTRICT_NAME { get; set; } // Data is a JSON string
        public string LG_DT_Code { get; set; }
    }
}
