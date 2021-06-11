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
        public async Task<ActionResult> ImportCampaignReport(HttpPostedFileBase excelFile)
        {
            string excel = Server.MapPath("~/CampaignReport/" + excelFile.FileName);
            // Deletes previous files before updating new keywords
            Array.ForEach(Directory.GetFiles(Server.MapPath("~/CampaignReport/")), System.IO.File.Delete);
            excelFile.SaveAs(excel);

            var campaigns = await UploadCampaignAsync(excel);
            var repo = new CampaignRepository();
            var camps = campaigns.ToViewModel<CampaignsReportViewModel, CampaignsReport>();
            repo.ResetCampaignReport(camps);
            return RedirectToAction("Index", "Trends");
        }

        private static async Task<List<CampaignsReportViewModel>> UploadCampaignAsync(string excel)
        {
            var file = new FileInfo(excel);
            List<CampaignsReportViewModel> CampaginFromExcel = await LoadExcelFile(file);
            return CampaginFromExcel;
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
                    var c = new CampaignsReportViewModel();
                    c.Campaign = ws.Cells[row, col].Value == null ? "" : ws.Cells[row, col].Value.ToString();
                    c.Sku = ws.Cells[row, col + 2].Value == null ? null : ws.Cells[row, col + 2].Value.ToString();
                    output.Add(c);
                    row += 1;
                }
            }
            return output;
        }


        [HttpPost]
        public async Task<ActionResult> ImportSearchTerm(HttpPostedFileBase excelFile)
        {
            string excel = Server.MapPath("~/SearchReport/" + excelFile.FileName);
            // Deletes previous files before updating new keywords
            Array.ForEach(Directory.GetFiles(Server.MapPath("~/SearchReport/")), System.IO.File.Delete);
            excelFile.SaveAs(excel);

            var search = await UploadSearchTermAsync(excel);
            var repo = new SearchTermRepository();
            var searchTerm = search.ToViewModel<SearchTermReportViewModel, SearchTermReport>();
            repo.ResetSearchTermReport(searchTerm);
            return RedirectToAction("Index", "Trends");
        }

        private static async Task<List<SearchTermReportViewModel>> UploadSearchTermAsync(string excel)
        {
            var file = new FileInfo(excel);
            List<SearchTermReportViewModel> SearchTermFromExcel = await LoadSTExcelFile(file);
            return SearchTermFromExcel;
        }

        private static async Task<List<SearchTermReportViewModel>> LoadSTExcelFile(FileInfo file)
        {
            var output = new List<SearchTermReportViewModel>();
            using (var package = new ExcelPackage(file))
            {
                await package.LoadAsync(file);
                var ws = package.Workbook.Worksheets[0];

                int row = 2;
                //int col = 5;

                while (string.IsNullOrWhiteSpace(ws.Cells[row, 5].Value?.ToString()) == false)
                {
                    var c = new SearchTermReportViewModel();
                    c.Campaign = ws.Cells[row, 5].Value == null ? null : ws.Cells[row, 5].Value.ToString();
                    c.SearchTerm = ws.Cells[row, 9].Value == null ? null : ws.Cells[row, 9].Value.ToString();

                    c.Sales = decimal.Parse(ws.Cells[row, 15].Value == null ? "0" : ws.Cells[row, 15].Value.ToString());
                    c.Acos = decimal.Parse(ws.Cells[row, 16].Value == null ? "0" : ws.Cells[row, 16].Value.ToString());
                    c.Units = int.Parse(ws.Cells[row, 19].Value == null ? "0" : ws.Cells[row, 19].Value.ToString());

                    if (c.Units >= 3)
                    {
                        output.Add(c);
                    }
                    row += 1;

                }
            }
            return output;
        }


        //Done with all the actions
        [HttpPost]
        public void CleanData()
        {
            new CampaignRepository().CleanReport();

            //var repoR = new ResultRepository();
            //repoR.MergeData();
        }
    }
}