using ConsistencyCalculator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsistencyCalculator.Desktop.ViewModels
{
    public class MainViewModel
    {
        public DeckEditorViewModel DeckEditor { get; }
        public SimulationViewModel Simulation { get; }

        public MainViewModel()
        {
            var deck = new Deck();

            DeckEditor = new DeckEditorViewModel(deck);
            Simulation = new SimulationViewModel(deck);
        }
    }

}
