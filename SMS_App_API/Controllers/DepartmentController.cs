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
    [RoutePrefix("api/Department")]
    public class DepartmentController : ApiController
    {
        public readonly string _connectionString;
        public readonly DepartmentRepository _DepartmentRepository;
        public DepartmentController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _DepartmentRepository = new DepartmentRepository(_connectionString);
        }
        // GET: api/Department
        public async Task<HttpResponseMessage> Get()
        {
            List<tbl_Department> obj_tbl_Department_Li = new List<tbl_Department>();

            obj_tbl_Department_Li = await _DepartmentRepository.get_Department();

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_Department_Li);
            });
        }

        // GET: api/Department/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, "");
            });
        }

        // POST: api/Department
        public async Task<HttpResponseMessage> Post()
        {
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, "");
            });
        }

        // PUT: api/Department/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Department/5
        public void Delete(int id)
        {
        }
    }
}
