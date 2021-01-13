using schemeforfarmer_Project_VS.Models;
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

    public class FarmerHomeController : ApiController
    {
            
            dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();
            List<spDisplayPending_Result> pending_Results = new List<spDisplayPending_Result>();

        [HttpGet]
        public HttpResponseMessage ViewPending(int id)

        {

            pending_Results = entities.spDisplayPending(id).ToList();
            if (pending_Results.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Pending Requests!");
            }
            else
            {
                return Request.CreateResponse<IEnumerable<spDisplayPending_Result>>(HttpStatusCode.OK, pending_Results);
            }
        }
        
    }
}
