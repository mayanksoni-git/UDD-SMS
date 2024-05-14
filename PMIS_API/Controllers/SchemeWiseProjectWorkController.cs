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
    [RoutePrefix("api/SchemeWiseProjectWork")]
    public class SchemeWiseProjectWorkController : ApiController
    {
        public readonly string _connectionString;
        public readonly SchemeWiseProjectWorkRepository _SchemeWiseProjectWorkRepository;
        public SchemeWiseProjectWorkController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _SchemeWiseProjectWorkRepository = new SchemeWiseProjectWorkRepository(_connectionString);
        }

        // POST: api/SchemeWiseProjectWork
        public async Task<HttpResponseMessage> Post([FromBody] SearchCriteria obj_SearchCriteria)
        {
            List<tbl_SchemeWiseProjectWork> obj_tbl_SchemeWiseProjectWork_Li = new List<tbl_SchemeWiseProjectWork>();

            obj_tbl_SchemeWiseProjectWork_Li = await _SchemeWiseProjectWorkRepository.get_SchemeWiseProjectWork(obj_SearchCriteria);

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_SchemeWiseProjectWork_Li);
            });            
        }
    }
}
