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
    public class PNBAssignDPLimitRepository : RepositoryAsyn
    {
        public PNBAssignDPLimitRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<string> get_PNBAssignDPLimit(int SNAAccountMaster_Id)
        {
            string _val = get_tbl_PNBAssignDPLimit(SNAAccountMaster_Id);
            return _val;
        }

        public async Task<tbl_SNAAccountDPLimit> get_PNBAssignDPLimit(DP_Limit obj_DP_Limit)
        {
            tbl_SNAAccountDPLimit obj_tbl_SNAAccountDPLimit = get_tbl_PNBAssignDPLimit(obj_DP_Limit);
            return obj_tbl_SNAAccountDPLimit;
        }

        private tbl_SNAAccountDPLimit get_tbl_PNBAssignDPLimit(DP_Limit obj_DP_Limit)
        {
            tbl_SNAAccountDPLimit obj_tbl_SNAAccountDPLimit = new tbl_SNAAccountDPLimit();

            string response = "false";
            PNB_Global obj_PNB_Global = new PNB_Global();

            string pnb_thumb = System.Configuration.ConfigurationManager.AppSettings["pnb_thumb"];
            string AMRUT_thumb = System.Configuration.ConfigurationManager.AppSettings["AMRUT_thumb"];

            tbl_SNA_API_History obj_tbl_SNA_API_History = new tbl_SNA_API_History();

            #region Plain Request
            PNB_Assign_DP_Limit_Req obj_PNB_Assign_DP_Limit_Req = new PNB_Assign_DP_Limit_Req();
            obj_PNB_Assign_DP_Limit_Req.Account_Number = obj_DP_Limit.SNAAccountMaster_ACCT_NO;
            obj_PNB_Assign_DP_Limit_Req.DP_Limit = obj_DP_Limit.AssignedLimit;
            obj_PNB_Assign_DP_Limit_Req.Entity_Code = "AMRUT";
            obj_PNB_Assign_DP_Limit_Req.Unique_Id = DateTime.Now.Ticks.ToString();
            string PNB_GetAccountBal_Req_json = Newtonsoft.Json.JsonConvert.SerializeObject(obj_PNB_Assign_DP_Limit_Req);
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
            obj_tbl_SNA_API_History.SNA_API_History_API_Type = "Get_DP_Limit";
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

                PNB_Assign_DP_Limit_Res obj_PNB_Assign_DP_Limit_Res = new PNB_Assign_DP_Limit_Res();
                obj_PNB_Assign_DP_Limit_Res = Newtonsoft.Json.JsonConvert.DeserializeObject<PNB_Assign_DP_Limit_Res>(PNB_GetAccountBal_Res_json);
                #endregion

                obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_AddedBy = 1;
                if (obj_PNB_Assign_DP_Limit_Res.TranAmt == null)
                {
                    obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_DPLimit = 0;
                }
                else
                {
                    obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_DPLimit = obj_PNB_Assign_DP_Limit_Res.TranAmt;
                }
                obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_ErrorCode = obj_PNB_Assign_DP_Limit_Res.ErrorCode;
                obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_ErrorDesc = obj_PNB_Assign_DP_Limit_Res.ErrorDesc;
                obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_SNAAccountMaster_Id = obj_DP_Limit.SNAAccountMaster_Id;
                obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_Status = 1;
                obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_StatusPNB = obj_PNB_Assign_DP_Limit_Res.Status;
                obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_TranDate = obj_PNB_Assign_DP_Limit_Res.TranDate;
                obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_TranId = obj_PNB_Assign_DP_Limit_Res.TranId;
                obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_TranParticular = obj_PNB_Assign_DP_Limit_Res.TranParticular;
                obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_TranRmks = obj_PNB_Assign_DP_Limit_Res.TranRmks;
                obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_Unique_Id = obj_PNB_Assign_DP_Limit_Res.Unique_Id;

                if (new DataLayer().Update_SNA_Account_DP_Limit(obj_tbl_SNA_API_History, obj_tbl_SNAAccountDPLimit))
                {
                    response = "true";
                }
                else
                {
                    obj_tbl_SNAAccountDPLimit = null;
                    response = "false";
                }
            }
            else
            {
                if (new DataLayer().Update_SNA_Account_DP_Limit(obj_tbl_SNA_API_History, null))
                {
                    obj_tbl_SNAAccountDPLimit = null;
                    response = "false";
                }
                else
                {
                    obj_tbl_SNAAccountDPLimit = null;
                    response = "false";
                }
            }
            return obj_tbl_SNAAccountDPLimit;
        }
        private string get_tbl_PNBAssignDPLimit(int SNAAccountMaster_Id)
        {
            string response = "false";
            PNB_Global obj_PNB_Global = new PNB_Global();

            string pnb_thumb = System.Configuration.ConfigurationManager.AppSettings["pnb_thumb"];
            string AMRUT_thumb = System.Configuration.ConfigurationManager.AppSettings["AMRUT_thumb"];

            DataSet ds = new DataSet();
            ds = new DataLayer().get_tbl_SNAAccountMaster(SNAAccountMaster_Id);
            if (Utility.CheckDataSet(ds))
            {
                tbl_SNA_API_History obj_tbl_SNA_API_History = new tbl_SNA_API_History();

                #region Plain Request
                PNB_Assign_DP_Limit_Req obj_PNB_Assign_DP_Limit_Req = new PNB_Assign_DP_Limit_Req();
                obj_PNB_Assign_DP_Limit_Req.Account_Number = ds.Tables[0].Rows[0]["SNAAccountMaster_ACCT_NO"].ToString().Trim();
                //obj_PNB_Assign_DP_Limit_Req.Account_Number = ds.Tables[0].Rows[0]["SNAAccountMaster_ACCT_NO"].ToString().Trim();
                obj_PNB_Assign_DP_Limit_Req.DP_Limit = 13963303;
                obj_PNB_Assign_DP_Limit_Req.Entity_Code = "AMRUT";
                obj_PNB_Assign_DP_Limit_Req.Unique_Id = DateTime.Now.Ticks.ToString();
                string PNB_GetAccountBal_Req_json = Newtonsoft.Json.JsonConvert.SerializeObject(obj_PNB_Assign_DP_Limit_Req);
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
                obj_tbl_SNA_API_History.SNA_API_History_API_Type = "Get_DP_Limit";
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

                    PNB_Assign_DP_Limit_Res obj_PNB_Assign_DP_Limit_Res = new PNB_Assign_DP_Limit_Res();
                    obj_PNB_Assign_DP_Limit_Res = Newtonsoft.Json.JsonConvert.DeserializeObject<PNB_Assign_DP_Limit_Res>(PNB_GetAccountBal_Res_json);
                    #endregion

                    tbl_SNAAccountDPLimit obj_tbl_SNAAccountDPLimit = new tbl_SNAAccountDPLimit();
                    obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_AddedBy = 1;
                    if (obj_PNB_Assign_DP_Limit_Res.TranAmt == null)
                    {
                        obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_DPLimit = 0;
                    }
                    else
                    {
                        obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_DPLimit = obj_PNB_Assign_DP_Limit_Res.TranAmt;
                    }                    
                    obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_ErrorCode = obj_PNB_Assign_DP_Limit_Res.ErrorCode;
                    obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_ErrorDesc = obj_PNB_Assign_DP_Limit_Res.ErrorDesc;
                    obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_SNAAccountMaster_Id= SNAAccountMaster_Id;
                    obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_Status = 1;
                    obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_StatusPNB = obj_PNB_Assign_DP_Limit_Res.Status;
                    obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_TranDate = obj_PNB_Assign_DP_Limit_Res.TranDate;
                    obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_TranId = obj_PNB_Assign_DP_Limit_Res.TranId;
                    obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_TranParticular = obj_PNB_Assign_DP_Limit_Res.TranParticular;
                    obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_TranRmks = obj_PNB_Assign_DP_Limit_Res.TranRmks;
                    obj_tbl_SNAAccountDPLimit.SNAAccountDPLimit_Unique_Id = obj_PNB_Assign_DP_Limit_Res.Unique_Id;

                    if (new DataLayer().Update_SNA_Account_DP_Limit(obj_tbl_SNA_API_History, obj_tbl_SNAAccountDPLimit))
                    {
                        response = "true";
                    }
                    else
                    {
                        response = "false";
                    }
                }
                else
                {
                    if (new DataLayer().Update_SNA_Account_DP_Limit(obj_tbl_SNA_API_History, null))
                    {
                        response = "false";
                    }
                    else
                    {
                        response = "false";
                    }
                }
            }
            else
            {
                response = "false";
            }
            return response;
        }

        [HttpPost]
        public PNB_Global Get_Account_Balance(PNB_Global obj_PNB_Global, ref string StatusCode, ref string ReasonPhrase)
        {
            string responseString = string.Empty;
            PNB_Global obj_PNB_Global_R = new PNB_Global();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["API_Base_URL"] + "/Assign_DP_Limit");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(System.Configuration.ConfigurationManager.AppSettings["API_User"] +":"+ System.Configuration.ConfigurationManager.AppSettings["API_Pass"]);
                string val = System.Convert.ToBase64String(plainTextBytes);
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
                StringContent content = new StringContent(JsonConvert.SerializeObject(obj_PNB_Global));
                try
                {
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
                catch(Exception ee)
                {
                    obj_PNB_Global_R = null;
                }
            }
            return obj_PNB_Global_R;
        }
    }
}
