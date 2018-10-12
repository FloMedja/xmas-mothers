using System.Threading.Tasks;

namespace ChristmasMothers.Business.Interface
{
    /// <summary>
    /// Performs HTTP GET requests (uses memory caching for a little performance boost).
    /// </summary>
    /// <remarks>Use for relatively small requests that are subject to be repeated
    /// often.</remarks>
    public interface IHttpGetService
    {
        /// <summary>
        /// Performs an HTTP GET request to the specified URI and parses the response body as
        /// a string.
        /// </summary>
        /// <param name="uri">URI to get.</param>
        /// <returns>Response body as a string.</returns>
        Task<string> GetAsync(string uri);

        /// <summary>
        /// Performs an HTTP GET request to the specified URI and parses the response body as
        /// JSON using the specified target type.
        /// </summary>
        /// <typeparam name="TResponse">Type of object to map the response to.</typeparam>
        /// <param name="uri">URI to get.</param>
        /// <returns>Response body as a <typeparamref name="TResponse"/>.</returns>
        Task<TResponse> GetJsonAsync<TResponse>(string uri);
    }
}
