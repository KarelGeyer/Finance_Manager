using Common.Models.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Interfaces;

namespace UserService.Helpers
{
	public class Jwt : IJwt
	{
		/// <inheritdoc/>
		public string CreateJWT(User user)
		{
			JwtSecurityToken token =
				new(
					claims: new List<Claim>() { new Claim(ClaimTypes.Name, user.Username) },
					expires: DateTime.Now.AddMinutes(30),
					signingCredentials: new SigningCredentials(
						new SymmetricSecurityKey(Encoding.UTF8.GetBytes("temporary_key")),
						SecurityAlgorithms.HmacSha256
					)
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		/// <inheritdoc/>
		public bool VerifyJWT(string token, out string errorMessage)
		{
			TokenValidationParameters validationParameters =
				new()
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateIssuerSigningKey = false,
					ValidateLifetime = false,
					ValidIssuer = "issuer",
					ValidAudience = "audience",
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("temporary_key"))
				};

			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
				errorMessage = string.Empty;

				return true;
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return false;
			}
		}
	}
}
