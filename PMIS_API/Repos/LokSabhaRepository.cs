using PMIS_API.App_Code;
using PMIS_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PMIS_API.Repos
{
    public class LokSabhaRepository : RepositoryAsyn
    {
        public LokSabhaRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_LokSabha>> get_LokSabha()
        {
            List<tbl_LokSabha> obj_tbl_LokSabha_Li = get_tbl_LokSabha();
            return obj_tbl_LokSabha_Li;
        }
        private List<tbl_LokSabha> get_tbl_LokSabha()
        {
            List<tbl_LokSabha> obj_tbl_LokSabha_Li = new List<tbl_LokSabha>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_LokSabha();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_LokSabha obj_tbl_LokSabha = new tbl_LokSabha();

                        obj_tbl_LokSabha.LokSabha_Status = 1;
                        obj_tbl_LokSabha.LokSabha_Name = ds.Tables[0].Rows[i]["LokSabha_Name"].ToString();
                        obj_tbl_LokSabha.District_Name = ds.Tables[0].Rows[i]["District_Name"].ToString();
                        try
                        {
                            obj_tbl_LokSabha.LokSabha_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["LokSabha_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_LokSabha.LokSabha_Id = 0;
                        }
                        try
                        {
                            obj_tbl_LokSabha.District_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["LokSabha_DistrictId"].ToString());
                        }
                        catch
                        {
                            obj_tbl_LokSabha.District_Id = 0;
                        }
                        obj_tbl_LokSabha_Li.Add(obj_tbl_LokSabha);
                    }
                }
                else
                {
                    obj_tbl_LokSabha_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_LokSabha_Li = null;
            }
            return obj_tbl_LokSabha_Li;
        }
    }
}
