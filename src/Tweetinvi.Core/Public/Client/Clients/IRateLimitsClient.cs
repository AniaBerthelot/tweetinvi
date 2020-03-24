using System.Threading.Tasks;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace Tweetinvi.Client
{
    public interface IRateLimitsClient
    {
        /// <summary>
        /// Load the client's rate limits in the cache
        /// </summary>
        Task InitializeRateLimitsManager();

        /// <inheritdoc cref="GetRateLimits(IGetRateLimitsParameters)" />
        Task<ICredentialsRateLimits> GetRateLimits();

        /// <inheritdoc cref="GetRateLimits(IGetRateLimitsParameters)" />
        Task<ICredentialsRateLimits> GetRateLimits(RateLimitsSource from);

        /// <summary>
        /// Get the rate limits of the current client
        /// </summary>
        /// <para> Read more : https://developer.twitter.com/en/docs/developer-utilities/rate-limit-status/api-reference/get-application-rate_limit_status </para>
        /// <returns>The client's rate limits</returns>
        Task<ICredentialsRateLimits> GetRateLimits(IGetRateLimitsParameters parameters);

        /// <inheritdoc cref="GetEndpointRateLimit(IGetEndpointRateLimitsParameters)"/>
        Task<IEndpointRateLimit> GetEndpointRateLimit(string url);

        /// <inheritdoc cref="GetEndpointRateLimit(IGetEndpointRateLimitsParameters)"/>
        Task<IEndpointRateLimit> GetEndpointRateLimit(string url, RateLimitsSource from);

        /// <summary>
        /// Get a specific endpoint's rate limits of the current client
        /// </summary>
        /// <para> Read more : https://developer.twitter.com/en/docs/developer-utilities/rate-limit-status/api-reference/get-application-rate_limit_status </para>
        /// <returns>The endpoint's rate limits, or null if the endpoint is not support by Tweetinvi</returns>
        Task<IEndpointRateLimit> GetEndpointRateLimit(IGetEndpointRateLimitsParameters parameters);

        /// <inheritdoc cref="WaitForQueryRateLimit(IEndpointRateLimit)" />
        Task WaitForQueryRateLimit(string url);

        /// <summary>
        /// Wait for new requests to a specific endpoint become available
        /// </summary>
        Task WaitForQueryRateLimit(IEndpointRateLimit endpointRateLimit);

        /// <summary>
        /// Clear the rate limits cached for a specific set of credentials
        /// </summary>
        Task ClearRateLimitCache(IReadOnlyTwitterCredentials credentials);

        /// <summary>
        /// Clear the rate limits cached for the client's credentials
        /// </summary>
        Task ClearRateLimitCache();

        /// <summary>
        /// Clear the rate limits of all the credentials
        /// </summary>
        Task ClearAllRateLimitCache();
    }
}