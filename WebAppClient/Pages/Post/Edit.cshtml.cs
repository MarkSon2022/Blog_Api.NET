using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace WebAppClient.Pages.Post
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private string _bearerToken;
        private readonly string _url = "https://localhost:7140/api/posts";
        private readonly string _urlAuthor = "https://localhost:7140/api/users";
        private readonly string _urlCategory = "https://localhost:7140/api/categories";

        public EditModel()
        {
            _httpClient = new HttpClient();
        }

        [BindProperty]
        public BusinessObject.Post Post { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
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
            //check if post existed
            var response = await _httpClient.GetAsync(_url + $"/{id}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Post = JsonConvert.DeserializeObject<BusinessObject.Post>(content);
            }
            else
            {
                return NotFound();
            }

            // get email author and name author to edit
            var email = HttpContext.Session.GetString("email");
            var responseAuthor = await _httpClient.GetAsync(_urlAuthor + $"/{email}/getEmail");
            var responseCategory = await _httpClient.GetAsync(_urlCategory);

            if (responseAuthor.IsSuccessStatusCode && responseCategory.IsSuccessStatusCode)
            {
                //author
                string contentAuthor = await responseAuthor.Content.ReadAsStringAsync();
                BusinessObject.User user = JsonConvert.DeserializeObject<BusinessObject.User>(contentAuthor);
                if (user != null)
                {
                    Post.AuthorId = user.Id;
                }
                //categories
                string contentCategory = await responseCategory.Content.ReadAsStringAsync();
                List<BusinessObject.Category>? categories = JsonConvert.DeserializeObject<List<BusinessObject.Category>>(contentCategory);
                ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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
            //
            string jsonPayload = JsonConvert.SerializeObject(Post);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_url, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }
        }

      
    }
}
