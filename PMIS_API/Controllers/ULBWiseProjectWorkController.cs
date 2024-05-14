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
    [RoutePrefix("api/ULBWiseProjectWork")]
    public class ULBWiseProjectWorkController : ApiController
    {
        public readonly string _connectionString;
        public readonly ULBWiseProjectWorkRepository _ULBWiseProjectWorkRepository;
        public ULBWiseProjectWorkController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _ULBWiseProjectWorkRepository = new ULBWiseProjectWorkRepository(_connectionString);
        }

        // POST: api/ULBWiseProjectWork
        public async Task<HttpResponseMessage> Post([FromBody] SearchCriteria obj_SearchCriteria)
        {
            List<tbl_ULBWiseProjectWork> obj_tbl_ULBWiseProjectWork_Li = new List<tbl_ULBWiseProjectWork>();

            obj_tbl_ULBWiseProjectWork_Li = await _ULBWiseProjectWorkRepository.get_ULBWiseProjectWork(obj_SearchCriteria);

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_ULBWiseProjectWork_Li);
            });            
        }
    }
}
