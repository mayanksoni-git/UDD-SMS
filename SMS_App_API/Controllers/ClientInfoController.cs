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
    [RoutePrefix("api/ClientInfo")]
    public class ClientInfoController : ApiController
    {
        public ClientInfoController()
        {
            
        }

        // GET: api/ClientInfo
        public async Task<HttpResponseMessage> Get()
        {
            List<Client_Info> obj_Client_Info_Li = new List<Client_Info>();
            Client_Info obj_Client_Info = new Client_Info();

            //obj_Client_Info = new Client_Info();
            //obj_Client_Info.Client_Info_Base_URL = "api.jnupepayment.in";
            //obj_Client_Info.Client_Info_Code = "JALNIGAM UP (AMRUT / STATE)";
            //obj_Client_Info.Client_Info_Id = 2;
            //obj_Client_Info.Client_Info_Logo = "";
            //obj_Client_Info_Li.Add(obj_Client_Info);

            //obj_Client_Info = new Client_Info();
            //obj_Client_Info.Client_Info_Base_URL = "apicnds.jnupepayment.in";
            //obj_Client_Info.Client_Info_Code = "CnDS";
            //obj_Client_Info.Client_Info_Id = 4;
            //obj_Client_Info.Client_Info_Logo = "";
            //obj_Client_Info_Li.Add(obj_Client_Info);

            //obj_Client_Info = new Client_Info();
            //obj_Client_Info.Client_Info_Base_URL = "http://apisarovar.jnupepayment.in";
            //obj_Client_Info.Client_Info_Code = "AMRUT Sarovar";
            //obj_Client_Info.Client_Info_Id = 3;
            //obj_Client_Info.Client_Info_Logo = "";
            //obj_Client_Info_Li.Add(obj_Client_Info);

            obj_Client_Info = new Client_Info();
            obj_Client_Info.Client_Info_Base_URL = "https://apimapp.urbanschemes.up.in";
            obj_Client_Info.Client_Info_Code = "PMS Login";
            obj_Client_Info.Client_Info_Id = 1;
            obj_Client_Info.Client_Info_Logo = "";
            obj_Client_Info_Li.Add(obj_Client_Info);            

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, obj_Client_Info_Li);
            });
        }

        // GET: api/ClientInfo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ClientInfo
        public async Task<HttpResponseMessage> Post()
        {
            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, "");
            });
        }

        // PUT: api/ClientInfo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ClientInfo/5
        public void Delete(int id)
        {
        }
    }
}
