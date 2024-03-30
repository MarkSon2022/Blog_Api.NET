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

namespace WebAppClient.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7140/api/users";
        private string _bearerToken;

        public EditModel()
        {
            _httpClient=new HttpClient();
        }

        [BindProperty]
        public BusinessObject.User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
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
            //check id
            if (id == null )
            {
                return NotFound();
            }
            var response = await _httpClient.GetAsync(_url+$"/{id}/getId");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                User = JsonConvert.DeserializeObject<BusinessObject.User>(content);
            }
            else { 
                return NotFound();
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
            string jsonPayload = JsonConvert.SerializeObject(User);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response= await _httpClient.PutAsync( _url, content );
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else {
                return Page();
            }
        }

      
    }
}
