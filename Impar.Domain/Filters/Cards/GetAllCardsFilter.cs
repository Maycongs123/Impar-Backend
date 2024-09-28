using Impar.Common.Pagging;

namespace Impar.Domain.Filters.Cards
{
    public record GetAllCardsFilter : PageFilter
    {
        public string? Name { get; init; }
    }
}
