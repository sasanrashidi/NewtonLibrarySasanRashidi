using System;
using Microsoft.EntityFrameworkCore;

namespace NewtonLibrarySasanRashidi.Data
{
    public class Context : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCard> UserCards { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(bk => bk.BookIsLoaned)
                .HasColumnName("Loaned")
                .HasColumnType("bit");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"
                    Server=localhost;
                    Database=NewtonLibrarySasanRashidi;
                    Trust Server Certificate = Yes;
                    User Id=NewtonLibrarySasanRashidi;
                    Password=NewtonLibrarySasanRashidi");
        }
    }
}



