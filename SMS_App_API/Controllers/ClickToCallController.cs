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
    [RoutePrefix("api/ClickToCall")]
    public class ClickToCallController : ApiController
    {
        public readonly string _connectionString;
        public readonly ClickToCallRepository _ClickToCallRepository;
        public ClickToCallController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _ClickToCallRepository = new ClickToCallRepository(_connectionString);
        }

        // GET: api/ClickToCall/1
        public async Task<HttpResponseMessage> Get(int id)
        {
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, "");
            });
        }
        // POST: api/ClickToCall
        public async Task<HttpResponseMessage> Post([FromBody] Click_To_Call_Request obj_Click_To_Call_Request)
        {
            Click_To_Call_Response obj_Click_To_Call_Response = new Click_To_Call_Response();
            obj_Click_To_Call_Response = await _ClickToCallRepository.get_Click_To_Call(obj_Click_To_Call_Request);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_Click_To_Call_Response);
            });
        }
        // PUT: api/ClickToCall/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ClickToCall/5
        public void Delete(int id)
        {
        }
    }
}
