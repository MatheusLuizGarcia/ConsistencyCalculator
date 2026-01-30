using ConsistencyCalculator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Import
{
    public class YdkImporter
    {
        private readonly CardResolver _resolver = new();

        public async Task<Deck> ImportAsync(string[] lines)
        {
            var deck = new Deck();
            var counts = new Dictionary<string, int>();

            bool readingMain = false;

            foreach (var raw in lines)
            {
                var line = raw.Trim();

                if (line.StartsWith("#main"))
                {
                    readingMain = true;
                    continue;
                }

                if (line.StartsWith("#extra") || line.StartsWith("!side"))
                {
                    break;
                }

                if (!readingMain)
                    continue;

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                counts.TryAdd(line, 0);
                counts[line]++;
            }

            foreach (var (id, qty) in counts)
            {
                var card = await _resolver.ResolveAsync(id);
                deck.AddCard(card, qty);
            }

            return deck;
        }
    }
}
