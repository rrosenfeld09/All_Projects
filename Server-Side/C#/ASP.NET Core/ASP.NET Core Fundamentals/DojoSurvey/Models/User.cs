using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DojoSurvey.Models
{
    public class User
    {
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name {get; set;}
        [Required]
        public string Location {get; set;}
        [Required]
        public string Language {get; set;}
        [MinLength(2)]
        [MaxLength(20)]
        public string Comment {get; set;}

    }
}