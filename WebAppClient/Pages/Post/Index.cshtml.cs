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
using System.Net.Http;

namespace WebAppClient.Pages.Post
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7140/api/posts";
        // Store the Bearer token here
        private string _bearerToken;

        public IndexModel()
        {
            _httpClient = new HttpClient();
        }

        public IList<BusinessObject.Post>? Post { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            //add token
            _bearerToken= HttpContext.Session.GetString("token");
            // Add the Authorization header with the Bearer token
            _httpClient.DefaultRequestHeaders.Authorization =new 
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

        public async Task<IActionResult> OnPost(string title)
        {
            //add token
            _bearerToken = HttpContext.Session.GetString("token");
            // Add the Authorization header with the Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", _bearerToken);
            //check if username or email != null
            if (title != null)
            {
                string searchUrl = _url + $"?$filter=contains(Title, \'{title}\')";
                HttpResponseMessage responseSearch = await _httpClient.GetAsync(searchUrl);

                if (responseSearch.IsSuccessStatusCode)
                {
                    string contentSearch = await responseSearch.Content.ReadAsStringAsync();
                    var posts = JsonConvert.DeserializeObject<List<BusinessObject.Post>>(contentSearch);
                    if (posts != null)
                    {
                        Post = posts;
                        return Page();
                    }
                    else
                    {
                        //return all
                        _bearerToken = HttpContext.Session.GetString("token");
                        _httpClient.DefaultRequestHeaders.Authorization = new
                            AuthenticationHeaderValue("Bearer", _bearerToken);
                        HttpResponseMessage responseAll = await _httpClient.GetAsync(_url);
                        string contentAll = await responseAll.Content.ReadAsStringAsync();
                        Post = JsonConvert.DeserializeObject<List<BusinessObject.Post>>(contentAll);
                        return Page();
                    }
                }
            }

            //return all 
            _bearerToken = HttpContext.Session.GetString("token");
            _httpClient.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", _bearerToken);
            HttpResponseMessage response = await _httpClient.GetAsync(_url);
            string content = await response.Content.ReadAsStringAsync();
            Post = JsonConvert.DeserializeObject<List<BusinessObject.Post>>(content);
            return Page();
        }
    }
}
