using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Tareaje.Data;
using Tareaje.Interfaces;

namespace Tareaje.Services {
    public class AuthService :IAuthService{
        private readonly string _URL = @"api/auth";
        private readonly HttpClient _httpClient;
        private static User? user;

        public AuthService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<Usuario> login(Auth auth) {
            var response = await _httpClient.PostAsJsonAsync($"{_URL}/login", auth);
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }

        public async Task<Usuario> isLogged() {
            string userId = "";

            try {
                userId = Preferences.Get(PreferencesModel._userPreferences, "");
            } catch (Exception ex) {
                Preferences.Remove(PreferencesModel._userPreferences);
                Console.WriteLine(ex.Message);
                return null;
            }

            if (string.IsNullOrEmpty(userId))
                return null;

            var response = await renewUsuario(userId);

            if (response.Id == 0)
                return null;

            Preferences.Remove(PreferencesModel._userPreferences);
            Preferences.Set(PreferencesModel._userPreferences, response.Id.ToString());
            return response;
        }

        public async Task<Usuario> renewUsuario(string userId) {
            var response = await _httpClient.GetAsync($"{_URL}/renew/{userId}");
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }

        public async Task<bool> isValidKey(string key) {
            string userId = Preferences.Get(PreferencesModel._userPreferences, "");
            var request = new ValidKeyRequest { UserId = Convert.ToInt64(userId), key = key };
            var response = await _httpClient.PostAsJsonAsync($"{_URL}/valid-key", request);
            var licencia =  await response.Content.ReadFromJsonAsync<Licencia>();
            if(licencia.Id == 0) return false;

            return true;
        }

        public async Task<KeyResponse> asignarKey(AsignarLicenciaRequest request) {
            var response = await _httpClient.PutAsJsonAsync($"api/licencia", request);
            KeyResponse keyResponse = new();
            keyResponse.Code = (int) response.StatusCode;
            keyResponse.Response = await response.Content.ReadFromJsonAsync<DefaultResponse>();
            return keyResponse;
        }
    }
}
