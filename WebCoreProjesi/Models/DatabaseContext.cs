using Microsoft.EntityFrameworkCore;
using WebCoreProjesi.Models.Entities;

namespace WebCoreProjesi.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public  DbSet<User> Users { get; set; }
        public  DbSet<Kategori> Kategoriler { get; set; }
    }
}
