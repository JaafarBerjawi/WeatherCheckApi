namespace Common.Entities
{
    public class HttpGetContext
    {
        public string BaseUrl { get; set; }
        public string HttpMethod { get; set; }
        public Dictionary<string, object> QueryParams { get; set; }
        public bool ThrowIfCallFailed { get; set; }

    }
}
