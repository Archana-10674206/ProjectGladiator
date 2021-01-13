using schemeforfarmer_Project_VS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace schemeforfarmer_Project_VS.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]

    public class PreviousBidsController : ApiController
    {
        dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();
        [HttpGet]
        public HttpResponseMessage PrevBids(int id)
        {
            var activecrops = entities.sp_PreviousBids(id).ToList();
            if (activecrops.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Previous Bids!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, activecrops);
        }
    }
}


