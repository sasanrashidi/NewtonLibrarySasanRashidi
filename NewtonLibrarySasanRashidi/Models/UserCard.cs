using System;
namespace NewtonLibrarySasanRashidi.Models
{
    public class UserCard
    {
        public int Id { get; set; } // Ett unikt nummer för varje användarkort

        // En säkerhets-PIN för användarkortet som genereras slumpmässigt mellan 1000 och 9999
        public int Pin { get; set; } = new Random().Next(1000, 9999);

        // En lista över böcker som är kopplade till användarkortet
        // Märket "?" innebär att Books kan vara null (inga böcker kopplade än)
        public ICollection<Book>? Books { get; set; }

    }
}
