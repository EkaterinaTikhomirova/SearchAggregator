namespace SearchAggregator.Services.CustomSearchers.SeacherResponses
{
    public class BingCustomSearchResponse
    {
        public WebPages webPages { get; set; }
    }

    public class WebPages
    {
        public WebPage[] value { get; set; }
    }
    public class WebPage
    {
        public string name { get; set; }
        public string url { get; set; }
        public string snippet { get; set; }
    }
}
