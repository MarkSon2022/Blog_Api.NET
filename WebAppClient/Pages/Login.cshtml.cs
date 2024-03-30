using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using BusinessObject.ViewModel;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics.Metrics;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Configuration;
using System.Security.Claims;

namespace WebAppClient.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7140/api/users/login";

        public LoginModel()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LoginRequest LoginVM { get; set; } = default!;

        public string ErrorMessage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || User == null)
            {
                ModelState.AddModelError(nameof(ErrorMessage), "You do not have permission to do this function");
                return Page();
            }
            else
            {
                string jsonPayload = JsonConvert.SerializeObject(LoginVM);
                var contentLogin = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_url, contentLogin);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    JwtRequestToken requestToken = JsonConvert.DeserializeObject<JwtRequestToken>(data);
                    HttpContext.Session.SetString("token", requestToken.Token);
                    var getRole= getRoleFromToken(requestToken.Token);
                    if (getRole.Equals("admin"))
                    {
                        HttpContext.Session.SetString("email", getEmailFromToken(requestToken.Token));
                        return RedirectToPage("User/Index");
                    }
                    else if (getRole.Equals("staff")) {
                        HttpContext.Session.SetString("email", getEmailFromToken(requestToken.Token));
                        return RedirectToPage("/Post/Index");
                    }
                    else if (getRole.Equals("user"))
                    {
                        HttpContext.Session.SetString("email", getEmailFromToken(requestToken.Token));
                        return RedirectToPage("/ForOnlyRoleUser/ListViewPost");
                    }
                    else {
                        ModelState.AddModelError(nameof(ErrorMessage), "You do not have permission to do this function");
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(ErrorMessage), "You do not have permission to do this function");
                    return Page();
                }
            }
        }

        private string getRoleFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken= tokenHandler.ReadJwtToken(token);
            var roleClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            return roleClaim?.Value;
        }

        private string getEmailFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
            var emailClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            return emailClaim?.Value;
        }
    }
}
