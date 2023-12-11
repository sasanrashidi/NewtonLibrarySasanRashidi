using System;
using System.ComponentModel.DataAnnotations;

namespace NewtonLibrarySasanRashidi.Models
{
    public class User
    {
        public int Id { get; set; } // Ett unikt nummer för varje användare

        [MaxLength(50)] // Begränsar längden på användarens förnamn till högst 50 tecken
        public string? FirstName { get; set; } // Användarens förnamn

        [MaxLength(50)] // Begränsar längden på användarens efternamn till högst 50 tecken
        public string? LastName { get; set; } // Användarens efternamn

        public UserCard? userCard { get; set; } // Användarens kortinformation
        // Märket "?" innebär att userCard kan vara null (ingen kortinformation kopplad än)
    }
}