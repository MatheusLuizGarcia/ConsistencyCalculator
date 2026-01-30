using ConsistencyCalculator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Conditions
{
    public interface ISimulationCondition
    {
        string Description { get; }
        bool Evaluate(Hand hand);
    }
}
