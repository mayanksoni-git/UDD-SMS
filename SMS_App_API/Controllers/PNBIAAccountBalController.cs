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
    [RoutePrefix("api/PNBIAAccountBal")]
    public class PNBIAAccountBalController : ApiController
    {
        public readonly string _connectionString;
        public readonly PNBIAAccountBalRepository _PNBIAAccountBalRepository;
        public PNBIAAccountBalController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _PNBIAAccountBalRepository = new PNBIAAccountBalRepository(_connectionString);
        }

        // GET: api/PNBIAAccountBal/1
        public async Task<HttpResponseMessage> Get(int id)
        {
            tbl_SNAAccountBalance obj_tbl_SNAAccountBalance = new tbl_SNAAccountBalance();
            obj_tbl_SNAAccountBalance = await _PNBIAAccountBalRepository.get_PNBIAAccountBal(id);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_SNAAccountBalance);
            });
        }
               
        // PUT: api/PNBIAAccountBal/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PNBIAAccountBal/5
        public void Delete(int id)
        {
        }
    }
}
