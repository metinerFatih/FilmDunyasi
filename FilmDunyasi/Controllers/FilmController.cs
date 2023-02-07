using FilmDunyasi.Context;
using FilmDunyasi.Data;
using FilmDunyasi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

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
            var filmler = _db.Filmler.Include(x => x.Turler).ToList();

            return View(filmler);
        }
        public IActionResult Ekle()
        {
            ViewBag.Turler = _db.Turler.ToList();
            var vm = new FilmEkleViewModel(); //Ekleme sayfamıza bir model gönderip bu konuda hata almamızı engellemek için.
            return View(vm);
        }
        [HttpPost]
        public IActionResult Ekle(FilmEkleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Film film = new Film()
                {
                    Ad = vm.Ad,
                    Puan = vm.Puan!.Value,
                    Yil = vm.Yil,
                    Turler = _db.Turler.Where(x => vm.Turler.Contains(x.Id)).ToList()
                };
                _db.Filmler.Add(film);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Turler = _db.Turler.ToList();
            return View(vm);// hata aldığımızda tiklediğimiz
        }

        [HttpGet]
        public IActionResult Duzenle(int id)
        {
            ViewBag.Turler = _db.Turler.ToList();
            Film? film = _db.Filmler.SingleOrDefault(x => x.Id == id);
            if (film == null)
                return NotFound();
            _db.Entry(film).Collection(x => x.Turler).Load(); // _db.Filmler.Include(x => x.Turler).ToList(); ile aynı işlevi görür.

            var vm = new FilmDuzenleViewModel()
            {
                Id = film.Id,
                Ad = film.Ad,
                Yil = film.Yil,
                Puan = film.Puan,
                Turler = film.Turler.Select(x => x.Id).ToArray()
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult Duzenle(FilmDuzenleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var film = _db.Filmler.Include(x => x.Turler).SingleOrDefault(x => x.Id == vm.Id);
                if (film == null)
                    return NotFound();

                film.Id = vm.Id;
                film.Yil = vm.Yil;
                film.Puan = vm.Puan!.Value;
                film.Ad = vm.Ad;
                film.Turler.Clear();
                film.Turler = _db.Turler.Where(x => vm.Turler.Contains(x.Id)).ToList();

                _db.Filmler.Update(film);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Turler = _db.Turler.ToList();
            return View(vm);
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
