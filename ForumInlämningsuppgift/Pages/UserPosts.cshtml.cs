using Microsoft.AspNetCore.Mvc.RazorPages;
using ForumInlämningsuppgift.Models;
using System.Net.Http.Json;

namespace ForumInlämningsuppgift.Pages
{
    public class UserPostsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public UserPostsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public List<PostDTO> Posts { get; set; } = new();
        public string Nickname { get; set; }

        public async Task OnGetAsync(string userId)
        {
            var url = $"https://crithappensapi.azurewebsites.net/api/PostDTO/user/{userId}";
            var posts = await _httpClient.GetFromJsonAsync<List<PostDTO>>(url);
            if (posts != null && posts.Any())
            {
                Posts = posts;
            }
        }
    }
}
