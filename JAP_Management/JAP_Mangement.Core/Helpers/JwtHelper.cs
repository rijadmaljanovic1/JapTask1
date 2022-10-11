using JAP_Management.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JAP_Management.Core.Helpers
{
    public static class JwtHelper
    {
        public static string SecretKey { get; set; } = "!###>This_is_a_private_key<###!";
        public static string UserIdClaimType { get; set; } = "userId";
        public static string Issuer { get; set; } = "Rijad";
        public static string Audience { get; set; } = "Mistral";


        public static string GetToken(BaseUser user, int expirationTime, IList<string> roles)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var loginCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(UserIdClaimType, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            var tokenOptions = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationTime),
                signingCredentials: loginCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

        public static string GetUserIdFromToken(ClaimsPrincipal user)
        {
            try
            {
                var claimsIdentity = user.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                return userId;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
