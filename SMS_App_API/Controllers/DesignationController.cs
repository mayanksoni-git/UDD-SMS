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
    [RoutePrefix("api/Designation")]
    public class DesignationController : ApiController
    {
        public readonly string _connectionString;
        public readonly DesignationRepository _DesignationRepository;
        public DesignationController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _DesignationRepository = new DesignationRepository(_connectionString);
        }
        // GET: api/Designation
        public async Task<HttpResponseMessage> Get()
        {
            List<tbl_Designation> obj_tbl_Designation_Li = new List<tbl_Designation>();

            obj_tbl_Designation_Li = await _DesignationRepository.get_Designation();

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_Designation_Li);
            });
        }

        // GET: api/Designation/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, "");
            });
        }

        // POST: api/Designation
        public async Task<HttpResponseMessage> Post()
        {
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, "");
            });
        }

        // PUT: api/Designation/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Designation/5
        public void Delete(int id)
        {
        }
    }
}
