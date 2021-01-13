using schemeforfarmer_Project_VS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;

namespace schemeforfarmer_Project_VS.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class sendemailController : ApiController
    {


        dbFarmerScheme3Entities entities = new dbFarmerScheme3Entities();

        [HttpGet]
        public HttpResponseMessage forget_Password_SendMail(string email)
        {

            var user = entities.tblUsers.Where(f => f.EmailId == email).FirstOrDefault();


            if (user == null)
            {

                var farmer = entities.tblFarmers.Where(f => f.fEmailId == email).FirstOrDefault();
                var bidder = entities.tblBidders.Where(b => b.bEmailId == email).FirstOrDefault();

                if (farmer == null && bidder == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid User");
                else
                {
                    if (farmer != null)
                    {
                        string to = farmer.fEmailId;

                        MailMessage mm = new MailMessage();
                        mm.From = new MailAddress("farmerfriend886@gmail.com");
                        mm.To.Add(to);
                        mm.Subject = "RESET PASSWORD";
                        mm.Body = " <a href='http://localhost:4200/resetpassword'> Reset password</a>";
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.UseDefaultCredentials = true;
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.Credentials = new System.Net.NetworkCredential("farmerfriend886@gmail.com", "farmerfriend");
                        smtp.Send(mm);
                        return Request.CreateResponse(HttpStatusCode.OK, "Email sent");

                    }
                    else if (bidder != null)
                    {
                        string to = bidder.bEmailId;

                        MailMessage mm = new MailMessage();
                        mm.From = new MailAddress("farmerfriend886@gmail.com");
                        mm.To.Add(to);
                        mm.Subject = "RESET FORGOT PASSWORD";
                        mm.Body = " <a href='http://localhost:4200/resetpassword '> Reset password</a>";
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.UseDefaultCredentials = true;
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.Credentials = new System.Net.NetworkCredential("farmerfriend886@gmail.com", "farmerfriend");
                        smtp.Send(mm);
                        return Request.CreateResponse(HttpStatusCode.OK, "Email sent");
                    }
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not in tblUser");

            }
            else
            {

                string to = user.EmailId;

                MailMessage mm = new MailMessage();
                mm.From = new MailAddress("farmerfriend886@gmail.com");
                mm.To.Add(to);
                mm.Subject = "RESET PASSWORD";
                mm.Body = " <a href='http://localhost:4200/resetpassword '> Reset password</a>";
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.UseDefaultCredentials = true;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("farmerfriend886@gmail.com", "farmerfriend");
                smtp.Send(mm);
                return Request.CreateResponse(HttpStatusCode.OK, "Email sent");


                //    if(user.fId != null )
                //    {
                //        message.Body = "Resetting <a href= 'http:/localhost:4200/ResetPassword?id='" + user.fId + "&token"+ token +"'> Click! </a>";
                //    }
                //    else if(user.bId != null)
                //    {
                //        message.Body = "Resetting <a href= 'http:/localhost:4200/ResetPassword?id='" + user.bId + "&token" + token + "'> Click! </a>";
                //    }
                //    else
                //    {
                //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Fid & bId both are null");
                //    }



            }
            //return Request.CreateResponse(HttpStatusCode.OK, "Email Sent");       
        }



    
        }
    }


