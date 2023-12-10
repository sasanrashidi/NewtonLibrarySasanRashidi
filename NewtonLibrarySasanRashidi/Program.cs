using NewtonLibrarySasanRashidi.Data;

namespace NewtonLibrarySasanRashidi;

class Program
{
    static void Main(string[] args)
    {
        DataAccess data = new DataAccess();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Welcome to the library program!");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Create random entries");
            Console.WriteLine("2. Add a loan card to a user");
            Console.WriteLine("3. Mark a book as returned");
            Console.WriteLine("4. Add a book ID to a person's loan card");
            Console.WriteLine("5. Add a book to the database");
            Console.WriteLine("6. Add a person to the database");
            Console.WriteLine("7. Clear the database");
            Console.WriteLine("8. Exit");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    data.MakingRandomABUUC();
                    break;

                case "2":
                    Console.WriteLine("Enter the user ID to add a loan card:");
                    int userIdToAddCard = Convert.ToInt32(Console.ReadLine());
                    data.AddUserCardToUser(userIdToAddCard);
                    break;

                case "3":
                    Console.WriteLine("Enter the book ID to mark as returned:");
                    int bookIdToMarkReturned = Convert.ToInt32(Console.ReadLine());
                    data.BookMarkedAsNotLoaned(bookIdToMarkReturned);
                    break;

                case "4":
                    Console.WriteLine("Enter the person ID for the loan card:");
                    int personId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the book ID for the loan card:");
                    int bookIdForCard = Convert.ToInt32(Console.ReadLine());
                    data.AddBookIdToUserCard(personId, bookIdForCard);
                    break;

                case "5":
                    Console.WriteLine("Enter the book title:");
                    string bookTitle = Console.ReadLine();
                    Console.WriteLine("Enter the author ID for the book:");
                    int authorIdForBook = Convert.ToInt32(Console.ReadLine());
                    data.AddBookToDataBase(bookTitle, authorIdForBook);
                    break;

                case "6":
                    Console.WriteLine("Enter the first name:");
                    string firstName = Console.ReadLine();
                    Console.WriteLine("Enter the last name:");
                    string lastName = Console.ReadLine();
                    data.AddUserToDatabase(firstName, lastName);
                    break;

                case "7":
                    Console.WriteLine("Choose what you want to clear:");
                    Console.WriteLine("a. Clear all users");
                    Console.WriteLine("b. Clear a specific user");
                    Console.WriteLine("c. Clear all books");
                    Console.WriteLine("d. Clear a specific book");
                    Console.WriteLine("e. Clear all authors");
                    Console.WriteLine("f. Clear a specific author");

                    string subInput = Console.ReadLine();
                    switch (subInput)
                    {
                        case "a":
                            data.ClearAllUsers();
                            break;

                        case "b":
                            Console.WriteLine("Enter the user ID to clear:");
                            int userId = Convert.ToInt32(Console.ReadLine());
                            data.ClearUserById(userId);
                            break;

                        case "c":
                            data.ClearAllBooks();
                            break;

                        case "d":
                            Console.WriteLine("Enter the book ID to clear:");
                            int bookId = Convert.ToInt32(Console.ReadLine());
                            data.ClearBookById(bookId);
                            break;

                        case "e":
                            data.ClearAllAuthors();
                            break;

                        case "f":
                            Console.WriteLine("Enter the author ID to clear:");
                            int authorId = Convert.ToInt32(Console.ReadLine());
                            data.ClearAuthorById(authorId);
                            break;

                        default:
                            Console.WriteLine("Invalid choice for clearing.");
                            break;
                    }
                    break;

                case "8":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}



