using System;
using System.Collections.Generic;

namespace Ninja_Wizard_Samurai
{
    public class Ninja : Human
    {
        public Ninja(string name) : base(name, 5, 5, 175, 5)
        {

        }

        public void steal(object obj)
        {
            Human victim = obj as Human;
            this.attack(victim);
            this.health += 10;
            Console.WriteLine("{0} stole something from {1}! {2}'s new health is {3}. {4}'s health is now {5}", this.name, victim.name, this.name, this.health, victim.name, victim.health);
        }

        public void get_away()
        {
            this.health -= 15;
            Console.WriteLine("{0} just got away! Health decreased to {1}", this.name, this.health);
        }

    }
}