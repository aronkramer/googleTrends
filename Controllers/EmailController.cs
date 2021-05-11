using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BillTracker.Controllers
{
    public class EmailController : Controller
    {
        public void SendUpdateTrendsEmail(string email)
        {
            var test = "";
            var fromAddress = new MailAddress("message.platinumsuppliesllc@gmail.com", "PlatinumSupplies Cloud");
            var toAddress = new MailAddress("message.platinumsuppliesllc@gmail.com");


            string fromPassword = ConfigurationManager.AppSettings["Password"].ToString();
            string subject = $"Google Trends " +
                $"{TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Eastern Standard Time")}";
            string body = $"<div><a href='https://platinumsupplies.cloud/Trends/DownloadTrends'><button>Download</button></a></div>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

        }
    }
}