using ConsistencyCalculator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Core.Simulation
{
    public class SimulationResult
    {
        public double FiveCardsSuccess { get; }
        public double SixCardsSuccess { get; }

        public IReadOnlyList<TagStatistics> TagStats5 { get; }
        public IReadOnlyList<TagStatistics> TagStats6 { get; }

        public SimulationResult(
            double fiveCards,
            double sixCards,
            List<TagStatistics> tagStats5,
            List<TagStatistics> tagStats6)
        {
            FiveCardsSuccess = fiveCards;
            SixCardsSuccess = sixCards;
            TagStats5 = tagStats5;
            TagStats6 = tagStats6;
        }
    }
}
