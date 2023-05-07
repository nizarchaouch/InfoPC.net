using InfoPC.Models.InfoPC;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoPC.Controllers
{
    public class InfoPcController : Controller
    {
        private readonly InfoPcDbContext _context;
        
        public InfoPcController(InfoPcDbContext context)
        {
            _context = context;
        }
        // GET: InfoPcControleur
        public ActionResult Index()
        {
            return View(_context.Marques.ToList());
        }

        
        public IActionResult Search(string name, string nationality)
        {

            var marques = _context.Marques.ToList();
            ViewBag.Name = marques.Select(m => m.Name).ToList();
            ViewBag.Nationality = nationality;

            if (!string.IsNullOrEmpty(name) && name != "All")
            {
                marques = marques.Where(m => m.Name == name).ToList();
            }

            if (!string.IsNullOrEmpty(nationality))
            {
                marques = marques.Where(m => m.Nationality.Contains(nationality)).ToList();
            }
            if (name == "ALL")
            {
                marques = _context.Marques.ToList();
            }
            if (name == "ALL" && !string.IsNullOrEmpty(nationality))
            {
                marques = marques.Where(m => m.Nationality.Contains(nationality)).ToList();
            }
            return View("Search", marques);
        }


        // GET: InfoPcControleur/Details/5
        public ActionResult Details(int id)
        {
            return View(_context.Marques.Find(id));
        }

        // GET: InfoPcControleur/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InfoPcControleur/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection , Marque m)
        {
            try
            {
                _context.Marques.Add(m);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
       
        // GET: InfoPcControleur/Edit/5
        public ActionResult Edit(int id)
        {
            Marque marque = _context.Marques.Find(id);
            return View(marque);
        }

        // POST: InfoPcControleur/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Marque m)
        {
            try
            {
                _context.Marques.Update(m);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult MarqueAndTheirPc()
        {
            var InfoPcDbContext = _context.Ordinateurs.ToList();
            return View(_context.Marques.ToList());
        }

        // GET: InfoPcControleur/Delete/5
        public ActionResult Delete(int id)
        {
            Marque marque = _context.Marques.Find(id);
            return View(marque);
        }

        // POST: InfoPcControleur/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection, Marque m)
        {
            try
            {
                _context.Marques.Remove(m);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
