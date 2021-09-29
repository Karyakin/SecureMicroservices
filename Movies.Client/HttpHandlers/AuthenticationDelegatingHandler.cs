using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;

namespace Movies.Client.HttpHandlers
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ClientCredentialsTokenRequest _tokenRequest;

        public AuthenticationDelegatingHandler(IHttpClientFactory httpClientFactory, ClientCredentialsTokenRequest tokenRequest)
        {
            _httpClientFactory = httpClientFactory;
            _tokenRequest = tokenRequest;
        }

     
       protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
       {
           var httpClient = _httpClientFactory.CreateClient("IDPClient");
           var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(_tokenRequest, cancellationToken: cancellationToken);

           if (tokenResponse.IsError)
           {
               throw new InvalidOperationException("Something went wrong");
           }

           request.SetBearerToken(tokenResponse.AccessToken);

           return await base.SendAsync(request, cancellationToken);
        }
    }
}
