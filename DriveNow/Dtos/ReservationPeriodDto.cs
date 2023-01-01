using DriveNow.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveNow.Dtos
{
    public class ReservationPeriodDto
    {
     
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

    }
}
