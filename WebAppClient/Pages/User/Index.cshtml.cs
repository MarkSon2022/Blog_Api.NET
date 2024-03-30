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

namespace WebAppClient.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7140/api/users";
        private string _bearerToken;

        public IndexModel()
        {
            _httpClient = new HttpClient();
        }

        public IList<BusinessObject.User> User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            ////add token
            _bearerToken = HttpContext.Session.GetString("token");
            //// Add the Authorization header with the Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", _bearerToken);
            ////
            HttpResponseMessage response = await _httpClient.GetAsync(_url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                User = JsonConvert.DeserializeObject<List<BusinessObject.User>>(content);
                return Page();
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }

        public async Task<IActionResult> OnPost(string username, string email)
        {
            //add token
            _bearerToken = HttpContext.Session.GetString("token");
            // Add the Authorization header with the Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", _bearerToken);
            //check if username or email != null
            if (username != null || email != null)
            {
                string searchUrl= string.Empty;
                // check username
                if (string.IsNullOrEmpty(username)) {
                    searchUrl = _url + $"?$filter=contains(Email, \'{email}\')";
                    username = string.Empty;
                }
                // check email
                else if (string.IsNullOrEmpty(email))
                {
                    searchUrl = _url + $"?$filter=contains(Username, \'{username}\')";
                    email = string.Empty;
                }
                // if email and user are not empty
                else {
                    searchUrl = _url + $"?$filter=contains(Username, \'{username}\') or contains(Email, \'{email}\')";
                }
                HttpResponseMessage responseSearch = await _httpClient.GetAsync(searchUrl);

                if (responseSearch.IsSuccessStatusCode)
                {
                    string contentSearch = await responseSearch.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<BusinessObject.User>>(contentSearch);
                    if (users != null)
                    {
                        User = users;
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
                        User = JsonConvert.DeserializeObject<List<BusinessObject.User>>(contentAll);
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
            User = JsonConvert.DeserializeObject<List<BusinessObject.User>>(content);
            return Page();
        }
    }
}
