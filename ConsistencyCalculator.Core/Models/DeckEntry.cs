using ConsistencyCalculator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Models
{
    public class DeckEntry
    {
        public Card Card { get; }
        public int Quantity { get; set; }

        public DeckEntry(Card card, int quantity)
        {
            Card = card;
            Quantity = quantity;
        }
    }
}
