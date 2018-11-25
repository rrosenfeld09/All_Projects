

namespace budget.Models
{
    public class HomePageViewModel
    {
        public User user {get; set;}

        public Goal goal {get; set;}

        public double monthly_income {get; set;}

        public string option {get; set;}

        public int amount {get; set;}

        public int user_id {get; set;}

        public int left_over {get; set;}
    }
}