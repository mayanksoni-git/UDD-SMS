using ePayment_API.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ePayment_API.Models;

namespace ePayment_API.Controllers
{
    [RoutePrefix("api/PNBAssignDPLimit")]
    public class PNBAssignDPLimitController : ApiController
    {
        public readonly string _connectionString;
        public readonly PNBAssignDPLimitRepository _PNBAssignDPLimitRepository;
        public PNBAssignDPLimitController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _PNBAssignDPLimitRepository = new PNBAssignDPLimitRepository(_connectionString);
        }

        // GET: api/PNBAssignDPLimit/1
        public async Task<HttpResponseMessage> Get(int id)
        {
            string _val = await _PNBAssignDPLimitRepository.get_PNBAssignDPLimit(id);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, _val);
            });
        }
        // POST: api/PNBAssignDPLimit
        public async Task<HttpResponseMessage> Post([FromBody] DP_Limit obj_DP_Limit)
        {
            tbl_SNAAccountDPLimit obj_tbl_SNAAccountDPLimit = new tbl_SNAAccountDPLimit();
            obj_tbl_SNAAccountDPLimit = await _PNBAssignDPLimitRepository.get_PNBAssignDPLimit(obj_DP_Limit);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_SNAAccountDPLimit);
            });
        }
        // PUT: api/PNBAssignDPLimit/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PNBAssignDPLimit/5
        public void Delete(int id)
        {
        }
    }
}
