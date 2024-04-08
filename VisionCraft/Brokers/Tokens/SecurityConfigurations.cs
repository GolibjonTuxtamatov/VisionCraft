using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VisionCraft.Models.Teams;
using VisionCraft.Models.Tokens;

namespace VisionCraft.Brokers.Tokens
{
    public class SecurityConfigurations : ISecurityConfigurations
    {
        private readonly TokenConfiguration tokenConfiguration;

        public SecurityConfigurations(IConfiguration configuration)
        {
            this.tokenConfiguration = new TokenConfiguration();
            configuration.Bind("JwtSettings", this.tokenConfiguration);
        }

        public async ValueTask<string> CreateToken(Team team)
        {
            byte[] convertedKetToBytes = Encoding.UTF8.GetBytes(this.tokenConfiguration.Key);

            var securityKey = new SymmetricSecurityKey(convertedKetToBytes);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,team.Id.ToString()),
                new Claim(ClaimTypes.Email, team.Email),
                new Claim(ClaimTypes.Role,team.Role.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
