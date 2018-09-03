using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace QuotingDojo.Models
{
    public class Quote
    {
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name {get; set;}

        [Required]
        [MinLength(2)]
        [MaxLength(150)]
        public string QuoteString {get; set;}
    }
}