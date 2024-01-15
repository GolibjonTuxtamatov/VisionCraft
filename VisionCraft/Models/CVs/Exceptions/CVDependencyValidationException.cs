using Xeptions;

namespace VisionCraft.Models.CVs.Exceptions
{
    public class CVDependencyValidationException : Xeption
    {
        public CVDependencyValidationException(Xeption innerException)
            : base("CV dependency validation error occured, try again.", innerException)
        { }
    }
}
