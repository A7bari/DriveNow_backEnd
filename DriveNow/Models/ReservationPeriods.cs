using System.ComponentModel.DataAnnotations;

namespace DriveNow.Models
{
    public class ReservationPeriods
    {
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime EndDate { get; set; } 

    }
}
