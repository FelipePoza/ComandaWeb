using ComandaWeb.DAL.Usuario;
using ComandaWeb.Seguranca;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ComandaWeb.ProvedorAutenticacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<Usuario> _signInManager;

        public LoginController(SignInManager<Usuario> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Token([FromBody]LoginModel user)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(user.Login, user.Senha, true, true);

                if (resultado.Succeeded)
                {
                    var direitos = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,user.Login),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                    };

                    var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ComandaWeb-Autenticacao-Valida"));
                    var credentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: "ComandaWeb.WebApp",
                        audience: "Postman",
                        claims: direitos,
                        signingCredentials: credentials,
                        expires: DateTime.Now.AddMinutes(30)
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(tokenString);
                }

                return Unauthorized(); //401
            }

            return BadRequest(); //400
        }

    }
}
