using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using System.Net.NetworkInformation;
using System.Text;
using System.Net.Http.Headers;

namespace WebAppClient.Pages.Post
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private string _bearerToken;
        private readonly string _url = "https://localhost:7140/api/posts";
        private readonly string _urlAuthor = "https://localhost:7140/api/users";
        private readonly string _urlCategory = "https://localhost:7140/api/categories";

        public CreateModel()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> OnGet()
        {
            //add token
            _bearerToken = HttpContext.Session.GetString("token");
            // Add the Authorization header with the Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", _bearerToken);
            //
            if (string.IsNullOrEmpty(_bearerToken))
            {
                return RedirectToPage("/Login");
            }
            //
            var email =HttpContext.Session.GetString("email");
            var responseOwn = await _httpClient.GetAsync(_urlAuthor+ $"/{email}/getEmail");
            var responseCategory = await _httpClient.GetAsync(_urlCategory);

            if (responseOwn.IsSuccessStatusCode && responseCategory.IsSuccessStatusCode)
            {
                //own
                string contentOwn= await responseOwn.Content.ReadAsStringAsync();
                var author=JsonConvert.DeserializeObject<BusinessObject.User>(contentOwn);
                if (author != null) {
                    Post.AuthorId = author.Id;
                }
                //categories
                string contentCategory = await responseCategory.Content.ReadAsStringAsync();
                List<BusinessObject.Category>? categories = JsonConvert.DeserializeObject<List<BusinessObject.Category>>(contentCategory);
                ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");

                return Page();
            }
            else
            {
                return RedirectToPage("./Index");
            }

        }

        [BindProperty]
        public BusinessObject.Post Post { get; set; } = new BusinessObject.Post()!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //add token
            _bearerToken = HttpContext.Session.GetString("token");
            // Add the Authorization header with the Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", _bearerToken);
            //check token
            if (string.IsNullOrEmpty(_bearerToken))
            {
                return RedirectToPage("/Login");
            }
            //add to db
            string jsonPayload = JsonConvert.SerializeObject(Post);
            var contentPost = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url, contentPost);

            return RedirectToPage("./Index");
        }
    }
}
