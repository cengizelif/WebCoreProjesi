using System.ComponentModel.DataAnnotations;

namespace WebCoreProjesi.Models.ViewModel
{
    public class CreateUserModel
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

        [Required]
        [StringLength(250)]
        [Compare(nameof(Password))]
        public string Password2 { get; set; }

        [StringLength(20)]
        [Required]
        public string Role { get; set; }

        public bool Aktivate { get; set; }
    }
}
