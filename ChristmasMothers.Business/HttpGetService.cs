using System;
using System.Net.Http;
using System.Threading.Tasks;
using ChristmasMothers.Business.Interface;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace ChristmasMothers.Business
{
    /// <summary>
    /// Performs HTTP GET requests (uses memory caching for a little performance boost).
    /// </summary>
    /// <remarks>Use for relatively small requests that are subject to be repeated
    /// often.</remarks>
    public class HttpGetService : IHttpGetService
    {
        /// <summary>
        /// Cache entry keys prefix.
        /// </summary>
        private static string CacheKeyPrefix = "HttpService:";

        /// <summary>
        /// Cache entry items lifetime.
        /// </summary>
        private static TimeSpan CacheLifetime = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Memory cache instance.
        /// </summary>
        private IMemoryCache _cache;
        
        public HttpGetService(IMemoryCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Performs an HTTP GET request to the specified URI and parses the response body as
        /// a string.
        /// </summary>
        /// <param name="uri">URI to get.</param>
        /// <returns>Response body as a string.</returns>
        public async Task<string> GetAsync(string uri)
        {
            var key = CacheKeyPrefix + uri.ToString();

            return await _cache.GetOrCreateAsync(key, async entry =>
            {
                // Set cache entry lifetime.
                entry.AbsoluteExpiration = DateTimeOffset.Now.Add(CacheLifetime);

                // Perform actual fetch.
                using (var httpClient = new HttpClient())
                {
                    return await httpClient.GetStringAsync(uri);
                }
            });
        }

        /// <summary>
        /// Performs an HTTP GET request to the specified URI and parses the response body as
        /// JSON using the specified target type.
        /// </summary>
        /// <typeparam name="TResponse">Type of object to map the response to.</typeparam>
        /// <param name="uri">URI to get.</param>
        /// <returns>Response body as a <typeparamref name="TResponse"/>.</returns>
        public async Task<TResponse> GetJsonAsync<TResponse>(string uri)
        {
            return JsonConvert.DeserializeObject<TResponse>(await GetAsync(uri));
        }
    }
}
