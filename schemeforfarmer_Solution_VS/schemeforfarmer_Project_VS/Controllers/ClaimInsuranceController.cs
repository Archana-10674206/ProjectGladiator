using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using schemeforfarmer_Project_VS.Models;

using System.Web.Http.Cors;

namespace schemeforfarmer_Project_VS.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]

    public class ClaimInsuranceController : ApiController
    {
        dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();
        

        public HttpResponseMessage postclaiminsurance(tblInsuranceClaim insuranceClaim)
         {
            //check for farmerid in session matches with farmerid in tblinsurance(this says he applied for insurance)
            if (insuranceClaim.Policyno == entities.tblInsurances.Select(t => t.InsuranceApplicationId).FirstOrDefault())
            {

                if (insuranceClaim.SumInsured == entities.tblInsurances.Select(t => t.SumInsured).FirstOrDefault())
                {
                    insuranceClaim.ClaimStatus = "pending";
                    Convert.ToDecimal(insuranceClaim.SumInsured);
                    entities.tblInsuranceClaims.Add(insuranceClaim);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "your insurance is claimed");

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "you should enter the correct suminsured by company");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "policy no mismatch");

            }
        }
       
    }
}
