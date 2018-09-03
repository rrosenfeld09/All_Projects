using System;
using System.Collections.Generic;

namespace WizardNinjaSamurai
{
    public class Samurai : Human 
    {

        public Samurai(string name) : base(name, 3, 3, 3, 200)
        {

        }
        
        public void DeathBlow(Object obj)
        {
            Human enemy = obj as Human;
            if (enemy.health < 50)
            {
                enemy.health = 0;
                Console.WriteLine("Aw, bummer...{0} is dead", enemy.name);
            }
            else
            {
                enemy.health -=15;
                Console.WriteLine("Close but no cigar, {0}. {1} was hurt but isn't dead yet!", name, enemy.name);
            }
        }

        public void Meditate()
        {
            health = 200;
            Console.WriteLine("Nice! {0}'s health is now fully restored!", name);
        }
    }
}