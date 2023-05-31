using System.ComponentModel.DataAnnotations;

namespace WebCoreProjesi.Models.ViewModel
{
    public class EditUserModel
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(30)]
        public string? Name { get; set; }

        [StringLength(30)]
        public string? Surname { get; set; } = null;

        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        [Required]
        public string Role { get; set; }

        public bool Aktivate { get; set; }
    }
}
