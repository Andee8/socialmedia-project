using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialMediaClient
{
    public class PostService
    {
        private readonly HttpClient client;

        public PostService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<bool> CreatePostAsync(Post post)
        {
            try
            {
                var json = JsonSerializer.Serialize(post);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/posts", content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }


        public async Task<List<Post>> GetPostsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("posts");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Post>>(jsonString);
                }
                return new List<Post>();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Request error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public async Task<List<Post>> GetPostsByUserAsync(string username)
        {
            var response = await client.GetAsync($"postsByUser/{username}");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Post>>(jsonString);
            }
            return new List<Post>();
        }
        public async Task<List<Post>> GetPostsByTitleAndUserAsync(string title, string userName)
        {
            var response = await client.GetAsync($"posts/byTitleAndUser?title={title}&userName={userName}");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Post>>(jsonString);
            }
            return new List<Post>();
        }
        public async Task<List<Post>> GetPostsByUserIdAsync(int userId)
        {
            HttpResponseMessage response = await client.GetAsync($"api/posts/user/{userId}");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Post>>(jsonString);
            }
            return new List<Post>();
        }

        public async Task<bool> DeletePostByTitleAsync(string title, int userId)
        {
            var response = await client.DeleteAsync($"api/posts?title={title}&userId={userId}");
            return response.IsSuccessStatusCode;
        }

    }
}
