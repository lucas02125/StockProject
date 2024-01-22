namespace api.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;

        /// <summary>
        /// Following below is pagitation
        /// skip(2) means pass first two values and display the rest
        /// take(2) means take first two and leave it at that
        /// </summary>
        public int PageNumber { get; set; } = 1;
        public int SkipNumber { get; set; } = 20;
    }
}