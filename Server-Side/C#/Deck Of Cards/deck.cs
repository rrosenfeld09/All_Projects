using System;
using System.Collections.Generic;
namespace Deck_Of_Cards
{
    public class Deck
    {


        public List<Card> cardsList = new List<Card>();
        string[] suits = {"hearts", "diamonds", "spades", "clubs"};

        List<Card> backup;
            
        // initialize

        public Deck()
        {
            foreach(string suit in suits)
            {
                for(int i =0; i < 13; i++)
                {
                    string sv = suit;
                    int v = (i+1);
                    string s = "";
                    if(v ==1)
                    {
                        s = "ace";
                    }
                    else if (v ==2)
                    {
                        s = "2";
                    }
                    else if (v ==3)
                    {
                        s = "3";
                    }
                    else if (v ==4)
                    {
                        s = "4";
                    }
                    else if (v ==5)
                    {
                        s = "5";
                    }
                    else if (v ==6)
                    {
                        s = "6";
                    }
                    else if (v ==7)
                    {
                        s = "7";
                    }
                    else if (v ==8)
                    {
                        s = "8";
                    }
                    else if (v ==9)
                    {
                        s = "9";
                    }
                    else if (v ==10)
                    {
                        s = "10";
                    }
                    else if (v ==11)
                    {
                        s = "jack";
                    }
                    else if (v ==12)
                    {
                        s = "queen";
                    }
                    else if (v ==13)
                    {
                        s = "king";
                    }


                    cardsList.Add(new Card(s, sv, v));
                    
                }
            }

            this.shuffle();
            backup = cardsList;


        }
        public Card deal()
        {
            if (cardsList.Count > 0)
            {
                Card card = cardsList[0];
                cardsList.RemoveAt(0);
                return card;
            }
            return null;
        }

        public void reset()
        {
            cardsList = backup;
        }

        public void shuffle()
        {
            Random rand = new Random();

            for (int i = 0; i <cardsList.Count; i++)
            {
                Card temp = cardsList[i];
                cardsList.RemoveAt(i);
                cardsList.Insert(rand.Next(0,52), temp);
            }
            
        }

    }
}