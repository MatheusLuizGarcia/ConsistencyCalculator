using ConsistencyCalculator.Core.Entities;
using ConsistencyCalculator.Core.Import;
using ConsistencyCalculator.Core.Models;
using ConsistencyCalculator.Desktop.Commands;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;

namespace ConsistencyCalculator.Desktop.ViewModels
{
    public class DeckEditorViewModel : BaseViewModel
    {
        private readonly Deck _deck; 
        public int TotalCards => Cards.Sum(c => c.Quantity); 
        
        private string _tagsText = "";

        public string TagsText
        {
            get => _tagsText;
            set
            {
                if (_tagsText == value)
                    return;

                _tagsText = value;
                OnPropertyChanged();
            }
        }
        private void Card_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CardEntryViewModel.Quantity))
                OnPropertyChanged(nameof(TotalCards));
        }

        public ObservableCollection<CardEntryViewModel> Cards { get; } = new();

        public ICommand AddCardCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand ImportYdkCommand { get; }
        public DeckEditorViewModel(Deck deck)
        {
            _deck = deck;

            Cards.CollectionChanged += (_, __) =>
            {
                OnPropertyChanged(nameof(TotalCards));

                foreach (var card in Cards)
                    card.PropertyChanged += Card_PropertyChanged;
            };


            AddCardCommand = new RelayCommand(AddCard);
            RemoveCommand = new RelayCommand<CardEntryViewModel>(Remove); 
            ImportYdkCommand = new RelayCommand(async () => await ImportYdk());

            Reload();
        }
        private async Task ImportYdk()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Yu-Gi-Oh Deck (*.ydk)|*.ydk"
            };

            if (dialog.ShowDialog() != true)
                return;

            var lines = File.ReadAllLines(dialog.FileName);

            var importer = new YdkImporter();
            var importedDeck = await importer.ImportAsync(lines);

            _deck.Entries.Clear();
            foreach (var entry in importedDeck.Entries)
                _deck.Entries.Add(entry);

            Reload();
        }


        private void Reload()
        {
            Cards.Clear();
            foreach (var entry in _deck.Entries)
                Cards.Add(new CardEntryViewModel(entry));
        }

        private void AddCard()
        {
            if (string.IsNullOrWhiteSpace(CardName))
                return;

            var tags = CardTags
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .ToHashSet();

            var card = new Card
            {
                Name = CardName,
                Tags = tags
            };

            _deck.AddCard(card, Quantity);

            Reload();

            CardName = "";
            CardTags = "";
            Quantity = 1;

            OnPropertyChanged(nameof(CardName));
            OnPropertyChanged(nameof(CardTags));
            OnPropertyChanged(nameof(Quantity));
        }

        private void Remove(CardEntryViewModel entry)
        {
            _deck.RemoveCard(entry.Name);
            Reload();
        }

        public string CardName { get; set; } = "";
        public string CardTags { get; set; } = "";
        public int Quantity { get; set; } = 1;
    }
}
