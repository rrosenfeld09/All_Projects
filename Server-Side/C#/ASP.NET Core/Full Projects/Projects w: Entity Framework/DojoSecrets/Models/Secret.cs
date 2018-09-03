using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace DojoSecrets.Models
{
    public class Secret
    {
        [Key]
        public int secret_id {get; set;}

        [Required(ErrorMessage = "Can't be blank")]
        public string message {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public int user_id {get; set;}

        public User user {get; set;}

        public List<Like> likes {get; set;}

        public int num_likes {get; set;}

        public Secret()
        {
            likes = new List<Like>();
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }
    }
}