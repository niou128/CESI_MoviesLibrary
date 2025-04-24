using CESI_MoviesLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CESI_MoviesLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<UserMovie> UserMovies => Set<UserMovie>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=app.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMovie>().HasKey(um => new { um.UserId, um.MovieId });
        }
    }
}
