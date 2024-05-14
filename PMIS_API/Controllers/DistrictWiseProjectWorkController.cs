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
    [RoutePrefix("api/DistrictWiseProjectWork")]
    public class DistrictWiseProjectWorkController : ApiController
    {
        public readonly string _connectionString;
        public readonly DistrictWiseProjectWorkRepository _DistrictWiseProjectWorkRepository;
        public DistrictWiseProjectWorkController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _DistrictWiseProjectWorkRepository = new DistrictWiseProjectWorkRepository(_connectionString);
        }

        // POST: api/DistrictWiseProjectWork
        public async Task<HttpResponseMessage> Post([FromBody] SearchCriteria obj_SearchCriteria)
        {
            List<tbl_DistrictWiseProjectWork> obj_tbl_DistrictWiseProjectWork_Li = new List<tbl_DistrictWiseProjectWork>();

            obj_tbl_DistrictWiseProjectWork_Li = await _DistrictWiseProjectWorkRepository.get_DistrictWiseProjectWork(obj_SearchCriteria);

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_DistrictWiseProjectWork_Li);
            });            
        }
    }
}
