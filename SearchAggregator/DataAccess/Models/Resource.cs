using System.Collections.Generic;

namespace SearchAggregator.DataAccess.Models
{
    public class Resource : BaseEntity
    {
        public string UrlAddress { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<KeywordResource> KeywordResources { get; set; }
    }
}
