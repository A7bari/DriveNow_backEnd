using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DriveNow.Models
{
    public class Contrat
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public float Amount { get; set; }
        [Required]
        public ReservationPeriods? reservationPeriods { get; set; }
    }
}
