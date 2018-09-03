using System;
using System.Collections.Generic;

namespace WizardNinjaSamurai
{
    public class Ninja : Human
    {
        public Ninja(string name) : base(name, 3, 3, 175, 100)
        {

        }

        public void Steal(object obj)
        {
            Human enemy = obj as Human;
            attack(enemy);
            health += 10;
            Console.WriteLine("Oh man, {0} is a thief! {1} just got robbed. {2}'s new health is {3}", name, enemy.name, name, health);
        }

        public void GetAway()
        {
            health -= 10;
            Console.WriteLine("Okay, {0} got away but their new health is {1}", name, health);
            
        }
    }
}