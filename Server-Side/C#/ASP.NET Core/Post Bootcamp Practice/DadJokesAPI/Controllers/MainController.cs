using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DadJokesAPI.Controllers
{
    public class MainController : Controller
    {
        string one = "Did you hear about the restaurant on the moon? Great food, no atmosphere";

        string two = "What do you call a fake noodle? An impasta";

        string three = "How many apples grow on a tree? All of them";
        
        string four = "I just watched a program about beavers. It was the best dam program I've ever seen";

        string five = "How does a penguin buikld it's house? Igloose it together";


        [HttpGet("/dadjoke")]
        public IActionResult Joke()
        {
            Random rand = new Random();
            int val = rand.Next(1, 5);

            string joke;

            if(val == 1)
            {
                joke = one;
            }
            else if(val == 2)
            {
                joke = two;
            }
            else if(val == 3)
            {
                joke = three;
            }
            else if(val == 4)
            {
                joke = four;
            }
            else
            {
                joke = five;
            }

            return Json($"{joke}");
        }
    }
}