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
    [RoutePrefix("api/ProjectRoadReinst")]
    public class ProjectRoadReinstController : ApiController
    {
        public readonly string _connectionString;
        public readonly ProjectRoadReinstRepository _ProjectRoadReinstRepository;
        public ProjectRoadReinstController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _ProjectRoadReinstRepository = new ProjectRoadReinstRepository(_connectionString);
        }
        // GET: api/ProjectRoadReinst
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ProjectRoadReinst/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ProjectRoadReinst
        public async Task<HttpResponseMessage> Post([FromBody]tbl_ProjectRoadReinst obj_ProjectRoadReinst)
        {
            string dd = Newtonsoft.Json.JsonConvert.SerializeObject(obj_ProjectRoadReinst);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "ProjectRoadReinst.txt", dd);
            bool retVal = await _ProjectRoadReinstRepository.update_ProjectRoadReinst(obj_ProjectRoadReinst);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, retVal);
            });
        }

        // PUT: api/ProjectRoadReinst/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProjectRoadReinst/5
        public void Delete(int id)
        {
        }
    }
}
