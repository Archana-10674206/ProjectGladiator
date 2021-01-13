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
    public class ResetController : ApiController
    {
        
            dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();

            [HttpGet]
            public IEnumerable<tblUser> GetAllUsers()
            {
                return entities.tblUsers.ToList();
            }

            [HttpGet]
            public HttpResponseMessage Reset_Password(string email, string password)
            {
                tblUser user = entities.tblUsers.Where(f => f.EmailId == email).FirstOrDefault();

                try
                {
                    if (user != null)
                    {
                        user.Password = Pass.ConvertToEncrypt(password);
                        entities.SaveChanges();

                        if (user.fId != null)
                        {
                            tblFarmer farmer = entities.tblFarmers.Where(f => f.fId == user.fId).FirstOrDefault();
                            farmer.fPassword = Pass.ConvertToEncrypt(password);
                            entities.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, "Password updated");
                        }
                        else if (user.bId != null)
                        {
                            tblBidder bidder = entities.tblBidders.Where(b => b.bId == user.bId).FirstOrDefault();
                            bidder.bPassword = Pass.ConvertToEncrypt(password);
                            entities.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, "Password updated");

                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, "Password updated for admin");
                        }

                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "User not found");
                    }
                }
                catch (Exception)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Password not updated");
                }


                // return Request.CreateResponse(HttpStatusCode.NotFound, "Password updated for admin");

            }

        }
    }

