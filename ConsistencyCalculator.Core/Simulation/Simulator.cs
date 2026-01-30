using ConsistencyCalculator.Core.Conditions;
using ConsistencyCalculator.Core.Entities;
using ConsistencyCalculator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Simulation
{
    public class Simulator
    {
        private readonly Random _rng = new();

        public SimulationResult Run(
            Deck deck,
            SimulationRequest request)
        {
            var usedTags = request.Condition.GetUsedTags();

            var tagStats5 = CreateTagStats(usedTags);
            var tagStats6 = CreateTagStats(usedTags);

            int success5 = 0;
            int success6 = 0;

            for (int i = 0; i < request.Iterations; i++)
            {
                var hand5 = Hand.Draw(deck, 5, _rng);
                var hand6 = Hand.Draw(deck, 6, _rng);

                bool ok5 = request.Condition.Evaluate(hand5);
                bool ok6 = request.Condition.Evaluate(hand6);

                if (ok5) success5++;
                if (ok6) success6++;

                UpdateTagStats(tagStats5, hand5);
                UpdateTagStats(tagStats6, hand6);
            }

            return new SimulationResult(
                Percent(success5, request.Iterations),
                Percent(success6, request.Iterations),
                tagStats5.Values.ToList(),
                tagStats6.Values.ToList());
        }
        private Dictionary<string, TagStatistics> CreateTagStats(
            IEnumerable<string> tags)
        {
            return tags.ToDictionary(
                t => t,
                t => new TagStatistics(t),
                StringComparer.OrdinalIgnoreCase);
        }

        private void UpdateTagStats(
            Dictionary<string, TagStatistics> stats,
            Hand hand)
        {
            foreach (var kv in stats)
            {
                bool present = hand.Cards.Any(c => c.Tags.Contains(kv.Key));
                kv.Value.Register(present);
            }
        }

        private static double Percent(int value, int total)
            => total == 0 ? 0 : (double)value / total * 100;
    }
}
