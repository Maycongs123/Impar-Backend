namespace Impar.Domain.Entities.Cards
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PhotoId { get; set; }
        public CardStatusEnum Status { get; set; }
    }
}
