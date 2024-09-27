using Impar.Domain.Entities.Cards;

namespace Impar.DTO.Response.Cards
{
    public class CardResponse
    {
        /// <summary>
        /// Card id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Card name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Card status.
        /// </summary>
        public CardStatusEnum Status { get; set; }

        public static implicit operator CardResponse(Card c)
        {
            if (c is null)
                return null;

            return new CardResponse
            {
                Id = c.Id,
                Name = c.Name,
                Status = c.Status,
            };
        }
    }

    public static class CardExtensions
    {
        public static CardResponse ToResponse(this Card value)
        {
            if (value is null) return null;
            return (CardResponse)value;
        }

        public static IEnumerable<CardResponse> ToResponse(this IEnumerable<Card> values)
        {
            if (values is null) return null;
            return values.Select(b => b.ToResponse());
        }
    }
}
