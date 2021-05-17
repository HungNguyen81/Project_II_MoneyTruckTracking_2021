using System.Security.Claims;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System;
using MoneyTruckTrackingAPI.Models;

namespace MoneyTruckTrackingAPI.Helpers
{
    public class TokenHelper
    {
        public const string ISSUER = "Nguyen Ngoc Hung";
        public const string AUDIENCE = "Nguyen Hung";
        // SECRET is a base64-encoded string
        public const string SECRET = "OFRC1j9aaR2BvADxNWlG2pmuD392UfQBZZLM1fuzDEzDlEpSsn+btrpJKd3FfY855OMA9oK4Mc8y48eYUrVUSw==";


        // generate a random secure secret
        public static string GenerateSecureSecret()
        {
            var hmac = new HMACSHA256();
            return Convert.ToBase64String(hmac.Key);
        }

        // Generate Token
        public static string GenerateToken(Account acc)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(SECRET);

            var claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, acc.Id.ToString()),
                new Claim("IsAdmin", acc.Admin.ToString())
            });

            var signingCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                );

            // create token using token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = ISSUER,
                Audience = AUDIENCE,
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = signingCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
