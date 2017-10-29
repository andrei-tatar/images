namespace Backend.Contracts.Queries
{
    public class ListImagesRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Filter { get; set; }
        public ListImageSortBy SortBy { get; set; }

        public enum ListImageSortBy
        {
            Date,
            Location,
        }
    }
}