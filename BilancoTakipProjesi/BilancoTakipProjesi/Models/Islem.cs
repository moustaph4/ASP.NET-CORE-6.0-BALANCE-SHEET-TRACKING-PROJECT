using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime.ConstrainedExecution;

namespace BilancoTakipProjesi.Models
{
    public class Islem
    {
        [Key]
        public int IslemId { get; set; }

        // Her işlemin bir Kategorisi vardır, bir kategoride birden fazla işlem bulunabilir

        [Range(1,int.MaxValue, ErrorMessage = "Lütfen bir kategori seçiniz!")]
        public int KategoriId { get; set; }
        public Kategori? Kategori { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Tutar 0 dan büyük olmalıdır!")]
        public int Tutar { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string? Bilgi { get; set; }
        public DateTime Tarih  { get; set; } = DateTime.Now;

        [NotMapped]
        public string? IconluBaslik
        {
            get
            {
                return Kategori == null ? "" : Kategori.Icon + " " + Kategori.Baslik;
            }
        }

        [NotMapped]
        public string? GosterilenTutar
        {
            get
            {
                return ((Kategori == null || Kategori.KategoriTipi == "Gider") ? "- " : "+ ") + Tutar.ToString("C0");
            }
        }


    }
}
