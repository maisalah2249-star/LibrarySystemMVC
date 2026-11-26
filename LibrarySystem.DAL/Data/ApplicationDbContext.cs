using LibrarySystem.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Author>().HasQueryFilter(a => !a.IsDeleted);
            builder.Entity<Book>().HasQueryFilter(b => !b.IsDeleted);
        }
    }
}