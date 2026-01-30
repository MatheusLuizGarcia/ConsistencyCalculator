using ConsistencyCalculator.Core.Conditions;
using ConsistencyCalculator.Core.Entities;
using ConsistencyCalculator.Core.Simulation;
using ConsistencyCalculator.Desktop.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ConsistencyCalculator.Desktop.ViewModels
{
    public class SimulationViewModel : BaseViewModel
    {
        public ObservableCollection<SimulationResultRow> Results { get; }
            = new(); 

        public string ResultText =>
            string.Join(
                Environment.NewLine,
                Results.Select(r =>
                    $"{r.Label}: 5 cards = {Math.Round(r.Probability5, 2)}% | 6 cards = {Math.Round(r.Probability6, 2)}%"
                )
            );

        private readonly Simulator simulator;
        public Deck _deck { get; }

        public SimulationViewModel(Deck deck)
        {
            simulator = new Simulator();
            _deck = deck;
            RunSimulationCommand = new RelayCommand(() =>
                RunSimulation());

            Results.CollectionChanged += (_, __) =>
            {
                OnPropertyChanged(nameof(ResultText));
            };
        }

        public void RunSimulation()
        {
            Results.Clear();

            ISimulationCondition condition;
            try
            {
                condition = ConditionParser.Parse(ConditionDescription);
            }
            catch (Exception ex)
            {
                Results.Add(new SimulationResultRow(
                    ex.Message,
                    0,
                    0
                    ));

                return;
            }
            var request = new SimulationRequest(condition, Iterations);

            var result = simulator.Run(_deck, request);

            foreach (var tag5 in result.TagStats5)
            {
                var tag6 = result.TagStats6.First(t => t.Tag == tag5.Tag);

                Results.Add(new SimulationResultRow(
                    tag5.Tag,
                    tag5.Percentage,
                    tag6.Percentage));
            }

            Results.Add(new SimulationResultRow(
                condition.Description,
                result.FiveCardsSuccess,
                result.SixCardsSuccess));

            OnPropertyChanged(nameof(Results));
        }
        private int _iterations = 100000;

        public int Iterations
        {
            get => _iterations;
            set
            {
                if (_iterations == value)
                    return;

                _iterations = value;
                OnPropertyChanged(nameof(Iterations));
            }
        }

        private string _conditionDescription = string.Empty;

        public string ConditionDescription
        {
            get => _conditionDescription;
            set
            {
                if (_conditionDescription == value)
                    return;

                _conditionDescription = value;
                OnPropertyChanged(nameof(ConditionDescription));
            }
        }
        public RelayCommand RunSimulationCommand { get; }
    }
    public class SimulationResultRow
    {
        public SimulationResultRow(string label, double probability5, double probability6)
        {
            Label = label;
            Probability5 = probability5;
            Probability6 = probability6;
        }

        public string Label { get; set; } = ""; // starter, brick, Resultado
        public double Probability5 { get; set; }
        public double Probability6 { get; set; }
    }
}
