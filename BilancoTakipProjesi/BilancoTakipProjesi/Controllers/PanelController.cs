using BilancoTakipProjesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
using System.Globalization;

namespace BilancoTakipProjesi.Controllers
{
    public class PanelController : Controller
    {

        private readonly UygulamaDbContext _context;

        public PanelController(UygulamaDbContext context)
        {
            _context=context;
        }

        public async Task<ActionResult> Index()
        {

            // Son 7 günün verilerini alıcaz.

            DateTime BaslangicTarih = DateTime.Today.AddDays(-6);

            DateTime BitisTarih = DateTime.Today;

            List<Islem> SecilmisIslemler = await _context.Islems.Include(x => x.Kategori)
                .Where(y => y.Tarih >= BaslangicTarih && y.Tarih <= BitisTarih).ToListAsync();


            // Toplam Gelir

            int ToplamGelir = SecilmisIslemler.Where(i => i.Kategori.KategoriTipi == "Gelir").Sum(j => j.Tutar);
            ViewBag.ToplamGelir = ToplamGelir.ToString("C0");

            // Toplam Gider

            int ToplamGider = SecilmisIslemler.Where(i => i.Kategori.KategoriTipi == "Gider").Sum(j => j.Tutar);
            ViewBag.ToplamGider = ToplamGider.ToString("C0");

            // Toplam Para

            int Bakiye = ToplamGelir - ToplamGider;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.Bakiye = String.Format(culture, "{0:C0}", Bakiye);


            // Doughnut Chart - Kategoriye Göre Giderler

            ViewBag.DoughnutChartData = SecilmisIslemler
                .Where(i => i.Kategori.KategoriTipi == "Gider")
                .GroupBy(j => j.Kategori.KategoriId)
                .Select(k => new
                {

                    iconluKategoriBasligi = k.First().Kategori.Icon + " " + k.First().Kategori.Baslik,
                    tutar = k.Sum(j => j.Tutar),
                    tutarFormati = k.Sum(j => j.Tutar).ToString("C0"),

                }).ToList();

            ViewBag.SonIslemler = await _context.Islems
                .Include(i => i.Kategori)
                .OrderByDescending(j => j.Tarih)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }
}
