using ePayment_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ePayment_API.Repos
{
    public class DepartmentRepository : RepositoryAsyn
    {
        public DepartmentRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_Department>> get_Department()
        {
            List<tbl_Department> obj_tbl_Department_Li = get_tbl_Department();
            return obj_tbl_Department_Li;
        }
        private List<tbl_Department> get_tbl_Department()
        {
            List<tbl_Department> obj_tbl_Department_Li = new List<tbl_Department>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_Department();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_Department obj_tbl_Department = new tbl_Department();
                        
                        obj_tbl_Department.Department_Name = ds.Tables[0].Rows[i]["Department_Name"].ToString();
                        try
                        {
                            obj_tbl_Department.Department_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Department_Id"].ToString());
                        }
                        catch
                        {
                            obj_tbl_Department.Department_Id = 0;
                        }
                        try
                        {
                            obj_tbl_Department.Department_Status = Convert.ToInt32(ds.Tables[0].Rows[i]["Department_Status"].ToString());
                        }
                        catch
                        {
                            obj_tbl_Department.Department_Status = 0;
                        }
                        obj_tbl_Department_Li.Add(obj_tbl_Department);
                    }
                }
                else
                {
                    obj_tbl_Department_Li = null;
                }
            }
            catch (Exception)
            {
                obj_tbl_Department_Li = null;
            }
            return obj_tbl_Department_Li;
        }
    }
}
