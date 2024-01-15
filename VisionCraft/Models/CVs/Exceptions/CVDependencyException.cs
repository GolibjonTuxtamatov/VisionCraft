using Xeptions;

namespace VisionCraft.Models.CVs.Exceptions
{
    public class CVDependencyException : Xeption
    {
        public CVDependencyException(Xeption innerException)
            : base("Failed storage dependecy error occured, contact support.",
                 innerException)
        { }
    }
}
