using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebCoreProjesi.Models.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(30)]
        public string? Name { get; set; }

        [StringLength(30)]
        public string? Surname { get; set; } = null;

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }
        public bool Aktivate { get; set; }
        public DateTime CreateDate { get; set; }

        [StringLength(20)]
        [Required]
        public string Role { get; set; } = "user";

        [StringLength(255)]
        public string ProfilImageFileName { get; set; }

    }
}
