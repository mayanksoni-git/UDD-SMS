using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using Newtonsoft.Json.Linq; // You need to install Newtonsoft.Json via NuGet


public partial class ApiDataPage3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }



    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fileUpload.HasFile)
        {
            try
            {
                // Read the file content
                string fileContent;
                using (StreamReader reader = new StreamReader(fileUpload.PostedFile.InputStream))
                {
                    fileContent = reader.ReadToEnd();
                }

                // Parse JSON
                JObject json = JObject.Parse(fileContent);

                // Check if 'Data' is a JArray
                JToken dataToken = json["Data"];
                if (dataToken != null && dataToken.Type == JTokenType.Array)
                {
                    JArray dataArray = (JArray)dataToken;

                    // Connection string
                    string connectionString = "Data Source=103.248.60.254;Initial Catalog=uddsmsdb;user id=uddsmsdbuser;password=6x?35jpK6;MultipleActiveResultSets=True";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        foreach (var item in dataArray)
                        {
                            DateTime receivedDate, acknowledgeDate;

                            // Try parsing dates
                            DateTime.TryParseExact((string)item["ReceivedDate"], "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out receivedDate);
                            DateTime.TryParseExact((string)item["AcknowledgeDate"], "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out acknowledgeDate);

                            // Prepare SQL command
                            string sql = "INSERT INTO Complaints (LetterRefNo, Complainant, LetterSubject, Detail, MemberName, ReceivedDate, AcknowledgeDate, DistrictName, LG_DT_Code) " +
                                         "VALUES (@LetterRefNo, @Complainant, @LetterSubject, @Detail, @MemberName, @ReceivedDate, @AcknowledgeDate, @DistrictName, @LG_DT_Code)";

                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("@LetterRefNo", (int)item["LetterRefNo"]);
                                cmd.Parameters.AddWithValue("@Complainant", (string)item["Complainant"]);
                                cmd.Parameters.AddWithValue("@LetterSubject", (string)item["LetterSubject"]);
                                cmd.Parameters.AddWithValue("@Detail", (string)item["detail"]);
                                cmd.Parameters.AddWithValue("@MemberName", (string)item["MemberName"]);
                                cmd.Parameters.AddWithValue("@ReceivedDate", receivedDate);
                                cmd.Parameters.AddWithValue("@AcknowledgeDate", acknowledgeDate);
                                cmd.Parameters.AddWithValue("@DistrictName", (string)item["DISTRICT_NAME"]);
                                cmd.Parameters.AddWithValue("@LG_DT_Code", (int)item["LG_DT_Code"]);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                else
                {
                    Response.Write("Invalid JSON format: 'Data' is not an array.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Response.Write("Error: "+ ex.Message);
            }
        }
        else
        {
            Response.Write("No file uploaded.");
        }
    }



    //protected void btnStoreData_Click(object sender, EventArgs e)
    //{
    //    string jsonData = @"{""IsResponse"":""Y"",""Msg"":null,""Count"":""2799"",""Data"":[{""LetterRefNo"":15349,""Complainant"":""श्री राजीव गुम्बर, मान. विधायक"",""LetterSubject"":""सहारनपुर यू0पी0पी0सी0एल0 द्वारा अनुचित मनमाना टैरिफ एवं जुर्माना लगाने के प्रकरण में जॉच कराकर नियमा"",""detail"":""सहारनपुर यू0पी0पी0सी0एल0 द्वारा अनुचित मनमाना टैरिफ एवं जुर्माना लगाने के प्रकरण में जॉच कराकर नियमानुसार आवश्यक कार्यवाही कराये जाने"",""MemberName"":""3 - Saharanpur nagar - Shri Rajeev Gumber"",""ReceivedDate"":""24/06/2024"",""AcknowledgeDate"":""10/08/2024"",""DISTRICT_NAME"":""SAHARANPUR"",""LG_DT_Code"":177},{""LetterRefNo"":14042,""Complainant"":""श्रीमती कोमल पंवार"",""LetterSubject"":""जनपद-सहारनपुर की नगर पालिका परिषद, सरसावा में राज्य सेक्टर की योजनाओं में धनराशि स्वीकृत किये जाने ह"",""detail"":""जनपद-सहारनपुर की नगर पालिका परिषद, सरसावा में राज्य सेक्टर की योजनाओं में धनराशि स्वीकृत किये जाने हेतु"",""MemberName"":""Mrs. Komal Panwar - Independent"",""ReceivedDate"":""24/06/2024"",""AcknowledgeDate"":""26/07/2024"",""DISTRICT_NAME"":""SAHARANPUR"",""LG_DT_Code"":177},{""LetterRefNo"":14038,""Complainant"":""डॉ. अजय कुमार सिंह"",""LetterSubject"":""नगर निगम सहारनपुर में सीवरेज एवं जलनिकासी योजनान्तर्गत नाला निर्माण कार्य कराये जाने हेतु रू0 1260.5"",""detail"":""नगर निगम सहारनपुर में सीवरेज एवं जलनिकासी योजनान्तर्गत नाला निर्माण कार्य कराये जाने हेतु रू0 1260.54 लाख की धनराशि अवमुक्त किये जाने हेतु"",""MemberName"":""Dr. Ajay Kumar Singh - Bharatiya Janata Party"",""ReceivedDate"":""26/06/2024"",""AcknowledgeDate"":""26/07/2024"",""DISTRICT_NAME"":""SAHARANPUR"",""LG_DT_Code"":177},{""LetterRefNo"":13471,""Complainant"":""श्री राजीव गुम्बर"",""LetterSubject"":""जनपद-सहारनपुर के नगर निगम में  विभिन्न वार्डो में मुख्यमंत्री नगरीय अल्पविकसित एवं मलिन बस्ती विकास"",""detail"":""जनपद-सहारनपुर के नगर निगम में  विभिन्न वार्डो में मुख्यमंत्री नगरीय अल्पविकसित एवं मलिन बस्ती विकास (अनुदान सं-83) येाजनान्तर्गत सड़क निर्माण कार्य किये जाने हेतु वित्तीय स्वीकृति किये जाने"",""MemberName"":""3 - Saharanpur nagar - Shri Rajeev Gumber"",""ReceivedDate"":""24/06/2024"",""AcknowledgeDate"":""22/07/2024"",""DISTRICT_NAME"":""SAHARANPUR"",""LG_DT_Code"":177}]}";

    //    // Parse JSON
    //    JObject json = JObject.Parse(jsonData);
    //    JArray dataArray = (JArray)json["Data"];

    //    // Connection string
    //    //string connectionString = "Data Source=103.248.60.254;Initial Catalog=uddadmin;user id=uddsmsdbuser;password=6x?35jpK6;MultipleActiveResultSets=True";
    //    string connectionString = ConfigurationManager.AppSettings.Get("conn");

    //    using (SqlConnection conn = new SqlConnection(connectionString))
    //    {
    //        conn.Open();

    //        foreach (var item in dataArray)
    //        {
    //            DateTime receivedDate, acknowledgeDate;

    //            // Try parsing dates
    //            DateTime.TryParseExact((string)item["ReceivedDate"], "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out receivedDate);
    //            DateTime.TryParseExact((string)item["AcknowledgeDate"], "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out acknowledgeDate);

    //            // Prepare SQL command
    //            string sql = "INSERT INTO Complaints (LetterRefNo, Complainant, LetterSubject, Detail, MemberName, ReceivedDate, AcknowledgeDate, DistrictName, LG_DT_Code) " +
    //                         "VALUES (@LetterRefNo, @Complainant, @LetterSubject, @Detail, @MemberName, @ReceivedDate, @AcknowledgeDate, @DistrictName, @LG_DT_Code)";

    //            using (SqlCommand cmd = new SqlCommand(sql, conn))
    //            {
    //                cmd.Parameters.AddWithValue("@LetterRefNo", (int)item["LetterRefNo"]);
    //                cmd.Parameters.AddWithValue("@Complainant", (string)item["Complainant"]);
    //                cmd.Parameters.AddWithValue("@LetterSubject", (string)item["LetterSubject"]);
    //                cmd.Parameters.AddWithValue("@Detail", (string)item["detail"]);
    //                cmd.Parameters.AddWithValue("@MemberName", (string)item["MemberName"]);
    //                cmd.Parameters.AddWithValue("@ReceivedDate", receivedDate);
    //                cmd.Parameters.AddWithValue("@AcknowledgeDate", acknowledgeDate);
    //                cmd.Parameters.AddWithValue("@DistrictName", (string)item["DISTRICT_NAME"]);
    //                cmd.Parameters.AddWithValue("@LG_DT_Code", (int)item["LG_DT_Code"]);

    //                cmd.ExecuteNonQuery();
    //            }
    //        }
    //    }
    //}
}

