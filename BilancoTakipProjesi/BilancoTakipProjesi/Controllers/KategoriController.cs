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
    public class KategoriController : Controller
    {
        private readonly UygulamaDbContext _context;

        public KategoriController(UygulamaDbContext context)
        {
            _context = context;
        }

        // GET: Kategori
        public async Task<IActionResult> Index()
        {
              return _context.Kategoris != null ? 
                          View(await _context.Kategoris.ToListAsync()) :
                          Problem("Entity set 'UygulamaDbContext.Kategoris'  is null.");
        }


        // GET: Kategori/EkleYadaGuncelle
        public IActionResult EkleYadaGuncelle(int id=0)
        {
            if (id == 0)
            {
                return View(new Kategori());
            }
            else
            {
                return View(_context.Kategoris.Find(id));    
            }
            
        }

        // POST: Kategori/EkleYadaGuncelle
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EkleYadaGuncelle([Bind("KategoriId,Baslik,Icon,KategoriTipi")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                if (kategori.KategoriId == 0) // KategoriId = 0 ise Kategori yoktur, kategoriyi ekle
                {
                    _context.Add(kategori);
                }
                else // KategoriId = 0 değilse Kategori var demektir, kategorinin değerini güncelle.
                {
                    _context.Update(kategori); 
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategori);
        }



        // POST: Kategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kategoris == null)
            {
                return Problem("Entity set 'UygulamaDbContext.Kategoris'  is null.");
            }
            var kategori = await _context.Kategoris.FindAsync(id);
            if (kategori != null)
            {
                _context.Kategoris.Remove(kategori);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
