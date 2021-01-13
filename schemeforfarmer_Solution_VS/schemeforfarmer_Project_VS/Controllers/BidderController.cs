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
using schemeforfarmer_Project_VS.Models;

namespace schemeforfarmer_Project_VS.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class BidderController : ApiController
    {

        dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();

        [HttpPost]
        public HttpResponseMessage Register()
        {
            DbContextTransaction transaction = entities.Database.BeginTransaction();

            string imageName = null;
            string imageName1 = null;
            string imageName2 = null;

            var httpRequest = HttpContext.Current.Request;

            //Upload Image
            try
            {

                var postedFile = httpRequest.Files["Image"];
                var postedFile1 = httpRequest.Files["Pan"];
                var postedFile2 = httpRequest.Files["TraderLicense"];

                //Create custom filename

                imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
                postedFile.SaveAs(filePath);

                imageName1 = new String(Path.GetFileNameWithoutExtension(postedFile1.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName1 = imageName1 + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile1.FileName);
                var filePath1 = HttpContext.Current.Server.MapPath("~/Image/" + imageName1);
                postedFile1.SaveAs(filePath1);

                imageName2 = new String(Path.GetFileNameWithoutExtension(postedFile2.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName2 = imageName2 + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile2.FileName);
                var filePath2 = HttpContext.Current.Server.MapPath("~/Image/" + imageName2);
                postedFile2.SaveAs(filePath2);



                tblBidder br = new tblBidder();

                //Save to db
                using (dbFarmerScheme3Entities db = new dbFarmerScheme3Entities())
                {

                    string email = httpRequest["Email"];
                    var e = db.tblBidders.Where(x => x.bEmailId == email).FirstOrDefault();

                    if (e == null)
                    {
                        string acc = httpRequest["AccountNo"];
                        var a = db.tblBidders.Where(x => x.bAccountNo == acc).FirstOrDefault();
                        if (a == null)
                        {
                            try
                            {
                                br.bAadhar = filePath;

                                br.bUserName = httpRequest["Username"];
                                br.bContactNo = httpRequest["Contact"];
                                br.bEmailId = httpRequest["Email"];
                                br.bAddress = httpRequest["Address"];
                                br.bCity = httpRequest["City"];
                                br.bState = httpRequest["State"];
                                br.bPincode = httpRequest["Pincode"];
                                br.bAccountNo = httpRequest["AccountNo"];
                                br.bIFSCcode = httpRequest["ifsccode"];

                                br.bPan = filePath1;
                                br.bTraderLicense = filePath2;
                                br.bPassword = httpRequest["Password"];

                                br.StatusOfBidderDocx = "pending";

                                entities.tblBidders.Add(br);
                                entities.SaveChanges();

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {

                                transaction.Rollback();
                                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                            }
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Account No already exist");
                        }
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Email already exist");
                    }
                }
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "File size should be less than 2MB");
            }

            return Request.CreateResponse(HttpStatusCode.Created, "Successfully Registered");
        }



        [Route("api/bidder/GetSaleData")]
        public HttpResponseMessage Get()
        {

            if (entities.sp_getCropData().ToList().Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Data Found");
            }
            return Request.CreateResponse<IEnumerable<sp_getCropData_Result>>(HttpStatusCode.OK, entities.sp_getCropData().ToList());
        }
        [HttpGet]
        [Route("api/bidder/currentMax")]
        public HttpResponseMessage Get(int cropid, int bidderid)
        {
            var amt = entities.sp_GetMAxBidAmount(cropid,bidderid);
            if (amt == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Crop Not Found");
            }
            return Request.CreateResponse<dynamic>(HttpStatusCode.OK, amt);
        }
        [HttpPost]
        [Route("api/bidder/newbid")]
        public HttpResponseMessage Post(int fid, tblBid bid)
        {
            //entities.tblBids.Add(bid); 
            DbContextTransaction transaction = entities.Database.BeginTransaction();
            try
            {
                entities.sp_newBid(bid.CropId, bid.BidderId, bid.BidAmount, bid.DateOfBid);
                entities.SaveChanges();
                int bidID = entities.tblBids.Max(x => x.bId);
                entities.sp_InsertintoBidCrops(bidID, fid, bid.CropId, bid.BidderId);
                entities.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Could not insert data ");
            }
            return Request.CreateResponse(HttpStatusCode.Created);
        }
        [HttpGet]
        [Route("api/bidder/getwondata")]
        public HttpResponseMessage Getwondata(int bidderId)
        {
            List<sp_wonBids_Result> results = entities.sp_wonBids(bidderId).ToList();
            if (results.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No won Data Found");
            }
            return Request.CreateResponse<IEnumerable<sp_wonBids_Result>>(HttpStatusCode.OK, results);
        }
        [HttpGet]
        [Route("api/bidder/getlostdata")]
        public HttpResponseMessage Getlostdata(int bidderId)
        {
            List<sp_lostBids_Result> results = entities.sp_lostBids(bidderId).ToList();
            if (results.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Loss Data Found");
            }
            return Request.CreateResponse<IEnumerable<sp_lostBids_Result>>(HttpStatusCode.OK, results);
        }
    }


}

