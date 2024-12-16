using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SocialMediaDesktopClient;
public class MessageService
{
    private readonly HttpClient client;

    public MessageService()
    {
        client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:8080/api/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    public async Task<bool> SendMessageAsync(SocialMediaClient.Message message)
    {
        var json = JsonSerializer.Serialize(message);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync("api/messages", content);

        return response.IsSuccessStatusCode;
    }

}
