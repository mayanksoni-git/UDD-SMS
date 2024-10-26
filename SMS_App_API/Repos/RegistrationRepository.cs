using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using ePayment_API.Models;

namespace ePayment_API.Repos
{
    public class RegistrationRepository : RepositoryAsyn
    {
        public RegistrationRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<tbl_Person> User_Registration(tbl_Person obj_tbl_Person)
        {
            obj_tbl_Person = Registration(obj_tbl_Person);
            return obj_tbl_Person;
        }
        private tbl_Person Registration(tbl_Person obj_tbl_Person)
        {
            string Mode = "OTP";
            List<SMS_Objects> obj_SMS_Objects_Li = new List<SMS_Objects>();
            Random rnd = new Random();
            string OTP = rnd.Next(1121, 9979).ToString();
            DataSet ds = new DataSet();
            try
            {
                if (obj_tbl_Person.Person_UserName != null && obj_tbl_Person.Person_UserName != "" && obj_tbl_Person.Person_Password != null && obj_tbl_Person.Person_Password != "")
                {//User Name / Password Mode
                    ds = new DataLayer().getLoginDetails(obj_tbl_Person.Person_UserName, obj_tbl_Person.Person_Password);
                    Mode = "Password";
                }
                else
                {
                    ds = new DataLayer().getLoginDetails(obj_tbl_Person.Person_Mobile);
                    Mode = "OTP";
                }
                if (Utility.CheckDataSet(ds))
                {
                    SMS_Objects obj_SMS_Objects = new SMS_Objects();
                    obj_SMS_Objects.MobileNum = obj_tbl_Person.Person_Mobile;

                    obj_SMS_Objects.Sid = "MMIVAR";
                    obj_SMS_Objects.Template_Id = "1707163112190617035";
                    obj_SMS_Objects.SMS_Content = "Dear " + ds.Tables[0].Rows[0]["Person_Name"].ToString() + " OTP for Login To iCRM Web Portal is " + OTP + ".Please Use This OTP for Login. Thankyou Mivar";

                    obj_SMS_Objects_Li.Add(obj_SMS_Objects);
                    obj_tbl_Person.Base_URL = "https://www.jnupepayment.in/";
                    
                    obj_tbl_Person.FinancialYear_Id = 3;
                    obj_tbl_Person.OTP = OTP;
                    obj_tbl_Person.ULB_Name = "";
                    obj_tbl_Person.District_Name = "";
                    obj_tbl_Person.Person_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Person_Id"].ToString());
                    obj_tbl_Person.response = "success";
                    obj_tbl_Person.Role_ULB = 3;
                    obj_tbl_Person.Enable_Financial_Graph_On_Dash = 1;
                    obj_tbl_Person.Enable_Physical_Graph_On_Dash = 1;
                    obj_tbl_Person.Enable_Financial_Graph_On_L1 = 1;
                    obj_tbl_Person.Enable_Physical_Graph_On_L1 = 1;
                    obj_tbl_Person.Enable_Physical_Graph_On_Site_Progress = 1;
                    obj_tbl_Person.Enable_Financial_Graph_On_Site_Progress = 1;
                    obj_tbl_Person.Error_Message = "";
                    try
                    {
                        obj_tbl_Person.District_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Districts_DistID"].ToString());
                    }
                    catch
                    {
                        obj_tbl_Person.District_Id = 0;
                    }
                    try
                    {
                        obj_tbl_Person.ULB_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ULBID"].ToString());
                    }
                    catch
                    {
                        obj_tbl_Person.ULB_Id = 0;
                    }
                    try
                    {
                        obj_tbl_Person.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Zone_Id"].ToString());
                    }
                    catch
                    {
                        obj_tbl_Person.Zone_Id = 0;
                    }
                    try
                    {
                        obj_tbl_Person.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Circle_Id"].ToString());
                    }
                    catch
                    {
                        obj_tbl_Person.Circle_Id = 0;
                    }
                    try
                    {
                        obj_tbl_Person.Division_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Division_Id"].ToString());
                    }
                    catch
                    {
                        obj_tbl_Person.Division_Id = 0;
                    }
                    //obj_tbl_Person.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Circle_Id"].ToString());
                    obj_tbl_Person.Zone_Name = ds.Tables[0].Rows[0]["Zone_Name"].ToString();
                    obj_tbl_Person.Circle_Name = ds.Tables[0].Rows[0]["Circle_Name"].ToString();
                    obj_tbl_Person.Division_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                    obj_tbl_Person.Designation_Name = ds.Tables[0].Rows[0]["Designation_DesignationName"].ToString();
                    obj_tbl_Person.Department_Name = ds.Tables[0].Rows[0]["Department_Name"].ToString();
                    obj_tbl_Person.Person_FName = ds.Tables[0].Rows[0]["Person_FName"].ToString();
                    obj_tbl_Person.Person_Name = ds.Tables[0].Rows[0]["Person_Name"].ToString();
                    obj_tbl_Person.Person_Mobile = ds.Tables[0].Rows[0]["Person_Mobile1"].ToString();
                    obj_tbl_Person.Person_Mobile2 = ds.Tables[0].Rows[0]["Person_Mobile2"].ToString();
                    if (Mode == "OTP")
                    {
                        Utility.SendSMS(obj_SMS_Objects_Li);
                    }
                }
                else
                {
                    obj_tbl_Person.Person_Id = 0;
                    obj_tbl_Person.response = "Mobile No is Not Registred. Please Contact Administrator";
                    obj_tbl_Person.Error_Message = "Mobile No is Not Registred. Please Contact Administrator";
                    obj_tbl_Person.OTP = "";
                }
            }
            catch (Exception ex)
            {
                obj_tbl_Person.Person_Id = 0;
                obj_tbl_Person.response = "Mobile No is Not Registred. Please Contact Administrator";
                obj_tbl_Person.Error_Message = "Mobile No is Not Registred. Please Contact Administrator";
                obj_tbl_Person.OTP = "";
            }
            return obj_tbl_Person;
        }
    }
}
