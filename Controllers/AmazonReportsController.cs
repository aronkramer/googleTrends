using BillTracker.DataBase;
using BillTracker.Helpers;
using BillTracker.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BillTracker.Controllers
{
    [Authorize]
    public class AmazonReportsController : Controller
    {
        [HttpPost]
        public async Task ImportCampaignReport(HttpPostedFileBase excelFile)
        {
            string excel = Server.MapPath("~/CampaignReport/" + excelFile.FileName);
            // Deletes previous files before updating new keywords
            Array.ForEach(Directory.GetFiles(Server.MapPath("~/CampaignReport/")), System.IO.File.Delete);
            excelFile.SaveAs(excel);

            var campaigns = await UploadKWAsync(excel);
            var repo = new ReportsRepository();
            var camps = campaigns.ToViewModel<CampaignsReportViewModel, CampaignsReport>();
            repo.insert(camps);
        }

        private static async Task<List<CampaignsReportViewModel>> UploadKWAsync(string excel)
        {
            var file = new FileInfo(excel);
            List<CampaignsReportViewModel> kWFromExcel = await LoadExcelFile(file);
            return kWFromExcel;
        }

        private static async Task<List<CampaignsReportViewModel>> LoadExcelFile(FileInfo file)
        {
            var output = new List<CampaignsReportViewModel>();
            using (var package = new ExcelPackage(file))
            {
                await package.LoadAsync(file);
                var ws = package.Workbook.Worksheets[0];

                int row = 2;
                int col = 5;

                while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
                {
                    var t = new CampaignsReportViewModel();
                    t.Campaign = ws.Cells[row, col].Value == null ? "" : ws.Cells[row, col].Value.ToString();
                    t.Sku = ws.Cells[row, col + 2].Value == null ? null : ws.Cells[row, col + 2].Value.ToString();
                    output.Add(t);
                    row += 1;
                }
            }
            return output;
        }

    }
}