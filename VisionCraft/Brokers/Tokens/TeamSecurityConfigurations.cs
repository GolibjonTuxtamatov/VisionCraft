using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VisionCraft.Models.Teams;
using VisionCraft.Models.Tokens;

namespace VisionCraft.Brokers.Tokens
{
    public class TeamSecurityConfigurations : ITeamSecurityConfigurations
    {
        private readonly TokenConfiguration tokenConfiguration;

        public TeamSecurityConfigurations(IConfiguration configuration)
        {
            this.tokenConfiguration = new TokenConfiguration();
            configuration.Bind("AppSettings",this.tokenConfiguration);
        }

        public async ValueTask<string> CreateTeamToken(Team team)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, team.Id.ToString()),
                new Claim(ClaimTypes.Name, team.Name),
                new Claim(ClaimTypes.Email, team.Email),
                new Claim(ClaimTypes.Role,team.Role.ToString())
            };

            byte[] convertedKetToBytes = Encoding.UTF8.GetBytes(this.tokenConfiguration.Key);

            var sekurityKey = new SymmetricSecurityKey(convertedKetToBytes);

            var credentials = new SigningCredentials(sekurityKey, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
