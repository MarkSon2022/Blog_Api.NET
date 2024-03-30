using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace WebAppClient.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7140/api/users/register";

        public CreateModel()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BusinessObject.User User { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || User == null)
            {
                return Page();
            }

            string jsonPayload = JsonConvert.SerializeObject(User);
            var contentRegister = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url, contentRegister);

            return RedirectToPage("./Index");
        }
    }
}
