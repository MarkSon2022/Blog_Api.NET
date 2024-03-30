using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class JwtRequestToken
    {
        public string Token { get; set; }
        public string Type { get; set; } = "Bearer ";
    }
}
