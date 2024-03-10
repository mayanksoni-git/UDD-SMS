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
    [RoutePrefix("api/Inspection")]
    public class InspectionController : ApiController
    {
        public readonly string _connectionString;
        public readonly InspectionRepository _InspectionRepository;
        public InspectionController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _InspectionRepository = new InspectionRepository(_connectionString);
        }
        
        // POST: api/Inspection
        public async Task<HttpResponseMessage> Post([FromBody] SearchCriteria obj_SearchCriteria)
        {
            List<tbl_ProjectWorkInspection> obj_tbl_ProjectWorkInspection_Li = new List<tbl_ProjectWorkInspection>();

            obj_tbl_ProjectWorkInspection_Li = await _InspectionRepository.get_Inspection(obj_SearchCriteria);

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_ProjectWorkInspection_Li);
            });            
        }
    }
}
