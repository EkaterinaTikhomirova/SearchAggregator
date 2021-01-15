using System;
using System.Collections.Generic;

namespace SearchAggregator.DataAccess
{
    public class Keyword : BaseEntity
    {
        public string Word { get; set; }
        public Guid? PredecessorWordId { get; set; }

        public Keyword PredecessorWord { get; set; }
        public List<KeywordResource> KeywordResources { get; set; }
    }
}
