using ePayment_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ePayment_API.Repos
{
    public class ProfileRepository : RepositoryAsyn
    {
        public ProfileRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<tbl_Person> get_Profile(int Person_Id)
        {
            tbl_Person obj_tbl_Person = get_tbl_Person(Person_Id);
            return obj_tbl_Person;
        }
        private tbl_Person get_tbl_Person(int Person_Id)
        {
            tbl_Person obj_tbl_Person = new tbl_Person();
            try
            {
                DataSet ds = new DataLayer().get_tbl_Profile(Person_Id);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj_tbl_Person.Person_Name = ds.Tables[0].Rows[0]["Person_Name"].ToString();
                    obj_tbl_Person.Person_Address = ds.Tables[0].Rows[0]["Person_AddressLine1"].ToString();
                    try
                    {
                        obj_tbl_Person.Person_Base_64 = Utility.Image_ToBase64(obj_tbl_Person.Base_URL + ds.Tables[0].Rows[0]["Person_ProfilePIC"].ToString());
                    }
                    catch
                    {

                    }
                    obj_tbl_Person.Person_Email = ds.Tables[0].Rows[0]["Person_EmailId"].ToString();
                    obj_tbl_Person.Person_Mobile = ds.Tables[0].Rows[0]["Person_Mobile1"].ToString();
                    obj_tbl_Person.Person_Mobile2 = ds.Tables[0].Rows[0]["Person_Mobile2"].ToString();
                    obj_tbl_Person.Zone_Name = ds.Tables[0].Rows[0]["Zone_Name"].ToString();
                    obj_tbl_Person.Circle_Name = ds.Tables[0].Rows[0]["Circle_Name"].ToString();
                    obj_tbl_Person.Division_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                    obj_tbl_Person.Person_Pic_File = ds.Tables[0].Rows[0]["Person_ProfilePIC"].ToString();
                }
            }
            catch (Exception)
            {

            }
            return obj_tbl_Person;
        }
    }
}
