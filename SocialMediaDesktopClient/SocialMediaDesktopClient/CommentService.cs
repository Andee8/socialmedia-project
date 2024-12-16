using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialMediaClient
{
    public class CommentService
    {
        private readonly HttpClient client;

        public CommentService()
        {
            client = new HttpClient();
            client.BaseAddress = new System.Uri("http://localhost:8080/api/");
        }

        public async Task<List<Comment>> GetCommentsForPostAsync(int postId)
        {
            HttpResponseMessage response = await client.GetAsync($"comments/{postId}");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Comment>>(jsonString);
            }
            return null;
        }
    
    public async Task<List<Comment>> GetCommentsByUserIdAsync(int userId)
        {
            HttpResponseMessage response = await client.GetAsync($"api/comments/user/{userId}");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Comment>>(jsonString);
            }
            return new List<Comment>();
        }
    }
    }
