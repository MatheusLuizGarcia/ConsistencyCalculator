using ConsistencyCalculator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Entities
{
    public class Deck
    {
        public List<DeckEntry> Entries { get; } = new();

        public void AddCard(Card card, int quantity)
        {
            var entry = Entries.FirstOrDefault(e => e.Card.Name == card.Name);

            if (entry != null)
            {
                entry.Quantity += quantity;
            }
            else
            {
                Entries.Add(new DeckEntry(card, quantity));
            }
        }

        public void RemoveCard(string cardName)
        {
            Entries.RemoveAll(e => e.Card.Name == cardName);
        }

        public int TotalCards => Entries.Sum(e => e.Quantity);
    }
}
