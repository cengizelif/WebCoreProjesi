using Microsoft.EntityFrameworkCore;
using WebCoreProjesi.Models.Entities;
using WebCoreProjesi.Models.ViewModel;

namespace WebCoreProjesi.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public  DbSet<User> Users { get; set; }
        public  DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<WebCoreProjesi.Models.ViewModel.UserModel>? UserModel { get; set; }
        public DbSet<WebCoreProjesi.Models.ViewModel.CreateUserModel>? CreateUserModel { get; set; }
        public DbSet<WebCoreProjesi.Models.ViewModel.EditUserModel>? EditUserModel { get; set; }
    }
}
