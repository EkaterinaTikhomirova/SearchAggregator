using System.Collections.Generic;

namespace SearchAggregator.DataAccess.Models
{
    public class Keyword : BaseEntity
    {
        public string Word { get; set; }
        public List<KeywordResource> KeywordResources { get; set; }
    }
}
