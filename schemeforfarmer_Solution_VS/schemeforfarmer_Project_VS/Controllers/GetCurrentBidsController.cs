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

    public class GetCurrentBidsController : ApiController
    {
        dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();
        [HttpGet]
        public HttpResponseMessage currentbids(int id)
        {
            List<sp_GetCurrentBid_Result> current = new List<sp_GetCurrentBid_Result>();
            current = entities.sp_GetCurrentBid(id).ToList();
            if (current == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Bids for this Crop yet!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, current);
            }
        }
    }
}
