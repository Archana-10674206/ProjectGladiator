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
    public class ViewMarketPlaceController : ApiController
    {
        dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();
        [HttpGet]
        public HttpResponseMessage MarketPlace(int id)
        {

            List<marketplace_Result> cropsinMarket = new List<marketplace_Result>();
            cropsinMarket = entities.marketplace(id).ToList();
            if(cropsinMarket.Count==0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Active Crops for Bidding!");
            }
            else
            {
                return Request.CreateResponse<IEnumerable<marketplace_Result>>(HttpStatusCode.OK, cropsinMarket);
            }
            //List<sp_GetCurrentBid_Result> activecrops = new List<sp_GetCurrentBid_Result>();
            //activecrops = entities.sp_GetCurrentBid(id).ToList();
            //if (activecrops.Count == 0)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Active Crops for Bidding!");
            //}
            //return Request.CreateResponse<IEnumerable<sp_GetCurrentBid_Result>>(HttpStatusCode.OK, activecrops);
        }
    }
}
