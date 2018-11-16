using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace budget.Models
{
    public class Goal
    {
        [Key]
        public int goal_id {get; set;}

        [Required]
        public long annual_income {get; set;}

        [Required]
        public int monthly_living_expenses {get; set;}

        [Required]
        public int monthly_debt_payments {get; set;}

        [Required]
        public int discret_spend {get; set;}

        [Required]
        public string goal {get; set;}

        public int user_id {get; set;}
        
        public User user {get; set;}

        public DateTime created_at {get; set;}

        public DateTime updated_at {get; set;}

        public Goal()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}