using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers {
    public class SalesRecordsController(SalesRecordService salesRecordService) : Controller {
        private readonly SalesRecordService _salesRecordService = salesRecordService;
        public IActionResult Index() {
            return View();
        }
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate) {

            if (!minDate.HasValue) minDate = new DateTime(2018, 01, 01);
            ViewData["MinDate"] = minDate.Value.ToString("yyyy-MM-dd");

            if (!maxDate.HasValue) maxDate = new DateTime(2018, 12, 31);
            ViewData["MaxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var sales = await _salesRecordService.FindByDateAsync(minDate, maxDate);

            return View(sales);
        }
        public async Task<IActionResult> GroupSearch(DateTime? minDate, DateTime? maxDate) {
            if (!minDate.HasValue) minDate = new DateTime(2018, 01, 01);
            ViewData["MinDate"] = minDate.Value.ToString("yyyy-MM-dd");

            if (!maxDate.HasValue) maxDate = new DateTime(2018, 12, 31);
            ViewData["MaxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var sales = await _salesRecordService.FindByDateGroupAsync(minDate, maxDate);

            return View(sales);
        }

    }
}
