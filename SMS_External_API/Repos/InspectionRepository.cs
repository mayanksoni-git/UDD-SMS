using SMS_External_API.App_Code;
using SMS_External_API.Repos;
using SMS_External_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SMS_External_API.Repos
{
    public class InspectionRepository : RepositoryAsyn
    {
        public InspectionRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_ProjectWorkInspection>> get_Inspection(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_ProjectWorkInspection> obj_tbl_ProjectWorkInspection_Li = get_tbl_ProjectWorkInspection(obj_SearchCriteria);
            return obj_tbl_ProjectWorkInspection_Li;
        }
        private List<tbl_ProjectWorkInspection> get_tbl_ProjectWorkInspection(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_ProjectWorkInspection> obj_tbl_ProjectWorkInspection_Li = new List<tbl_ProjectWorkInspection>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_ProjectWorkInspection(obj_SearchCriteria.ProjectWork_Id);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_ProjectWorkInspection obj_tbl_ProjectWorkInspection = new tbl_ProjectWorkInspection();
                        try
                        {
                            obj_tbl_ProjectWorkInspection.ProjectWorkInspection_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWorkInspection_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkInspection.ProjectWorkInspection_Id = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWorkInspection.ProjectWorkInspection_Work_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWorkInspection_Work_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkInspection.ProjectWorkInspection_Work_Id = 0;
                        }
                        obj_tbl_ProjectWorkInspection.ProjectWorkInspection_Date = ds.Tables[0].Rows[i]["ProjectWorkInspection_Date"].ToString();
                        obj_tbl_ProjectWorkInspection.ProjectWorkInspection_InspectionBy = ds.Tables[0].Rows[i]["ProjectWorkInspection_InspectionBy"].ToString();
                        obj_tbl_ProjectWorkInspection.ProjectWorkInspection_Comments = ds.Tables[0].Rows[i]["ProjectWorkInspection_Comments"].ToString();
                        try
                        {
                            obj_tbl_ProjectWorkInspection.ProjectWorkInspection_Latitude = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkInspection_Latitude"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkInspection.ProjectWorkInspection_Latitude = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWorkInspection.ProjectWorkInspection_Longitude = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkInspection_Longitude"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkInspection.ProjectWorkInspection_Longitude = 0;
                        }
                        
                        obj_tbl_ProjectWorkInspection.ProjectWorkInspection_Photo_Path1 = ds.Tables[0].Rows[i]["ProjectWorkInspection_Photo_Path1"].ToString();
                        obj_tbl_ProjectWorkInspection.ProjectWorkInspection_Photo_Path2 = ds.Tables[0].Rows[i]["ProjectWorkInspection_Photo_Path2"].ToString();
                        obj_tbl_ProjectWorkInspection.ProjectWorkInspection_Photo_Path3 = ds.Tables[0].Rows[i]["ProjectWorkInspection_Photo_Path3"].ToString();
                        obj_tbl_ProjectWorkInspection.ProjectWorkInspection_Photo_Path4 = ds.Tables[0].Rows[i]["ProjectWorkInspection_Photo_Path4"].ToString();
                        obj_tbl_ProjectWorkInspection_Li.Add(obj_tbl_ProjectWorkInspection);
                    }
                }
                else
                {
                    obj_tbl_ProjectWorkInspection_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_ProjectWorkInspection_Li = null;
            }
            return obj_tbl_ProjectWorkInspection_Li;
        }
    }
}
