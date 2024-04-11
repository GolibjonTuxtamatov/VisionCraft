using System.Data;
using System.Text.RegularExpressions;
using VisionCraft.Models.Teams;
using VisionCraft.Models.Teams.Exceptions;

namespace VisionCraft.Services.Orchestrations.TeamOrchestrationServices
{
    public partial class TeamOrchestrstionService
    {
        private void ValidateNotNull(Team maybeTeam)
        {
            if (maybeTeam == null)
                throw new NullTeamException();
        }

        private void ValidateEmailAndPassword(string email, string password)
        {
            Validate(
                (Rule: IsInvalid(email), Parameter: nameof(Team.Email)),
                (Rule: IsInvalid(password), Parameter: nameof(Team.Password)));

            Validate(
                (Rule: IsInvalidEmail(email), Parameter: nameof(Team.Email)));
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Value is required"
        };

        private static dynamic IsInvalidEmail(string email) => new
        {
            Condidtion = !ValidateEmail(email),
            Message = "Email is invalid"
        };

        private static bool ValidateEmail(string email)
        {
            var rgx = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            var match = rgx.Match(email);

            if (match.Success)
                return true;
            else return false;
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] values)
        {
            var invalidTeamException = new InvalidTeamException();

            foreach ((dynamic rule, string parameter) in values)
            {
                if (rule.Condition)
                {
                    invalidTeamException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidTeamException.ThrowIfContainsErrors();
        }
    }
}
