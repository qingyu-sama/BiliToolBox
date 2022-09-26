namespace BiliToolBox.Tools
{
    public class WebTool
    {
        private static readonly HttpClient httpClient = new();

        public static string DoGetString(string url)
        {
            return httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        public static string SendRequestGetString(string url, HttpMethod httpMethod, Dictionary<string, string>? headers = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            if (headers != null)
            {
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);
            }
            return httpClient.Send(request).Content.ReadAsStringAsync().Result;
        }
    }
}
