using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data;
using System.Web.UI.WebControls;

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
        string jsonBody = @"
        {
            ""SecretKey"": ""Jeet12-jBh2CcK2H@uf3S8o1222082024"",
            ""ClientId"": ""jeettosangam82024"",
            ""Date"": ""14/05/2024""
        }";

        try
        {
            // Fetch the data from the API
            var apiData = await FetchDataFromApiAsync(apiUrl, jsonBody);

            // Bind the data to the GridView
            if (apiData != null)
            {
                gvApiData.DataSource = apiData;
                gvApiData.DataBind();
            }
        }
        catch (Exception ex)
        {
            // Handle errors (you can log the error or display a message)
            // For simplicity, we'll just display the exception message
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
    }

    private async Task<DataTable> FetchDataFromApiAsync(string apiUrl, string jsonBody)
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

            // Deserialize the outer JSON object
            var outerJsonObject = Newtonsoft.Json.Linq.JObject.Parse(jsonResponse);

            // Extract the "Data" field (which is a JSON string)
            var dataJsonString = outerJsonObject["Data"].ToString();

            // Deserialize the "Data" JSON string into a DataTable
            var dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dataJsonString);

            return dataTable;
        }
    }
}
