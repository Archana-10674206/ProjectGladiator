using schemeforfarmer_Project_VS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace schemeforfarmer_Project_VS.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class PlaceSellRequestController : ApiController
    {

        dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();

        [HttpPost]
        public HttpResponseMessage AddCropDetails()
        {
            string document = null;
            var httpRequest = HttpContext.Current.Request;

            var postedFile = httpRequest.Files["SPhCert"];

            document = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(15).ToArray()).Replace(" ", "-");
            document = document + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Image/" + document);
            postedFile.SaveAs(filePath);

            tblCropForSale cropForSale = new tblCropForSale();

            cropForSale.SoilPhCertificate = filePath;
            cropForSale.CropType = httpRequest["croptype"];
            cropForSale.CropName = httpRequest["cropname"];
            cropForSale.Quantity = Convert.ToInt32(httpRequest["quantity"]);
            cropForSale.FertilizerType = httpRequest["fertilizertype"];

            cropForSale.StatusOfCropSaleReq = "pending";
            var x = httpRequest["farmerid"];
            cropForSale.FarmerId = Convert.ToInt32(x);

            // cropForSale.FarmerId = Convert.ToInt32(httpRequest["farmerid"]);

            DbContextTransaction transaction = entities.Database.BeginTransaction();
            if (ModelState.IsValid)
            {
                try
                {
                    entities.sp_Place_Request(cropForSale.FarmerId,cropForSale.CropType, cropForSale.CropName, cropForSale.Quantity,
                     cropForSale.FertilizerType, cropForSale.SoilPhCertificate);
                    entities.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Could not Place Request");
                }
            }

            return Request.CreateResponse(HttpStatusCode.Created, cropForSale);
        }
    }
}

