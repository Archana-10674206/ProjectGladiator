using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using schemeforfarmer_Project_VS.Models;

namespace schemeforfarmer_Project_VS.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AdminController : ApiController
    {
        
            dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();
            [HttpGet]
            [Route("api/admin/getPendingSales")]
            public HttpResponseMessage Getsale()
            {
                List<sp_getPendingSaleData_Result> results = entities.sp_getPendingSaleData().ToList();
                if (results.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No pending sale Data");
                }
                return Request.CreateResponse<IEnumerable<sp_getPendingSaleData_Result>>(HttpStatusCode.OK, results);
            }
            [HttpGet]
            [Route("api/admin/getPendingBids")]
            public HttpResponseMessage Getbids()
            {
                List<sp_getpendingbids1_Result> results = entities.sp_getpendingbids1().ToList();
                if (results.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No pending Bids Found");
                }
                return Request.CreateResponse<IEnumerable<sp_getpendingbids1_Result>>(HttpStatusCode.OK, results);
            }
            [HttpGet]
            [Route("api/admin/getPendingBidders")]
            public HttpResponseMessage GetBidders()
            {
                List<sp_getPendingBidders_Result> results = entities.sp_getPendingBidders().ToList();
                if (results.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No bidder is waitng for Approval");
                }
                return Request.CreateResponse<IEnumerable<sp_getPendingBidders_Result>>(HttpStatusCode.OK, results);
            }

            [HttpGet]
            [Route("api/admin/getPendingFarmers")]
            public HttpResponseMessage GetFarmers()
            {
                List<sp_getPendingFarmers_Result> results = entities.sp_getPendingFarmers().ToList();
                if (results.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No bidder is waitng for Approval");
                }
                return Request.CreateResponse<IEnumerable<sp_getPendingFarmers_Result>>(HttpStatusCode.OK, results);
            }
            [HttpPost]
            [Route("api/admin/approveFarmer")]
            public HttpResponseMessage Post(tblFarmer farmer)
            {
                DbContextTransaction transaction = entities.Database.BeginTransaction();
                try
                {
                    entities.sp_approveFarmer(farmer.fId, farmer.ApprovedBy, farmer.ApprovedDate, farmer.fPassword, farmer.fEmailId);
                    entities.SaveChanges();
                    EmailModel mail = new EmailModel();
                    mail.to = farmer.fEmailId;
                    mail.subject = "Account Activated";
                    mail.body = "username:" + farmer.fEmailId + "\n password:" + farmer.fPassword;
                    HttpClient http = new HttpClient();
                    http.BaseAddress = new Uri("http://localhost:61674/api/Email");
                    var consumewebApi = http.PostAsJsonAsync<EmailModel>("email", mail);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Not able to approve the user");

                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            [HttpPost]
            [Route("api/admin/approvebidder")]
            public HttpResponseMessage Post(tblBidder bidder)
            {
                DbContextTransaction transaction = entities.Database.BeginTransaction();
                try
                {
                    entities.sp_approveBidder(bidder.bId, bidder.ApprovedBy, bidder.ApprovedDate, bidder.bPassword, bidder.bEmailId);
                    entities.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Not able to approve the user");

                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            [HttpPost]
            [Route("api/admin/approvebid")]
            public HttpResponseMessage Post(tblBid bid)
            {
            DbContextTransaction transaction = entities.Database.BeginTransaction();
            try
            {
                entities.sp_approveBid(bid.bId, bid.CropId);
                entities.SaveChanges();
                tblCropForSale crop = entities.tblCropForSales.Where(c => c.CropId == bid.CropId).FirstOrDefault();
                tblFarmer farmer = entities.tblFarmers.Where(f => f.fId == crop.FarmerId).FirstOrDefault();
                EmailModel mail = new EmailModel();
                mail.to = farmer.fEmailId;
                mail.subject = "Crop Sold ";
                mail.body = "Your crop sold id" + bid.CropId + "\t of type\t" + crop.CropType + "is sold for  amount " +
                    bid.BidAmount + "Rs/-;\n money will be credited after crop is shipped";
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri("http://localhost:61674/api/Email");
                var consumewebApi = http.PostAsJsonAsync<EmailModel>("email", mail);
                //Sending confirmation to bidder
                EmailModel bidderMail = new EmailModel();
                tblBidder bidder = entities.tblBidders.Where(b => bid.BidderId == b.bId).FirstOrDefault();
                bidderMail.to = bidder.bEmailId;
                bidderMail.subject = "Congrats..! Your Bid Cinfirmed";
                bidderMail.body = "your bid is confirmed for crop id" + bid.CropId + "with Amount Of" + bid.BidAmount + "<button>MakePaymet</button>";

                var consumewebApi2 = http.PostAsJsonAsync<EmailModel>("email", bidderMail);
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Something went Wrong Try to bid again");

            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
            [HttpPost]
            [Route("api/admin/approveSaleData")]
            public HttpResponseMessage Post(tblCropForSale sale)
            {
                DbContextTransaction transaction = entities.Database.BeginTransaction();
                try
                {
                    entities.sp_approvesale(sale.CropId, sale.CropType, sale.CropName);
                    entities.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Not able to approve the user");

                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            [HttpGet]
            [Route("api/admin/getCliamData")]
            public HttpResponseMessage GetClaimData()
            {
                List<sp_getClaimInfo_Result> results = entities.sp_getClaimInfo().ToList();
                if (results.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No ClaimData Found");
                }
                return Request.CreateResponse<IEnumerable<sp_getClaimInfo_Result>>(HttpStatusCode.OK, results);
            }
        [HttpPost]
        [Route("api/admin/approveclaim")]
        public HttpResponseMessage approveclaim(DateTime dateofloss, tblInsurance insurance)
        {
            DbContextTransaction transaction = entities.Database.BeginTransaction();
            try
            {

                var result = entities.sp_validateClaim(insurance.DateofApplication, dateofloss, insurance.FarmerId, insurance.CropType).FirstOrDefault();
                entities.sp_updateClaim(Convert.ToInt32(insurance.InsuranceApplicationId), Convert.ToInt32(result));
                entities.SaveChanges();
                transaction.Commit();
                tblFarmer farmer = entities.tblFarmers.Where(fa => fa.fId == insurance.FarmerId).FirstOrDefault();
                if (result == 1)
                {
                    EmailModel email = new EmailModel();
                    HttpClient http = new HttpClient();
                    http.BaseAddress = new Uri("http://localhost:61674/api/Email");

                    email.to = farmer.fEmailId;
                    email.subject = "Insurace Claim approved";
                    email.body = "your policy number of" + insurance.InsuranceApplicationId + "\t is approved Money will be credited in you account number:" + farmer.fAccountNo;

                    var consumewebApi = http.PostAsJsonAsync<EmailModel>("email", email);
                }
                else
                {
                    EmailModel email = new EmailModel();
                    HttpClient http = new HttpClient();
                    http.BaseAddress = new Uri("http://localhost:61674/api/Email");
                    email.to = farmer.fEmailId;

                    email.subject = "Insurace Claim NOT approved";
                    email.body = "your policy number of" + insurance.InsuranceApplicationId + "\t Is Expired, you are not allowed to CLAIM";
                    var consumewebApi = http.PostAsJsonAsync<EmailModel>("email", email);
                }
                

            }
            catch (Exception e)
            {
                transaction.Rollback();
                return Request.CreateErrorResponse(HttpStatusCode.Ambiguous, e.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
            [HttpPost]
            [Route("api/admin/rejectFarmer")]
            public HttpResponseMessage RejectFarmer(string message, tblFarmer farmer)
            {
                try
                {
                    EmailModel mail = new EmailModel();
                    mail.to = farmer.fEmailId;
                    mail.subject = "Application Rejected";
                    mail.body = "Your account not approved because of follwing reasons" + message+"Kindly register again";
                    HttpClient http = new HttpClient();
                    http.BaseAddress = new Uri("http://localhost:61674/api/Email");
                    var consumewebApi = http.PostAsJsonAsync<EmailModel>("email", mail);
                     entities.Entry(farmer).State = System.Data.Entity.EntityState.Deleted;
                     entities.SaveChanges();

                }
                catch (Exception)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Email Not Found ");
                }
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            [HttpPost]
            [Route("api/admin/rejectBidder")]
            public HttpResponseMessage RejectBidder(string message, tblBidder bidder)
            {
                try
                {
                    EmailModel mail = new EmailModel();
                    mail.to = bidder.bEmailId;
                    mail.subject = "Application Rejected";
                    mail.body = "Your account not approved because of follwing reason. " + message+"kindly register again";
                    HttpClient http = new HttpClient();
                    http.BaseAddress = new Uri("http://localhost:61674/api/Email");
                    var consumewebApi = http.PostAsJsonAsync<EmailModel>("email", mail);
                   entities.Entry(bidder).State = System.Data.Entity.EntityState.Deleted;
                   entities.SaveChanges();

                 }
                catch (Exception)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Email Not Found ");
                }
                return Request.CreateResponse(HttpStatusCode.OK);

            }

        }
    }
