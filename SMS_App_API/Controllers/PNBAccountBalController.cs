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
    [RoutePrefix("api/PNBAccountBal")]
    public class PNBAccountBalController : ApiController
    {
        public readonly string _connectionString;
        public readonly PNBAccountBalRepository _PNBAccountBalRepository;
        public PNBAccountBalController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _PNBAccountBalRepository = new PNBAccountBalRepository(_connectionString);
        }

        // GET: api/PNBAccountBal/1
        public async Task<HttpResponseMessage> Get(int id)
        {
            tbl_SNAAccountBalance obj_tbl_SNAAccountBalance = new tbl_SNAAccountBalance();
            obj_tbl_SNAAccountBalance = await _PNBAccountBalRepository.get_PNBAccountBal(id);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_SNAAccountBalance);
            });
        }
               
        // PUT: api/PNBAccountBal/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PNBAccountBal/5
        public void Delete(int id)
        {
        }
    }
}
