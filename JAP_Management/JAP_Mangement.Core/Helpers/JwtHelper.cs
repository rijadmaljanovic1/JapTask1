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


        public static string GetToken(BaseUser user, int expirationTime)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var loginCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: new List<Claim> { new Claim(UserIdClaimType, user.Id.ToString()) },
                expires: DateTime.UtcNow.AddMinutes(expirationTime),
                signingCredentials: loginCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

        public static int GetUserIdFromToken(ClaimsPrincipal user)
        {
            var id = user.Claims.FirstOrDefault(c => c.Type == UserIdClaimType)?.Value ?? "0";
            return Int32.Parse(id);
        }
    }
}
