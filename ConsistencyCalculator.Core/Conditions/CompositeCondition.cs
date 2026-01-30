using ConsistencyCalculator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Conditions
{
    public enum LogicalOperator
    {
        And,
        Or
    }

    public class CompositeCondition : ISimulationCondition
    {
        public LogicalOperator Operator { get; }
        public IReadOnlyList<ISimulationCondition> Conditions { get; }

        public CompositeCondition(
            LogicalOperator op,
            IEnumerable<ISimulationCondition> conditions)
        {
            Operator = op;
            Conditions = conditions.ToList();

            if (Conditions.Count < 2)
                throw new ArgumentException("CompositeCondition requires at least two conditions");
        }

        public bool Evaluate(Hand hand)
        {
            return Operator switch
            {
                LogicalOperator.And => Conditions.All(c => c.Evaluate(hand)),
                LogicalOperator.Or => Conditions.Any(c => c.Evaluate(hand)),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public string Description =>
            string.Join(
                Operator == LogicalOperator.And ? " && " : " || ",
                Conditions.Select(c => c.Description));
    }

}
