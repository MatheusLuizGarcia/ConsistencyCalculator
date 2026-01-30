using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Conditions
{
    public static class ComparisonOperatorExtension
    {
        public static bool Compare(
        this ComparisonOperator op,
        int left,
        int right)
        {
            return op switch
            {
                ComparisonOperator.GreaterThan => left > right,
                ComparisonOperator.GreaterOrEqual => left >= right,
                ComparisonOperator.LessThan => left < right,
                ComparisonOperator.LessOrEqual => left <= right,
                ComparisonOperator.Equal => left == right,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
