using Impar.Domain.Entities.Cards;

namespace Impar.Domain.Views.Cards
{
    public class GetAllCardsView
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public string Name { get; set; }
        public CardStatusEnum Status { get; set; }
        public string PhotoBase64 { get; set; }
    }
}
