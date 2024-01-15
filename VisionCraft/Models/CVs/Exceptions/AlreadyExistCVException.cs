using Xeptions;

namespace VisionCraft.Models.CVs.Exceptions
{
    public class AlreadyExistCVException : Xeption
    {
        public AlreadyExistCVException(Exception innerException)
            : base("CV already exist.", innerException)
        { }
    }
}
