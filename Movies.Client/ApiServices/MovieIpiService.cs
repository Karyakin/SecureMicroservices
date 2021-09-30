using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.ApiServices
{
    public class MovieIpiService : IMovieIpiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MovieIpiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<UserInfoViewModel> GetUserInfo()
        {
            var idpClient = _httpClientFactory.CreateClient("IDPClient");
            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();
            if (metaDataResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong");
            }

            var accessToken = await _httpContextAccessor
                .HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var userInfoResponse = await idpClient.GetUserInfoAsync(
                new UserInfoRequest
                {
                    Address = metaDataResponse.UserInfoEndpoint,
                    Token = accessToken
                }
            );

            if (userInfoResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong while getting user info");
            }


            var userInfoDictionary = new Dictionary<string, string>();
            foreach (var claim in userInfoResponse.Claims)
            {
                userInfoDictionary.Add(claim.Type, claim.Value);
            }

            return new UserInfoViewModel(userInfoDictionary);
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            #region WAY 1

            var httpClient = _httpClientFactory.CreateClient("MovieAPIClient");

            //var request = new HttpRequestMessage(HttpMethod.Get, "api/movies");
            var request = new HttpRequestMessage(HttpMethod.Get, "/movies");
            var response = await httpClient.SendAsync(
                    request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var listMovie = JsonConvert.DeserializeObject<List<Movie>>(content);
            return listMovie;

            #endregion

            #region WAY 2

            /* var apiClientCredentials = new ClientCredentialsTokenRequest
             {
                   Address = "https://localhost:5005/connect/token",
                   ClientId = "movieClient",
                   ClientSecret = "secret",
                   //this is the scope our Protected requires
                   Scope = "movieAPI"

             };
             //1. create new http talk to our IdentityServer(localhost:5005)
             var client = new HttpClient();

             //just check if we can reach the Discovery document. Not 100% needed but.. 
             var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5005");
             if (disco.IsError)
             {
                 return null;
             }

             //2. Authenticates and gen an access token from IdentityServer
             var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiClientCredentials);
             if (tokenResponse.IsError)
             {
                 return null;
             }

             //3. Set access_token in the request Authorization: Bearer<token>
             var apiClient = new HttpClient();
             apiClient.SetBearerToken(tokenResponse.AccessToken);

             //4. Send a request to our Protected API
             var response =await apiClient.GetAsync("https://localhost:5001/api/Movies");
             response.EnsureSuccessStatusCode();

             var content = await response.Content.ReadAsStringAsync();

             var listMovie = JsonConvert.DeserializeObject<List<Movie>>(content);
             return listMovie;*/

            /* шлак 
                var movieList = new List<Movie>
                {
                    new Movie()
                    {
                        Id = 1,
                        Genre = "Comics",
                        ImageUrl = "image/src",
                        Owner = "Dima",
                        Rating ="5,5" ,
                        ReleaseDate = DateTime.Now ,
                        Title = "asd"
                    },
                    new Movie()
                    {
                        Id = 2,
                        Genre = "Triller",
                        ImageUrl = "image/src",
                        Owner = "Vova",
                        Rating ="5,5" ,
                        ReleaseDate = DateTime.Now ,
                        Title = "asd"
                    }
                };

                return await Task.FromResult(movieList);*/

            #endregion

        }

        public Task<Movie> GetMovie()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> CreateMovies(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> UpdateMovies(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }


    }
}
