using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WebAppClient.Pages.ForOnlyRoleUser
{
    public class ListViewPostModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7140/api/posts";
        // Store the Bearer token here
        private string _bearerToken;

        public ListViewPostModel()
        {
            _httpClient = new HttpClient();
        }

        public IList<BusinessObject.Post>? Post { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            //add token
            _bearerToken = HttpContext.Session.GetString("token");
            // Add the Authorization header with the Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new
                            AuthenticationHeaderValue("Bearer", _bearerToken);

            HttpResponseMessage response = await _httpClient.GetAsync(_url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Post = JsonConvert.DeserializeObject<List<BusinessObject.Post>>(content);
                return Page();
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }

        
    }
}
