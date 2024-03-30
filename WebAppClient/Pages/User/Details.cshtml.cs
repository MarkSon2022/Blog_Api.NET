using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WebAppClient.Pages.User
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7140/api/users";
        private string _bearerToken;
        public DetailsModel()
        {
            _httpClient = new HttpClient();
        }

        public BusinessObject.User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ////add token
            _bearerToken = HttpContext.Session.GetString("token");
            //// Add the Authorization header with the Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", _bearerToken);
            ////check token
            if (string.IsNullOrEmpty(_bearerToken))
            {
                return RedirectToPage("/Login");
            }

            var response = await _httpClient.GetAsync(_url + $"/{id}/getId");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                User = JsonConvert.DeserializeObject<BusinessObject.User>(content);

            }
            else
            {
                return NotFound();
            }
            return Page();
        }
    }
}
