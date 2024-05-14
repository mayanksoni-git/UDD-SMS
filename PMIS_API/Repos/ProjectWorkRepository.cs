using PMIS_API.App_Code;
using PMIS_API.Repos;
using PMIS_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PMIS_API.Repos
{
    public class ProjectWorkRepository : RepositoryAsyn
    {
        public ProjectWorkRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_ProjectWork>> get_ProjectWork(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_ProjectWork> obj_tbl_ProjectWork_Li = get_tbl_ProjectWork(obj_SearchCriteria);
            return obj_tbl_ProjectWork_Li;
        }
        private List<tbl_ProjectWork> get_tbl_ProjectWork(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_ProjectWork> obj_tbl_ProjectWork_Li = new List<tbl_ProjectWork>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_ProjectWork(obj_SearchCriteria);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_ProjectWork obj_tbl_ProjectWork = new tbl_ProjectWork();
                        try
                        {
                            obj_tbl_ProjectWork.ProjectWork_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWork_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWork.ProjectWork_Id = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWork.ProjectWork_Scheme_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWork_Project_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWork.ProjectWork_Scheme_Id = 0;
                        }
                        obj_tbl_ProjectWork.ProjectWork_Code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString();
                        obj_tbl_ProjectWork.ProjectWork_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString();
                        obj_tbl_ProjectWork.ProjectWork_GO_Date = ds.Tables[0].Rows[i]["ProjectWork_GO_Date"].ToString();
                        obj_tbl_ProjectWork.ProjectWork_GO_Number = ds.Tables[0].Rows[i]["ProjectWork_GO_No"].ToString();
                        obj_tbl_ProjectWork.ProjectWork_GO_Path = ds.Tables[0].Rows[i]["ProjectWork_GO_Path"].ToString();
                        obj_tbl_ProjectWork.ProjectWork_Agreement_Date = ds.Tables[0].Rows[i]["ProjectWorkPkg_Agreement_Date"].ToString();
                        obj_tbl_ProjectWork.ProjectWork_Completion_Date = ds.Tables[0].Rows[i]["ProjectWorkPkg_Due_Date"].ToString();
                        obj_tbl_ProjectWork.ProjectWork_Completion_Date_Extended = ds.Tables[0].Rows[i]["Target_Date_Agreement_Extended"].ToString();
                        try
                        {
                            obj_tbl_ProjectWork.ProjectWork_Sanctioned_Cost = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWork_Budget"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWork.ProjectWork_Sanctioned_Cost = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWork.ProjectWork_Tender_Cost = Convert.ToDecimal(ds.Tables[0].Rows[i]["tender_cost"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWork.ProjectWork_Tender_Cost = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWork.ProjectWork_Released_Amount = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total_Release"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWork.ProjectWork_Released_Amount = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWork.ProjectWork_Expenditure_Amount = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total_Expenditure"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWork.ProjectWork_Expenditure_Amount = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWork.ProjectWork_Physical_Progress_Per = Convert.ToDecimal(ds.Tables[0].Rows[i]["Physical_Progress"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWork.ProjectWork_Physical_Progress_Per = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWork.ProjectWork_Financial_Progress_Per = Convert.ToDecimal(ds.Tables[0].Rows[i]["Financial_Progress"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWork.ProjectWork_Financial_Progress_Per = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWork.ULB_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWork_DivisionId"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWork.ULB_Id = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWork.District_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_CircleId"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWork.District_Id = 0;
                        }

                        try
                        {
                            obj_tbl_ProjectWork.LokSabha_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["LokSabha_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWork.LokSabha_Id = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWork.VidhanSabha_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["VidhanSabha_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWork.VidhanSabha_Id = 0;
                        }
                        obj_tbl_ProjectWork.LokSabha_Name = ds.Tables[0].Rows[i]["LokSabha_Name"].ToString();
                        obj_tbl_ProjectWork.VidhanSabha_Name = ds.Tables[0].Rows[i]["VidhanSabha_Name"].ToString();
                        obj_tbl_ProjectWork.ULB_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString();
                        obj_tbl_ProjectWork.ProjectType_Name = ds.Tables[0].Rows[i]["ProjectType_Name"].ToString();
                        obj_tbl_ProjectWork.District_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();                        
                        obj_tbl_ProjectWork_Li.Add(obj_tbl_ProjectWork);
                    }
                }
                else
                {
                    obj_tbl_ProjectWork_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_ProjectWork_Li = null;
            }
            return obj_tbl_ProjectWork_Li;
        }
    }
}
