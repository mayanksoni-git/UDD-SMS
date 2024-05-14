using ePayment_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ePayment_API.Repos
{
    public class EngineeringWingRepository : RepositoryAsyn
    {
        public EngineeringWingRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_Person>> get_EngineeringWing(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_Person> obj_tbl_Person_Li = get_tbl_Person(obj_SearchCriteria);
            return obj_tbl_Person_Li;
        }
        private List<tbl_Person> get_tbl_Person(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_Person> obj_tbl_Person_Li = new List<tbl_Person>();
            try
            {
                DataSet ds = new DataLayer().get_EngineeringWing(obj_SearchCriteria.Person_Id, obj_SearchCriteria.Zone_Id, obj_SearchCriteria.Circle_Id, obj_SearchCriteria.Division_Id, obj_SearchCriteria.ProjectDPR_Id, obj_SearchCriteria.ProjectWork_Id);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_Person obj_tbl_Person = new tbl_Person();
                        try
                        {
                            obj_tbl_Person.Person_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Person_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_Person.Person_Id = 0;
                        }
                        obj_tbl_Person.UserType_Desc = ds.Tables[0].Rows[i]["UserType_Desc_E"].ToString();
                        obj_tbl_Person.Person_Name = ds.Tables[0].Rows[i]["Person_Name"].ToString();
                        obj_tbl_Person.Designation_Name = ds.Tables[0].Rows[i]["Designation_DesignationName"].ToString();
                        obj_tbl_Person.Person_Mobile = ds.Tables[0].Rows[i]["Person_Mobile1"].ToString();
                        obj_tbl_Person.Person_Mobile2 = ds.Tables[0].Rows[i]["Person_Mobile2"].ToString();
                        obj_tbl_Person_Li.Add(obj_tbl_Person);
                    }
                }
                else
                {
                    obj_tbl_Person_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_Person_Li = null;
            }
            return obj_tbl_Person_Li;
        }
    }
}
