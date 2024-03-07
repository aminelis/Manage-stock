using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using db_Api.Models.Generated.Connexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using db_Api.Models.Generated;
using System.Security.Cryptography; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace VotreNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Db_PartialContext _context;

        // Déclaration d'une liste pour stocker les tokens invalidés
private List<string> _invalidTokens = new List<string>();

        public AuthController(IConfiguration configuration, Db_PartialContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        

        [HttpPost]
        [AllowAnonymous] // Allow access without authentication
        [Route("register")]
public async Task<IActionResult> Register([FromBody] RegisterModel model)
{
    // Vérifiez si l'utilisateur existe déjà dans la base de données
    bool userExists = await IsUserExistsAsync(model.Username);
    if (userExists)
    {
        return BadRequest("L'utilisateur existe déjà.");
    }

    // Créez un nouvel utilisateur avec les informations fournies
    var newUser = new UserModel
    {
        Username = model.Username,
        Password = HashPassword(model.Password) // N'oubliez pas de hacher le mot de passe avant de le stocker en production
    };

    // Ajoutez le nouvel utilisateur à la base de données
    _context.users.Add(newUser);
    await _context.SaveChangesAsync();



    return Ok();
}


        [HttpPost]
        [AllowAnonymous] // Allow access without authentication
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Votre logique pour authentifier l'utilisateur par rapport à la base de données
            // Vous pouvez utiliser ASP.NET Core Identity ou tout autre mécanisme de votre choix
            
            // Exemple simplifié : vérification du nom d'utilisateur et du mot de passe dans la base de données
            bool isValidCredentials = await IsValidCredentialsAsync(model.Username, HashPassword(model.Password));
            if (!isValidCredentials)
            {
                return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");
            }

            // Exemple simplifié : vérification du role
            string role = await GetUserRole(model.Username, HashPassword(model.Password));
            if (role == "user" && model.Role == "admin")
            {
                return Unauthorized("Vous n'êtes pas un admin.");
            }

                // Générez un token JWT pour le nouvel utilisateur
            var token = GenerateJwtToken(model.Username);

            
            // Ajouter le token JWT à un cookie HTTP
            Response.Cookies.Append("JWT", token, new CookieOptions
            {
              HttpOnly = false, // Empêche JavaScript d'accéder au cookie
              Secure = false,   // Requiert une connexion HTTPS pour envoyer le cookie (assurez-vous d'utiliser HTTPS dans un environnement de production)
              SameSite = SameSiteMode.Strict, // Protège contre les attaques CSRF
              Expires = DateTime.UtcNow.AddMinutes(30) // Durée de vie du cookie (par exemple, 30 minutes)
            });

            


            return Ok(new { token });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Logique pour déconnecter l'utilisateur
            // Ceci dépendra de votre implémentation spécifique
            // Peut-être vider ou invalider le token JWT
                // Supprimez le cookie JWT de la réponse
        string token = Request.Cookies["JWT"];
        Response.Cookies.Delete("JWT");
        InvalidateToken(token);
            return Ok("Utilisateur déconnecté avec succès");
        }

        // Méthode pour générer un token JWT
        private string GenerateJwtToken( string username)
{
    var secretKey = _configuration.GetSection("AppSettings:key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("shhh.. this is my top secret"));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials= signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
}


// Méthode pour invalider un token lors de la déconnexion
[ApiExplorerSettings(IgnoreApi = true)]
public void InvalidateToken(string token)
{
    _invalidTokens.Add(token);
}

// Méthode pour vérifier si un token est invalide
[ApiExplorerSettings(IgnoreApi = true)]
public bool IsTokenInvalid(string token)
{
    return _invalidTokens.Contains(token);
}


        // Méthode pour vérifier si l'utilisateur existe déjà dans la base de données
        private async Task<bool> IsUserExistsAsync(string username)
        {
            // Recherchez l'utilisateur dans la base de données en fonction du nom d'utilisateur
            var user = await _context.users.FirstOrDefaultAsync(u => u.Username == username);
            
            // Si l'utilisateur est trouvé, retournez true, sinon retournez false
            return user != null;
        }

        // Méthode pour vérifier les informations d'identification de l'utilisateur par rapport à la base de données
        private async Task<bool> IsValidCredentialsAsync(string username, string password)
        {
            // Recherchez l'utilisateur dans la base de données en fonction du nom d'utilisateur et du mot de passe
            var user = await _context.users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            
            // Si l'utilisateur est trouvé, retournez true, sinon retournez false
            return user != null;
        }
        private async Task<string> GetUserRole(string username, string password)
{
    // Recherchez l'utilisateur dans la base de données en fonction du nom d'utilisateur et du mot de passe
    var user = await _context.users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
    
    // Si l'utilisateur est trouvé, retournez son rôle
    // Sinon, retournez une chaîne vide
    return user != null ? user.Role : string.Empty;
}

        // Méthode pour hacher le mot de passe
private string HashPassword(string password)
{
    using (var sha256 = SHA256.Create())
    {
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}
    }
}
