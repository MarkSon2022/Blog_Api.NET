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
using System.Net;
using System.Text;

namespace WebAppClient.Pages.User
{
    public class ActivateModel : PageModel
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string _bearerToken;
        private readonly string _url = "https://localhost:7140/api/users";

        public ActivateModel()
        {
            _httpClient = new HttpClient();
        }

        [BindProperty]
        public BusinessObject.User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
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
            var response = await _httpClient.GetAsync(_url + $"/{id}/getId");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                User = JsonConvert.DeserializeObject<BusinessObject.User>(content);
                return Page();
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {//add token
            _bearerToken = HttpContext.Session.GetString("token");
            // Add the Authorization header with the Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", _bearerToken);
            //
            string jsonPayload = JsonConvert.SerializeObject(User);
            Boolean status = true;
            //
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_url + $"/{id}/active/{status}", content);
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
