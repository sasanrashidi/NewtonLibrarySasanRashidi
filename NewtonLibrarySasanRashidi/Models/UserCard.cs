using System;
namespace NewtonLibrarySasanRashidi.Models
{
    public class UserCard
    {
        public int Id { get; set; }

        public int Pin { get; set; } = new Random().Next(1000, 9999);

        public ICollection<Book>? Books { get; set; }
    }
}
