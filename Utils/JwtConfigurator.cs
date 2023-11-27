using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoListBk.Persistence.Models;

namespace ToDoListBk.Utils;

public class JwtConfigurator
{
    public static string GetToken(UserModel userInfo, IConfiguration config)
    {
        string SecretKey = config["Jwt:SecretKey"];
        string Issuer = config["Jwt:Issuer"];
        string Audience = config["Jwt:Audience"];
        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var creddentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
            new Claim("idUser", userInfo.UserId.ToHashId()),

        };

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creddentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static int GetTokenIdUsuario(ClaimsIdentity identity)
    {
        if (identity != null)
        {
            IEnumerable<Claim> claims = identity.Claims;
            foreach (var claim in claims)
            {
                if (claim.Type == "idUser")
                {
                    return int.Parse(claim.Value);
                }
            }
        }
        return 0;
    }
    public static string GetTokenRoleUsuario(ClaimsIdentity identity)
    {
        if (identity != null)
        {
            IEnumerable<Claim> claims = identity.Claims;
            foreach (var claim in claims)
            {
                if (claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                {
                    return claim.Value;
                }
            }
        }
        return "";
    }
}
