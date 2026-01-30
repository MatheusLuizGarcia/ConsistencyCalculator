using ConsistencyCalculator.Core.Entities;

namespace ConsistencyCalculator.Desktop.ViewModels
{
    public class CardViewModel
    {
        private readonly Card _card;

        public string Name => _card.Name;
        public string TagsAsText => string.Join(", ", _card.Tags);

        public CardViewModel(Card card)
        {
            _card = card;
        }
    }
}
