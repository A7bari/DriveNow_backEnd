using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveNow.Models
{
    public class Owner
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OwnerId { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public bool HasAgancy { get; set; } = false;
    }
}
