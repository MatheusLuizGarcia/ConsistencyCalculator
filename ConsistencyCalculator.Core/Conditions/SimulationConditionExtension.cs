using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Conditions
{
    public static class SimulationConditionExtension
    {
        public static HashSet<string> GetUsedTags(this ISimulationCondition condition)
        {
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            Collect(condition, set);
            return set;
        }

        private static void Collect(ISimulationCondition condition, HashSet<string> set)
        {
            switch (condition)
            {
                case TagComparisonCondition tag:
                    set.Add(tag.Tag);
                    break;

                case CompositeCondition composite:
                    foreach (var child in composite.Conditions)
                        Collect(child, set);
                    break;
            }
        }
    }
}
