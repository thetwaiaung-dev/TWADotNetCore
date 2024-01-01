using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TWADotNetCore.MVC.Models;

namespace TWADotNetCore.MVC.Services
{
    public class ReportService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReportService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void CreateCell(IRow currentRow, int cellIndex, string value, HSSFCellStyle cellStyle)
        {
            ICell cell = currentRow.CreateCell(cellIndex);
            cell.SetCellValue(value);
            cell.CellStyle = cellStyle;
        }

        public void CreateBlogExcel(List<BlogModel> blogs)
        {
            HSSFWorkbook workBook = new HSSFWorkbook();
            HSSFFont font = (HSSFFont)workBook.CreateFont();
            font.FontHeightInPoints = 11;
            font.FontName = "Tahoma";

            HSSFCellStyle borderedCellStyle = (HSSFCellStyle)workBook.CreateCellStyle();
            borderedCellStyle.SetFont(font);
            borderedCellStyle.BorderLeft = BorderStyle.Medium;
            borderedCellStyle.BorderTop = BorderStyle.Medium;
            borderedCellStyle.BorderRight = BorderStyle.Medium;
            borderedCellStyle.BorderBottom = BorderStyle.Medium;
            borderedCellStyle.VerticalAlignment = VerticalAlignment.Center;

            ISheet sheet = workBook.CreateSheet("BlogReport");

            //create The Headers of the excel
            IRow headerRow = sheet.CreateRow(0);

            //create the actual cell
            CreateCell(headerRow, 0, "Blog Id", borderedCellStyle);
            CreateCell(headerRow, 1, "Blog Title", borderedCellStyle);
            CreateCell(headerRow, 2, "Blog Author", borderedCellStyle);
            CreateCell(headerRow, 3, "Blog Content", borderedCellStyle);

            //this where the data row starts from 
            int rowIndex = 1;

            foreach (BlogModel blog in blogs)
            {
                //creating the current data row
                IRow currentRow = sheet.CreateRow(rowIndex);
                CreateCell(currentRow, 0, blog.Blog_Id.ToString(), borderedCellStyle);
                CreateCell(currentRow, 1, blog.Blog_Title, borderedCellStyle);
                CreateCell(currentRow, 2, blog.Blog_Author, borderedCellStyle);
                CreateCell(currentRow, 3, blog.Blog_Content, borderedCellStyle);

                rowIndex++;
            }

            //auto sized all the affected columns
            int lastColumn = sheet.GetRow(0).LastCellNum;
            for (int i = 0; i <= lastColumn; i++)
            {
                sheet.AutoSizeColumn(i);
                GC.Collect();
            }

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "reports/blogReport_" + Guid.NewGuid() + ".xls");
            using (var fileData = new FileStream(path, FileMode.Create))
            {
                workBook.Write(fileData);
            }

        }

        public async Task<IWorkbook> ExportBlogExcel(List<BlogModel> blogs)
        {
            IWorkbook workBook = new XSSFWorkbook();
            ISheet sheet = workBook.CreateSheet("Blog Report");

            XSSFFont font = (XSSFFont)workBook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;

            var titleRow = sheet.CreateRow(0);
            titleRow.Height = 800;
            var titlecol = titleRow.CreateCell(0);
            titlecol.SetCellValue("Blog Report");
            titlecol.CellStyle.WrapText = true;
            titlecol.CellStyle.Alignment = HorizontalAlignment.Center;
            titlecol.CellStyle.VerticalAlignment = VerticalAlignment.Center;
            titlecol.CellStyle.SetFont(font);

            var mergeTitle = new CellRangeAddress(0, 0, 0, 3);
            sheet.AddMergedRegion(mergeTitle);

            var headerRow = sheet.CreateRow(1);
            headerRow.Height = 500;

            int count = 0;
            foreach (string header in new string[] { "Blog Id", "Blog Title", "Blog Author", "Blog Content" })
            {
                var col = headerRow.CreateCell(count);
                col.SetCellValue(header);
                ICellStyle fCellStyle = workBook.CreateCellStyle();
                fCellStyle.FillForegroundColor = IndexedColors.Yellow.Index;
                fCellStyle.FillPattern = FillPattern.SolidForeground;
                col.CellStyle = fCellStyle;

                count++;
            }

            int rowIndex = 2;
            foreach (var blog in blogs)
            {
                var row = sheet.CreateRow(rowIndex);

                row.CreateCell(0).SetCellValue(blog.Blog_Id);
                row.CreateCell(1).SetCellValue(blog.Blog_Title);
                row.CreateCell(2).SetCellValue(blog.Blog_Author);
                row.CreateCell(3).SetCellValue(blog.Blog_Content);

                rowIndex++;
            }

            return workBook;
        }

        public void WriteExcelToResponse(IWorkbook book, HttpContext httpContext, string templateName)
        {
            var response = httpContext.Response;
            response.ContentType = "application/vnd.ms-excel";
            if (!string.IsNullOrEmpty(templateName))
            {
                var contentDisposition = new Microsoft.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                contentDisposition.SetHttpFileName(templateName);
                response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();
            }
            book.Write(response.Body);
        }
    }
}
