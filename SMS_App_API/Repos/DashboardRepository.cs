using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ePayment_API.Models;

namespace ePayment_API.Repos
{
    public class DashboardRepository : RepositoryAsyn
    {
        public DashboardRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<tbl_Dashboard> get_Dashboard_Data(int Person_Id)
        {
            tbl_Dashboard obj_tbl_Dashboard = Dashboard_Data(Person_Id);
            return obj_tbl_Dashboard;
        }

        public async Task<tbl_Dashboard> get_Dashboard_Data(SearchCriteria obj_SearchCriteria)
        {
            tbl_Dashboard obj_tbl_Dashboard = Dashboard_Data(obj_SearchCriteria);
            return obj_tbl_Dashboard;
        }

        private tbl_Dashboard Dashboard_Data(int Person_Id)
        {
            tbl_Dashboard obj_tbl_Dashboard = new tbl_Dashboard();
            try
            {
                DataSet ds = new DataLayer().get_Dashboard_Data(Person_Id, 0, 0, 0);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        obj_tbl_Dashboard.Total_Runing_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["Total_Running_ULB"].ToString());
                    }
                    catch
                    {

                    }
                }
                else
                {
                    obj_tbl_Dashboard.Total_Runing_Projects = 0;
                }
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    try
                    {
                        obj_tbl_Dashboard.Total_Executing_Agency = Convert.ToInt32(ds.Tables[1].Rows[0]["Total_Running_Vendor"].ToString());
                    }
                    catch
                    {

                    }
                }
                else
                {
                    obj_tbl_Dashboard.Total_Executing_Agency = 0;
                }
                if (ds != null && ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                {
                    try
                    {
                        obj_tbl_Dashboard.Total_Inspection = Convert.ToInt32(ds.Tables[2].Rows[0]["Total_Running_Inspection"].ToString());
                    }
                    catch
                    {

                    }
                }
                else
                {
                    obj_tbl_Dashboard.Total_Inspection = 0;
                }

                ds = new DataLayer().get_Dashboard_Data2(Person_Id, 0, 0, 0);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        obj_tbl_Dashboard.Total_Budget = Convert.ToDecimal(ds.Tables[0].Compute("sum(ProjectWork_Budget)", "").ToString());
                    }
                    catch
                    {
                        obj_tbl_Dashboard.Total_Budget = 0;
                    }

                    try
                    {
                        obj_tbl_Dashboard.Total_Funds_Released = Convert.ToDecimal(ds.Tables[0].Compute("sum(tender_cost)", "").ToString());
                    }
                    catch
                    {
                        obj_tbl_Dashboard.Total_Funds_Released = 0;
                    }
                    decimal PhysicalProgress = 0;
                    decimal FinancialProgress = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        try
                        {
                            PhysicalProgress += Convert.ToDecimal(ds.Tables[0].Rows[i]["Physical_Progress"].ToString());
                        }
                        catch
                        {
                            PhysicalProgress += 0;
                        }

                        try
                        {
                            FinancialProgress += Convert.ToDecimal(ds.Tables[0].Rows[i]["Financial_Progress"].ToString());
                        }
                        catch
                        {
                            FinancialProgress += 0;
                        }
                    }

                    obj_tbl_Dashboard.Physical_Progress = decimal.Round(PhysicalProgress / ds.Tables[0].Rows.Count, 2, MidpointRounding.AwayFromZero);
                    obj_tbl_Dashboard.Financial_Progress = decimal.Round(FinancialProgress / ds.Tables[0].Rows.Count, 2, MidpointRounding.AwayFromZero);
                    obj_tbl_Dashboard.Total_Funds_Expenditure = decimal.Round((obj_tbl_Dashboard.Financial_Progress * obj_tbl_Dashboard.Total_Funds_Released) / 100, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    obj_tbl_Dashboard.Total_Budget = 0;
                    obj_tbl_Dashboard.Total_Funds_Released = 0;
                    obj_tbl_Dashboard.Physical_Progress = 0;
                    obj_tbl_Dashboard.Financial_Progress = 0;
                    obj_tbl_Dashboard.Total_Funds_Expenditure = 0;
                }
            }
            catch (Exception)
            {
                obj_tbl_Dashboard = null;
            }
            return obj_tbl_Dashboard;
        }

        private tbl_Dashboard Dashboard_Data(SearchCriteria obj_SearchCriteria)
        {
            tbl_Dashboard obj_tbl_Dashboard = new tbl_Dashboard();
            try
            {
                DataSet ds = new DataLayer().get_Dashboard_Data(obj_SearchCriteria.Person_Id, obj_SearchCriteria.Zone_Id, obj_SearchCriteria.Circle_Id, obj_SearchCriteria.Division_Id);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        obj_tbl_Dashboard.Total_Runing_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["Running_Projects"].ToString());
                    }
                    catch
                    {

                    }

                    try
                    {
                        obj_tbl_Dashboard.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["Total_Projects"].ToString());
                    }
                    catch
                    {

                    }

                    try
                    {
                        obj_tbl_Dashboard.Total_Completed_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["Completed_Projects"].ToString());
                    }
                    catch
                    {

                    }
                }
                else
                {
                    obj_tbl_Dashboard.Total_Runing_Projects = 0;
                    obj_tbl_Dashboard.Total_Completed_Projects = 0;
                    obj_tbl_Dashboard.Total_Projects = 0;
                }
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    try
                    {
                        obj_tbl_Dashboard.Total_Executing_Agency = Convert.ToInt32(ds.Tables[1].Rows[0]["Total_Running_Vendor"].ToString());
                    }
                    catch
                    {

                    }
                }
                else
                {
                    obj_tbl_Dashboard.Total_Executing_Agency = 0;
                }
                if (ds != null && ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                {
                    try
                    {
                        obj_tbl_Dashboard.Total_Inspection = Convert.ToInt32(ds.Tables[2].Rows[0]["Total_Running_Inspection"].ToString());
                    }
                    catch
                    {

                    }
                }
                else
                {
                    obj_tbl_Dashboard.Total_Inspection = 0;
                }

                ds = new DataLayer().get_Dashboard_Data2(obj_SearchCriteria.Person_Id, obj_SearchCriteria.Zone_Id, obj_SearchCriteria.Circle_Id, obj_SearchCriteria.Division_Id);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        obj_tbl_Dashboard.Total_Budget = Convert.ToDecimal(ds.Tables[0].Compute("sum(ProjectWork_Budget)", "").ToString());
                    }
                    catch
                    {
                        obj_tbl_Dashboard.Total_Budget = 0;
                    }

                    try
                    {
                        obj_tbl_Dashboard.Total_Funds_Released = Convert.ToDecimal(ds.Tables[0].Compute("sum(tender_cost)", "").ToString());
                    }
                    catch
                    {
                        obj_tbl_Dashboard.Total_Funds_Released = 0;
                    }
                    decimal PhysicalProgress = 0;
                    decimal FinancialProgress = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        try
                        {
                            PhysicalProgress += Convert.ToDecimal(ds.Tables[0].Rows[i]["Physical_Progress"].ToString());
                        }
                        catch
                        {
                            PhysicalProgress += 0;
                        }

                        try
                        {
                            FinancialProgress += Convert.ToDecimal(ds.Tables[0].Rows[i]["Financial_Progress"].ToString());
                        }
                        catch
                        {
                            FinancialProgress += 0;
                        }
                    }
                    obj_tbl_Dashboard.Physical_Progress = decimal.Round(PhysicalProgress / ds.Tables[0].Rows.Count, 2, MidpointRounding.AwayFromZero);
                    obj_tbl_Dashboard.Financial_Progress = decimal.Round(FinancialProgress / ds.Tables[0].Rows.Count, 2, MidpointRounding.AwayFromZero);
                    obj_tbl_Dashboard.Total_Funds_Expenditure = decimal.Round((obj_tbl_Dashboard.Financial_Progress * obj_tbl_Dashboard.Total_Funds_Released) / 100, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    obj_tbl_Dashboard.Total_Budget = 0;
                    obj_tbl_Dashboard.Total_Funds_Released = 0;
                    obj_tbl_Dashboard.Physical_Progress = 0;
                    obj_tbl_Dashboard.Financial_Progress = 0;
                    obj_tbl_Dashboard.Total_Funds_Expenditure = 0;
                }
            }
            catch (Exception)
            {
                obj_tbl_Dashboard = null;
            }
            return obj_tbl_Dashboard;
        }
    }
}
