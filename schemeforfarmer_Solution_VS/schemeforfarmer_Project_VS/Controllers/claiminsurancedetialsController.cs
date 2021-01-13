using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using schemeforfarmer_Project_VS.Models;

namespace schemeforfarmer_Project_VS.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class claiminsurancedetialsController : ApiController
    {
        dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();
        [HttpGet]
        public HttpResponseMessage Getdetails(long appid)
        {
            proc_getdetailsofinsuree_Result result = null;
            result = entities.proc_getdetailsofinsuree(appid).FirstOrDefault();
            return Request.CreateResponse<proc_getdetailsofinsuree_Result>(result);
        }
    }
}
