using LibraryApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LibraryApi.Services
{
	public class TokenService : ITokenService
	{
		private readonly JwtSettings _jwtSettings;

		public TokenService(JwtSettings jwtSettings)
		{
			_jwtSettings = jwtSettings;
		}

		public (string Token, DateTime ExpiresAt) GenerateToken(User user)
		{
			// Token içerisine yazacagımız bilgileri (Claims)
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Name,user.Username),
				new Claim(ClaimTypes.Role,user.Role)
			};

			//İmza için kullanılacak gizli anahtarı hazırlayalom 

			var keyBytes = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
			var key = new SymmetricSecurityKey(keyBytes);
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			//Token Bitiş Zamanı

			var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes);

			//Token Olustur

			var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: expiresAt,
                    signingCredentials: creds);

			//string çevir

			var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
			return (tokenString,expiresAt);

		}
	}
}
