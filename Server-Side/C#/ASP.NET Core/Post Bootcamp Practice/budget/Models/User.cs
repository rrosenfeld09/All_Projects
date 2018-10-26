using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace budget.Models
{
    public class User
    {
        [Key]
        public int user_id {get; set;}

        [Required]
        public string name {get; set;}

        [Required]
        public string email {get; set;}

        [Required]
        public string password {get; set;}

        [Required]
        [NotMapped]
        public string confirm_pw {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public User()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}