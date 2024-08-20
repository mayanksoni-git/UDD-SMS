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
    [RoutePrefix("api/VidhanSabha")]
    public class VidhanSabhaController : ApiController
    {
        public readonly string _connectionString;
        public readonly VidhanSabhaRepository _VidhanSabhaRepository;
        public VidhanSabhaController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _VidhanSabhaRepository = new VidhanSabhaRepository(_connectionString);
        }

        // GET: api/VidhanSabha/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            List<tbl_VidhanSabha> obj_tbl_VidhanSabha_Li = new List<tbl_VidhanSabha>();

            obj_tbl_VidhanSabha_Li = await _VidhanSabhaRepository.get_VidhanSabha(id);

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_VidhanSabha_Li);
            });
        }
    }
}
