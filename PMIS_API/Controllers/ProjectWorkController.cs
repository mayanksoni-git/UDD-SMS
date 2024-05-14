using PMIS_API.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PMIS_API.Models;

namespace PMIS_API.Controllers
{
    [RoutePrefix("api/ProjectWork")]
    public class ProjectWorkController : ApiController
    {
        public readonly string _connectionString;
        public readonly ProjectWorkRepository _ProjectWorkRepository;
        public ProjectWorkController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _ProjectWorkRepository = new ProjectWorkRepository(_connectionString);
        }
        
        // POST: api/ProjectWork
        public async Task<HttpResponseMessage> Post([FromBody] SearchCriteria obj_SearchCriteria)
        {
            List<tbl_ProjectWork> obj_tbl_ProjectWork_Li = new List<tbl_ProjectWork>();

            obj_tbl_ProjectWork_Li = await _ProjectWorkRepository.get_ProjectWork(obj_SearchCriteria);

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_ProjectWork_Li);
            });            
        }
    }
}
