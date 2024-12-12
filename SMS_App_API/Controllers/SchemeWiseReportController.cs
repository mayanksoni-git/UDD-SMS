using ePayment_API.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ePayment_API.Models;
using System.Web.ModelBinding;
using System.IO;

namespace ePayment_API.Controllers
{
    [RoutePrefix("api/SchemeWiseReport")]
    public class SchemeWiseReportController : ApiController
    {
        public readonly string _connectionString;
        public readonly SchemeWiseReportRepository _SchemeWiseReportRepository;
        public SchemeWiseReportController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _SchemeWiseReportRepository = new SchemeWiseReportRepository(_connectionString);
        }
        // GET: api/SchemeWiseReport
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/SchemeWiseReport
        public async Task<HttpResponseMessage> Post([FromBody] SearchCriteria obj_SearchCriteria)
        {
            if (obj_SearchCriteria.Reporting_Mode == "Breakup")
            {
                List<tbl_FinancialYear> obj_tbl_FinancialYear_Li = await _SchemeWiseReportRepository.get_Scheme_Wise_Report_Breakup(obj_SearchCriteria);
                return await Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_FinancialYear_Li);
                });
            }
            else
            {
                List<tbl_Scheme_Wise_Report> obj_tbl_Scheme_Wise_Report_Li = await _SchemeWiseReportRepository.get_Scheme_Wise_Report(obj_SearchCriteria);
                List<tbl_Scheme_Wise_Report2> obj_tbl_Scheme_Wise_Report_Li2 = new List<tbl_Scheme_Wise_Report2>();

                obj_tbl_Scheme_Wise_Report_Li2 = obj_tbl_Scheme_Wise_Report_Li
                .Select(report => new tbl_Scheme_Wise_Report2
                {
                    Project_Id = report.Project_Id,
                    FinancialYear_Id = report.FinancialYear_Id,
                    Project_Name = report.Project_Name,
                    Total_ULB = report.Total_ULB,
                    Project_Budget = report.Project_Budget.ToString("F2"), // Convert to string with 2 decimal places
                    Total_Work = report.Total_Work,
                    BudgetAllocated = report.BudgetAllocated.ToString("F2"), // Convert to string with 2 decimal places
                    Fund_Released = report.Fund_Released.ToString("F2"), // Convert to string with 2 decimal places
                    Expenditure = report.Expenditure,
                    Balance = report.Balance,
                    Physical_Progress = report.Physical_Progress,
                    Financial_Progress = report.Financial_Progress,
                    Financial_Progress_Per = report.Financial_Progress_Per
                })
                .ToList();

                return await Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_Scheme_Wise_Report_Li2);
                });
            }
        }

        // PUT: api/SchemeWiseReport/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SchemeWiseReport/5
        public void Delete(int id)
        {
        }
    }
}
