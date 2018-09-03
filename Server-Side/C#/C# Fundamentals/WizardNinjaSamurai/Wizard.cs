using System;
using System.Collections.Generic;

namespace WizardNinjaSamurai
{
    public class Wizard : Human
    {


        public Wizard(string name) : base(name, 3, 25, 3, 50)
        {

        }

        public void heal()
        {
            health += (intelligence * 5);
            Console.WriteLine("That's right! {0} just got healed. New health: {1}", this.name, health);
        }

        public void fireball(Object obj)
        {
            Human enemy = obj as Human;
            Random rand = new Random();
            enemy.health -= rand.Next(25,51);

        }


    }
}