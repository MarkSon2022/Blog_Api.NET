using BlogApi.Model;
using BusinessObject;
using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Service;
using Service.Impl;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private IUserService userService;
        private IConfiguration configuration;

        public UserController(IUserService userService, IConfiguration configuration) { 
            this.userService = userService;
            this.configuration = configuration; 

        }
        //PS0002
        //@2
        [HttpPost("login")]
        public ActionResult LoginAccount([FromBody] LoginRequest loginRequest)
        {
          
            if (loginRequest != null && loginRequest.Username != null && loginRequest.Password != null)
            {
                User user = userService.GetByUsernameAndPassword(loginRequest.Username, loginRequest.Password);
                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        configuration["Jwt:Issuer"],
                        configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                        );
                    JwtRequestToken response = new JwtRequestToken();
                    response.Token= new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(response);
                }
                else
                {
                    return BadRequest("You don't have permission to access this function");
                }
            }
            else
            {
                return BadRequest("You don't have permission to access this function");
            }

        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] UserDto user) {
            if(ModelState.IsValid){
                User updateUser = new User();
                updateUser.Username = user.Username;
                updateUser.Password = user.Password;
                updateUser.Id = user.Id;
                updateUser.Email = user.Email;
                updateUser.Role = user.Role;

                var checkUserByUsername =userService.GetByUsername(updateUser.Username);
                if (checkUserByUsername != null) { 
                    return BadRequest("This user already exist");    
                }
                var checkUserById = userService.GetById(updateUser.Id);
                if (checkUserByUsername != null)
                {
                    return BadRequest("This user already exist");
                }
                var createUser= userService.Add(updateUser);
                return new CreatedAtActionResult(nameof(Register), "User", new { id = user.Id }, user);
            }
            else
            {
                return BadRequest("You don't have permission to access this function");
            }
        }

        [HttpPut]
        public ActionResult Update([FromBody] UserDto user)
        {
            if (ModelState.IsValid)
            {
                User updateUser = new User();
                updateUser.Username=user.Username;
                updateUser.Password=user.Password;
                updateUser.Id=user.Id;
                updateUser.Email=user.Email;
                updateUser.Role=user.Role;  
                var  checkUser= userService.Update(updateUser);
                return Ok(checkUser);
            }
            else
            {
                return BadRequest("Update Fail!");
            }
        }

        [HttpPut("{id}/active/{status}")]
        public ActionResult Activate(string id, Boolean status)
        {
            if (id != null)
            {
                var user= userService.GetById(id);
                var checkUser = userService.ActivateUser(user, status);
                return Ok(checkUser);
            }
            else
            {
                return BadRequest("Activate/deactivate fail!");
            }
        }

        [EnableQuery]
        [HttpGet]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,staff")]
        public ActionResult GetAll()
        {
            return Ok(userService.GetAll());
        }

        [HttpGet("{id}/getId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,staff,user")]
        public ActionResult GetById(string id)
        {
            if(id == null)
            {
                return NotFound("There is no user exits with this id "+id);
            }
            var user = userService.GetById(id);
            if (user == null)
            {
                return NotFound("There is no user exits with this id " + id);
            }
            else {
                return Ok(user);
            }
        }

        [HttpGet("{username}/getUsername")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public ActionResult GetByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return NotFound("There is no user exits with this id " + username);
            }
            var user = userService.GetByUsername(username);
            if (user == null)
            {
                return NotFound("There is no user exits with this id " + username);
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpGet("{email}/getEmail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,staff,user")]
        public ActionResult GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return NotFound("There is no user exits with this id " + email);
            }
            var user = userService.GetByEmail(email);
            if (user == null)
            {
                return NotFound("There is no user exits with this id " + email);
            }
            else
            {
                return Ok(user);
            }
        }

    }

}
