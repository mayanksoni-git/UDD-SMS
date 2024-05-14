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
    public class PNBGetPPATransactionDetailsRepository : RepositoryAsyn
    {
        public PNBGetPPATransactionDetailsRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<PNB_GetPPATransactionDetails_Res>> get_PNBGetPPATransactionDetails(PPA_Details obj_PPA_Details)
        {
            List<PNB_GetPPATransactionDetails_Res> obj_PNB_GetPPATransactionDetails_Res_Li = get_tbl_PNBGetPPATransactionDetails(obj_PPA_Details);
            return obj_PNB_GetPPATransactionDetails_Res_Li;
        }

        private List<PNB_GetPPATransactionDetails_Res> get_tbl_PNBGetPPATransactionDetails(PPA_Details obj_PPA_Details)
        {
            tbl_SNAPPATransactionDetails obj_tbl_SNAPPATransactionDetails = new tbl_SNAPPATransactionDetails();
            string response = "false";
            PNB_Global obj_PNB_Global = new PNB_Global();

            string pnb_thumb = System.Configuration.ConfigurationManager.AppSettings["pnb_thumb"];
            string AMRUT_thumb = System.Configuration.ConfigurationManager.AppSettings["AMRUT_thumb"];

            tbl_SNA_API_History obj_tbl_SNA_API_History = new tbl_SNA_API_History();

            #region Plain Request
            PNB_GetPPATransactionDetails_Req obj_PNB_GetPPATransactionDetails_Req = new PNB_GetPPATransactionDetails_Req();
            obj_PNB_GetPPATransactionDetails_Req.PPA_Number = obj_PPA_Details.PPA_Number; //ds.Tables[0].Rows[0]["SNAAccountMaster_ACCT_NO"].ToString().Trim();
            obj_PNB_GetPPATransactionDetails_Req.Entity_Code = "AMRUT";
            obj_PNB_GetPPATransactionDetails_Req.Req_Type = obj_PPA_Details.Req_Type;
            obj_PNB_GetPPATransactionDetails_Req.UploadDate = obj_PPA_Details.PPA_Date;
            obj_PNB_GetPPATransactionDetails_Req.Unique_Id = DateTime.Now.Ticks.ToString();
            string PNB_GetAccountBal_Req_json = Newtonsoft.Json.JsonConvert.SerializeObject(obj_PNB_GetPPATransactionDetails_Req);
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
            obj_tbl_SNA_API_History.SNA_API_History_API_Type = "Get_PPA_Transaction_Level_Details";
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


                List<PNB_GetPPATransactionDetails_Res> obj_PNB_GetPPATransactionDetails_Res_Li = new List<PNB_GetPPATransactionDetails_Res>();
                obj_PNB_GetPPATransactionDetails_Res_Li = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PNB_GetPPATransactionDetails_Res>>(PNB_GetAccountBal_Res_json);

                return obj_PNB_GetPPATransactionDetails_Res_Li;
                #endregion
            }
            else
            {
                return null;
            }
        }

        private string get_tbl_PNBGetPPATransactionDetails(int SNAAccountMaster_Id)
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
                PNB_GetPPATransactionDetails_Req obj_PNB_GetPPATransactionDetails_Req = new PNB_GetPPATransactionDetails_Req();
                obj_PNB_GetPPATransactionDetails_Req.PPA_Number = "C011924379312"; //ds.Tables[0].Rows[0]["SNAAccountMaster_ACCT_NO"].ToString().Trim();
                obj_PNB_GetPPATransactionDetails_Req.Entity_Code = "AMRUT";
                obj_PNB_GetPPATransactionDetails_Req.Unique_Id = DateTime.Now.Ticks.ToString();
                string PNB_GetAccountBal_Req_json = Newtonsoft.Json.JsonConvert.SerializeObject(obj_PNB_GetPPATransactionDetails_Req);
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
                obj_tbl_SNA_API_History.SNA_API_History_API_Type = "Get_PPA_Transaction_Level_Details";
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


                    PNB_GetPPATransactionDetails_Res obj_PNB_GetPPATransactionDetails_Res = new PNB_GetPPATransactionDetails_Res();
                    obj_PNB_GetPPATransactionDetails_Res = Newtonsoft.Json.JsonConvert.DeserializeObject<PNB_GetPPATransactionDetails_Res>(PNB_GetAccountBal_Res_json);
                    #endregion

                    tbl_SNAPPATransactionDetails obj_tbl_SNAPPATransactionDetails = new tbl_SNAPPATransactionDetails();
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_AddedBy = 1;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Invoice_Id = SNAAccountMaster_Id;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Transaction_Amount = obj_PNB_GetPPATransactionDetails_Res.Transaction_Amount;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Agency_Account_Name = obj_PNB_GetPPATransactionDetails_Res.Agency_Account_Name;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Error_Desc = obj_PNB_GetPPATransactionDetails_Res.Error_Desc;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Agency_Account_Number = obj_PNB_GetPPATransactionDetails_Res.Agency_Account_Number;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Status = 1;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Beneficiary_Account_Number = obj_PNB_GetPPATransactionDetails_Res.Beneficiary_Account_Number;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Beneficiary_IFSC_Code = obj_PNB_GetPPATransactionDetails_Res.Beneficiary_IFSC_Code;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Beneficiary_Name = obj_PNB_GetPPATransactionDetails_Res.Beneficiary_Name;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Payment_Date = obj_PNB_GetPPATransactionDetails_Res.Payment_Date;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Payment_Product = obj_PNB_GetPPATransactionDetails_Res.Payment_Product;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Payment_Status = obj_PNB_GetPPATransactionDetails_Res.Payment_Status;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_PFMS_Batch_Number = obj_PNB_GetPPATransactionDetails_Res.PFMS_Batch_Number;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_PFMS_Tran_ID = obj_PNB_GetPPATransactionDetails_Res.PFMS_Tran_ID;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Response_Status = obj_PNB_GetPPATransactionDetails_Res.Response_Status;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Return_Reason = obj_PNB_GetPPATransactionDetails_Res.Return_Reason;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Transaction_Amount = obj_PNB_GetPPATransactionDetails_Res.Transaction_Amount;
                    obj_tbl_SNAPPATransactionDetails.SNAPPATransactionDetails_Unique_Id = obj_PNB_GetPPATransactionDetails_Res.Unique_Id;

                    if (new DataLayer().Update_SNA_PPA_Details(obj_tbl_SNA_API_History, null))
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
                    if (new DataLayer().Update_SNA_PPA_Details(obj_tbl_SNA_API_History, null))
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
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["API_Base_URL"] + "/Get_PPA_Transaction_Level_Details");
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
