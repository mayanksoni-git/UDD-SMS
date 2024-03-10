using SMS_External_API.App_Code;
using SMS_External_API.Repos;
using SMS_External_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SMS_External_API.Repos
{
    public class InstallmentRepository : RepositoryAsyn
    {
        public InstallmentRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_ProjectWorkInstallment>> get_Installment(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_ProjectWorkInstallment> obj_tbl_ProjectWorkInstallment_Li = get_tbl_ProjectWorkInstallment(obj_SearchCriteria);
            return obj_tbl_ProjectWorkInstallment_Li;
        }
        private List<tbl_ProjectWorkInstallment> get_tbl_ProjectWorkInstallment(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_ProjectWorkInstallment> obj_tbl_ProjectWorkInstallment_Li = new List<tbl_ProjectWorkInstallment>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_ProjectWorkInstallment(obj_SearchCriteria.ProjectWork_Id, obj_SearchCriteria.Scheme_Id);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_ProjectWorkInstallment obj_tbl_ProjectWorkInstallment = new tbl_ProjectWorkInstallment();
                        try
                        {
                            obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWorkInstallment_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_Id = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_Work_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWorkInstallment_Work_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_Work_Id = 0;
                        }
                        obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_GO_Date = ds.Tables[0].Rows[i]["ProjectWorkInstallment_GO_Date"].ToString();
                        obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_GO_Date = ds.Tables[0].Rows[i]["ProjectWorkInstallment_GO_Date"].ToString();
                        obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_GO_Number = ds.Tables[0].Rows[i]["ProjectWorkInstallment_GO_Number"].ToString();
                        try
                        {
                            obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_TotalRelease = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkInstallment_TotalRelease"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_TotalRelease = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_CentralShare = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkInstallment_CentralShare"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_CentralShare = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_StateShare = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkInstallment_StateShare"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_StateShare = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_ULBShare = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkInstallment_ULBShare"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_ULBShare = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_Centage = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkInstallment_Centage"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_Centage = 0;
                        }
                        obj_tbl_ProjectWorkInstallment.ProjectWorkInstallment_Document_Path = ds.Tables[0].Rows[i]["ProjectWorkInstallment_Document_Path"].ToString();
                        obj_tbl_ProjectWorkInstallment_Li.Add(obj_tbl_ProjectWorkInstallment);
                    }
                }
                else
                {
                    obj_tbl_ProjectWorkInstallment_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_ProjectWorkInstallment_Li = null;
            }
            return obj_tbl_ProjectWorkInstallment_Li;
        }
    }
}
