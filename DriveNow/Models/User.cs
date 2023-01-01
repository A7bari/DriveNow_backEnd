using DriveNow.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveNow.Models
{
    public abstract class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; }= string.Empty;

        [Phone]
        public string? phone { get; set; }

        public byte[] PasswordHash { get; set; } 

        public byte[] PasswordSalt { get; set; }

        [Required]
        public Roles Role { get; set; }

    }
}
