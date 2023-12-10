using System;
using Microsoft.EntityFrameworkCore;
using NewtonLibrarySasanRashidi.Models;
using System.ComponentModel;
using Helpers;

namespace NewtonLibrarySasanRashidi.Data
{
    public enum BookTitle // Enum som representerar olika boktitlar med beskrivningar
    {
        [Description("To Kill a Movkingbird")] Bok1,
        [Description("The Catcher in tje Rye")] Bok2,
        [Description("The Great Gatsby")] Bok3,
        [Description("Pride and Prejudice")] Bok4,
        [Description("Brave New World")] Bok5,
        [Description("The Lord och the Rings")] Bok6,
        [Description("Harry Potter and the Philosoåjers Stone")] Bok7,
        [Description("The Chronicles of Narnia")] Bok8,
        [Description("The Hitchhikers Guid to the Galaxy")] Bok9,
        [Description("I am Zlatan")] Bok10,
    }

    public class DataAccess
    {
        public csSeedGenerator rnd = new csSeedGenerator(); // Instans av SeedGenerator för slumpmässiga data

        public void MakingRandomABUUC() // Metod för att skapa slumpmässiga författare, böcker, och användare och spara dem i databasen
        {
            using (var context = new Context()) // Skapar en ny instans av databaskontexten
            {
                for (int i = 0; i < 50; i++)
                {
                    var author = new Author { Name = rnd.FullName }; // Skapar en slumpmässig författare
                    var book = new Book { Year = rnd.Next(1800, 2023), Title = GetEnumDescreption(rnd.FromEnum<BookTitle>()) }; // Skapar en slumpmässig bok med år och titel
                    var user = new User { FirstName = rnd.FirstName, LastName = rnd.LastName }; // Skapar en slumpmässig användare
                    var userCard = new UserCard();

                    context.Authors.Add(author); // Lägger till författaren i databasen
                    context.Books.Add(book); // Lägger till boken i databasen
                    context.Users.Add(user); // Lägger till användaren i databasen
                }
                context.SaveChanges(); // Sparar ändringarna i databasen
            }

        }

        public void AddUserToDatabase(string firstName, string lastName) // Metod för att lägga till en användare i databasen
        {
            using (var context = new Context()) // Skapar en ny instans av databaskontexten
            {
                var user = new User // Skapar en ny användarinstans
                {
                    FirstName = firstName, // Sätter förnamnet för användaren
                    LastName = lastName // Sätter efternamnet för användaren
                };

                context.Users.Add(user); // Lägger till användaren i databaskontexten
                context.SaveChanges(); // Sparar ändringarna i databasen
            }
        }

        public void AddUserCardToUser(int id) // Metod för att lägga till en användarkort till en användare i databasen
        {
            using (var context = new Context()) // Skapar en ny instans av databaskontexten
            {
                var user = context.Users.Find(id); // Letar efter användaren med det angivna id:t i databasen

                if (user == null) // Om användaren inte finns
                {
                    Console.WriteLine("User do not excist!");
                    return; // Avslutar funktionen
                }

                var userCard = new UserCard(); // Skapar en ny instans av användarkortet
                user.userCard = userCard; // Tilldelar användaren det nya användarkortet

                context.SaveChanges();
            }
        }

        public void AddBookToDataBase(string title, params int[] authorId) // Metod för att lägga till en bok med tillhörande författare i databasen
        {

            using (var context = new Context()) // Skapar en ny instans av databaskontexten
            {
                var author = context.Authors.Where(a => authorId.Contains(a.Id)).ToList(); // Hämtar författarna från databasen baserat på deras id:n

                if (author == null) // Om inga författare hittades
                {
                    Console.WriteLine("Author do not excist!");
                    return; // Avslutar funktionen
                }

                var book = new Book // Skapar en ny instans av boken
                {
                    Title = title, // Tilldelar bokens titel
                    Authors = author, // Tilldelar författarna till boken
                    Year = new Random().Next(1990, 2023) // Genererar ett slumpmässigt år för boken
                };
                context.Books.Add(book); // Lägger till boken i databaskontexten
                context.SaveChanges(); // Sparar ändringarna i databasen
            }
        }

