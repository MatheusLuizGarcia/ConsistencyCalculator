using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsistencyCalculator.Core.Conditions
{
    public static class ConditionParser
    {
        private static readonly Regex ComparisonRegex =
            new(@"^\s*(\w+)\s*(>=|<=|==|>|<)\s*(\d+)\s*$");

        public static ISimulationCondition Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Condition is empty");

            input = input.Trim();

            if (input.Contains("||"))
            {
                var parts = input
                    .Split("||", StringSplitOptions.TrimEntries);

                return new CompositeCondition(
                    LogicalOperator.Or,
                    parts.Select(Parse)
                );
            }

            if (input.Contains("&&"))
            {
                var parts = input
                    .Split("&&", StringSplitOptions.TrimEntries);

                return new CompositeCondition(
                    LogicalOperator.And,
                    parts.Select(Parse)
                );
            }
            return ParseComparison(input);
        }

        private static ISimulationCondition ParseComparison(string expression)
        {
            var match = ComparisonRegex.Match(expression);

            if (!match.Success)
                throw new FormatException($"Invalid condition: {expression}");

            string tag = match.Groups[1].Value;
            string op = match.Groups[2].Value;
            int value = int.Parse(match.Groups[3].Value);

            return new TagComparisonCondition(
                tag,
                ParseOperator(op),
                value
            );
        }

        private static ComparisonOperator ParseOperator(string op) => op switch
        {
            ">" => ComparisonOperator.GreaterThan,
            ">=" => ComparisonOperator.GreaterOrEqual,
            "<" => ComparisonOperator.LessThan,
            "<=" => ComparisonOperator.LessOrEqual,
            "==" => ComparisonOperator.Equal,
            _ => throw new FormatException($"Unknown operator {op}")
        };
    }
}
