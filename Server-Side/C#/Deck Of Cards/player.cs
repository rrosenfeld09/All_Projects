using System;
using System.Collections.Generic;

namespace Deck_Of_Cards
{
    public class Player
    {
        public string name {get; set;}

        private Deck deck = new Deck();
        
        public List<Card> hand = new List<Card>();

        public List<Card> getHand()
        {
            return hand;
        }

        

        public void draw()
        {
            for (int i = 0; i < 5; i++)
            {
                hand.Add(deck.deal());
            }   
        }

        public Card discard(int idx)
        {
            if (idx - 1 < hand.Count)
            {
                Card temp = hand[idx];
                hand.RemoveAt(idx);
                return temp;
            }
            return null;
        }
    }
}