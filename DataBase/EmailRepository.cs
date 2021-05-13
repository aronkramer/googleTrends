using BillTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace BillTracker.DataBase
{
    public class EmailRepository 
        //: Repository<Emails>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public MailAddress GetAllEmails()
        {
            return new MailAddress("ahron@platinumsuppliesllc.com"); 
        }
    }
}