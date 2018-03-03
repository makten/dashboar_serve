using System.Collections.Generic;

namespace dashboard.Core.Models
{
    //Re-usable Class
    public class QueryResult<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}