        public void AddBookIdToUserCard(int userId, int bookId) // Metod för att lägga till en bok till ett UserCard.
        {
            using (var context = new Context()) // Skapar en ny instans av databaskontexten
            {
                var user = context.Users.Include(p => p.userCard).SingleOrDefault(p => p.Id == userId); // Hämtar användaren med det givna id:t inklusive dess användarkort från databasen

                if (user == null) // Om användaren inte finns
                {
                    Console.WriteLine($"This user {userId} don´t excist");
                    return; // Avslutar funktionen
                }

                if (user.userCard == null) // Om användaren inte har ett användarkort
                {
                    Console.WriteLine($"This person dosnt have a Usercard");
                    return; // Avslutar funktionen
                }

                var book = context.Books.Find(bookId); // Letar efter boken med det angivna id:t i databasen

                if (book != null) // Om boken finns
                {
                    book.UserCardId = user.userCard.Id; // Tilldelar boken ett användarkorts-id
                    context.SaveChanges(); // Sparar ändringarna i databasen
                }
                else // Om boken inte finns
                {
                    Console.WriteLine($"This book {bookId} dosn´t excist");
                }
            }
        }

        public void BookMarkedAsNotLoaned(int bookId) //Metod som markerar en lånad bok som icke lånad.
        {
            using (var context = new Context()) // Skapar en ny instans av databaskontexten
            {
                var book = context.Books // Hämtar böcker från databasen inklusive dess användarkort
                    .Include(b => b.UserCard)
                    .FirstOrDefault(b => b.Id == bookId); // Hämtar den specifika boken med det angivna id:t

                if (book != null) // Om boken finns
                {
                    book.UserCardId = null; // Tar bort referensen till användarkortet från boken
                    book.UserCard?.Books.Remove(book); // Tar bort boken från användarkortets lista över böcker (om det finns ett användarkort)

                    context.SaveChanges();
                }
                else // Om boken inte finns
                {
                    Console.WriteLine("This book cant be returned");
                }
            }
        }

        private string GetEnumDescreption(Enum value) // Funktion för att hämta beskrivningen av en enum
        {
            var efield = value.GetType().GetField(value.ToString()); // Hämtar fältet för den angivna enum-värdet

            if (Attribute.GetCustomAttribute(efield, typeof(DescriptionAttribute)) is DescriptionAttribute attribute) // Om det finns en DescriptionAttribute-klass för det aktuella fältet
            {
                return attribute.Description;  // Returnerar beskrivningen från DescriptionAttribute
            }
            return value.ToString(); // Returnerar enum-värdet som sträng om ingen beskrivning hittades
        }

        public void ClearAll() // Funktion för att rensa alla poster från databastabellerna
        {
            using (var context = new Context()) // Skapar en ny instans av databaskontexten
            {
                var allUsers = context.Users.ToList(); // Hämtar alla användare från databasen och lagrar dem i en lista
                context.Users.RemoveRange(allUsers); // Tar bort alla användare från databasen

                var allBooks = context.Books.ToList(); // Hämtar alla böcker från databasen och lagrar dem i en lista
                context.Books.RemoveRange(allBooks); // Tar bort alla böcker från databasen

                var allAuthors = context.Authors.ToList(); // Hämtar alla författare från databasen och lagrar dem i en lista
                context.Authors.RemoveRange(allAuthors); // Tar bort alla författare från databasen

                var allUserCards = context.UserCards.ToList(); // Hämtar alla användarkort från databasen och lagrar dem i en lista
                context.UserCards.RemoveRange(allUserCards); // Tar bort alla användarkort från databasen

                context.SaveChanges(); // Sparar ändringarna i databasen

            }
        }

