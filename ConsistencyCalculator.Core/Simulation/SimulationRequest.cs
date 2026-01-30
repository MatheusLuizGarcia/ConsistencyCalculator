using ConsistencyCalculator.Core.Conditions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Simulation
{
    public class SimulationRequest
    {
        public ISimulationCondition Condition { get; }
        public int Iterations { get; }

        public SimulationRequest(ISimulationCondition condition, int iterations)
        {
            Condition = condition;
            Iterations = iterations;
        }
    }
}
