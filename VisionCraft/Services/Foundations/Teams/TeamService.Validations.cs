using System.Data;
using System.Text.RegularExpressions;
using VisionCraft.Models.Teams;
using VisionCraft.Models.Teams.Exceptions;

namespace VisionCraft.Services.Foundations.Teams
{
    public partial class TeamService
    {

        private void ValidateOnAdd(Team team)
        {
            ValidateNotNull(team);

            Validate(
                (Rule: IsInvalid(team.Id), Parameter: nameof(Team.Id)),
                (Rule: IsInvalid(team.Name), Parameter: nameof(Team.Name)),
                (Rule: IsInvalid(team.Email), Parameter: nameof(Team.Email)),
                (Rule: IsInvalid(team.Password), Parameter: nameof(Team.Password)));


            Validate(
                (Rule: IsInvalidEmail(team.Email), Parameter: nameof(Team.Email)));
        }

        private static void ValidateNotNull(Team team)
        {
            if (team == null)
                throw new NullTeamException();
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalidEmail(string text) => new
        {
            Condition = !ValidateEmail(text),
            Message = "Email is invalid."
        };

        private static bool ValidateEmail(string email)
        {
            var rgx = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            var match = rgx.Match(email);

            if (match.Success)
                return true;
            else return false;
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidTeamException = new InvalidTeamException();

            foreach ((dynamic rule, string parameter) in validations)
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
