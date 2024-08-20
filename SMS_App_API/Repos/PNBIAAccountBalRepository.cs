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
    public class PNBIAAccountBalRepository : RepositoryAsyn
    {
        public PNBIAAccountBalRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<tbl_SNAAccountBalance> get_PNBIAAccountBal(int SNAAccountMaster_Id)
        {
            tbl_SNAAccountBalance obj_tbl_SNAAccountBalance = get_tbl_PNBIAAccountBal(SNAAccountMaster_Id);
            return obj_tbl_SNAAccountBalance;
        }
        private tbl_SNAAccountBalance get_tbl_PNBIAAccountBal(int SNAAccountMaster_Id)
        {
            string response = "false";
            PNB_Global obj_PNB_Global = new PNB_Global();
            tbl_SNAAccountBalance obj_tbl_SNAAccountBalance = new tbl_SNAAccountBalance();
            string pnb_thumb = System.Configuration.ConfigurationManager.AppSettings["pnb_thumb"];
            string AMRUT_thumb = System.Configuration.ConfigurationManager.AppSettings["AMRUT_thumb"];

            DataSet ds = new DataSet();
            ds = new DataLayer().get_tbl_SNAAccountMaster(SNAAccountMaster_Id);
            if (Utility.CheckDataSet(ds))
            {
                tbl_SNA_API_History obj_tbl_SNA_API_History = new tbl_SNA_API_History();

                #region Plain Request
                PNB_GetAccountBal_Req obj_PNB_GetAccountBal_Req = new PNB_GetAccountBal_Req();
                obj_PNB_GetAccountBal_Req.Account_Number = ds.Tables[0].Rows[0]["SNAAccountMaster_ACCT_NO"].ToString().Trim();
                obj_PNB_GetAccountBal_Req.Entity_Code = "AMRUT";
                obj_PNB_GetAccountBal_Req.Request_Id = DateTime.Now.Ticks.ToString();
                string PNB_GetAccountBal_Req_json = Newtonsoft.Json.JsonConvert.SerializeObject(obj_PNB_GetAccountBal_Req);
                #endregion

                #region Encrypted Request
                string rand_AES_key = EncryptionLogic.GenerateKey();
                obj_PNB_Global.EncData = EncryptionLogic.Encrypt(PNB_GetAccountBal_Req_json, rand_AES_key);
                obj_PNB_Global.EncAesKey = EncryptionLogic.Encryption_PNB(rand_AES_key, pnb_thumb);
                //obj_PNB_Global.EncAesKey = EncryptionLogic.Encryption_AMRUT(rand_AES_key, AMRUT_thumb);
                obj_PNB_Global.Sign = EncryptionLogic.SignData_AMRUT(PNB_GetAccountBal_Req_json, AMRUT_thumb);
                string final_Request_String = Newtonsoft.Json.JsonConvert.SerializeObject(obj_PNB_Global);
                #endregion

                obj_tbl_SNA_API_History.SNA_API_History_AddedBy = 1;
                obj_tbl_SNA_API_History.SNA_API_History_AES_Key = rand_AES_key;
                obj_tbl_SNA_API_History.SNA_API_History_API_Type = "Fetch_Account_Balance_And_DPLimit";
                obj_tbl_SNA_API_History.SNA_API_History_JSON_Request_Encrypted = final_Request_String;
                obj_tbl_SNA_API_History.SNA_API_History_JSON_Request_Plain = PNB_GetAccountBal_Req_json;
                obj_tbl_SNA_API_History.SNA_API_History_Status = 1;

                #region Encrypted Response
                string StatusCode = "";
                string ReasonPhrase = "";
                PNB_Global obj_PNB_Global_R = new PNB_Global();
                obj_PNB_Global_R = Get_Account_Balance(obj_PNB_Global, ref StatusCode, ref ReasonPhrase);
                obj_tbl_SNA_API_History.SNA_API_History_StatusCode = StatusCode;
                obj_tbl_SNA_API_History.SNA_API_History_ReasonPhrase = ReasonPhrase;
                #endregion

                if (obj_PNB_Global_R != null)
                {
                    #region Decrypted Response
                    obj_tbl_SNA_API_History.SNA_API_History_JSON_Response_Encrypted = Newtonsoft.Json.JsonConvert.SerializeObject(obj_PNB_Global_R);
                    bool isVerify = EncryptionLogic.VerifySign_AMRUT(obj_PNB_Global_R.Sign, PNB_GetAccountBal_Req_json, AMRUT_thumb);
                    string EncAesKey = EncryptionLogic.Decryption_AMRUT(obj_PNB_Global_R.EncAesKey, AMRUT_thumb);
                    string AesKey = ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(EncAesKey));
                    string PNB_GetAccountBal_Res_json = EncryptionLogic.Decrypt(obj_PNB_Global_R.EncData, AesKey);

                    obj_tbl_SNA_API_History.SNA_API_History_JSON_Response_Plain = PNB_GetAccountBal_Res_json;


                    PNB_GetAccountBal_Res obj_PNB_GetAccountBal_Res = new PNB_GetAccountBal_Res();
                    obj_PNB_GetAccountBal_Res = Newtonsoft.Json.JsonConvert.DeserializeObject<PNB_GetAccountBal_Res>(PNB_GetAccountBal_Res_json);
                    #endregion

                    obj_tbl_SNAAccountBalance.SNAAccountBalance_AddedBy = 1;
                    try
                    {
                        obj_tbl_SNAAccountBalance.SNAAccountBalance_Balance = Convert.ToDecimal(obj_PNB_GetAccountBal_Res.DP_Limit);
                    }
                    catch
                    {
                        obj_tbl_SNAAccountBalance.SNAAccountBalance_Balance = obj_PNB_GetAccountBal_Res.Amount;
                    }
                    obj_tbl_SNAAccountBalance.SNAAccountBalance_Error_Desc = obj_PNB_GetAccountBal_Res.Error_Desc;
                    obj_tbl_SNAAccountBalance.SNAAccountBalance_Request_Id = obj_PNB_GetAccountBal_Res.Request_Id;
                    obj_tbl_SNAAccountBalance.SNAAccountBalance_SNAAccountMaster_Id = SNAAccountMaster_Id;
                    obj_tbl_SNAAccountBalance.SNAAccountBalance_Status = 1;
                    obj_tbl_SNAAccountBalance.SNAAccountBalance_StatusPNB = obj_PNB_GetAccountBal_Res.Status;

                    if (new DataLayer().Update_SNA_Account_Balance(obj_tbl_SNA_API_History, obj_tbl_SNAAccountBalance))
                    {
                        response = "true";
                        obj_tbl_SNAAccountBalance.SNAAccountBalance_Error_Desc = response;
                    }
                    else
                    {
                        response = "false";
                        obj_tbl_SNAAccountBalance.SNAAccountBalance_Error_Desc = response;
                    }
                }
                else
                {
                    if (new DataLayer().Update_SNA_Account_Balance(obj_tbl_SNA_API_History, null))
                    {
                        response = "false";
                        obj_tbl_SNAAccountBalance.SNAAccountBalance_Error_Desc = response;
                    }
                    else
                    {
                        response = "false";
                        obj_tbl_SNAAccountBalance.SNAAccountBalance_Error_Desc = response;
                    }
                }
            }
            else
            {
                response = "false";
                obj_tbl_SNAAccountBalance.SNAAccountBalance_Error_Desc = response;
            }
            return obj_tbl_SNAAccountBalance;
        }

        [HttpPost]
        public PNB_Global Get_Account_Balance(PNB_Global obj_PNB_Global, ref string StatusCode, ref string ReasonPhrase)
        {
            string responseString = string.Empty;
            PNB_Global obj_PNB_Global_R = new PNB_Global();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["API_Base_URL"] + "/Fetch_Account_Balance_And_DPLimit");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(System.Configuration.ConfigurationManager.AppSettings["API_User"] + ":" + System.Configuration.ConfigurationManager.AppSettings["API_Pass"]);
                string val = System.Convert.ToBase64String(plainTextBytes);
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
                StringContent content = new StringContent(JsonConvert.SerializeObject(obj_PNB_Global));
                var response = client.PostAsync(client.BaseAddress, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    StatusCode = "";
                    ReasonPhrase = "";
                    responseString = response.Content.ReadAsStringAsync().Result;
                    obj_PNB_Global_R = response.Content.ReadAsAsync<PNB_Global>().Result;
                }
                else
                {
                    StatusCode = ((int)response.StatusCode).ToString();
                    ReasonPhrase = response.ReasonPhrase;
                    obj_PNB_Global_R = null;
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
            return obj_PNB_Global_R;
        }
    }
}
