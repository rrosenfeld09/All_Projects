using System;

namespace HumanOOP
{
    public class Human
    {
        public string name = "";
        public int strenth = 3;
        public int intelligence = 3;
        public int dexterity = 3;
        public int health = 100;

        public Human (string newName)
        {
            name = newName;
        }

        public Human (string newName, int newStrength, int newIntelligence, int newDexterity, int newHealth)
        {
            name = newName;
            strenth = newStrength;
            intelligence = newIntelligence;
            dexterity = newDexterity;
            health = newHealth;
        }

        public void Attact(Human attacked)
        {
            int damage = this.strenth * 5;
            attacked.health -= damage;

            Console.WriteLine("Oh no! Looks like {0} has attacked {1}! {2} was assessed {3} damage!", this.name, attacked.name, attacked.name, damage);
        }
    }
}