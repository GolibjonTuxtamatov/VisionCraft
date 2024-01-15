using System.Data;
using VisionCraft.Models.CVs;
using VisionCraft.Models.CVs.Exceptions;

namespace VisionCraft.Services.Foundations.CVs
{
    public partial class CVService
    {
        private static void ValidateOnAdd(CV cv)
        {
            ValidateCVNotNull(cv);

            Validate((Rule: IsInvalid(cv.Id), Parameter: nameof(CV.Id)));
        }

        private static void ValidateCVNotNull(CV cv)
        {
            if (cv == null)
                throw new NullCVException();
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidCVException = new InvalidCVException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidCVException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }

                invalidCVException.ThrowIfContainsErrors();
            }
        }
    }
}
