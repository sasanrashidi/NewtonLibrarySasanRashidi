using System;
using Microsoft.EntityFrameworkCore;
using NewtonLibrarySasanRashidi.Models;

namespace NewtonLibrarySasanRashidi.Data
{
    public class Context : DbContext // Skapar en klass för databasens kontext som ärver från DbContext
    {
        // Databasens "hyllor" där vi kan placera saker
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCard> UserCards { get; set; }


        // En instruktion för hur böckernas data ska lagras
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>() // Bestämmer hur en specifik del av bokinformationen ska sparas
                .Property(bk => bk.BookIsLoaned) // Boken är utlånad eller inte
                .HasColumnName("Loaned") // Vad vi kallar utlåningsstatus i databasen
                .HasColumnType("bit"); // Typen av information som sparar utlåningsstatus
        }

        // Anvisningar för hur man kopplar sig till databasen
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Här anges informationen för att ansluta till databasen
            optionsBuilder.UseSqlServer(@"
                    Server=localhost;
                    Database=NewtonLibrarySasanRashidi;
                    Trust Server Certificate = Yes;
                    User Id=NewtonLibrarySasanRashidi;
                    Password=NewtonLibrarySasanRashidi");
        }
    }
}



