using System;
using System.ComponentModel.DataAnnotations;

namespace NewtonLibrarySasanRashidi.Models
{
    public class Author
    {
        public int Id { get; set; }

        [MaxLength(50)]

        public string Name { get; set; }

        public ICollection<Book>? Books { get; set; }


    }
}

