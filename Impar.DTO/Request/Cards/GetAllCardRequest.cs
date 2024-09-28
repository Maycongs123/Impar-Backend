
using Impar.Common.Pagging;
using Impar.Domain.Filters.Cards;

namespace Impar.DTO.Request.Cards
{
    public record GetAllCardRequest : PageRequest
    {
        public string? Name { get; set; }

        public override GetAllCardsFilter ToPageFilter() => (GetAllCardsFilter)this;

        public static implicit operator GetAllCardsFilter(GetAllCardRequest request)
        {
            if (request is null)
                return null;

            var pageFilter = ToPageFilter(request);

            return new GetAllCardsFilter
            {
                Name = request.Name,
                OrderBy = pageFilter.OrderBy,
                Offset = pageFilter.Offset,
                PageSize = pageFilter.PageSize
            };
        }
    }
}
