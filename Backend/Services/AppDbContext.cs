using Microsoft.EntityFrameworkCore;
using UserManagementApp.Services;
using UserManagementApp.Models;

namespace UserManagementApp.Services{
public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

        }

}

}