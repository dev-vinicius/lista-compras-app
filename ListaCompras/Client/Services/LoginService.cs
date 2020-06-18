using Blazored.LocalStorage;
using ListaCompras.Client.Security;
using ListaCompras.Client.Util;
using ListaCompras.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ListaCompras.Client.Services
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        public LoginService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<dynamic> Register(User user)
        {
            var response = await _httpClient.PostAsync("api/users", ApiUtil.ObjectToHttpContent(user));
            var result = ApiUtil.HttpContentToObject(response.Content);
            return result;

        }

        public async Task<dynamic> Login(User user)
        {
            var response = await _httpClient.PostAsync("api/users/login", ApiUtil.ObjectToHttpContent(user));
            var loginResult = await ApiUtil.HttpContentToObject(response.Content);

            if (response.IsSuccessStatusCode)
            {
                var loginUser = await ApiUtil.HttpContentToObject<User>(response.Content);
                await _localStorage.SetItemAsync("authToken", loginUser.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginUser.Token);
                ((CustomAuthStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(user.Email);
            }

            return loginResult;
        }

        public async Task<User> GetUserInfo()
        {
            var response = await _httpClient.GetAsync("api/users");
            if (response.IsSuccessStatusCode)
            {
                return await ApiUtil.HttpContentToObject<User>(response.Content);
            }
            else
            {
                return null;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((CustomAuthStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

    }
}
