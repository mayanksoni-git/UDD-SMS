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
    [RoutePrefix("api/PNBGetPPADetails")]
    public class PNBGetPPADetailsController : ApiController
    {
        public readonly string _connectionString;
        public readonly PNBGetPPADetailsRepository _PNBGetPPADetailsRepository;
        public PNBGetPPADetailsController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _PNBGetPPADetailsRepository = new PNBGetPPADetailsRepository(_connectionString);
        }

        // GET: api/PNBGetPPADetails/1
        public async Task<HttpResponseMessage> Get(int id)
        {
            string _val = await _PNBGetPPADetailsRepository.get_PNBGetPPADetails(id);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, _val);
            });
        }
        // POST: api/PNBGetPPADetails
        public async Task<HttpResponseMessage> Post([FromBody] PPA_Details obj_PPA_Details)
        {
            tbl_SNAPPADetails obj_tbl_SNAPPADetails = new tbl_SNAPPADetails();
            obj_tbl_SNAPPADetails = await _PNBGetPPADetailsRepository.get_PNBGetPPADetails(obj_PPA_Details);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_SNAPPADetails);
            });
        }
        // PUT: api/PNBGetPPADetails/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PNBGetPPADetails/5
        public void Delete(int id)
        {
        }
    }
}
