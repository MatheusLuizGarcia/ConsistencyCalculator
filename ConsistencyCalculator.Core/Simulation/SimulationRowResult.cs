using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Simulation
{
    public class SimulationRowResult
    {
        public string Label { get; }

        public double FiveCardsPercentage { get; }
        public double SixCardsPercentage { get; }

        public SimulationRowResult(
            string label,
            double fiveCards,
            double sixCards)
        {
            Label = label;
            FiveCardsPercentage = fiveCards;
            SixCardsPercentage = sixCards;
        }
    }
}
