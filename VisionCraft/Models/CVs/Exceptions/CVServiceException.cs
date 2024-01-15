using Xeptions;

namespace VisionCraft.Models.CVs.Exceptions
{
    public class CVServiceException : Xeption
    {
        public CVServiceException(Xeption innerException)
            :base("CV service error occured, contact support.",
                 innerException)
        { }
    }
}
