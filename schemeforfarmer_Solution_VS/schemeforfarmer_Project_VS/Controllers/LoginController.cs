using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using schemeforfarmer_Project_VS.Models;
using System.Web.Http.Cors;

namespace schemeforfarmer_Project_VS.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private dbFarmerScheme3Entities db = new dbFarmerScheme3Entities();

        // GET: api/Login
        public IQueryable<tblUser> GettblUsers()
        {
            return db.tblUsers;
        }

        // GET: api/Login/5
        [ResponseType(typeof(tblUser))]
        public IHttpActionResult GettblUser(string id)
        {
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            return Ok(tblUser);
        }

        // PUT: api/Login/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblUser(string id, tblUser tblUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblUser.EmailId)
            {
                return BadRequest();
            }

            db.Entry(tblUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Login
        [ResponseType(typeof(tblUser))]
        public HttpResponseMessage PosttblUser(Logindetails obj)
        {
            var ff = db.tblFarmers.Where(f => f.fEmailId == obj.EmailId).FirstOrDefault();
            var bb = db.tblBidders.Where(b => b.bEmailId == obj.EmailId).FirstOrDefault();
            if (ff != null || bb != null )
            {
                 
                if (db.tblUsers.Where(u => u.EmailId==obj.EmailId).FirstOrDefault()!=null)
                {
                    tblUser uobj = db.tblUsers.Where(uo => uo.EmailId == obj.EmailId).FirstOrDefault();
                    if (obj.Password == Pass.ConvertToDecrypt(uobj.Password))
                    {
                        if (uobj.fId != null)
                        {
                            obj.fId = (int)uobj.fId;
                            var x = db.tblFarmers.Where(f => f.fId == obj.fId).FirstOrDefault();
                            obj.Username = x.fUserName;
                            obj.UserType = "Farmer";
                        }
                        if(uobj.bId!=null)
                        {
                            obj.bId = (int)uobj.bId;
                            var y = db.tblBidders.Where(b => b.bId == obj.bId).FirstOrDefault();
                            obj.Username = y.bUserName;
                            obj.UserType = "Bidder";
                        }
                        
                        return Request.CreateResponse<Logindetails>(HttpStatusCode.OK, obj);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Incorrect Password");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,"documents are not verified yet");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"You are not registered");
            }
            /*if (obj.EmailId != (db.tblUsers.Select(f => f.EmailId).FirstOrDefault()))
                {
                if (obj.UserType == "farmer")
                {
                    var fobj = db.tblFarmers.Where(f => f.fEmailId == obj.EmailId).FirstOrDefault();
                    if (fobj != null)
                    {
                        if (fobj.fPassword == obj.Password)
                        {

                            obj.fId = fobj.fId;

                            //obj.UserType = "farmer";
                            //db.tblUsers.Add(obj);
                            //db.SaveChanges();

                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Incorrect Password");
                        }
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "You are not Registered");
                    }

                }
                else if (obj.UserType == "bidder")
                {
                    var bobj = db.tblBidders.Where(b => b.bEmailId == obj.EmailId).FirstOrDefault();
                    if (bobj != null)
                    {
                        if (bobj.bPassword == obj.Password)
                        {

                            obj.fId = bobj.bId;
                            obj.Password = bobj.bPassword;
                            obj.EmailId = bobj.bEmailId;
                            obj.UserType = "bidder";
                            db.tblUsers.Add(obj);
                            db.SaveChanges();
                            return Request.CreateResponse<tblUser>(HttpStatusCode.OK, obj);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Incorrect Password");
                        }
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "You are not Registered");
                    }

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "usertype is required");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, "user already exists");
            }
            return Request.CreateResponse<tblUser>(HttpStatusCode.OK, obj);*/
        }

        // DELETE: api/Login/5
        [ResponseType(typeof(tblUser))]
        public IHttpActionResult DeletetblUser(string id)
        {
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            db.tblUsers.Remove(tblUser);
            db.SaveChanges();

            return Ok(tblUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblUserExists(string id)
        {
            return db.tblUsers.Count(e => e.EmailId == id) > 0;
        }
    }
}