using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace WebAppClient.Pages.Post
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string _bearerToken;
        private readonly string _url = "https://localhost:7140/api/posts";

        public DeleteModel()
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
            //
            var response = await _httpClient.GetAsync(_url + $"/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Post = JsonConvert.DeserializeObject<BusinessObject.Post>(content);
                return Page();
            }
            else
            {
                return NotFound();
            }

        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //add token
            _bearerToken = HttpContext.Session.GetString("token");
            // Add the Authorization header with the Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", _bearerToken);
            //
            var response = await _httpClient.DeleteAsync(_url + $"/{id}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
