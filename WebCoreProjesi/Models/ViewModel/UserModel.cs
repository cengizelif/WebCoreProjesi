using System.ComponentModel.DataAnnotations;

namespace WebCoreProjesi.Models.ViewModel
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; } = null;
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool Aktivate { get; set; }
        public DateTime CreateDate { get; set; }
        public string Role { get; set; } 
        public string ProfilImageFileName { get; set; }

    }
}
