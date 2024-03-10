using SMS_External_API.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SMS_External_API.Models;

namespace SMS_External_API.Controllers
{
    [RoutePrefix("api/District")]
    public class DistrictController : ApiController
    {
        public readonly string _connectionString;
        public readonly DistrictRepository _DistrictRepository;
        public DistrictController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _DistrictRepository = new DistrictRepository(_connectionString);
        }
        // GET: api/District
        public async Task<HttpResponseMessage> Get()
        {
            List<tbl_District> obj_tbl_District_Li = new List<tbl_District>();

            obj_tbl_District_Li = await _DistrictRepository.get_District();

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_District_Li);
            });
        }
    }
}
