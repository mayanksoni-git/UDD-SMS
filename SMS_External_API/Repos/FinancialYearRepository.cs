using SMS_External_API.App_Code;
using SMS_External_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SMS_External_API.Repos
{
    public class FinancialYearRepository : RepositoryAsyn
    {
        public FinancialYearRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_FinancialYear>> get_FinancialYear()
        {
            List<tbl_FinancialYear> obj_tbl_FinancialYear_Li = get_tbl_FinancialYear();
            return obj_tbl_FinancialYear_Li;
        }
        private List<tbl_FinancialYear> get_tbl_FinancialYear()
        {
            List<tbl_FinancialYear> obj_tbl_FinancialYear_Li = new List<tbl_FinancialYear>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_FinancialYear();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_FinancialYear obj_tbl_FinancialYear = new tbl_FinancialYear();
                        
                        obj_tbl_FinancialYear.FinancialYear_Name = ds.Tables[0].Rows[i]["FinancialYear_Comments"].ToString();
                        obj_tbl_FinancialYear.FinancialYear_StartDate = ds.Tables[0].Rows[i]["FinancialYear_StartYear"].ToString();
                        obj_tbl_FinancialYear.FinancialYear_EndDate = ds.Tables[0].Rows[i]["FinancialYear_EndYear"].ToString();
                        try
                        {
                            obj_tbl_FinancialYear.FinancialYear_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["FinancialYear_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_FinancialYear.FinancialYear_Id = 0;
                        }
                        obj_tbl_FinancialYear_Li.Add(obj_tbl_FinancialYear);
                    }
                }
                else
                {
                    obj_tbl_FinancialYear_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_FinancialYear_Li = null;
            }
            return obj_tbl_FinancialYear_Li;
        }
    }
}
