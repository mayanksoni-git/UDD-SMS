using PMIS_API.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PMIS_API.Models;

namespace PMIS_API.Controllers
{
    [RoutePrefix("api/LokSabha")]
    public class LokSabhaController : ApiController
    {
        public readonly string _connectionString;
        public readonly LokSabhaRepository _LokSabhaRepository;
        public LokSabhaController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _LokSabhaRepository = new LokSabhaRepository(_connectionString);
        }
        // GET: api/LokSabha
        public async Task<HttpResponseMessage> Get()
        {
            List<tbl_LokSabha> obj_tbl_LokSabha_Li = new List<tbl_LokSabha>();

            obj_tbl_LokSabha_Li = await _LokSabhaRepository.get_LokSabha();

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_LokSabha_Li);
            });
        }
    }
}
