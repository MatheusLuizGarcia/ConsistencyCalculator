using ConsistencyCalculator.Core.Conditions;
using ConsistencyCalculator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Conditions
{
    public class TagComparisonCondition : ISimulationCondition
    {
        public string Tag { get; }
        public ComparisonOperator Operator { get; }
        public int Value { get; }

        public string Description => $"{Tag} {OperatorToString()} {Value}";

        public TagComparisonCondition(
            string tag,
            ComparisonOperator op,
            int value)
        {
            Tag = tag;
            Operator = op;
            Value = value;
        }
        public bool Evaluate(Hand hand)
        {
            int count = hand.Cards.Count(c => c.Tags.Contains(Tag));
            return Operator.Compare(count, Value);
        }
        public bool IsSatisfied(Dictionary<string, int> tagCounts)
        {
            tagCounts.TryGetValue(Tag, out int count);

            return Operator switch
            {
                ComparisonOperator.GreaterThan => count > Value,
                ComparisonOperator.GreaterOrEqual => count >= Value,
                ComparisonOperator.LessThan => count < Value,
                ComparisonOperator.LessOrEqual => count <= Value,
                ComparisonOperator.Equal => count == Value,
                _ => false
            };
        }

        private string OperatorToString() => Operator switch
        {
            ComparisonOperator.GreaterThan => ">",
            ComparisonOperator.GreaterOrEqual => ">=",
            ComparisonOperator.LessThan => "<",
            ComparisonOperator.LessOrEqual => "<=",
            ComparisonOperator.Equal => "==",
            _ => "?"
        };
    }
}

