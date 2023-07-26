using Microsoft.EntityFrameworkCore;

namespace BilancoTakipProjesi.Models
{
    public class UygulamaDbContext:DbContext
    {
        public UygulamaDbContext(DbContextOptions options):base(options)
        {}

        public DbSet<Islem> Islems { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }

    }
}
