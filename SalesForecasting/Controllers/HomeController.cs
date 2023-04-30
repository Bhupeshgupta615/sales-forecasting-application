using Microsoft.AspNetCore.Mvc;
using SalesForecasting.IRepository;
using SalesForecasting.Models;
using System.Diagnostics;

namespace SalesForecasting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _logger = logger;
            _homeRepository = homeRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string state,string year)
        {
            ViewBag.States = await _homeRepository.GetAllState();
            ViewBag.Years = await _homeRepository.GetYear();
            int selectedYear = 2018;
            if(!string.IsNullOrEmpty(year))
            {
                selectedYear = Convert.ToInt32(year);
            }
              
            var data =  await _homeRepository.GetYearSales(selectedYear);

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> yeargetsale(string state,string year)
        {
            
            return RedirectToAction("Index", "Home", new { state = state, year = year });
          
        }

        [HttpGet]
        public async Task<IActionResult> yearforcast(string state, string year,string increment)
        {
            ViewBag.States = await _homeRepository.GetAllState();
            ViewBag.Years = await _homeRepository.GetYear();
            int selectedYear = 2018;
            int selectedIncrement = 5;
            if (!string.IsNullOrEmpty(year))
            {
                selectedYear = Convert.ToInt32(year);
            }
            if (!string.IsNullOrEmpty(increment))
            {
                selectedIncrement = Convert.ToInt32(increment);
            }

            var data = await _homeRepository.GetForcastedYearSales(selectedYear,state,selectedIncrement);

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> getyearforcast(string state, string year,string increment)
        {

            return RedirectToAction("yearforcast", "Home", new { state = state, year = year, increment= increment });

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}