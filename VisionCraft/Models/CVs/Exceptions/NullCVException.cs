using Xeptions;

namespace VisionCraft.Models.CVs.Exceptions
{
    public class NullCVException : Xeption
    {
        public NullCVException():
            base("CV is null")
        { }
    }
}
