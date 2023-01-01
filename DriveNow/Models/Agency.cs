using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DriveNow.Models
{
    public class Agency
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Adress { get; set; } = string.Empty;
        [JsonIgnore]
        public Owner Owner { get; set; }
        public int OwnerId { get; set; }

    }
}
