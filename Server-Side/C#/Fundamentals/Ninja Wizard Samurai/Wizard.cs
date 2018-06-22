using System;
using System.Collections.Generic;

namespace Ninja_Wizard_Samurai
{
    public class Wizard : Human
    {
        public Wizard(string name) : base(name, 5, 25, 5, 50)
        {

        } 

        public void heal()
        {
            this.health += 10 * this.intelligence;
            Console.WriteLine("Nice! Your new health is {0}!", this.health);

        }

        public void fireball(object obj)
        {
            Human attacked = obj as Human;
            Random rand = new Random();
            int dmg = rand.Next(20, 51);
            attacked.health -= dmg;
            Console.WriteLine("Oh man, you just damanged {0} by {1} points! Their new health is {2}", attacked.name, dmg, attacked.health);
        }
    }
}