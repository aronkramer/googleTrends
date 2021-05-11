using BillTracker.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.IO;

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
                sheet.Column(2).Width = 10;
                sheet.Column(3).Width = 30;
                sheet.Column(4).Width = 20;

                int serialNumber = 1;               

                foreach (TopKeywordViewModels kw in topKeywords)
                {
                    cell = sheet.Cells[rowIndex, 1];
                    cell.Value = serialNumber++;

                    cell = sheet.Cells[rowIndex, 2];
                    cell.Value = kw.SKU;

                    cell = sheet.Cells[rowIndex, 3];
                    cell.Value = kw.Asin;

                    cell = sheet.Cells[rowIndex, 4];
                    cell.Value = kw.Keyword;

                    rowIndex = rowIndex + 1;
                }
                return excelPackage.GetAsByteArray();
            }
        }
    }
}