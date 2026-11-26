namespace LibrarySystem.BLL.Models
{
    public sealed class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        private PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize) : base(items)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            pageIndex = Math.Max(pageIndex, 1);
            pageSize = Math.Max(pageSize, 1);
            var items = source.ToList();

            return new PaginatedList<T>(
                items.Skip((pageIndex - 1) * pageSize).Take(pageSize),
                items.Count,
                pageIndex,
                pageSize);
        }

        public static PaginatedList<T> Create(IEnumerable<T> items, int totalCount, int pageIndex, int pageSize)
        {
            return new PaginatedList<T>(items, totalCount, pageIndex, pageSize);
        }
    }
}