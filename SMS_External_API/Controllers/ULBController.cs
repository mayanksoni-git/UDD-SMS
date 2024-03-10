using SMS_External_API.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SMS_External_API.Models;

namespace SMS_External_API.Controllers
{
    [RoutePrefix("api/ULB")]
    public class ULBController : ApiController
    {
        public readonly string _connectionString;
        public readonly ULBRepository _ULBRepository;
        public ULBController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _ULBRepository = new ULBRepository(_connectionString);
        }

        // GET: api/ULB/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            List<tbl_ULB> obj_tbl_ULB_Li = new List<tbl_ULB>();

            obj_tbl_ULB_Li = await _ULBRepository.get_ULB(id);

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_ULB_Li);
            });
        }
    }
}
