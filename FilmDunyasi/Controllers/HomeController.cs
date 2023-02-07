using FilmDunyasi.Context;
using FilmDunyasi.Data;
using FilmDunyasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FilmDunyasi.Controllers
{
    public class HomeController : Controller
    {
        private readonly UygulamaDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UygulamaDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index(int? turId)
        {
            IQueryable<Film> filmler = _db.Filmler.Include(x => x.Turler);
            
            if (turId != null)
            {
                // öyle filmleri getir ki;
                // o filmin türlerinin en az birisinin id'si turId'ye eşit olsun.
                filmler = filmler.Where(x => x.Turler.Any(t => t.Id == turId));
            }
            ViewBag.Turler = _db.Turler.Select(x => new SelectListItem()
            {
                Text = x.Ad,
                Value = x.Id.ToString()
            }).ToList();

            var vm = new HomeViewModel()
            {
                Filmler = filmler.ToList(),// ToList diyinceye kadar veritabanına sorgu gitmez.
                TurId = turId
            };

            return View(vm);
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