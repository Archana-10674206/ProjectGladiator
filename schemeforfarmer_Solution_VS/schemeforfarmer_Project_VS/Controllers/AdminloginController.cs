using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using schemeforfarmer_Project_VS.Models;

namespace schemeforfarmer_Project_VS.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AdminloginController : ApiController
    {
        dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();
        public HttpResponseMessage post(Logindetails obj)
        {
            if (entities.tblUsers.Where(u => u.EmailId == obj.EmailId).FirstOrDefault() != null)
            {
                tblUser uobj = entities.tblUsers.Where(uo => uo.EmailId == obj.EmailId).FirstOrDefault();
                if (obj.Password == uobj.Password) 
                {
                    obj.UserType = "admin";
                    return Request.CreateResponse<Logindetails>(HttpStatusCode.OK, obj);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "incorrect password");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "you are not authorized");
            }
        }
    }
}
