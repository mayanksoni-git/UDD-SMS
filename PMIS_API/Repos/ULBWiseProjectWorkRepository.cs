using PMIS_API.App_Code;
using PMIS_API.Repos;
using PMIS_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PMIS_API.Repos
{
    public class ULBWiseProjectWorkRepository : RepositoryAsyn
    {
        public ULBWiseProjectWorkRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_ULBWiseProjectWork>> get_ULBWiseProjectWork(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_ULBWiseProjectWork> obj_tbl_ULBWiseProjectWork_Li = get_tbl_ULBWiseProjectWork(obj_SearchCriteria);
            return obj_tbl_ULBWiseProjectWork_Li;
        }
        private List<tbl_ULBWiseProjectWork> get_tbl_ULBWiseProjectWork(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_ULBWiseProjectWork> obj_tbl_ULBWiseProjectWork_Li = new List<tbl_ULBWiseProjectWork>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_ProjectWork_ULB_Wise(obj_SearchCriteria);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_ULBWiseProjectWork obj_tbl_ULBWiseProjectWork = new tbl_ULBWiseProjectWork();
                        try
                        {
                            obj_tbl_ULBWiseProjectWork.District_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_CircleId"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ULBWiseProjectWork.District_Id = 0;
                        }
                        obj_tbl_ULBWiseProjectWork.District_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                        try
                        {
                            obj_tbl_ULBWiseProjectWork.ULB_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWork_DivisionId"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ULBWiseProjectWork.ULB_Id = 0;
                        }
                        obj_tbl_ULBWiseProjectWork.ULB_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString();
                        try
                        {
                            obj_tbl_ULBWiseProjectWork.Total_Projects_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Projects_Count"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ULBWiseProjectWork.Total_Projects_Count = 0;
                        }
                        try
                        {
                            obj_tbl_ULBWiseProjectWork.Sanctioned_Cost = Convert.ToDecimal(ds.Tables[0].Rows[i]["Sanctioned_Cost"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ULBWiseProjectWork.Sanctioned_Cost = 0;
                        }
                        try
                        {
                            obj_tbl_ULBWiseProjectWork.Tender_Cost = Convert.ToDecimal(ds.Tables[0].Rows[i]["Tender_Cost"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ULBWiseProjectWork.Tender_Cost = 0;
                        }
                        try
                        {
                            obj_tbl_ULBWiseProjectWork.Total_Release = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total_Release"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ULBWiseProjectWork.Total_Release = 0;
                        }
                        try
                        {
                            obj_tbl_ULBWiseProjectWork.Total_Expenditure = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total_Expenditure"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ULBWiseProjectWork.Total_Expenditure = 0;
                        }
                        try
                        {
                            obj_tbl_ULBWiseProjectWork.Completed_Projects_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Completed_Projects_Count"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ULBWiseProjectWork.Completed_Projects_Count = 0;
                        }
                        try
                        {
                            obj_tbl_ULBWiseProjectWork.Ongoing_Projects_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Ongoing_Projects_Count"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ULBWiseProjectWork.Ongoing_Projects_Count = 0;
                        }
                        try
                        {
                            obj_tbl_ULBWiseProjectWork.Completed_Projects = Convert.ToDecimal(ds.Tables[0].Rows[i]["Completed_Projects"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ULBWiseProjectWork.Completed_Projects = 0;
                        }
                        try
                        {
                            obj_tbl_ULBWiseProjectWork.Ongoing_Projects = Convert.ToDecimal(ds.Tables[0].Rows[i]["Ongoing_Projects"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ULBWiseProjectWork.Ongoing_Projects = 0;
                        }
                        obj_tbl_ULBWiseProjectWork_Li.Add(obj_tbl_ULBWiseProjectWork);
                    }
                }
                else
                {
                    obj_tbl_ULBWiseProjectWork_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_ULBWiseProjectWork_Li = null;
            }
            return obj_tbl_ULBWiseProjectWork_Li;
        }
    }
}
