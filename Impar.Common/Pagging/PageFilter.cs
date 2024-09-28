namespace Impar.Common.Pagging
{
    public record PageFilter
    {
        public OrderBy OrderBy { get; init; }
        public int PageSize { get; init; }
        public int Offset { get; init; }

        public int CurrentPage => ((Offset / PageSize) + 1);
    }

    public record OrderBy
    {
        public string Column { get; init; }
        public string Direction { get; init; }
    }
}
