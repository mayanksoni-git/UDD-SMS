using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ePayment_API.Models;

namespace ePayment_API.Repos
{
    public class ProjectRoadReinstRepository : RepositoryAsyn
    {
        public ProjectRoadReinstRepository(string connectionString) : base(connectionString) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public async Task<bool> update_ProjectRoadReinst(tbl_ProjectRoadReinst obj_ProjectRoadReinst)
        {
            return new DataLayer().Update_tbl_ProjectRoadReinst(obj_ProjectRoadReinst);
        }
    }
}
