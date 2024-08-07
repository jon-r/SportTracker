namespace SportTracker.Server.Data
{
    public class PagedResult<T> : PagedResultBase
        where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = [];
        }
    }
}
