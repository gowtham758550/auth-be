using auth.Models;
using Microsoft.EntityFrameworkCore;

namespace auth.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {}

        public DbSet<User>? User {get; set;}
        public DbSet<Otp>? Otp {get; set;}
    }
}