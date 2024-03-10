using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Web.SessionState;
using System.Web.UI;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

/// <summary>
/// Summary description for DataLayerSNA
/// </summary>
public partial class DataLayerSNA : Page, IRequiresSessionState
{
    public DataLayerSNA()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string getSNA_Total_Balance(string Parent_Master_Id)
    {
        try
        {
            tbl_SNAAccountBalance obj_MeetingStatus = new tbl_SNAAccountBalance();
            string baseURL = ConfigurationManager.AppSettings.Get("API");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL + "PNBAccountBalSNA/"+ Parent_Master_Id);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                var rVal = response.Content.ReadAsStringAsync().Result;
                obj_MeetingStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<tbl_SNAAccountBalance>(rVal);
                if (obj_MeetingStatus != null)
                {
                    if (obj_MeetingStatus.SNAAccountBalance_Error_Desc == "true")
                    {
                        return AllClasses.convert_To_Indian_No_Format(decimal.Round(obj_MeetingStatus.SNAAccountBalance_Balance / 100000, 3, MidpointRounding.AwayFromZero).ToString(), "Decimal");
                    }
                    else
                    {
                        return "0.00";
                    }
                }
                else
                {
                    return "0.00";
                }
            }
            else
            {
                return "0.00";
            }
        }
        catch
        {
            return "0.00";
        }
    }

    public string getSNA_IA_Balance(int SNAAccountMaster_Id)
    {
        try
        {
            tbl_SNAAccountBalance obj_MeetingStatus = new tbl_SNAAccountBalance();
            string baseURL = ConfigurationManager.AppSettings.Get("API");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL + "PNBIAAccountBal/" + SNAAccountMaster_Id.ToString());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                var rVal = response.Content.ReadAsStringAsync().Result;
                obj_MeetingStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<tbl_SNAAccountBalance>(rVal);
                if (obj_MeetingStatus != null)
                {
                    if (obj_MeetingStatus.SNAAccountBalance_Error_Desc == "true")
                    {
                        return AllClasses.convert_To_Indian_No_Format(obj_MeetingStatus.SNAAccountBalance_Balance.ToString(), "Decimal");
                    }
                    else
                    {
                        return "0.00";
                    }
                }
                else
                {
                    return "0.00";
                }
            }
            else
            {
                return "0.00";
            }
        }
        catch
        {
            return "0.00";
        }
    }
    public string getSNA_Total_Balance(int SNAAccountMaster_Id)
    {
        try
        {
            tbl_SNAAccountBalance obj_MeetingStatus = new tbl_SNAAccountBalance();
            string baseURL = ConfigurationManager.AppSettings.Get("API");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL + "PNBAccountBal/" + SNAAccountMaster_Id.ToString());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                var rVal = response.Content.ReadAsStringAsync().Result;
                obj_MeetingStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<tbl_SNAAccountBalance>(rVal);
                if (obj_MeetingStatus != null)
                {
                    if (obj_MeetingStatus.SNAAccountBalance_Error_Desc == "true")
                    {
                        return AllClasses.convert_To_Indian_No_Format(decimal.Round(obj_MeetingStatus.SNAAccountBalance_Balance / 100000, 3, MidpointRounding.AwayFromZero).ToString(), "Decimal");
                    }
                    else
                    {
                        return "0.00";
                    }
                }
                else
                {
                    return "0.00";
                }
            }
            else
            {
                return "0.00";
            }
        }
        catch
        {
            return "0.00";
        }
    }

    public bool setSNA_DP_Limit(DP_Limit obj_DP_Limit, ref string Msg)
    {
        try
        {
            string json_obj_DP_Limit = Newtonsoft.Json.JsonConvert.SerializeObject(obj_DP_Limit);
            tbl_SNAAccountDPLimit obj_tbl_SNAAccountDPLimit = new tbl_SNAAccountDPLimit();
            string baseURL = ConfigurationManager.AppSettings.Get("API");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL + "PNBAssignDPLimit");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent context = new StringContent(json_obj_DP_Limit, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress, context).Result;
            if (response.IsSuccessStatusCode)
            {
                var rVal = response.Content.ReadAsStringAsync().Result;
                obj_tbl_SNAAccountDPLimit = Newtonsoft.Json.JsonConvert.DeserializeObject<tbl_SNAAccountDPLimit>(rVal);
                if (obj_tbl_SNAAccountDPLimit != null)
                {
                    if (obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_StatusPNB == "S")
                    {
                        return true;
                    }
                    else
                    {
                        Msg = obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_ErrorDesc;
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    public tbl_SNAPPADetails getPPADetails(PPA_Details obj_PPA_Details)
    {
        try
        {
            string json_obj_DP_Limit = Newtonsoft.Json.JsonConvert.SerializeObject(obj_PPA_Details);
            tbl_SNAPPADetails obj_tbl_SNAPPADetails = new tbl_SNAPPADetails();
            string baseURL = ConfigurationManager.AppSettings.Get("API");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL + "PNBGetPPADetails");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent context = new StringContent(json_obj_DP_Limit, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress, context).Result;
            if (response.IsSuccessStatusCode)
            {
                var rVal = response.Content.ReadAsStringAsync().Result;
                obj_tbl_SNAPPADetails = Newtonsoft.Json.JsonConvert.DeserializeObject<tbl_SNAPPADetails>(rVal);
                return obj_tbl_SNAPPADetails;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    public List<PNB_GetPPADetails_Res> GetPPABatchDetails(PPA_Details obj_PPA_Details)
    {
        try
        {
            string json_obj_DP_Limit = Newtonsoft.Json.JsonConvert.SerializeObject(obj_PPA_Details);
            List<PNB_GetPPADetails_Res> obj_PNB_GetPPADetails_Res_Li = new List<PNB_GetPPADetails_Res>();
            string baseURL = ConfigurationManager.AppSettings.Get("API");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL + "PNBGetPPABatchDetails");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent context = new StringContent(json_obj_DP_Limit, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress, context).Result;
            if (response.IsSuccessStatusCode)
            {
                var rVal = response.Content.ReadAsStringAsync().Result;
                obj_PNB_GetPPADetails_Res_Li = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PNB_GetPPADetails_Res>>(rVal);
                return obj_PNB_GetPPADetails_Res_Li;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    public List<PNB_GetPPATransactionDetails_Res> GetPPATransactionDetails(PPA_Details obj_PPA_Details)
    {
        try
        {
            string json_obj_DP_Limit = Newtonsoft.Json.JsonConvert.SerializeObject(obj_PPA_Details);
            List<PNB_GetPPATransactionDetails_Res> obj_PNB_GetPPATransactionDetails_Res_Li = new List<PNB_GetPPATransactionDetails_Res>();
            string baseURL = ConfigurationManager.AppSettings.Get("API");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL + "PNBGetPPATransactionDetails");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent context = new StringContent(json_obj_DP_Limit, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress, context).Result;
            if (response.IsSuccessStatusCode)
            {
                var rVal = response.Content.ReadAsStringAsync().Result;
                obj_PNB_GetPPATransactionDetails_Res_Li = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PNB_GetPPATransactionDetails_Res>>(rVal);
                return obj_PNB_GetPPATransactionDetails_Res_Li;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }
}