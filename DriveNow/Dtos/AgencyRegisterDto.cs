using DriveNow.Models;
using Microsoft.Build.Framework;

namespace DriveNow.Dtos
{
    public class AgencyRegisterDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Adress { get; set; } = string.Empty;

      
    }
}
