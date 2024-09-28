using Impar.Domain.Entities.Cards;
using Impar.Domain.Views.Cards;

namespace Impar.DTO.Response.Cards
{
    public class GetAllCardsResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int PhotoId { get; init; }
        public string PhotoBase64 { get; init; }
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
