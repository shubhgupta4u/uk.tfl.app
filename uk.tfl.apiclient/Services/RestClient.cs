using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using uk.tfl.apiclient.Helpers;
using uk.tfl.apiclient.Interfaces;

namespace uk.tfl.apiclient.Services
{
    public class RestClient : HttpClient, IRestClient
    {
        private readonly string _baseUri;
        private readonly string _apiKey;
        private readonly string _apiId;
        Dictionary<string, string> _defaultQueries;


        public RestClient(IConfiguration configuration)
        {
            InitializeTlsProtocol();
            this._defaultQueries = new Dictionary<string, string>();
            this._baseUri = configuration["ApiSetting:BaseUrl"];
            this._apiKey = configuration["ApiSetting:ApiKey"];
            this._apiId = configuration["ApiSetting:ApiId"];
            this._defaultQueries.Add("app_id", this._apiId);
            this._defaultQueries.Add("app_key", this._apiKey);
        }

        public async Task<T> GetAsync<T>(string url, Dictionary<string, string> query)
        {
            foreach(var key in this._defaultQueries.Keys)
            {
                if (!query.ContainsKey(key))
                {
                    query.Add(key, this._defaultQueries[key]);
                }
            }
            DefaultRequestHeaders.Clear();
            DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };

            using (HttpResponseMessage response = await GetAsync(Utils.AddQueryString(this._baseUri + url, query)))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else if(response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception(string.Format("{0} is not a valid road", url.Substring(url.IndexOf("/")+1)));
                }
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<T> GetAsync<T>(string url)
        {

            return await this.GetAsync<T>(url, this._defaultQueries);
        }

        private void InitializeTlsProtocol()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }
    }
}
