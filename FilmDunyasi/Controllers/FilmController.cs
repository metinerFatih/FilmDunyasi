using FilmDunyasi.Context;
using FilmDunyasi.Data;
using Microsoft.AspNetCore.Mvc;

namespace FilmDunyasi.Controllers
{
    public class FilmController : Controller
    {
        private readonly UygulamaDbContext _db;
        public FilmController(UygulamaDbContext db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            var filmler = _db.Filmler.ToList();
            return View(filmler);
        }
        public IActionResult Ekle()
        {
            ViewBag.Turler = _db.Turler.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Film film)
        {
            if(ModelState.IsValid)
            {
                _db.Filmler.Add(film);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Turler = _db.Turler.ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Duzenle(int id)
        {
            var film = _db.Filmler.SingleOrDefault(x => x.Id == id);
            if (film == null)
                return NotFound();

            return View(film);
        }

        [HttpPost]
        public IActionResult Duzenle(Film film)
        {
            if (ModelState.IsValid)
            {
                _db.Filmler.Update(film);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Turler = _db.Turler.ToList();
            return View();
        }

        public IActionResult Sil(int id)
        {
            var film = _db.Filmler.SingleOrDefault(x => x.Id == id);
            if (film == null)
                return NotFound();

            return View(film);
        }

        [HttpPost]
        public IActionResult SilOnay(int id)
        {
            var film = _db.Filmler.SingleOrDefault(x => x.Id == id);
            if (film == null)
                return NotFound();

            _db.Filmler.Remove(film);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
