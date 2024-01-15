using Xeptions;

namespace VisionCraft.Models.CVs.Exceptions
{
    public class FailedServiceException : Xeption
    {
        public FailedServiceException(Exception innerException)
            : base("Failed service error occured.", innerException)
        { }
    }
}
