using MessagePack;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DriveNow.Models
{
    public class Complain
    {
        [Key("ComplainId"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComplainId { get; set; }
        [Required]
        public string? Subject { get; set; }
        [Required]
        public string? Message { get; set; }
        [Required]
        public DateTime? date { get; set; }
        [JsonIgnore]
        public User user { get; set; }
        public int UserId { get; set; }
    }
}
