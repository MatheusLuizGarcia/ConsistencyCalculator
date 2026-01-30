using ConsistencyCalculator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Models
{
    public class Hand
    {
        public IReadOnlyList<Card> Cards { get; }

        public Hand(IEnumerable<Card> cards)
        {
            Cards = cards.ToList();
        }

        public static Hand Draw(Deck deck, int size, Random rng)
        {
            var pool = new List<Card>();

            foreach (var entry in deck.Entries)
            {
                for (int i = 0; i < entry.Quantity; i++)
                    pool.Add(entry.Card);
            }

            var drawn = pool
                .OrderBy(_ => rng.Next())
                .Take(size)
                .ToList();

            return new Hand(drawn);
        }
    }
}