        public void ClearAllUsers() // Funktion för att ta bort alla användare från databasen
        {
            using (var context = new Context()) // Skapar en ny instans av databaskontexten
            {
                var allUsers = context.Users.ToList(); // Hämtar alla användare från databasen och lägger dem i en lista
                context.Users.RemoveRange(allUsers); // Tar bort alla användare från databasen

                context.SaveChanges();
            }
        }

        public void ClearUserById(int userId) // Funktion för att ta bort en användare från databasen med hjälp av användar-ID
        {
            using (var context = new Context()) // Skapar en ny instans av databaskontexten
            {
                var userToRemove = context.Users.Include(uc => uc.userCard).SingleOrDefault(u => u.Id == userId); // Hämtar användaren och dess användarkort från databasen

                if (userToRemove != null) // Om användaren finns
                {
                    if (userToRemove.userCard != null) // Om användaren har ett användarkort
                    {
                        var UserCardId = userToRemove.userCard.Id; // Hämtar användarkorts-ID för användaren

                        var userLinkBook = context.Books.SingleOrDefault(u => u.UserCardId == UserCardId); // Hittar böcker kopplade till användarens användarkort

                        if (userLinkBook != null) // Om det finns böcker kopplade till användarens användarkort
                        {
                            userLinkBook.UserCard = null; // Tar bort kopplingen till användarkortet från boken
                        }
                    }

                    context.Users.Remove(userToRemove); // Tar bort användaren från databasen
                    context.SaveChanges(); // Sparar ändringarna i databasen

                    Console.WriteLine($"User ID {userId} deleted.");
                }
                else // Om användaren inte finns
                {
                    Console.WriteLine($"User ID {userId} is not available.");
                }
            }
        }

        public void ClearAllBooks() // Funktion för att ta bort alla böcker från databasen
        {
            using (var context = new Context()) // Skapar en ny instans av databaskontexten
            {
                var allBooks = context.Books.ToList(); // Hämtar alla böcker från databasen och lägger dem i en lista
                context.Books.RemoveRange(allBooks); // Tar bort alla böcker från databasen

                context.SaveChanges();
            }
        }

        public void ClearBookById(int bookId) // Funktion för att ta bort en bok från databasen med hjälp av bok-ID
        {
            using (var context = new Context()) // Skapar en ny instans av databaskontexten
            {
                var bookToClear = context.Books.FirstOrDefault(b => b.Id == bookId); // Hämtar boken med det givna bok-ID:t från databasen

                if (bookToClear != null) // Om boken finns
                {
                    context.Books.Remove(bookToClear); // Tar bort boken från databasen

                    context.SaveChanges();
                    Console.WriteLine($"Book ID {bookId} deleted.");
                }
                else // Om boken inte finns
                {
                    Console.WriteLine($"Book ID {bookId} is not avalible.");
                }
            }
        }

        public void ClearAllAuthors() // Funktion för att ta bort alla författare från databasen
        {
            using (var context = new Context()) // Skapar en ny instans av databaskontexten
            {
                var allAuthors = context.Authors.ToList(); // Hämtar alla författare från databasen och lägger dem i en lista
                context.Authors.RemoveRange(allAuthors); // Tar bort alla författare från databasen

                context.SaveChanges();
            }
        }

        public void ClearAuthorById(int authorId) // Funktion för att ta bort en författare från databasen med hjälp av författar-ID
        {
            using (var context = new Context()) // Skapar en anslutning till databasen
            {
                var authorToClear = context.Authors.FirstOrDefault(a => a.Id == authorId); // Hämtar författaren med det angivna författar-ID:t från databasen

                if (authorToClear != null) // Om författaren finns
                {
                    context.Authors.Remove(authorToClear); // Tar bort författaren från databasen

                    context.SaveChanges();
                    Console.WriteLine($"Author ID {authorId} deleted.");
                }
                else // Om författaren inte finns
                {
                    Console.WriteLine($"Author ID {authorId} is not avalible.");
                }
            }
        }

    }
}



