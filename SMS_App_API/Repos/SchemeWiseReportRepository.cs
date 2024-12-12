using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ePayment_API.Models;

namespace ePayment_API.Repos
{
    public class SchemeWiseReportRepository : RepositoryAsyn
    {
        public SchemeWiseReportRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_Scheme_Wise_Report2>> get_Scheme_Wise_Report(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_Scheme_Wise_Report2> obj_tbl_Scheme_Wise_Report_Li = get_tbl_Scheme_Wise_Report(obj_SearchCriteria, 0);
            return obj_tbl_Scheme_Wise_Report_Li;
        }

        public async Task<List<tbl_FinancialYear>> get_Scheme_Wise_Report_Breakup(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_FinancialYear> obj_tbl_FinancialYear_Li = new List<tbl_FinancialYear>();
            DataSet ds = new DataLayer().get_tbl_Zone(obj_SearchCriteria.Zone_Id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tbl_FinancialYear obj_tbl_FinancialYear = new tbl_FinancialYear();
                    obj_tbl_FinancialYear.FinancialYear_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                    obj_tbl_FinancialYear.FinancialYear_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();

                    //obj_SearchCriteria.Zone_Id = obj_tbl_FinancialYear.FinancialYear_Id;
                    List<tbl_Scheme_Wise_Report2> obj_tbl_Scheme_Wise_Report_Li = get_tbl_Scheme_Wise_Report(obj_SearchCriteria, obj_tbl_FinancialYear.FinancialYear_Id);
                    obj_tbl_FinancialYear.obj_tbl_Scheme_Wise_Report_Li = obj_tbl_Scheme_Wise_Report_Li;

                    if (obj_tbl_Scheme_Wise_Report_Li != null && obj_tbl_Scheme_Wise_Report_Li.Count > 0)
                    {
                        for (int j = 0; j < obj_tbl_Scheme_Wise_Report_Li.Count; j++)
                        {
                            obj_tbl_FinancialYear.FinancialYear_ProjectCount += obj_tbl_Scheme_Wise_Report_Li[j].Total_Work;
                        }
                        obj_tbl_FinancialYear_Li.Add(obj_tbl_FinancialYear);
                    }
                }
            }
            return obj_tbl_FinancialYear_Li;
        }

        private List<tbl_Scheme_Wise_Report2> get_tbl_Scheme_Wise_Report(SearchCriteria obj_SearchCriteria, int Zone_Id_Search)
        {
            List<tbl_Scheme_Wise_Report2> obj_tbl_Scheme_Wise_Report_Li = new List<tbl_Scheme_Wise_Report2>();
            try
            {
                DataSet ds = new DataLayer().get_Scheme_Wise_Report(obj_SearchCriteria, Zone_Id_Search);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_Scheme_Wise_Report2 obj_tbl_Scheme_Wise_Report = new tbl_Scheme_Wise_Report2();
                        obj_tbl_Scheme_Wise_Report.FinancialYear_Id = Zone_Id_Search;
                        obj_tbl_Scheme_Wise_Report.Project_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Project_Id"].ToString());
                        obj_tbl_Scheme_Wise_Report.Project_Name = ds.Tables[0].Rows[i]["Project_Name"].ToString();
                        try
                        {
                            obj_tbl_Scheme_Wise_Report.Total_ULB = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_ULB"].ToString());
                        }
                        catch
                        { }
                        try
                        {
                            obj_tbl_Scheme_Wise_Report.Total_Work = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Work"].ToString());
                        }
                        catch
                        { }
                        try
                        {
                            obj_tbl_Scheme_Wise_Report.Balance = Convert.ToDecimal(ds.Tables[0].Rows[i]["Balance"].ToString());
                        }
                        catch
                        { }
                        try
                        {
                            //obj_tbl_Scheme_Wise_Report.BudgetAllocated = Convert.ToDecimal(ds.Tables[0].Rows[i]["Fund_Released"].ToString());
                            obj_tbl_Scheme_Wise_Report.BudgetAllocated = ds.Tables[0].Rows[i]["Fund_Released"].ToString();
                        }
                        catch
                        { }
                        try
                        {
                            obj_tbl_Scheme_Wise_Report.Expenditure = Convert.ToDecimal(ds.Tables[0].Rows[i]["Expenditure"].ToString());
                        }
                        catch
                        { }
                        try
                        {
                            obj_tbl_Scheme_Wise_Report.Financial_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Expenditure"].ToString());
                        }
                        catch
                        { }
                        try
                        {
                            obj_tbl_Scheme_Wise_Report.Financial_Progress_Per = Convert.ToDecimal(ds.Tables[0].Rows[i]["Financial_Progress"].ToString());
                        }
                        catch
                        {
                            obj_tbl_Scheme_Wise_Report.Financial_Progress_Per = 0;
                        }
                        try
                        {
                            //obj_tbl_Scheme_Wise_Report.Fund_Released = Convert.ToDecimal(ds.Tables[0].Rows[i]["Fund_Released"].ToString());
                            obj_tbl_Scheme_Wise_Report.Fund_Released = ds.Tables[0].Rows[i]["Fund_Released"].ToString();
                        }
                        catch
                        { }
                        try
                        {
                            obj_tbl_Scheme_Wise_Report.Physical_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Physical_Progress"].ToString());
                        }
                        catch
                        { }
                        try
                        {
                            //obj_tbl_Scheme_Wise_Report.Project_Budget = Convert.ToDecimal(ds.Tables[0].Rows[i]["Project_Budget"].ToString());
                            obj_tbl_Scheme_Wise_Report.Project_Budget = ds.Tables[0].Rows[i]["Project_Budget"].ToString();
                        }
                        catch
                        { }
                        obj_tbl_Scheme_Wise_Report_Li.Add(obj_tbl_Scheme_Wise_Report);
                    }
                }
                else
                {
                    obj_tbl_Scheme_Wise_Report_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_Scheme_Wise_Report_Li = null;
            }
            return obj_tbl_Scheme_Wise_Report_Li;
        }
    }
}
