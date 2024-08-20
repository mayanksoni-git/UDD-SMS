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
    [RoutePrefix("api/EngineeringWing")]
    public class EngineeringWingController : ApiController
    {
        public readonly string _connectionString;
        public readonly EngineeringWingRepository _EngineeringWingRepository;
        public EngineeringWingController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _EngineeringWingRepository = new EngineeringWingRepository(_connectionString);
        }
        // GET: api/EngineeringWing
        public async Task<HttpResponseMessage> Get()
        {
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, "");
            });
        }

        // GET: api/EngineeringWing/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, "");
            });
        }

        // POST: api/EngineeringWing
        public async Task<HttpResponseMessage> Post([FromBody] SearchCriteria obj_SearchCriteria)
        {
            string dd = Newtonsoft.Json.JsonConvert.SerializeObject(obj_SearchCriteria);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "EngineeringWing.txt", dd);

            List<tbl_Person> obj_tbl_PersonDetail_Li = new List<tbl_Person>();

            obj_tbl_PersonDetail_Li = await _EngineeringWingRepository.get_EngineeringWing(obj_SearchCriteria);

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_PersonDetail_Li);
            });            
        }

        // PUT: api/EngineeringWing/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/EngineeringWing/5
        public void Delete(int id)
        {
        }
    }
}
