using Xeptions;

namespace VisionCraft.Models.Pdf.Exceptions
{
    public class FailedServiecException : Xeption
    {
        public FailedServiecException(Exception innerException)
            :base("Failed service exception.",innerException)
        { }
    }
}
