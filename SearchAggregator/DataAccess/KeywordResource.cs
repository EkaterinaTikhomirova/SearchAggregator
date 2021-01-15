using System;

namespace SearchAggregator.DataAccess
{
    public class KeywordResource
    {
        public Guid KeywordId { get; set; }
        public Guid ResourceId { get; set; }

        public Keyword Keyword { get; set; }       
        public Resource Resource { get; set; }
    }
}
