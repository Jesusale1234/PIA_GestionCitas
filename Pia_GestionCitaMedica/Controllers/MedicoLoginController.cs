using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pia_GestionCitaMedica.DTOs;
using Pia_GestionCitaMedica.Entidades;

namespace Pia_GestionCitaMedica.Controllers
{
    [ApiController]
    [Route("Login")]
    public class MedicoLoginController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<CitaController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public MedicoLoginController(UserManager<IdentityUser> userManager, IConfiguration configuration,
            SignInManager<IdentityUser> signInManager, ApplicationDbContext dbContext, IMapper mapper)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RespuestaAutenticacion>> Register(CredencialesUsuario registerUser)
        {

            var user = new IdentityUser { UserName = registerUser.Email, Email = registerUser.Email};
            var result = await userManager.CreateAsync(user, registerUser.Pass);

            if (result.Succeeded)
            {
                var userid = user.Id;

                CuentasLogin cuentas = new CuentasLogin();
                cuentas.IdCuenta = userid;
                cuentas.Email = registerUser.Email;
                cuentas.Role = registerUser.Rol;

                var cuentaslogin = mapper.Map<CuentasLogin>(cuentas);
                dbContext.Add(cuentaslogin);
                await dbContext.SaveChangesAsync();


                if (cuentas.Role == "medico")
                {
                    var userAdmin = await userManager.FindByEmailAsync(cuentas.Email);
                    await userManager.AddClaimAsync(userAdmin, new Claim("EsMedico", "1"));
                }
                if (cuentas.Role == "paciente")
                {
                    var userAdmin = await userManager.FindByEmailAsync(cuentas.Email);
                    await userManager.AddClaimAsync(userAdmin, new Claim("EsPaciente", "1"));
                }

                return await ConstruirToken(registerUser);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialmedico)
        {
            var result = await signInManager.PasswordSignInAsync(credencialmedico.Email, credencialmedico.Pass, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return await ConstruirToken(credencialmedico);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }

        private async Task<RespuestaAutenticacion> ConstruirToken(CredencialesUsuario credencialmedico)
        {
            var claims = new List<Claim>
            {
                new Claim("Email", credencialmedico.Email)
            };
            var usuario = await userManager.FindByEmailAsync(credencialmedico.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(30);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiration
            };
        }
    }
}
