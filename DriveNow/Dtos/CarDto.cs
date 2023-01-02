using DriveNow.Models;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveNow.Dtos
{
    public class CarDto
    {

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
        public string? imgUrl { get; set; } = string.Empty;
        public int userId { get; set; }
    }
}
