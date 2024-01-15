using Xeptions;

namespace VisionCraft.Models.CVs.Exceptions
{
    public class FailedStorageCVException : Xeption
    {
        public FailedStorageCVException(Exception innerException)
            :base("Failed storage error occured.")
        { }
    }
}
