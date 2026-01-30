using ConsistencyCalculator.Core.Entities;
using ConsistencyCalculator.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsistencyCalculator.Desktop.ViewModels
{
    public class CardEntryViewModel : INotifyPropertyChanged
    {
        private readonly DeckEntry _entry;
        private string _tagsText = "";
        private int _quantity;

        public string Name { get; }
        public HashSet<string> Tags { get; private set; } = new();

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

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity == value)
                    return;

                _quantity = value;
                _entry.Quantity = value;
                OnPropertyChanged();
            }
        }

        public CardEntryViewModel(DeckEntry entry)
        {
            _entry = entry;

            Name = entry.Card.Name;
            _quantity = entry.Quantity;

            _tagsText = string.Join(", ", entry.Card.Tags);
        }

        /// <summary>
        /// Converts edited text into real tags.
        /// must be called at the end of editing a cell.
        /// </summary>
        public void CommitTags()
        {

            TagsText = Regex.Replace(TagsText, @"[^a-zA-Z0-9 ,_-]", "");
            var parsed = _tagsText
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            _entry.Card.Tags.Clear();
            foreach (var tag in parsed)
                _entry.Card.Tags.Add(tag);
            OnPropertyChanged(nameof(TagsText));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
