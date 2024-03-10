using SMS_External_API.App_Code;
using SMS_External_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SMS_External_API.Repos
{
    public class DistrictRepository : RepositoryAsyn
    {
        public DistrictRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_District>> get_District()
        {
            List<tbl_District> obj_tbl_District_Li = get_tbl_District();
            return obj_tbl_District_Li;
        }
        private List<tbl_District> get_tbl_District()
        {
            List<tbl_District> obj_tbl_District_Li = new List<tbl_District>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_District();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_District obj_tbl_District = new tbl_District();
                        
                        try
                        {
                            obj_tbl_District.District_Status = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Status"].ToString());
                        }
                        catch
                        {
                            obj_tbl_District.District_Status = 1;
                        }
                        obj_tbl_District.District_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                        try
                        {
                            obj_tbl_District.District_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_District.District_Id = 0;
                        }
                        obj_tbl_District_Li.Add(obj_tbl_District);
                    }
                }
                else
                {
                    obj_tbl_District_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_District_Li = null;
            }
            return obj_tbl_District_Li;
        }
    }
}
