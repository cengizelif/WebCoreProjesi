using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoreProjesi.Models.Entities
{
    [Table("Kategori")]
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }

        [Required]
        [StringLength(100)] 
        public string KategoriAdi { get; set; }
    }
}
