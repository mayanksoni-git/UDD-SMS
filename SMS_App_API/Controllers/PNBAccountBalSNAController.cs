using ePayment_API.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ePayment_API.Models;
using System.IO;

namespace ePayment_API.Controllers
{
    [RoutePrefix("api/PNBAccountBalSNA")]
    public class PNBAccountBalSNAController : ApiController
    {
        public readonly string _connectionString;
        public readonly PNBAccountBalSNARepository _PNBAccountBalSNARepository;
        public PNBAccountBalSNAController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _PNBAccountBalSNARepository = new PNBAccountBalSNARepository(_connectionString);
        }

        // GET: api/PNBAccountBalSNA/1
        public async Task<HttpResponseMessage> Get(int id)
        {
            tbl_SNAAccountBalance obj_tbl_SNAAccountBalance = new tbl_SNAAccountBalance();
            obj_tbl_SNAAccountBalance = await _PNBAccountBalSNARepository.get_PNBAccountBalSNA(id);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_SNAAccountBalance);
            });
        }

        // PUT: api/PNBAccountBalSNA/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/PNBAccountBalSNA/5
        public void Delete(int id)
        {
        }
    }
}
