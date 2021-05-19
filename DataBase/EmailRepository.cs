using BillTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace BillTracker.DataBase
{
    public class EmailRepository : Repository<Emails>
    //: Repository<Emails>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public List<MailAddress> GetGoogleTrendsEmails()
        {
            var emails = DbSet.Where(d => d.GoogleTrends == true).Select(k => k.EmailAddress).ToList();
            IEnumerable<MailAddress> addresses = emails.Select(i => new MailAddress(i));

            return addresses.ToList(); 
        }
    }
}