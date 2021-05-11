using Microsoft.Owin;
using Owin;
using Hangfire;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Mvc.Routing;

[assembly: OwinStartupAttribute(typeof(BillTracker.Startup))]
namespace BillTracker
{
    public partial class Startup
    {
        private const string NameOrConnectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=PlatinumSuppliesLLC;Integrated Security=True";
       
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration
             .UseSqlServerStorage(NameOrConnectionString);

            app.UseHangfireDashboard();
            RecurringJob.AddOrUpdate(() => PrintJob.Print(), Cron.Weekly(DayOfWeek.Sunday));            
            //RecurringJob.AddOrUpdate(() => PrintJob.Print(), Cron.Minutely());
            app.UseHangfireServer();
        }
    }
}
