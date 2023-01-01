using Microsoft.Build.Framework;

namespace DriveNow.Models
{
    public class Complain
    {
        [Required]
        public string? Subject { get; set; }
        [Required]
        public string? Message { get; set; }
        [Required]
        public DateTime? date { get; set; }
    }
}
