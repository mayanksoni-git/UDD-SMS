using ePayment_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ePayment_API.Repos
{
    public class DesignationRepository : RepositoryAsyn
    {
        public DesignationRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_Designation>> get_Designation()
        {
            List<tbl_Designation> obj_tbl_Designation_Li = get_tbl_Designation();
            return obj_tbl_Designation_Li;
        }
        private List<tbl_Designation> get_tbl_Designation()
        {
            List<tbl_Designation> obj_tbl_Designation_Li = new List<tbl_Designation>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_Designation();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_Designation obj_tbl_Designation = new tbl_Designation();
                        
                        obj_tbl_Designation.Designation_DesignationName = ds.Tables[0].Rows[i]["Designation_DesignationName"].ToString();
                        try
                        {
                            obj_tbl_Designation.Designation_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Designation_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_Designation.Designation_Id = 0;
                        }
                        try
                        {
                            obj_tbl_Designation.Designation_Status = Convert.ToInt32(ds.Tables[0].Rows[i]["Designation_Status"].ToString());
                        }
                        catch
                        {
                            obj_tbl_Designation.Designation_Status = 0;
                        }
                        try
                        {
                            obj_tbl_Designation.Designation_Level = Convert.ToInt32(ds.Tables[0].Rows[i]["Designation_Level"].ToString());
                        }
                        catch
                        {
                            obj_tbl_Designation.Designation_Level = 0;
                        }
                        obj_tbl_Designation_Li.Add(obj_tbl_Designation);
                    }
                }
                else
                {
                    obj_tbl_Designation_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_Designation_Li = null;
            }
            return obj_tbl_Designation_Li;
        }
    }
}
