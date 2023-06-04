namespace Entities.Models
{
    public abstract class QueryStringParameters
    {
        const int maxPageSize = 10;
        private int _pageSize = 1;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; }
    }
}
