using System;

namespace Ninja_Wizard_Samurai
{
    class Program
    {
        static void Main(string[] args)
        {
            Samurai richard = new Samurai("Richard");
            Human stan = new Human("Stan");
            Wizard jimmy = new Wizard("Jimmy");
            Ninja sarah = new Ninja("Sarah");

            jimmy.fireball(stan);
            stan.attack(richard);
            richard.meditate();
            sarah.steal(jimmy);

        }
    }
}
