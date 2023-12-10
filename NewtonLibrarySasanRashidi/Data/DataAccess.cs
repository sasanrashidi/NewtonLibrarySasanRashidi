using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Helpers;

namespace NewtonLibrarySasanRashidi.Data
{
    public enum BookTitle
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
        public csSeedGenerator rnd = new csSeedGenerator();

        public void MakingRandomABUUC()
        {
            using (var context = new Context())
            {
                for (int i = 0; i < 50; i++)
                {
                    var author = new Author { Name = rnd.FullName };
                    var book = new Book { Year = rnd.Next(1800, 2023), Title = GetEnumDescreption(rnd.FromEnum<BookTitle>()) };
                    var user = new User { FirstName = rnd.FirstName, LastName = rnd.LastName };
                    var userCard = new UserCard();

                    context.Authors.Add(author);
                    context.Books.Add(book);
                    context.Users.Add(user);
                }
                context.SaveChanges();
            }

        }

        public void AddUserToDatabase(string firstName, string lastName)
        {
            using (var context = new Context())
            {
                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void AddUserCardToUser(int id)
        {
            using (var context = new Context())
            {
                var user = context.Users.Find(id);

                if (user == null)
                {
                    Console.WriteLine("User do not excist!");
                    return;
                }

                var userCard = new UserCard();
                user.userCard = userCard;

                context.SaveChanges();
            }
        }

        public void AddBookToDataBase(string title, params int[] authorId)
        {

            using (var context = new Context())
            {
                var author = context.Authors.Where(a => authorId.Contains(a.Id)).ToList();
                if (author == null)
                {
                    Console.WriteLine("Author do not excist!");
                    return;
                }

                var book = new Book
                {
                    Title = title,
                    Authors = author,
                    Year = new Random().Next(1990, 2023)
                };
                context.Books.Add(book);
                context.SaveChanges();
            }
        }

        public void AddBookIdToUserCard(int userId, int bookId)
        {
            using (var context = new Context())
            {
                var user = context.Users.Include(p => p.userCard).SingleOrDefault(p => p.Id == userId);

                if (user == null)
                {
                    Console.WriteLine($"This user {userId} don´t excist");
                    return;
                }

                if (user.userCard == null)
                {
                    Console.WriteLine($"This person dosnt have a Usercard");
                    return;
                }

                var book = context.Books.Find(bookId);

                if (book != null)
                {
                    book.UserCardId = user.userCard.Id;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"This book {bookId} dosn´t excist");
                }
            }
        }

        public void BookMarkedAsNotLoaned(int bookId)
        {
            using (var context = new Context())
            {
                var book = context.Books
                    .Include(b => b.UserCard)
                    .FirstOrDefault(b => b.Id == bookId);

                if (book != null)
                {
                    book.UserCardId = null;
                    book.UserCard?.Books.Remove(book);

                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("This book cant be returned");
                }
            }
        }

        private string GetEnumDescreption(Enum value)
        {
            var efield = value.GetType().GetField(value.ToString());

            if (Attribute.GetCustomAttribute(efield, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            return value.ToString();
        }

        public void ClearAll()
        {
            using (var context = new Context())
            {
                var allUsers = context.Users.ToList();
                context.Users.RemoveRange(allUsers);

                var allBooks = context.Books.ToList();
                context.Books.RemoveRange(allBooks);

                var allAuthors = context.Authors.ToList();
                context.Authors.RemoveRange(allAuthors);

                var allUserCards = context.UserCards.ToList();
                context.UserCards.RemoveRange(allUserCards);

                context.SaveChanges();

            }
        }

        public void ClearAllUsers()
        {
            using (var context = new Context())
            {
                var allUsers = context.Users.ToList();
                context.Users.RemoveRange(allUsers);

                context.SaveChanges();
            }



        }

        public void ClearUserById(int userId)
        {
            using (var context = new Context())
            {
                var userToRemove = context.Users.Include(uc => uc.userCard).SingleOrDefault(u => u.Id == userId);
                if (userToRemove != null)
                {
                    if (userToRemove.userCard != null)
                    {
                        var UserCardId = userToRemove.userCard.Id;

                        var userLinkBook = context.Books.SingleOrDefault(u => u.UserCardId == UserCardId);

                        if (userLinkBook != null)
                        {
                            userLinkBook.UserCard = null;
                        }
                    }

                    context.Users.Remove(userToRemove);
                    context.SaveChanges();

                    Console.WriteLine($"User ID {userId} deleted.");
                }
                else
                {
                    Console.WriteLine($"User ID {userId} is not available.");
                }
            }
        }

        public void ClearAllBooks()
        {
            using (var context = new Context())
            {
                var allBooks = context.Books.ToList();
                context.Books.RemoveRange(allBooks);

                context.SaveChanges();
            }
        }

        public void ClearBookById(int bookId)
        {
            using (var context = new Context())
            {
                var bookToClear = context.Books.FirstOrDefault(b => b.Id == bookId);
                if (bookToClear != null)
                {
                    context.Books.Remove(bookToClear);

                    context.SaveChanges();
                    Console.WriteLine($"Book ID {bookId} deleted.");
                }
                else
                {
                    Console.WriteLine($"Book ID {bookId} is not avalible.");
                }
            }
        }

        public void ClearAllAuthors()
        {
            using (var context = new Context())
            {
                var allAuthors = context.Authors.ToList();
                context.Authors.RemoveRange(allAuthors);

                context.SaveChanges();
            }
        }

        public void ClearAuthorById(int authorId)
        {
            using (var context = new Context())
            {
                var authorToClear = context.Authors.FirstOrDefault(a => a.Id == authorId);
                if (authorToClear != null)
                {
                    context.Authors.Remove(authorToClear);

                    context.SaveChanges();
                    Console.WriteLine($"Author ID {authorId} deleted.");
                }
                else
                {
                    Console.WriteLine($"Author ID {authorId} is not avalible.");
                }
            }
        }

        public void ClearAllUserCards()
        {
            using (var context = new Context())
            {
                var allUserCards = context.UserCards.ToList();
                context.RemoveRange(allUserCards);

                context.SaveChanges();
            }
        }
    }
}



