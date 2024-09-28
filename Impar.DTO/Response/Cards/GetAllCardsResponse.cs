using Impar.Domain.Entities.Cards;
using Impar.Domain.Views.Cards;

namespace Impar.DTO.Response.Cards
{
    /// <summary>
    /// Response model for retrieving all cards.
    /// </summary>
    public class GetAllCardsResponse
    {
        /// <summary>
        /// Unique identifier for the card.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Name of the card.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Identifier for the photo associated with the card.
        /// </summary>
        public int PhotoId { get; init; }

        /// <summary>
        /// Photo of the card in Base64 format.
        /// </summary>
        public string PhotoBase64 { get; init; }

        /// <summary>
        /// Status of the card, represented by the enum CardStatusEnum.
        /// </summary>
        public CardStatusEnum Status { get; init; }

        public static implicit operator GetAllCardsResponse(GetAllCardsView cardsView)
        {
            if (cardsView is null) 
                return null;

            return new GetAllCardsResponse
            {
                Id = cardsView.Id,
                Name = cardsView.Name,
                Status = cardsView.Status,
                PhotoBase64 = cardsView.PhotoBase64,
                PhotoId = cardsView.PhotoId 
            };
        }
    }

    public static class GetAllCardExtensions
    {     
        public static GetAllCardsResponse ToResponse(this GetAllCardsView value)
            => (GetAllCardsResponse)value;
        public static IEnumerable<GetAllCardsResponse> ToResponse(this IEnumerable<GetAllCardsView> values)
            => values.Select(b => b.ToResponse());
    }

}
