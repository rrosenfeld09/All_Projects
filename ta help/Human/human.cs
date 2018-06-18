using System;

namespace Human
{
     public class Human
    {
        public string name;
        public int strength = 3;
        public int intelligence = 3;
        public int dexterity =3;
        public int health = 100;

        public Human(string na)
        {
            name = na;
        }

        public Human(string na, int st, int intel, int de, int he)
        {
            name = na;
            strength = st;
            intelligence = intel;
            dexterity = de;
            health = he;
        }

        public void attack(object humanToAttack)
        {
            
        }
    }
}