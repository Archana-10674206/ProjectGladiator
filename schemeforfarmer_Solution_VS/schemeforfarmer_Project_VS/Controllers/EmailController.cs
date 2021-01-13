using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;
using schemeforfarmer_Project_VS.Models;

namespace schemeforfarmer_Project_VS.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class EmailController : ApiController
    {
        
        public IHttpActionResult sendEmail(EmailModel email)
        {
            string subject = email.subject;
            string body = email.body;
            string to = email.to;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("farmerfriend886@gmail.com");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("farmerfriend886@gmail.com", "farmerfriend");
            smtp.Send(mail);
            return Ok();
        }
    }
}

