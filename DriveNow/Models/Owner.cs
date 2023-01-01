using Azure.Core;
using DriveNow.Helpers;
using MessagePack;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveNow.Models
{

    [Table("Owner")]
    public class Owner : User
    {

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public bool HasAgancy { get; set; } = false;
       
        public Agency? Agency { get; set; }



    }
}
