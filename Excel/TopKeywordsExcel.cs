using BillTracker.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BillTracker.Excel
{
    public class TopKeywordsExcel
    {
        int rowIndex = 1;
        ExcelRange cell;

        public byte[] GenerateExcel(List<TopKeywordViewModels> topKeywords)
        {
            using (var excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "Platinum";
                excelPackage.Workbook.Properties.Title = "Top Keywords";
                var sheet = excelPackage.Workbook.Worksheets.Add("Top keywords excel");
                sheet.Name = "Top keywords for SKU'S";
                sheet.Cells["A1:C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells["A1:C1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.CadetBlue);
                sheet.Column(1).Width = 30;
                sheet.Column(2).Width = 15;
                sheet.Column(3).Width = 20;

                cell = sheet.Cells[rowIndex, 1];
                cell.Value = "SKU";

                cell = sheet.Cells[rowIndex, 2];
                cell.Value = "Asin";

                cell = sheet.Cells[rowIndex, 3];
                cell.Value = "Keyword";

                rowIndex = 2;
                foreach (TopKeywordViewModels kw in topKeywords)
                {
                    cell = sheet.Cells[rowIndex, 1];
                    cell.Value = kw.SKU;

                    cell = sheet.Cells[rowIndex, 2];
                    cell.Value = kw.Asin;

                    cell = sheet.Cells[rowIndex, 3];
                    cell.Value = kw.Keyword;

                    rowIndex = rowIndex + 1;
                }
                return excelPackage.GetAsByteArray();
            }
        }
                
    }
}