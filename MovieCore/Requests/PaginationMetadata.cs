namespace MovieCore.Requests
{
    public class PaginationMetadata
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }


        public PaginationMetadata(int pageSize, int currentPage, int totalItemCount)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalItemCount = totalItemCount;
            TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        }
    }
}
