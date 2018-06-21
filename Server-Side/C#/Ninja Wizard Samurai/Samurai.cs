using System;
using System.Collections.Generic;

namespace Ninja_Wizard_Samurai
{
    public class Samurai : Human
    {
        public Samurai(string name) : base(name, 5, 5, 5, 100)
        {

        }

        public void death_blow(object obj)
        {
            Human victim = obj as Human;
            if (victim.health < 50)
            {
                victim.health = 0;
                Console.WriteLine("{0} just delivered a fatal blow to {1}", this.name, victim.name);
            }
            else
            {
                Console.WriteLine("That enemy has more than 50 health, use death blow when they're under that amount!");
            }
        }

        public void meditate()
        {
            health = 100;
            Console.WriteLine("Way to meditate, {0} your health is now full", this.name);
        }
    }
}