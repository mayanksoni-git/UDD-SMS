using PMIS_API.App_Code;
using PMIS_API.Repos;
using PMIS_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PMIS_API.Repos
{
    public class DistrictWiseProjectWorkRepository : RepositoryAsyn
    {
        public DistrictWiseProjectWorkRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_DistrictWiseProjectWork>> get_DistrictWiseProjectWork(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_DistrictWiseProjectWork> obj_tbl_DistrictWiseProjectWork_Li = get_tbl_DistrictWiseProjectWork(obj_SearchCriteria);
            return obj_tbl_DistrictWiseProjectWork_Li;
        }
        private List<tbl_DistrictWiseProjectWork> get_tbl_DistrictWiseProjectWork(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_DistrictWiseProjectWork> obj_tbl_DistrictWiseProjectWork_Li = new List<tbl_DistrictWiseProjectWork>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_ProjectWork_District_Wise(obj_SearchCriteria);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_DistrictWiseProjectWork obj_tbl_DistrictWiseProjectWork = new tbl_DistrictWiseProjectWork();
                        try
                        {
                            obj_tbl_DistrictWiseProjectWork.District_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_CircleId"].ToString());
                        }
                        catch
                        {
                            obj_tbl_DistrictWiseProjectWork.District_Id = 0;
                        }
                        obj_tbl_DistrictWiseProjectWork.District_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                        try
                        {
                            obj_tbl_DistrictWiseProjectWork.Total_Projects_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Projects_Count"].ToString());
                        }
                        catch
                        {
                            obj_tbl_DistrictWiseProjectWork.Total_Projects_Count = 0;
                        }
                        try
                        {
                            obj_tbl_DistrictWiseProjectWork.Sanctioned_Cost = Convert.ToDecimal(ds.Tables[0].Rows[i]["Sanctioned_Cost"].ToString());
                        }
                        catch
                        {
                            obj_tbl_DistrictWiseProjectWork.Sanctioned_Cost = 0;
                        }
                        try
                        {
                            obj_tbl_DistrictWiseProjectWork.Tender_Cost = Convert.ToDecimal(ds.Tables[0].Rows[i]["Tender_Cost"].ToString());
                        }
                        catch
                        {
                            obj_tbl_DistrictWiseProjectWork.Tender_Cost = 0;
                        }
                        try
                        {
                            obj_tbl_DistrictWiseProjectWork.Total_Release = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total_Release"].ToString());
                        }
                        catch
                        {
                            obj_tbl_DistrictWiseProjectWork.Total_Release = 0;
                        }
                        try
                        {
                            obj_tbl_DistrictWiseProjectWork.Total_Expenditure = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total_Expenditure"].ToString());
                        }
                        catch
                        {
                            obj_tbl_DistrictWiseProjectWork.Total_Expenditure = 0;
                        }
                        try
                        {
                            obj_tbl_DistrictWiseProjectWork.Completed_Projects_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Completed_Projects_Count"].ToString());
                        }
                        catch
                        {
                            obj_tbl_DistrictWiseProjectWork.Completed_Projects_Count = 0;
                        }
                        try
                        {
                            obj_tbl_DistrictWiseProjectWork.Ongoing_Projects_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Ongoing_Projects_Count"].ToString());
                        }
                        catch
                        {
                            obj_tbl_DistrictWiseProjectWork.Ongoing_Projects_Count = 0;
                        }
                        try
                        {
                            obj_tbl_DistrictWiseProjectWork.Completed_Projects = Convert.ToDecimal(ds.Tables[0].Rows[i]["Completed_Projects"].ToString());
                        }
                        catch
                        {
                            obj_tbl_DistrictWiseProjectWork.Completed_Projects = 0;
                        }
                        try
                        {
                            obj_tbl_DistrictWiseProjectWork.Ongoing_Projects = Convert.ToDecimal(ds.Tables[0].Rows[i]["Ongoing_Projects"].ToString());
                        }
                        catch
                        {
                            obj_tbl_DistrictWiseProjectWork.Ongoing_Projects = 0;
                        }
                        obj_tbl_DistrictWiseProjectWork_Li.Add(obj_tbl_DistrictWiseProjectWork);
                    }
                }
                else
                {
                    obj_tbl_DistrictWiseProjectWork_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_DistrictWiseProjectWork_Li = null;
            }
            return obj_tbl_DistrictWiseProjectWork_Li;
        }
    }
}
