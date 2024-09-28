using Impar.Domain.Entities.Cards;

namespace Impar.DTO.Request.Cards
{
    public class PatchRequest
    {
        public string? Name { get; init; }
        public CardStatusEnum? Status { get; init; }
        public string? PhotoBase64 { get; init; }
        public int? PhotoId { get; init; }
    }
}
