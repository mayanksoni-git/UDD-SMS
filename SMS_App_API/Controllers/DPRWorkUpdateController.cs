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
    [RoutePrefix("api/DPRWorkUpdate")]
    public class DPRWorkUpdateController : ApiController
    {
        public readonly string _connectionString;
        public readonly DPRWorkUpdateRepository _DPRWorkUpdateRepository;
        public DPRWorkUpdateController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _DPRWorkUpdateRepository = new DPRWorkUpdateRepository(_connectionString);
        }
        // GET: api/DPRWorkUpdate
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/DPRWorkUpdate/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DPRWorkUpdate
        public async Task<HttpResponseMessage> Post([FromBody]DPRWorkUpdate obj_DPRWorkUpdate)
        {
            string dd = Newtonsoft.Json.JsonConvert.SerializeObject(obj_DPRWorkUpdate);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "DPRWorkUpdate.txt", dd);
            bool retVal = await _DPRWorkUpdateRepository.update_ProjectDPRWorkStatus(obj_DPRWorkUpdate);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, retVal);
            });
        }

        // PUT: api/DPRWorkUpdate/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DPRWorkUpdate/5
        public void Delete(int id)
        {
        }
    }
}
