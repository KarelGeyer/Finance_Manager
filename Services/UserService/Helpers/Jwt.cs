using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Models.User;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using UserService.Interfaces;

namespace UserService.Helpers
{
	public class Jwt : IJwt
	{
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

		public bool VerifyJWT(string token, out JwtSecurityToken jwt, out string errorMessage)
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
				jwt = (JwtSecurityToken)validatedToken;
				errorMessage = string.Empty;

				return true;
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				jwt = null;
				return false;
			}
		}
	}
}
