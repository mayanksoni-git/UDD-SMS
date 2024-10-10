using ePayment_API.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ePayment_API.Models;

namespace ePayment_API.Controllers
{
    
    public class SchemeFundMasterController : ApiController
    {
        public readonly string _connectionString;
        public readonly SchemeFundMasterRepository _SchemeFundMasterRepository;
        public SchemeFundMasterController()
        {
            _connectionString = ConnectionString.DBConnectionString;
            _SchemeFundMasterRepository = new SchemeFundMasterRepository(_connectionString);
        }
        [Route("api/GetFinancialYear")]
        public async Task<HttpResponseMessage> GetFinancialYear()
        {
            //long? UserLoginID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID != 1)
            //{
            //    UserLoginID = UserManager.GetUserLoginInfo(User.Identity.Name).UserID;
            //}
            List<Financial_DropDown> obj_tbl_Project_Li = await _SchemeFundMasterRepository.get_FinnYear();
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_Project_Li);
            });
        }
        [Route("api/GetSchemes/{UserLoginID}")]
        public async Task<HttpResponseMessage> GetSchemes(long? UserLoginID)
        {
            //long? UserLoginID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID != 1)
            //{
            //    UserLoginID = UserManager.GetUserLoginInfo(User.Identity.Name).UserID;
            //}
            List<Scheme_DropDown> obj_tbl_Project_Li = await _SchemeFundMasterRepository.get_Schemes(UserLoginID);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_Project_Li);
            });
        }
        [Route("api/GetDistricts")]
        public async Task<HttpResponseMessage> Get_Districts()
        {
            //long? UserLoginID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID != 1)
            //{
            //    UserLoginID = UserManager.GetUserLoginInfo(User.Identity.Name).UserID;
            //}
            List<District_DropDown> obj_tbl_Project_Li = await _SchemeFundMasterRepository.Get_Districts();
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_Project_Li);
            });
        }
        [Route("api/GetULBTypes")]
        public async Task<HttpResponseMessage> Get_ULBTypes()
        {
            //long? UserLoginID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID != 1)
            //{
            //    UserLoginID = UserManager.GetUserLoginInfo(User.Identity.Name).UserID;
            //}
            List<ULBType_DropDown> obj_tbl_Project_Li = await _SchemeFundMasterRepository.Get_ULBTypes();
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_Project_Li);
            });
        }
        [Route("api/GetULBs/{ULBTypeId}")]
        public async Task<HttpResponseMessage> Get_ULBs(int ULBTypeId)
        {
            //long? UserLoginID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID != 1)
            //{
            //    UserLoginID = UserManager.GetUserLoginInfo(User.Identity.Name).UserID;
            //}
            List<ULB_DropDown> obj_tbl_Project_Li = await _SchemeFundMasterRepository.Get_ULBs(ULBTypeId );
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_Project_Li);
            });
        }
        [HttpPost]
        [Route("api/GetSchemeFundReport")]
        public async Task<HttpResponseMessage> GetSchemeFundReport([FromBody] SchemeFundMaster schemeFundMaster)
        {
            //long? UserLoginID = null;
            //if (UserManager.GetUserLoginInfo(User.Identity.Name).RoleID != 1)
            //{
            //    UserLoginID = UserManager.GetUserLoginInfo(User.Identity.Name).UserID;
            //}
            List<SchemeFundMaster> obj_tbl_Project_Li = await _SchemeFundMasterRepository.Get_SchemeFundReport(schemeFundMaster);
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_tbl_Project_Li);
            });
        }
    }

   
}
