using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AuctionWebAPI.Tests.Base
{
    public interface IAuctionWebAPICaller
    {
        Task<T> CallEndpointAsync<T>(string endpointPath, object requestBody, HttpMethod httpMethod, string contentType, HttpClient httpClient); /*where T : class*/
        T CallEndpoint<T>(string endpointPath, object requestBody, HttpMethod httpMethod, string contentType, HttpClient httpClient); /*where T : class*/
    }
}
