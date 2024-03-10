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
    [RoutePrefix("api/FinancialYear")]
    public class FinancialYearController : ApiController
    {
        public readonly string _connectionString;
        public readonly FinancialYearRepository _FinancialYearRepository;
        public FinancialYearController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _FinancialYearRepository = new FinancialYearRepository(_connectionString);
        }
        // GET: api/FinancialYear
        public async Task<HttpResponseMessage> Get()
        {
            List<tbl_FinancialYear> obj_tbl_FinancialYear_Li = new List<tbl_FinancialYear>();

            obj_tbl_FinancialYear_Li = await _FinancialYearRepository.get_FinancialYear();

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_FinancialYear_Li);
            });
        }
    }
}
