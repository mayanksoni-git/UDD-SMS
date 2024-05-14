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
    [RoutePrefix("api/PNBGetPPABatchDetails")]
    public class PNBGetPPABatchDetailsController : ApiController
    {
        public readonly string _connectionString;
        public readonly PNBGetPPABatchDetailsRepository _PNBGetPPABatchDetailsRepository;
        public PNBGetPPABatchDetailsController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _PNBGetPPABatchDetailsRepository = new PNBGetPPABatchDetailsRepository(_connectionString);
        }

        
        // POST: api/PNBGetPPABatchDetails
        public async Task<HttpResponseMessage> Post([FromBody] PPA_Details obj_PPA_Details)
        {
            List<PNB_GetPPADetails_Res> obj_PNB_GetPPADetails_Res_Li = new List<PNB_GetPPADetails_Res>();
            obj_PNB_GetPPADetails_Res_Li = await _PNBGetPPABatchDetailsRepository.get_PNBGetPPABatchDetails(obj_PPA_Details);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_PNB_GetPPADetails_Res_Li);
            });
        }
        // PUT: api/PNBGetPPABatchDetails/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PNBGetPPABatchDetails/5
        public void Delete(int id)
        {
        }
    }
}
