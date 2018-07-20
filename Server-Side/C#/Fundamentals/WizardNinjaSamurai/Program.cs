using System;

namespace WizardNinjaSamurai
{
    class Program
    {
        static void Main(string[] args)
        {
            Human richard = new Human("Richard");
            // Console.WriteLine("Human {0} info: {1} is strength", richard.name, richard.strength);

            Wizard john = new Wizard("John");
            // Console.WriteLine("Wizard {0} info: {1} is strength", john.name, john.strength);

            Ninja brad = new Ninja("Brad");

            // john.attack(richard);
            // john.heal();
            // brad.Steal(john);
            // brad.GetAway();

            Samurai virginia = new Samurai("Virginia");

            virginia.DeathBlow(richard); 
            for (int i = 0; i < 3; i++)
            {
                john.attack(richard);

            }    
            virginia.DeathBlow(richard);       
        }
    }
}
