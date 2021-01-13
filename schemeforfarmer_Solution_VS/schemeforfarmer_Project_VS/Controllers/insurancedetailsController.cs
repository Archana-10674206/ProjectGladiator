using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using schemeforfarmer_Project_VS.Models;
namespace schemeforfarmer_Project_VS.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class insurancedetailsController : ApiController
    {
        dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();
        public HttpResponseMessage postinsurancedetails(tblInsurance insurance)
        {
            int x = Convert.ToInt32(insurance.FarmerId);

            ObjectParameter sumfinal = new ObjectParameter("sumfinal", typeof(float));
            ObjectParameter fshare = new ObjectParameter("fshare", typeof(float));
            ObjectParameter gvtshare = new ObjectParameter("gvtshare", typeof(float));


            var result = entities.proc_calculateInsurance(insurance.CropType, insurance.CropName, insurance.Area, sumfinal, fshare, gvtshare);
            calculateinsurance ci = new calculateinsurance();
            ci.InsuranceCompany = "AGRICULTURE INSURANCE COMPANY";
            var msp = entities.tblCropDetails.Where(c => c.CropName == insurance.CropName).Select(t => t.MspPerQuintal).FirstOrDefault();
            var yeild = entities.tblCropDetails.Where(c => c.CropName == insurance.CropName).Select(t => t.YeildPerHectareTons).FirstOrDefault();
            ci.SumInsuredPerHectare = Convert.ToSingle(msp) * Convert.ToSingle(yeild) * 10;
            ci.SharePremium = Convert.ToSingle(gvtshare.Value);
            ci.PremiumAmount = Convert.ToSingle(fshare.Value);
            ci.CropName = insurance.CropName;
            ci.Area = Convert.ToSingle(insurance.Area);
            ci.SumInsured = Convert.ToSingle(sumfinal.Value);



            //if (insurance.FarmerId != null)
            //{
            //insurance.SumInsured = Convert.ToDecimal(Convert.ToSingle(msp) * Convert.ToSingle(yeild));
            //insurance.DateofApplication = DateTime.Now;
            //insurance.FarmerId = x;
            //entities.tblInsurances.Add(insurance);
            //entities.SaveChanges();
            //return Request.CreateResponse(HttpStatusCode.OK, "Succesfully applied");
            //}


            return Request.CreateResponse<calculateinsurance>(ci);
        }
    }
}
