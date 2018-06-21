using System;
using System.Collections.Generic;
namespace Deck_Of_Cards 
{
    public class Card
    {
        public string stringVal {get; set;}
        public string suitVal{get; set;}
        public int val {get; set;}

        public Card(string s, string sv, int v)
        {
            stringVal = s;
            suitVal = sv;
            val = v;
        }

    }
}