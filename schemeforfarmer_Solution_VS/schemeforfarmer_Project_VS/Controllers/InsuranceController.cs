using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using schemeforfarmer_Project_VS.Models;
using System.Data.Entity.Core.Objects;
using System.Web.Http.Cors;
using System.Net.Mail;

namespace schemeforfarmer_Project_VS.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class InsuranceController : ApiController
    {
        dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();
       


        public HttpResponseMessage postapplyinsurance(tblInsurance insurance)
        {
            int x = Convert.ToInt32(insurance.FarmerId);

            /*ObjectParameter sumfinal = new ObjectParameter("sumfinal", typeof(float));
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
            ci.SumInsured = Convert.ToSingle(sumfinal.Value);*/



            var msp = entities.tblCropDetails.Where(c => c.CropName == insurance.CropName).Select(t => t.MspPerQuintal).FirstOrDefault();
            var yeild = entities.tblCropDetails.Where(c => c.CropName == insurance.CropName).Select(t => t.YeildPerHectareTons).FirstOrDefault();

            insurance.SumInsured = Convert.ToDecimal((msp) * Convert.ToDecimal(yeild) * 10);
            insurance.DateofApplication = DateTime.Now;
            insurance.FarmerId = x;
            entities.tblInsurances.Add(insurance);
            entities.SaveChanges();
            var sum = Convert.ToDouble(insurance.SumInsured *Convert.ToDecimal(insurance.Area));
            var obj = entities.tblUsers.Where(f => f.fId == x).FirstOrDefault();
            var policyno = entities.tblInsurances.Where(k => k.FarmerId == x).FirstOrDefault();
            //sending email
            string to = obj.EmailId;

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("farmerfriend886@gmail.com");
            mm.To.Add(to);
            mm.Subject = "Insurance details";
            mm.Body = "your policyno:"+policyno.InsuranceApplicationId+"   Suminsured by company is:"+sum;
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = true;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("farmerfriend886@gmail.com", "farmerfriend");
            smtp.Send(mm);
            //return Request.CreateResponse(HttpStatusCode.OK, "Email sent");

            return Request.CreateResponse(HttpStatusCode.OK, "Succesfully applied");



        }

    }
}

