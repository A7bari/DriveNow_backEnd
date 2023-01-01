using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveNow.Models
{
    [Table("Tenant")]
    public class Tenant : User
    { 
        [System.ComponentModel.DataAnnotations.Required]
        public string CIN { get; set; }

    }
}
