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
    public class FarmerController : ApiController
    {
        dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();

        public IEnumerable<tblFarmer> Get()
        {
            return entities.tblFarmers.ToList();
        }

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
                var postedFile = httpRequest.Files["Aadhar"];
                var postedFile1 = httpRequest.Files["Pan"];
                var postedFile2 = httpRequest.Files["Certificate"];


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


                tblFarmer f = new tblFarmer();
                //Save to db
                using (dbFarmerScheme3Entities db = new dbFarmerScheme3Entities())
                {
                    string email = httpRequest["Email"];
                    var e = db.tblFarmers.Where(x => x.fEmailId == email).FirstOrDefault();


                    if (e == null)
                    {
                        string acc = httpRequest["AccountNo"];
                        var a = db.tblFarmers.Where(x => x.fAccountNo == acc).FirstOrDefault();
                        if (a == null)
                        {
                            try
                            {
                                f.fUserName = httpRequest["Username"];
                                f.fContactNo = httpRequest["Contact"];
                                f.fEmailId = httpRequest["Email"];
                                f.fAddress = httpRequest["Address"];
                                f.fCity = httpRequest["City"];
                                f.fState = httpRequest["State"];
                                f.fPincode = httpRequest["Pincode"];
                                f.fLandArea = Convert.ToSingle(httpRequest["LArea"]);

                                f.fLandAddress = httpRequest["LAddress"];
                                f.fLandPincode = httpRequest["LPincode"];
                                f.fAccountNo = httpRequest["AccountNo"];
                                f.fIFSCcode = httpRequest["ifsccode"];
                                f.fAadhar = filePath;
                                f.fPan = filePath1;
                                f.fCertificate = filePath2;

                                f.fPassword = Pass.ConvertToEncrypt(httpRequest["Password"]);

                                f.StatusOfFarmerDocx = "pending";

                                entities.tblFarmers.Add(f);
                                entities.SaveChanges();


                                transaction.Commit();
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data not Inserted");
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
                return Request.CreateResponse(HttpStatusCode.Created, "Successfully Registered");
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "File size should be less than 2MB");
            }
        }

    }
}

