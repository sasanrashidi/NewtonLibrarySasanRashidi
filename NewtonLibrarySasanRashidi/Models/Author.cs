using System;
using System.ComponentModel.DataAnnotations;

namespace NewtonLibrarySasanRashidi.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; } // Ett unikt nummer för varje författare

        [MaxLength(50)]// Begränsar längden på författarens namn till högst 50 tecken
        public string Name { get; set; } // Författarens namn

        public ICollection<Book>? Books { get; set; } // En lista över böcker som tillhör författaren
        // Märket "?" innebär att Books kan vara null (inga böcker kopplade än)


    }
}
