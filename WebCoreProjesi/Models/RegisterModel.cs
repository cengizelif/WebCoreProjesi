using System.ComponentModel.DataAnnotations;

namespace WebCoreProjesi.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(16)]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(16)]
        [Compare(nameof(Password))]
        public string Password2 { get; set; }
    }
}
