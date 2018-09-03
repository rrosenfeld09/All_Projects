using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace LostInTheWoods.Models
{
    public class Trail
    {
        [Key]
        public long Id {get; set;}
        [Required]
        public string Name {get; set;}
        [Required]
        [StringLength(400, MinimumLength = 10)]
        public string Description {get; set;}

        [Required]
        public double Length {get; set;}

        [Required]
        public int ElevationChange {get; set;}

        [Required]
        [RegularExpression(@"^\d+\.\d{0,10}$")]
        public double Longitude {get; set;}

        [Required]
        [RegularExpression(@"^\d+\.\d{0,10}$")]
        public double Latitude {get; set;}
    }
}