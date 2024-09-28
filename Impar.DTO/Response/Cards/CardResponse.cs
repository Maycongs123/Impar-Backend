using Impar.Domain.Entities.Cards;
using Impar.Domain.Views.Cards;

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

        /// <summary>
        /// Photo of the card in base64.
        /// </summary>
        public string PhotoBase64 { get; init; }

        /// <summary>
        /// PhotoId of the card.
        /// </summary>
        public int PhotoId { get; init; }

        public static implicit operator CardResponse(CardView c)
        {
            if (c is null)
                return null;

            return new CardResponse
            {
                Id = c.Id,
                Name = c.Name,
                Status = c.Status,
                PhotoBase64 = c.PhotoBase64,
                PhotoId = c.PhotoId
            };
        }
    }

    public static class CardExtensions
    {
        public static CardResponse ToResponse(this Card value, string base64Photo = null)
        {
            if (value is null) return null;

            return new CardResponse
            {
                Id = value.Id,
                Name = value.Name,
                Status = value.Status,
                PhotoId = value.PhotoId,
                PhotoBase64 = base64Photo
            };
        }

        public static CardResponse ToResponse(this CardView value)
        {
            if (value is null) return null;
            return (CardResponse)value;
        }

        public static IEnumerable<CardResponse> ToResponse(this IEnumerable<CardView> values)
        {
            if (values is null) return null;
            return values.Select(b => b.ToResponse());
        }
    }

}
