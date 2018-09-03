using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace DojoSecrets.Models
{
    public class Like
    {
        [Key]
        public int like_id {get; set;}

        public int user_id {get; set;}

        public User user {get; set;}

        public int secret_id {get; set;}

        public Secret secret {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public Like()
        {
            created_at = DateTime.UtcNow;
            updated_at = DateTime.UtcNow;
        }
    }
}