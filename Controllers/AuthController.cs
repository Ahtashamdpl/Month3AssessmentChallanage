using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Month3AssessmentCode.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Month3AssessmentCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signManager;
        private readonly IConfiguration _configuraton;
        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signManager, IConfiguration configuraton)
        {
            _userManager = userManager;
            _signManager = signManager;
            _configuraton = configuraton;
        }

        


        // GET: api/<AuthController>
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             var user = new IdentityUser { UserName = registerModel.Email, Email = registerModel.Email };
            var result= await _userManager.CreateAsync(user,registerModel.Password);
            if(result.Succeeded)
            {
                return Ok("Register Successfully");
            }
            return BadRequest(result.Errors);
        }

        // GET api/<AuthController>/5
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email,string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user= await _userManager.FindByEmailAsync(email);

            if(user== null)
                return Unauthorized();
            var result = await _signManager.PasswordSignInAsync(user,password,false,false);

            if(result.Succeeded)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }
            return Unauthorized();
        }


        private string GenerateToken(IdentityUser identityUser)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,identityUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,identityUser.Id)

};
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuraton["Jwt:SecretKey"]));
            var credes = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                
                issuer: _configuraton["Jwt:Issuer"],
                audience: _configuraton["Jwt:Audience"],
            claims: claims,
            expires:DateTime.Now.AddHours(1),
            signingCredentials: credes
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
