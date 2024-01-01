using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using TWADotNetCore.MVC.Models;
using TWADotNetCore.MVC.Services;

namespace TWADotNetCore.MVC.Controllers
{
    public class ReportController : Controller
    {
        private readonly BlogService _blogService;
        private readonly ReportService _reportService;

        public ReportController(BlogService blogService, ReportService reportService)
        {
            _blogService = blogService;
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            List<BlogModel> blogList = _blogService.GetAll();
            return View(blogList);
        }

        public IActionResult Create()
        {
            List<BlogModel> blogList = _blogService.GetAll();
            _reportService.CreateBlogExcel(blogList);
            return View("Index", blogList);
        }

        public async Task Export()
        {
            List<BlogModel> blogList = _blogService.GetAll();
            IWorkbook workBook = await _reportService.ExportBlogExcel(blogList);

            _reportService.WriteExcelToResponse(workBook, HttpContext, "test.xlsx");
        }
    }
}
