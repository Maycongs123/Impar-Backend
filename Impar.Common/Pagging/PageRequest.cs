namespace Impar.Common.Pagging
{
    public record PageRequest
    {
        public OrderByRequest OrderBy { get; init; }
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }

        public virtual PageFilter ToPageFilter()
        {
            return ToPageFilter(this);
        }

        public static PageFilter ToPageFilter(PageRequest request)
        {
            return new PageFilter
            {
                OrderBy = request.OrderBy,
                PageSize = request.PageSize,
                Offset = (request.CurrentPage - 1) * request.PageSize
            };
        }
    }

    public record OrderByRequest
    {
        public string Column { get; init; }
        public bool Ascendent { get; init; }

        public static implicit operator OrderBy(OrderByRequest orderBy)
        {
            return new OrderBy
            {
                Column = orderBy.Column,
                Direction = orderBy.Ascendent ? "Asc" : "Desc",
            };
        }
    }
}
