using BillTracker.DataBase;
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
        public void SendUpdateTrendsEmail()
        {
            var fromAddress = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(),
                "PlatinumSupplies Cloud");

            // TODO need to change this to list of emails
            var dataemails = new EmailRepository().GetAllEmails();

            var emails = new List<MailAddress>();
            var toAddress = dataemails;
            emails.Add(toAddress);
            // until here

            string fromPassword = ConfigurationManager.AppSettings["Password"].ToString();
            var timeInNJ = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Eastern Standard Time");

            string subject = $"Google Trends {timeInNJ}";
            string body = $@"Google Trends latest update has been completed at {timeInNJ}.
                            <div>
                                You can download the trends from the website <a href='https://platinumsupplies.cloud/Trends/Index'>here</a>
                                or use the button below.
                            </div>
                    <div>
                        <button href='https://platinumsupplies.cloud/Trends/DownloadTrends'
                            style = 'display: inline-block; background-color: #4e73df; border-radius: 10px; border: 4px double #cccccc;
                            text-align: center; font-size: 28px; padding: 20px; length: 100px; cursor: pointer; margin: 5px; color:white;'>
                            Download here!
                        </button>
                    </div>";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            foreach (var email in emails)
            {
                using (var message = new MailMessage(fromAddress, email)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}