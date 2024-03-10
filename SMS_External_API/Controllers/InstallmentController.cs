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
    [RoutePrefix("api/Installment")]
    public class InstallmentController : ApiController
    {
        public readonly string _connectionString;
        public readonly InstallmentRepository _InstallmentRepository;
        public InstallmentController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _InstallmentRepository = new InstallmentRepository(_connectionString);
        }
        
        // POST: api/Installment
        public async Task<HttpResponseMessage> Post([FromBody] SearchCriteria obj_SearchCriteria)
        {
            List<tbl_ProjectWorkInstallment> obj_tbl_ProjectWorkInstallment_Li = new List<tbl_ProjectWorkInstallment>();

            obj_tbl_ProjectWorkInstallment_Li = await _InstallmentRepository.get_Installment(obj_SearchCriteria);

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_ProjectWorkInstallment_Li);
            });            
        }
    }
}
