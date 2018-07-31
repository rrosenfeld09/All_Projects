using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using Dojodachi.Models;

namespace Dojodachi.Controllers
{
    public class DojodachiController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            int? num = HttpContext.Session.GetInt32("num");
            if (num == null)
            {
                DojodachiNew newGame = new DojodachiNew();
                HttpContext.Session.SetInt32("num", 1);
                HttpContext.Session.SetInt32("Fullness", newGame.Fullness);
                HttpContext.Session.SetInt32("Happiness", newGame.Happiness);
                HttpContext.Session.SetInt32("Energy", newGame.Energy);
                HttpContext.Session.SetInt32("Meals", newGame.Meals);
                HttpContext.Session.SetInt32("Playing", 1);
                HttpContext.Session.SetInt32("Emotion", 1);         
            }
            else
            {
                int currentEnergy = (int)HttpContext.Session.GetInt32("Energy");
                int currentFullness = (int)HttpContext.Session.GetInt32("Fullness");
                int currentHappiness = (int)HttpContext.Session.GetInt32("Happiness");
                int currentMeals = (int)HttpContext.Session.GetInt32("Meals");

                if(currentEnergy >= 100 && currentFullness >= 100 && currentHappiness >= 100)
                {
                    HttpContext.Session.SetString("Message", "Congratulations! You won!");
                    HttpContext.Session.SetInt32("Playing", 0);
                    HttpContext.Session.SetInt32("Emotion", 1);
                    
                }
                else if (currentFullness <= 0 || currentHappiness <= 0)
                {
                    HttpContext.Session.SetString("Message", "Oh no...you have lost :(");
                    HttpContext.Session.SetInt32("Playing", 0);
                    HttpContext.Session.SetInt32("Emotion", 0);

                }
            }
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.Playing = HttpContext.Session.GetInt32("Playing");
            ViewBag.Message = HttpContext.Session.GetString("Message");
            ViewBag.Emotion = HttpContext.Session.GetInt32("Emotion");
    
            return View();
        }

        [HttpGet("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet("feed")]
        public IActionResult Feed()
        {
            int numMeals = (int)HttpContext.Session.GetInt32("Meals");
            Random Chance = new Random();
            int randChance = Chance.Next(1,5);
            if (numMeals > 0 && randChance !=1)
            {
                HttpContext.Session.SetInt32("Meals", (int)HttpContext.Session.GetInt32("Meals") - 1);
                int currentMeals = (int)HttpContext.Session.GetInt32("Meals");

                Random rand = new Random();

                int initial = (int)HttpContext.Session.GetInt32("Fullness");
                int increase = rand.Next(5,11);
                int adjustment = initial + increase;
                HttpContext.Session.SetInt32("Fullness", adjustment);

                HttpContext.Session.SetString("Message", $"Nice! You just fed your Dojodachi! It now has {currentMeals} meals left but gained {increase} fullness!");
                HttpContext.Session.SetInt32("Emotion", 1);


            }
            else if (numMeals > 0 && randChance == 1)
            {
                HttpContext.Session.SetInt32("Meals", (int)HttpContext.Session.GetInt32("Meals") -1);
                int? totalMeals = HttpContext.Session.GetInt32("Meals");

                HttpContext.Session.SetString("Message", $"Oh no! Your Dojodachi didn't like that! You lost one meal! You have {totalMeals} meals left..");
                   HttpContext.Session.SetInt32("Emotion", 0);

            }
            else
            {
                HttpContext.Session.SetString("Message", "Oh no! You don't have any meals left!");
                HttpContext.Session.SetInt32("Emotion", 0);

            }

            return RedirectToAction("Index");
        }

        [HttpGet("play")]
        public IActionResult Play()
        {
            int numEnergy = (int)HttpContext.Session.GetInt32("Energy");
            Random Chance = new Random();
            int randChance = Chance.Next(1,5);
            if (numEnergy >= 5 && randChance != 1)
            {
                HttpContext.Session.SetInt32("Energy", (int)HttpContext.Session.GetInt32("Energy") - 5);

                Random rand = new Random();
                HttpContext.Session.SetInt32("Happiness", (int)HttpContext.Session.GetInt32("Happiness") + rand.Next(5, 11));
                int currentEnergy = (int)HttpContext.Session.GetInt32("Energy");
                int currentHappiness = (int)HttpContext.Session.GetInt32("Happiness");
                HttpContext.Session.SetString("Message", $"Woohoo! Playing rocks. Your Dojodachi now has {currentEnergy} energy left but now has {currentHappiness} happiness!");
                HttpContext.Session.SetInt32("Emotion", 1);


            }
            else if (numEnergy > 0 && randChance == 1)
            {
                HttpContext.Session.SetInt32("Energy", (int)HttpContext.Session.GetInt32("Energy") -5);
                int? totalEnergy = HttpContext.Session.GetInt32("Energy");

                HttpContext.Session.SetString("Message", $"Oh no! Your Dojodachi didn't like that! You lost 5 energy! You have {totalEnergy} energy left..");
                HttpContext.Session.SetInt32("Emotion", 0);

            }
             else
            {
                HttpContext.Session.SetString("Message", "Oh no! You don't have any energy left!");
                HttpContext.Session.SetInt32("Emotion", 0);

            }
            return RedirectToAction("Index");
        }

        [HttpGet("work")]
        public IActionResult Work()
        {
            if(HttpContext.Session.GetInt32("Energy") >= 5)
            {
                Random rand = new Random();
                int mealIncrease = rand.Next(1,4);

                HttpContext.Session.SetInt32("Energy", (int)HttpContext.Session.GetInt32("Energy") - 5);
                HttpContext.Session.SetInt32("Meals", (int)HttpContext.Session.GetInt32("Meals") + mealIncrease);

                HttpContext.Session.SetString("Message", $"Working pays off! You spent 5 energy but gained {mealIncrease} meals! ");
                HttpContext.Session.SetInt32("Emotion", 1);


            }
            else
            {
                HttpContext.Session.SetString("Message", "Oh no! You don't have enough energy to work!");
                HttpContext.Session.SetInt32("Emotion", 0);

            }
            return RedirectToAction("Index");
        }

        [HttpGet("sleep")]
        public IActionResult Sleep()
        {
            if(HttpContext.Session.GetInt32("Fullness") >= 5 && HttpContext.Session.GetInt32("Happiness") >= 5)
            {
                HttpContext.Session.SetInt32("Energy", (int)HttpContext.Session.GetInt32("Energy") + 15);
                HttpContext.Session.SetInt32("Fullness", (int)HttpContext.Session.GetInt32("Fullness") - 5);
                HttpContext.Session.SetInt32("Happiness", (int)HttpContext.Session.GetInt32("Happiness") - 5);

                HttpContext.Session.SetString("Message", "Nice you have increased you Dojodachi's energy by 15 points! Fulless and happiness have each been reduced by 5");
                HttpContext.Session.SetInt32("Emotion", 1);

            }
            else
            {
                HttpContext.Session.SetString("Message", "Oh no! You don't have enough fullness and happiness to do this!");
                HttpContext.Session.SetInt32("Emotion", 0);

            }
            return RedirectToAction("Index");
        }
    }
}