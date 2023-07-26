using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BilancoTakipProjesi.Models
{
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Bu alan boş geçilemez!")]
        public string Baslik { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        public string Icon { get; set; } = "";

        [Column(TypeName = "nvarchar(10)")]
        public string KategoriTipi { get; set; } = "Gider";

        [NotMapped]
        public string? IconluBaslik 
        {
            get
            {
                return this.Icon + " " + this.Baslik;
            } 
        }
    }
}
