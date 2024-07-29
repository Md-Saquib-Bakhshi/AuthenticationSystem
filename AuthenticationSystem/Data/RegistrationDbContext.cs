using Microsoft.EntityFrameworkCore;
using AuthenticationSystem.Models;

namespace AuthenticationSystem.Data
{
    public class RegistrationDbContext : DbContext
    {
        public RegistrationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Registration> RegistrationSet { get; set; }
    }
}
