using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using ePayment_API.Models;

namespace ePayment_API.Repos
{
    public class DownloadRepository : RepositoryAsyn
    {
        public DownloadRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<List<tbl_DownloadsOther>> get_Download_Data(int Person_Id)
        {
            List<tbl_DownloadsOther> obj_tbl_DownloadsOther = Download_Data(Person_Id);
            return obj_tbl_DownloadsOther;
        }

        public async Task<List<tbl_DownloadsOther>> get_Download_Data(SearchCriteria obj_SearchCriteria)
        {
            List<tbl_DownloadsOther> obj_tbl_DownloadsOther = Download_Data(0);
            return obj_tbl_DownloadsOther;
        }

        private List<tbl_DownloadsOther> Download_Data(int Person_Id)
        {
            List<tbl_DownloadsOther> obj_tbl_DownloadsOtherLi = new List<tbl_DownloadsOther>();
            try
            {
                DataSet ds = new DataLayer().get_tbl_DownloadsOther();
                if (Utility.CheckDataSet(ds))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tbl_DownloadsOther obj_tbl_DownloadsOther = new tbl_DownloadsOther();
                        obj_tbl_DownloadsOther.DownloadsOther_AddedBy = Convert.ToInt32(ds.Tables[0].Rows[i]["DownloadsOther_AddedBy"].ToString());
                        obj_tbl_DownloadsOther.DownloadsOther_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["DownloadsOther_Id"].ToString());
                        obj_tbl_DownloadsOther.DownloadsOther_Name = ds.Tables[0].Rows[i]["DownloadsOther_Name"].ToString();
                        obj_tbl_DownloadsOther.DownloadsOther_Date = ds.Tables[0].Rows[i]["DownloadsOther_Date"].ToString();
                        obj_tbl_DownloadsOther.DownloadsOther_Ref_No = ds.Tables[0].Rows[i]["DownloadsOther_Ref_No"].ToString();
                        if (ds.Tables[0].Rows[i]["DownloadsOther_URL"].ToString().Replace("\\", "/") == "")
                        {
                            obj_tbl_DownloadsOther.DownloadsOther_URL = "";
                        }
                        else
                        {
                            obj_tbl_DownloadsOther.DownloadsOther_URL = "https://www.jnupepayment.in/" + ds.Tables[0].Rows[i]["DownloadsOther_URL"].ToString().Replace("\\", "/");
                        }
                        obj_tbl_DownloadsOther.DownloadsOther_Status = 1;
                        obj_tbl_DownloadsOtherLi.Add(obj_tbl_DownloadsOther);
                    }
                }
            }
            catch (Exception)
            {
                obj_tbl_DownloadsOtherLi = null;
            }
            return obj_tbl_DownloadsOtherLi;
        }
    }
}
