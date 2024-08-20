using PMIS_API.App_Code;
using PMIS_API.Repos;
using PMIS_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PMIS_API.Repos
{
    public class SchemeWiseProjectWorkRepository : RepositoryAsyn
    {
        public SchemeWiseProjectWorkRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_SchemeWiseProjectWork>> get_SchemeWiseProjectWork(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_SchemeWiseProjectWork> obj_tbl_SchemeWiseProjectWork_Li = get_tbl_SchemeWiseProjectWork(obj_SearchCriteria);
            return obj_tbl_SchemeWiseProjectWork_Li;
        }
        private List<tbl_SchemeWiseProjectWork> get_tbl_SchemeWiseProjectWork(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_SchemeWiseProjectWork> obj_tbl_SchemeWiseProjectWork_Li = new List<tbl_SchemeWiseProjectWork>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_ProjectWork_Scheme_Wise(obj_SearchCriteria);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_SchemeWiseProjectWork obj_tbl_SchemeWiseProjectWork = new tbl_SchemeWiseProjectWork();
                        try
                        {
                            obj_tbl_SchemeWiseProjectWork.Scheme_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Project_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SchemeWiseProjectWork.Scheme_Id = 0;
                        }
                        obj_tbl_SchemeWiseProjectWork.Scheme_Name = ds.Tables[0].Rows[i]["Project_Name"].ToString();
                        try
                        {
                            obj_tbl_SchemeWiseProjectWork.Total_Projects_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Projects_Count"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SchemeWiseProjectWork.Total_Projects_Count = 0;
                        }
                        try
                        {
                            obj_tbl_SchemeWiseProjectWork.Sanctioned_Cost = Convert.ToDecimal(ds.Tables[0].Rows[i]["Sanctioned_Cost"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SchemeWiseProjectWork.Sanctioned_Cost = 0;
                        }
                        try
                        {
                            obj_tbl_SchemeWiseProjectWork.Tender_Cost = Convert.ToDecimal(ds.Tables[0].Rows[i]["Tender_Cost"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SchemeWiseProjectWork.Tender_Cost = 0;
                        }
                        try
                        {
                            obj_tbl_SchemeWiseProjectWork.Total_Release = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total_Release"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SchemeWiseProjectWork.Total_Release = 0;
                        }
                        try
                        {
                            obj_tbl_SchemeWiseProjectWork.Total_Expenditure = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total_Expenditure"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SchemeWiseProjectWork.Total_Expenditure = 0;
                        }
                        try
                        {
                            obj_tbl_SchemeWiseProjectWork.Completed_Projects_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Completed_Projects_Count"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SchemeWiseProjectWork.Completed_Projects_Count = 0;
                        }
                        try
                        {
                            obj_tbl_SchemeWiseProjectWork.Ongoing_Projects_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Ongoing_Projects_Count"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SchemeWiseProjectWork.Ongoing_Projects_Count = 0;
                        }
                        try
                        {
                            obj_tbl_SchemeWiseProjectWork.Completed_Projects = Convert.ToDecimal(ds.Tables[0].Rows[i]["Completed_Projects"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SchemeWiseProjectWork.Completed_Projects = 0;
                        }
                        try
                        {
                            obj_tbl_SchemeWiseProjectWork.Ongoing_Projects = Convert.ToDecimal(ds.Tables[0].Rows[i]["Ongoing_Projects"].ToString());
                        }
                        catch
                        {
                            obj_tbl_SchemeWiseProjectWork.Ongoing_Projects = 0;
                        }
                        obj_tbl_SchemeWiseProjectWork_Li.Add(obj_tbl_SchemeWiseProjectWork);
                    }
                }
                else
                {
                    obj_tbl_SchemeWiseProjectWork_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_SchemeWiseProjectWork_Li = null;
            }
            return obj_tbl_SchemeWiseProjectWork_Li;
        }
    }
}
