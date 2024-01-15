using Xeptions;

namespace VisionCraft.Models.CVs.Exceptions
{
    public class InvalidCVException : Xeption
    {
        public InvalidCVException()
            :base("CV is invalid.")
        { }
    }
}
