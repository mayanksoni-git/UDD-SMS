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
    [RoutePrefix("api/PNBGetPPATransactionDetails")]
    public class PNBGetPPATransactionDetailsController : ApiController
    {
        public readonly string _connectionString;
        public readonly PNBGetPPATransactionDetailsRepository _PNBGetPPATransactionDetailsRepository;
        public PNBGetPPATransactionDetailsController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _PNBGetPPATransactionDetailsRepository = new PNBGetPPATransactionDetailsRepository(_connectionString);
        }

        // POST: api/PNBGetPPATransactionDetails
        public async Task<HttpResponseMessage> Post([FromBody] PPA_Details obj_PPA_Details)
        {
            List<PNB_GetPPATransactionDetails_Res> obj_PNB_GetPPATransactionDetails_Res_Li = new List<PNB_GetPPATransactionDetails_Res>();
            obj_PNB_GetPPATransactionDetails_Res_Li = await _PNBGetPPATransactionDetailsRepository.get_PNBGetPPATransactionDetails(obj_PPA_Details);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_PNB_GetPPATransactionDetails_Res_Li);
            });
        }
        // PUT: api/PNBGetPPATransactionDetails/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PNBGetPPATransactionDetails/5
        public void Delete(int id)
        {
        }
    }
}
