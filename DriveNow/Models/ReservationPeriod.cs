using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveNow.Models
{
    public class ReservationPeriod
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime EndDate { get; set; }
        public List<Car> Cars { get; set; }

    }
}
