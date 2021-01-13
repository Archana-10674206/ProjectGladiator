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

    public class SoldHistoryController : ApiController
    {
        dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();
        [HttpGet]

        public HttpResponseMessage ShowSoldHistory(int id)
        {
            List<spDisplaySoldHistory_Result> soldcrops = new List<spDisplaySoldHistory_Result>();
            soldcrops = entities.spDisplaySoldHistory(id).ToList();
            if (soldcrops.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Sold Crops!");
            }
            return Request.CreateResponse<IEnumerable<spDisplaySoldHistory_Result>>(HttpStatusCode.OK, soldcrops);
        }
    }
}


