using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialMediaClient
{
    public class UserService
    {
        private readonly HttpClient client;

        public UserService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<User>> GetUsersByNameAsync(string username)
        {
            var response = await client.GetAsync($"users/{username}");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<User>>(jsonString);
            }
            return new List<User>();
        }
        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                var json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("users", content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<User>> GetUsersByNameAndEmailAsync(string name, string email)
        {
            try
            {
                var response = await client.GetAsync($"usersByNameAndEmail?name={Uri.EscapeDataString(name)}&email={Uri.EscapeDataString(email)}");
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<User>>(jsonString);
                }
                return new List<User>();
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> UpdateUserProfileAsync(int userId, string name, string email)
        {
            var userData = new { Id = userId, Name = name, Email = email };
            var json = JsonSerializer.Serialize(userData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/users/{userId}", content);
            return response.IsSuccessStatusCode;
        }


    }
}
