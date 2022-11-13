using Microsoft.Extensions.Logging;
using PTI.Microservices.Library.Configuration;
using PTI.Microservices.Library.Interceptors;
using PTI.Microservices.Library.Models.IpStackService.GetGeoLocationInfo;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PTI.Microservices.Library.Services
{
    /// <summary>
    /// Service i ncahrge of exposing access to IpStack functionality
    /// </summary>
    public sealed class IpStackService
    {
        private ILogger<IpStackService> Logger { get; }
        private CustomHttpClient CustomHttpClient { get; }
        private IpStackConfiguration IpStackConfiguration { get; }

        /// <summary>
        /// Creates a new instance of <see cref="IpStackService"/>
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ipStackConfiguration"></param>
        /// <param name="customHttpClient"></param>
        public IpStackService(ILogger<IpStackService> logger, 
            IpStackConfiguration ipStackConfiguration, CustomHttpClient customHttpClient)
        {
            this.Logger = logger;
            this.CustomHttpClient = customHttpClient;
            this.IpStackConfiguration = ipStackConfiguration;
        }

        /// <summary>
        /// Gets the geo location info for the specified ip address
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetGeoLocationInfoResponse> GetIpGeoLocationInfoAsync(IPAddress ipAddress,
            CancellationToken cancellationToken = default)
        {
            string requestUrl =
                $"http://api.ipstack.com/{ipAddress}" +
                $"?access_key={this.IpStackConfiguration.Key}&format=1";
            try
            {
                var result = await CustomHttpClient
                    .GetFromJsonAsync<GetGeoLocationInfoResponse>(requestUrl, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
