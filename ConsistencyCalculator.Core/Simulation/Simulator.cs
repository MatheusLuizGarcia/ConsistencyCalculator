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

                UpdateTagStats(tagStats5, hand5, request.Condition);
                UpdateTagStats(tagStats6, hand6, request.Condition);
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
            Hand hand,
            ISimulationCondition condition)
        {
            if (condition is not TagComparisonCondition tagCondition)
                return;

            foreach (var kv in stats)
            {
                int count = hand.Cards.Count(c => c.Tags.Contains(kv.Key));

                bool contributes = tagCondition.Operator.Compare(
                    count,
                    tagCondition.Value);

                kv.Value.Register(contributes);
            }
        }

        private static double Percent(int value, int total)
            => total == 0 ? 0 : (double)value / total * 100;
    }
}
