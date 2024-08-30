using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using IntcomexAPI.Models;
using System.Security.Cryptography;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    /// <summary>
    /// Autentica una llave de acceso y genera un token JWT.
    /// </summary>
    /// <param name="request">Modelo con la llave codificada.</param>
    /// <returns>Token JWT si la llave es valida.</returns>
    [HttpPost("authenticate")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [Produces("application/json")]
    public IActionResult Authenticate([FromBody] AccessKeyModel request)
    {

        string decodedKey;
        try
        {
            // Intenta decodificar la llave
            decodedKey = Encoding.UTF8.GetString(Convert.FromBase64String(request.EncodedKey)).Trim();
        }
        catch (FormatException)
        {
            return BadRequest("La llave proporcionada no es válida.");
        }

        // Busca la llave en la base de datos
        var accessKey = _context.AccessKeys.FirstOrDefault(k => k.Llave == decodedKey && k.Activa == "si" && k.FechaExpiracion > DateTime.UtcNow);


        if (accessKey == null)
        {
            return Unauthorized("Llave no válida o expirada");
        }

        // duracion del token desde la tabla Parametros
        var parametroDuracionToken = _context.Parametros.FirstOrDefault(p => p.Nombre.ToLower().Contains("Token_Duration")) ?? null;
        if (parametroDuracionToken == null)
        {
            return StatusCode(500, "La duracion del token no esta configurada.");
        }

        if (!double.TryParse(parametroDuracionToken.Valor, out double duracionToken))
        {
            return StatusCode(500, "La duracion del token configurada no es valida.");
        }

        // Genera el token JWT

        var secretKey = _configuration["JwtConfig:Secret"];
        var key = Encoding.ASCII.GetBytes(secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, decodedKey)
            }),
            Expires = DateTime.UtcNow.AddSeconds(duracionToken),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Ok(new
        {
            Token = tokenHandler.WriteToken(token)
        });
    }
}
