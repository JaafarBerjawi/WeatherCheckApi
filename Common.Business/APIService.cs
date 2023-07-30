using Common.Entities;
using Newtonsoft.Json;

namespace Common.Business
{
    public static class APIService
    {
        // Single Client for whole application
        private static readonly HttpClient httpClient = new HttpClient();
        public static T Get<T>(HttpGetContext getContext)
            where T : IHttpResponse
        {
            var uriBuilder = new UriBuilder(getContext.BaseUrl + (!string.IsNullOrEmpty(getContext.HttpMethod) ? $"/{getContext.HttpMethod.Trim()}" : ""))
            {
                Query = ToQueryString(getContext.QueryParams)
            };

            try
            {
                // Send a GET request
                HttpResponseMessage response = httpClient.GetAsync(uriBuilder.ToString()).Result;

                // Check if the response is successful and if the caller requires throwing if call fails
                if (getContext.ThrowIfCallFailed && !response.IsSuccessStatusCode)
                {
                    throw new Exception("Call failed");
                }
                //Read the content as a string
                string content = response.Content.ReadAsStringAsync().Result;

                var result = new JsonSerializer().Deserialize<T>(new JsonTextReader(new StringReader(content))) ?? throw new Exception("Could not deserialize response");
                result.StatusCode = response.StatusCode;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static string ToQueryString(Dictionary<string, object> dict)
        {
            var queryItems = new List<string>();
            foreach (var kvp in dict)
            {
                if (kvp.Value != null)
                    queryItems.Add($"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value.ToString())}");
            }
            return string.Join("&", queryItems);
        }
    }
}
