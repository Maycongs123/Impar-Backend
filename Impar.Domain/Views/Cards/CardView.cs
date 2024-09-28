using Impar.Domain.Entities.Cards;

namespace Impar.Domain.Views.Cards
{
    public class CardView : Card
    {
        public string PhotoBase64 { get; set; }
        public int PhotoId { get; set; }
    }
}
