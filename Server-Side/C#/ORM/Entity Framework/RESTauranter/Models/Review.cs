using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace RESTauranter.Models
{
    public class Review
    {
        [Key]
        public long id {get; set;}

        [Required (ErrorMessage = "Can't be blank!")]
        public string reviewer_name {get; set;}

        [Required (ErrorMessage = "Can't be blank!")]
        public string restaurant_name {get; set;}

        [Required (ErrorMessage = "Can't be blank!")]
        [MinLength(10, ErrorMessage = "Must be at least 10 characters long!")]
        public string review {get; set;}

        [Required (ErrorMessage = "Can't be blank!")]
        public DateTime date_of_visit {get; set;}

        [Required]
        public int stars {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public Review()
        {
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }
    }
}