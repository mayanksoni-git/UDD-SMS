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
    [RoutePrefix("api/Download")]
    public class DownloadController : ApiController
    {
        public readonly string _connectionString;
        public readonly DownloadRepository _DownloadRepository;
        public DownloadController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _DownloadRepository = new DownloadRepository(_connectionString);
        }
        // GET: api/Download
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Download/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            List<tbl_DownloadsOther> obj_tbl_DownloadsOther = await _DownloadRepository.get_Download_Data(id);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_DownloadsOther);
            });
        }

        // POST: api/Download
        public async Task<HttpResponseMessage> Post([FromBody]SearchCriteria obj_SearchCriteria)
        {
            List<tbl_DownloadsOther> obj_tbl_DownloadsOther = await _DownloadRepository.get_Download_Data(obj_SearchCriteria);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_DownloadsOther);
            });
        }

        // PUT: api/Download/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Download/5
        public void Delete(int id)
        {
        }
    }
}
