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
    [RoutePrefix("api/DeletePhoto")]
    public class DeletePhotoController : ApiController
    {
        public readonly string _connectionString;
        public readonly DeletePhotoRepository _DeletePhotoRepository;
        public DeletePhotoController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _DeletePhotoRepository = new DeletePhotoRepository(_connectionString);
        }
        // GET: api/DeletePhoto
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/DeletePhoto/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DeletePhoto
        public async Task<HttpResponseMessage> Post([FromBody]SearchCriteria obj_SearchCriteria)
        {
            bool retVal = await _DeletePhotoRepository.update_DeletePhoto(obj_SearchCriteria);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, retVal);
            });
        }

        // PUT: api/DeletePhoto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DeletePhoto/5
        public void Delete(int id)
        {
        }
    }
}
