using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using WebProgramlamaOdev2.Migrations;
using WebProgramlamaOdev2.Models;

namespace WebProgramlamaOdev2.Controllers
{
    public class UrunController : Controller
    {
        OdevContext db = new OdevContext();
        public IActionResult UrunIndex()
        {
            var model = db.Urunler.ToList();
            return View(model);
        }
        public IActionResult UrunListeleAdmin()
        {
            var model = db.Urunler.ToList();
            return View(model);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Urunler urun)
        {
            if (ModelState.IsValid)
            {
                db.Add(urun);
                db.SaveChanges();
                return RedirectToAction(nameof(UrunListeleAdmin));
            }
            return View();
        }
        public IActionResult Edit(int Id)
        {
            var std = db.Urunler.FirstOrDefault(x => x.Id == Id);
            return View(std);
        }
        [HttpPost]
        public IActionResult Edit(Urunler Urun) 
        {
            var urunedit = db.Urunler.FirstOrDefault(x=>x.Id == Urun.Id);
            if (urunedit != null)
            {
                db.Urunler.Remove(urunedit);
                db.Urunler.Add(Urun);
                db.SaveChanges();
            }
            return RedirectToAction("UrunListeleAdmin");
        }
        public IActionResult Delete(int Id)
        {
            var std = db.Urunler.FirstOrDefault(x => x.Id == Id);

            return View(std);
        }
        [HttpPost]
        public IActionResult Delete(Urunler urun)
        {
            var std = db.Urunler.FirstOrDefault(x => x.Id == urun.Id);
            if (std != null)
            {
                db.Urunler.Remove(std);
                db.SaveChanges();
            }
            return RedirectToAction("UrunListeleAdmin");

        }
        public IActionResult Details(int Id)
        {
            var std = db.Urunler.FirstOrDefault(x => x.Id == Id);
            return View(std);
        }
    }
}
