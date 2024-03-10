using SMS_External_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using SMS_External_API.App_Code;
using System.Threading.Tasks;

namespace SMS_External_API.Repos
{
    public class ULBRepository : RepositoryAsyn
    {
        public ULBRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_ULB>> get_ULB(int District_Id)
        {
            List<tbl_ULB> obj_tbl_ULB_Li = get_tbl_ULB(District_Id);
            return obj_tbl_ULB_Li;
        }
        private List<tbl_ULB> get_tbl_ULB(int District_Id)
        {
            List<tbl_ULB> obj_tbl_ULB_Li = new List<tbl_ULB>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_ULB(District_Id);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_ULB obj_tbl_ULB = new tbl_ULB();
                        
                        try
                        {
                            obj_tbl_ULB.ULB_Status = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_Status"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ULB.ULB_Status = 1;
                        }
                        obj_tbl_ULB.District_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                        obj_tbl_ULB.ULB_Type = ds.Tables[0].Rows[i]["Division_Type"].ToString();
                        obj_tbl_ULB.ULB_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString();
                        try
                        {
                            obj_tbl_ULB.ULB_DistrictId = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_CircleId"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ULB.ULB_DistrictId = 0;
                        }
                        try
                        {
                            obj_tbl_ULB.ULB_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ULB.ULB_Id = 0;
                        }
                        obj_tbl_ULB_Li.Add(obj_tbl_ULB);
                    }
                }
                else
                {
                    obj_tbl_ULB_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_ULB_Li = null;
            }
            return obj_tbl_ULB_Li;
        }
    }
}
