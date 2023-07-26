using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BilancoTakipProjesi.Models;

namespace BilancoTakipProjesi.Controllers
{
    public class IslemController : Controller
    {
        private readonly UygulamaDbContext _context;

        public IslemController(UygulamaDbContext context)
        {
            _context = context;
        }

        // GET: Islem
        public async Task<IActionResult> Index()
        {
            var uygulamaDbContext = _context.Islems.Include(i => i.Kategori);
            return View(await uygulamaDbContext.ToListAsync());
        }


        // GET: Islem/EkleYadaGuncelle
        public IActionResult EkleYadaGuncelle(int id = 0)
        {
            KategoriListesi();
            if (id == 0)
            {
                return View(new Islem());
            }
            else
            {
                return View(_context.Islems.Find(id));
            }
        }

        // POST: Islem/EkleYadaGuncelle
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EkleYadaGuncelle([Bind("IslemId,KategoriId,Tutar,Bilgi,Tarih")] Islem islem)
        {
            if (ModelState.IsValid)
            {
                if (islem.IslemId == 0) // IslemId = 0 ise İşlem yoktur, İşlemi ekle
                {
                    _context.Add(islem);
                }
                else // IslemId = 0 değilse İşlem var demektir, İşlemin değerini güncelle.
                {
                    _context.Update(islem);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(islem);
        }




        // POST: Islem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Islems == null)
            {
                return Problem("Entity set 'UygulamaDbContext.Islems'  is null.");
            }
            var islem = await _context.Islems.FindAsync(id);
            if (islem != null)
            {
                _context.Islems.Remove(islem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [NonAction]
        public void KategoriListesi()
        {
            var kategoriKoleksiyonu = _context.Kategoris.ToList();
            Kategori varsayılanKategori = new Kategori() { KategoriId=0, Baslik="Kategori Seciniz"};
            kategoriKoleksiyonu.Insert(0,varsayılanKategori);
            ViewBag.Kategoriler = kategoriKoleksiyonu; 
        }

    }
}
