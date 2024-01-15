using Xeptions;

namespace VisionCraft.Models.CVs.Exceptions
{
    public class CVValidationException : Xeption
    {
        public CVValidationException(Xeption innerException)
            : base("CV validation exception occured, fix the error and try again.",
                 innerException)
        { }
    }
}
