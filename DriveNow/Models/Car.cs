using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DriveNow.Models
{
    public class Car
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public float Price { get; set; } 

        [Required]
        public string Brand { get; set; } = string.Empty;

        [Required]
        public string Color { get; set; } = string.Empty;
        [Required]
        public int ProductionYear { get; set; }
        [Required]

        public FType FuelType { get; set; } = FType.Gasoline;
        [Required]
        public int Km { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
        
        public List<ReservationPeriod> ReservationPeriods { get; set; }

    }
}
