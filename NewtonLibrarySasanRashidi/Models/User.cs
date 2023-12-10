using System;
using System.ComponentModel.DataAnnotations;

namespace NewtonLibrarySasanRashidi.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        public UserCard? userCard { get; set; }

    }
}
