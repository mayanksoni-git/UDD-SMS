using PMIS_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using PMIS_API.App_Code;
using System.Threading.Tasks;

namespace PMIS_API.Repos
{
    public class VidhanSabhaRepository : RepositoryAsyn
    {
        public VidhanSabhaRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_VidhanSabha>> get_VidhanSabha(int LokSabha_Id)
        {
            List<tbl_VidhanSabha> obj_tbl_VidhanSabha_Li = get_tbl_VidhanSabha(LokSabha_Id);
            return obj_tbl_VidhanSabha_Li;
        }
        private List<tbl_VidhanSabha> get_tbl_VidhanSabha(int LokSabha_Id)
        {
            List<tbl_VidhanSabha> obj_tbl_VidhanSabha_Li = new List<tbl_VidhanSabha>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_VidhanSabha(LokSabha_Id);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_VidhanSabha obj_tbl_VidhanSabha = new tbl_VidhanSabha();

                        obj_tbl_VidhanSabha.VidhanSabha_Status = 1;
                        obj_tbl_VidhanSabha.LokSabha_Name = ds.Tables[0].Rows[i]["LokSabha_Name"].ToString();
                        obj_tbl_VidhanSabha.VidhanSabha_Name = ds.Tables[0].Rows[i]["VidhanSabha_Name"].ToString();
                        try
                        {
                            obj_tbl_VidhanSabha.VidhanSabha_LokSabhaId = Convert.ToInt32(ds.Tables[0].Rows[i]["VidhanSabha_LokSabhaId"].ToString());
                        }
                        catch
                        {
                            obj_tbl_VidhanSabha.VidhanSabha_LokSabhaId = 0;
                        }
                        try
                        {
                            obj_tbl_VidhanSabha.VidhanSabha_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["VidhanSabha_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_VidhanSabha.VidhanSabha_Id = 0;
                        }
                        obj_tbl_VidhanSabha_Li.Add(obj_tbl_VidhanSabha);
                    }
                }
                else
                {
                    obj_tbl_VidhanSabha_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_VidhanSabha_Li = null;
            }
            return obj_tbl_VidhanSabha_Li;
        }
    }
}
