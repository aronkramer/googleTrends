using BillTracker.DataBase;
using BillTracker.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillTracker.Excel
{
    public class TrendsExcel
    {
        int rowIndex = 2;
        ExcelRange cell;

        public byte[] GenerateExcel(List<TrendsViewModels> trends)
        {
            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "PlatinumSupplies";
                excelPackage.Workbook.Properties.Title = "Google Trends";
                var sheet = excelPackage.Workbook.Worksheets.Add("Trends excel");
                var repo = new TrendsRepository();
                var date = repo.GetLatestUpdateTime();
                sheet.Name = $"Trends for {date}";
                sheet.Cells["A1:D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells["A1:D1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.CadetBlue);
                sheet.Column(1).Width = 20;
                sheet.Column(2).Width = 20;
                sheet.Column(3).Width = 15;
                sheet.Column(4).Width = 10;


                cell = sheet.Cells[1, 1];
                cell.Value = "Product";

                cell = sheet.Cells[1, 2];
                cell.Value = "Keyword";

                cell = sheet.Cells[1, 3];
                cell.Value = "Date";

                cell = sheet.Cells[1, 4];
                cell.Value = "Ranking";

                foreach (TrendsViewModels st in trends)
                {
                    cell = sheet.Cells[rowIndex, 1];
                    cell.Value = st.Product;

                    cell = sheet.Cells[rowIndex, 2];
                    cell.Value = st.Keyword;

                    cell = sheet.Cells[rowIndex, 3];
                    cell.Value = st.Date;

                    cell = sheet.Cells[rowIndex, 4];
                    cell.Value = st.Ranking;

                    rowIndex = rowIndex + 1;
                }
                return excelPackage.GetAsByteArray();
            }
        }
    }
}