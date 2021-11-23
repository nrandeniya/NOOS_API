using NOOS_UI.Contracts;
using NOOS_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NOOS_UI.Static;
using NOOS_UI.Providers;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace NOOS_UI.Services
{
    public class SellerRepository : ISellerRepository
    {
        //inject client
        private readonly IHttpClientFactory _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public SellerRepository(IHttpClientFactory client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }



        public async Task<bool> Seller(SellerRegister seller)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.SellersEndpoint);
            request.Content = new StringContent(JsonConvert.SerializeObject(seller), Encoding.UTF8, "application/json");

            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
    }



}
