using System.ComponentModel.DataAnnotations;

namespace DriveNow.Dtos
{
    public class ComplainRegisterDto
    {
        [Required]
        public string? Subject { get; set; }
        [Required]
        public string? Message { get; set; }
        [Required]
        public DateTime? date { get; set; }

        public int UserId { get; set; }
        
    }
}
