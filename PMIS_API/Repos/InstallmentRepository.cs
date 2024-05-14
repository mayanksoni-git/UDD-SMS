using PMIS_API.App_Code;
using PMIS_API.Repos;
using PMIS_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PMIS_API.Repos
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
        public async Task<List<tbl_ProjectWorkGO>> get_Installment(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_ProjectWorkGO> obj_tbl_ProjectWorkGO_Li = get_tbl_ProjectWorkGO(obj_SearchCriteria);
            return obj_tbl_ProjectWorkGO_Li;
        }
        private List<tbl_ProjectWorkGO> get_tbl_ProjectWorkGO(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_ProjectWorkGO> obj_tbl_ProjectWorkGO_Li = new List<tbl_ProjectWorkGO>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_ProjectWorkGO(obj_SearchCriteria.ProjectWork_Id, obj_SearchCriteria.Scheme_Id);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_ProjectWorkGO obj_tbl_ProjectWorkGO = new tbl_ProjectWorkGO();
                        try
                        {
                            obj_tbl_ProjectWorkGO.ProjectWorkGO_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWorkGO_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkGO.ProjectWorkGO_Id = 0;
                        }
                        obj_tbl_ProjectWorkGO.ProjectWorkGO_GO_Date = ds.Tables[0].Rows[i]["ProjectWorkGO_GO_Date"].ToString();
                        obj_tbl_ProjectWorkGO.ProjectWorkGO_GO_Date = ds.Tables[0].Rows[i]["ProjectWorkGO_GO_Date"].ToString();
                        obj_tbl_ProjectWorkGO.ProjectWorkGO_GO_Number = ds.Tables[0].Rows[i]["ProjectWorkGO_GO_Number"].ToString();
                        try
                        {
                            obj_tbl_ProjectWorkGO.ProjectWorkGO_TotalRelease = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkGO_TotalRelease"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkGO.ProjectWorkGO_TotalRelease = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWorkGO.ProjectWorkGO_CentralShare = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkGO_CentralShare"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkGO.ProjectWorkGO_CentralShare = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWorkGO.ProjectWorkGO_StateShare = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkGO_StateShare"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkGO.ProjectWorkGO_StateShare = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWorkGO.ProjectWorkGO_ULBShare = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkGO_ULBShare"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkGO.ProjectWorkGO_ULBShare = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWorkGO.ProjectWorkGO_Centage = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProjectWorkGO_Centage"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkGO.ProjectWorkGO_Centage = 0;
                        }
                        try
                        {
                            obj_tbl_ProjectWorkGO.ProjectWorkGO_ULB_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWorkGO_ULB_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_ProjectWorkGO.ProjectWorkGO_ULB_Id = 0;
                        }
                        obj_tbl_ProjectWorkGO.ProjectWorkGO_Document_Path = ds.Tables[0].Rows[i]["ProjectWorkGO_Document_Path"].ToString();
                        obj_tbl_ProjectWorkGO.ProjectWorkGO_IssuingAuthority = ds.Tables[0].Rows[i]["ProjectWorkGO_IssuingAuthority"].ToString();
                        obj_tbl_ProjectWorkGO_Li.Add(obj_tbl_ProjectWorkGO);
                    }
                }
                else
                {
                    obj_tbl_ProjectWorkGO_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_ProjectWorkGO_Li = null;
            }
            return obj_tbl_ProjectWorkGO_Li;
        }
    }
}
